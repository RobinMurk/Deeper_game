using System;
using UnityEngine;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Trees;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MyCustomAi : MonoBehaviour
{
    public static MyCustomAi Instance;
    public static NavMeshAgent agent;
    public float range = 10f;
    private bool HasLineOfSight;
    public Waypoint StartingWaypoint;
    private Waypoint CurrentWaypoint;
    private Waypoint NextWaypoint;
    [SerializeField]
    private BehaviorTree _tree;
    public static Animator animator;
    private int isWalkingHash;
    private int isRunningHash;
    private int isSearchingHash;
    private Vector3 lastKnowPositionOfPlayer;
    private float _wanderRadius = 30f;
    



    
    void MoveTowardsPlayer(float distance, float stalkDistance)
    {
        /*
        // Get the direction from the agent to the player
        Vector3 directionToPlayer = (Player.Instance.transform.position - agent.transform.position).normalized;
        float distanceFromPlayer = Vector3.Distance(agent.transform.position, Player.Instance.transform.position);
        if (Mathf.Abs(distanceFromPlayer - stalkDistance) < 0.01) return;
        if (distanceFromPlayer - distance < stalkDistance)
            distance = distanceFromPlayer - stalkDistance;

        // Calculate the target destination 2 units away in the direction of the player
        Vector3 destination = agent.transform.position + directionToPlayer * distance;

        // Set the destination for the NavMeshAgent
        agent.SetDestination(destination);*/
        if(agent.remainingDistance <= 3){
            
        }
        agent.SetDestination(Player.Instance.transform.position);
    }

    void MoveToNextWaypoint(){
        bool isWalking = animator.GetBool(isWalkingHash);
        if (!isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        CurrentWaypoint = NextWaypoint;
        NextWaypoint = CurrentWaypoint.GetWaypoint();
        agent.SetDestination(NextWaypoint.transform.position);
    }
    private void Awake ()
    {
        Instance = this;
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isSearchingHash = Animator.StringToHash("isSearching");
        CurrentWaypoint = StartingWaypoint;
        NextWaypoint = StartingWaypoint;
        agent = GetComponent<NavMeshAgent>();
        _tree = new BehaviorTreeBuilder(gameObject)
            .Selector()
                .Sequence()
                    .Condition("CheckNextWaypoint", () =>
                    {
                        if (EventListener.Instance.Stalk ||
                            EventListener.Instance.Attack ||
                            EventListener.Instance.Investigate || 
                            agent.remainingDistance > 0.1)
                        {
                            return false;
                        }
                        animator.SetBool(isWalkingHash, false);
                        return true;
                    })
                    .WaitTime(5f)
                    .Do("GoToNextWaypoint", () => {
                        MoveToNextWaypoint();
                        return TaskStatus.Success;
                    })
                .End()
                .Sequence()
                    .Condition("Stalking", () =>
                    {   
                        if (!EventListener.Instance.Stalk) return false;
                        if (EventListener.Instance.Attack) return false;
                        return true;
                    })
                    .Do("Stalk", () => {
                        Vector3 direction = Player.Instance.transform.position - transform.position;
                        Quaternion rotation = Quaternion.LookRotation(direction);
                        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 5f); // Smooth rotation
                        return TaskStatus.Success;
                    })
                    .WaitTime(2f)
                    .RandomChance(1, 2)
                    .Do("StalkMove", () =>
                    {
                        MoveTowardsPlayer(2f, 5f);
                        return TaskStatus.Success;
                    })
                .End()
            .Sequence()
                .Condition("Attack", () =>
                {   
                    if (!EventListener.Instance.Attack) return false;
                    return true;
                })
                .Do("Attack", () =>
                {
                    MoveTowardsPlayer(1f, 1f);
                    return TaskStatus.Success;
                })
                .End()
            .Sequence()
                .Condition("InvestigateMovement", () =>
                {
                    if (agent.remainingDistance < 0.1f)
                    {
                        EventListener.Instance.CheckArea();
                        EventListener.Instance.isDocile = true;
                        EventListener.Instance.isAgrovated = false;
                    }
                    return !EventListener.Instance.InvestigateArea &&
                           EventListener.Instance.Investigate;
                })
                .Do("Investigate", () =>
                {
                    agent.SetDestination(lastKnowPositionOfPlayer);
                    return TaskStatus.Success;
                })
                .End()
            .Sequence()
                .Condition("InvestigateAroundMovementArea", () =>
                {
                    return !EventListener.Instance.BackToPatrol(Time.deltaTime) && 
                           EventListener.Instance.InvestigateArea;
                })
                .Do("CheckArea", () =>
                {
                    if (agent.remainingDistance <= agent.stoppingDistance) //done with path
                    {
                        Vector3 randomDirection = Random.insideUnitSphere * _wanderRadius;
                        randomDirection += transform.position;
                        randomDirection.y = transform.position.y;
                        NavMeshHit hit;
                        bool navMeshHit = false;
                        while (!navMeshHit)
                        {
                            if (NavMesh.SamplePosition(randomDirection, out hit, _wanderRadius, NavMesh.AllAreas))
                            {
                                agent.SetDestination(hit.position);
                                navMeshHit = true;
                            }
                        }
                    }

                    return TaskStatus.Success;
                })
                .End()
            .End()
            .Build();
    }

    private void Update () {
        // Update our tree every frame
        _tree.Tick();
        if(EventListener.Instance.isAgrovated && EventListener.Instance.isDocile){
            AudioManager.Instance.Play("EnemyScreetch");
            EventListener.Instance.isDocile = false;
        }
    }

    /// <summary>
    /// Spawns an enemy at the farthest checkpoint within a given radius.
    /// </summary>
    /// <param name="outOfBoundsDistance">The distance at which the enemy is considered out of bounds and will be respawned closer.</param>
    /// <param name="spawnDistance">The radius within which to search for checkpoints to spawn the enemy.</param>
    public void spawnCloserToPlayer(float outOfBoundsDistance, float spawnDistance)
    {   
        if ((Player.Instance.transform.position - transform.position).magnitude < outOfBoundsDistance) return;
        Collider[] hitColliders = Physics.OverlapSphere(Player.Instance.transform.position, spawnDistance, LayerMask.GetMask("Checkpoint"));
        if (hitColliders.Length == 0)
        {
            Debug.LogWarning("No checkpoints found within the specified radius.");
            return;
        }

        Collider farthestCheckpoint = null;
        float maxDistance = 0f;
        Debug.Log(hitColliders.Length);
        foreach (Collider checkpoint in hitColliders)
        {
            Debug.Log(checkpoint.name);
            float distance = Vector3.Distance(Player.Instance.transform.position, checkpoint.transform.position);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                farthestCheckpoint = checkpoint;
            }
        }

        // Spawn the enemy at the farthest checkpoint
        if (farthestCheckpoint != null)
        {
            transform.position = farthestCheckpoint.transform.position;
            CurrentWaypoint = farthestCheckpoint.gameObject.GetComponent<Waypoint>();
            Debug.Log($"Enemy spawned at checkpoint: {farthestCheckpoint.name}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Torch"))
        {
            Torch torch = other.gameObject.GetComponent<Torch>();
            torch.Extinguish();
        }
        else if (other.gameObject.name == "DetectionRadius")
        {
            EventListener.Instance.HeardNoise();
            lastKnowPositionOfPlayer = Player.Instance.transform.position;
        };
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "DetectionRadius")
            lastKnowPositionOfPlayer = Player.Instance.transform.position;
    }

    public void StopBreathing(){
        gameObject.GetComponent<AudioSource>().Stop();
    }
    public void StartBreathing(){
        gameObject.GetComponent<AudioSource>().Play();
    }
}