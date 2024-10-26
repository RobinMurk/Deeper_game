using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint[] WayPoints;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Waypoint GetNextWaypoint(){
        int size = WayPoints.Length;
        int index = Random.Range(0, size);
        return WayPoints[index];
    }


}
