using System;
using System.Collections.Generic;
using UnityEngine;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Trees;
using UnityEditor;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MyCustomAi : MonoBehaviour
{
    private NavMeshAgent agent;
    public float range = 10f;
    public Transform centerPoint;
    private bool HasLineOfSight;
    [SerializeField]
    private BehaviorTree _tree;
    
    private bool RandomPoint(Transform center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center.position + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }
    
    void MoveTowardsPlayer(float distance, float stalkDistance)
    {
        // Get the direction from the agent to the player
        Vector3 directionToPlayer = (Player.Instance.transform.position - agent.transform.position).normalized;
        float distanceFromPlayer = Vector3.Distance(agent.transform.position, Player.Instance.transform.position);
        if (Mathf.Abs(distanceFromPlayer - stalkDistance) < 0.01) return;
        if (distanceFromPlayer - distance < stalkDistance)
            distance = distanceFromPlayer - stalkDistance;

        // Calculate the target destination 2 units away in the direction of the player
        Vector3 destination = agent.transform.position + directionToPlayer * distance;

        // Set the destination for the NavMeshAgent
        agent.SetDestination(destination);
    }

    private bool PlayerInSight()
    {
        Vector3 direction = (Player.Instance.transform.position - transform.position).normalized;
        Ray ray = new Ray(transform.position, direction);
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * range, Color.green);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * range, Color.red);
        }
        return false;
    }
    
    private void Awake ()
    {
        bool moving = false;
        agent = GetComponent<NavMeshAgent>();
        _tree = new BehaviorTreeBuilder(gameObject)
            .Selector()
                .Sequence()
                    .Condition("NoLineOfSight", () =>
                    {
                    if (!EventListener.Instance.Triggered && !EventListener.Instance.Attack) return false;
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
                        if (EventListener.Instance.Triggered) return false;
                        if (EventListener.Instance.Attack) return false;
                        return true;
                    })
                    .Do("Custom Action", () => {
                        if (agent.remainingDistance <= agent.stoppingDistance)
                        {
                            Vector3 point;
                            if (RandomPoint(centerPoint, range, out point))
                            {
                                //Debug.DrawRay(point, Vector3.up, Color.red, 1.0f);
                                agent.SetDestination(point);
                            }
                        }
                        return TaskStatus.Success;
                    })
                .End()
                .Sequence()
                    .Condition("Stalking", () =>
                    {   
                        if (!EventListener.Instance.Triggered) return false;
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
