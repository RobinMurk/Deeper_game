using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float speed = 1f; // Time between moves
    public float wanderRadius = 10f; // Radius to wander within
    public Player Player;
    private NavMeshAgent navAgent;
    private float PingDelay = 5f; 
    private float TimeSinceLastPing = 5f;
    private bool Agro = false;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = speed;
        //InvokeRepeating(nameof(MoveToRandomPosition), 0f, moveInterval);
    }

    public void SetAgro(){
        Agro = true;
    }

    void Update(){
        Vector3 direction = Player.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 5f); // Smooth rotation
        if(!Agro) return;

        if(TimeSinceLastPing < PingDelay){
            TimeSinceLastPing += Time.deltaTime;
            return;
        }

        NavMeshHit hit;
        // Find the nearest point on the NavMesh to the random point
        if (NavMesh.SamplePosition(Player.transform.position, out hit, wanderRadius, NavMesh.AllAreas))
        {
            // Move the enemy to the new position
            navAgent.SetDestination(hit.position);
        }
    }
}
