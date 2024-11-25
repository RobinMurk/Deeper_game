using UnityEngine;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Trees;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MyCustomAi : MonoBehaviour
{
    public static NavMeshAgent agent;
    public float range = 10f;
    private bool HasLineOfSight;
    public Waypoint StartingWaypoint;
    private Waypoint CurrentWaypoint;
    private Waypoint NextWaypoint;
    [SerializeField]
    private BehaviorTree _tree;
    public static Animator animator;

    
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
        CurrentWaypoint = NextWaypoint;
        NextWaypoint = CurrentWaypoint.GetWaypoint();
        agent.SetDestination(NextWaypoint.transform.position);
    }

    private bool PlayerInSight()
    {
        Vector3 direction = (Player.Instance.transform.position - transform.position).normalized;
        Ray ray = new Ray(transform.position, direction);
        //Debug.DrawLine(ray.origin, ray.origin + ray.direction * range, Color.green);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
            //Debug.DrawLine(ray.origin, ray.origin + ray.direction * range, Color.red);
        }
        return false;
    }
    
    private void Awake ()
    {
        animator = GetComponent<Animator>();
        CurrentWaypoint = StartingWaypoint;
        NextWaypoint = StartingWaypoint;
        agent = GetComponent<NavMeshAgent>();
        _tree = new BehaviorTreeBuilder(gameObject)
            .Selector()
                .Sequence()
                    .Condition("NoLineOfSight", () =>
                    {
                        if (!EventListener.Instance.Stalk && !EventListener.Instance.Attack) return false;
                        if (PlayerInSight())
                        {
                            return false;
                        };
                        return true;
                    })
                    .Do("GetLineOfSight", () =>
                    {
                        MoveTowardsPlayer(2f, 1);
                        return TaskStatus.Success;
                    })
                .End()
                .Sequence()
                    .Condition("IsRoaming", () => {
                        //Debug.Log("condition lol");
                        if (EventListener.Instance.Stalk) return false;
                        if (EventListener.Instance.Attack) return false;
                        if(agent.remainingDistance > agent.stoppingDistance) return false;
                        return true;
                    })
                    .WaitTime(5f)
                    .Do("Roam", () => {
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
            .End()
            .Build();
    }

    private void Update () {
        // Update our tree every frame
        _tree.Tick();
    }   
}