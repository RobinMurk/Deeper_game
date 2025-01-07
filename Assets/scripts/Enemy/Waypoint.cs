using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint[] AdjacentWaypoints;

    public Waypoint GetWaypoint(){
        Waypoint NextWaypoint;
        int randomIndex = Random.Range(0, AdjacentWaypoints.Count());
        NextWaypoint = AdjacentWaypoints[randomIndex];
        return NextWaypoint;
    }
}
