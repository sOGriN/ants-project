using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WaypointRoute
{
    const float MIN_ACCEPTABLE_DISTANCE = 0.5f;
    public WaypointNextReachedEvent NextWaypointReachedEvent;
    public WaypointFinishReachedEvent FinalWaypointReachedEvent;

    Queue<WaypointNode> _waypoints;
    MovingObject _movingObject;
    public WaypointNode Destination
    {
        private set;
        get;
    }

    public WaypointRoute(MovingObject movingObject, List<WaypointNode> waypoints)
    {
        Debug.Log(waypoints[0].gameObject.name);
        Debug.Log(waypoints.Count);
        _waypoints = new Queue<WaypointNode>(waypoints);
        Destination = waypoints[waypoints.Count - 1];
        Debug.Log(Destination.gameObject.name);
        _movingObject = movingObject;
    }
    public WaypointRoute(MovingObject movingObject, WaypointNode waypoint)
    {
        _waypoints = new Queue<WaypointNode>();
        _waypoints.Enqueue(waypoint);
        Destination = waypoint;
        _movingObject = movingObject;
    }


    public bool TestNext(Vector3 position)
    {
        if (!TestNotEnd()) return false;
        if (DistanceTo(position) < MIN_ACCEPTABLE_DISTANCE)
        {
            WaypointNode lastWaypoint = _waypoints.Dequeue();
            if (NextWaypointReachedEvent != null)
                NextWaypointReachedEvent.Invoke(_movingObject, lastWaypoint);
            if (!TestNotEnd()&& FinalWaypointReachedEvent !=null)
                FinalWaypointReachedEvent.Invoke(_movingObject, lastWaypoint);
            return true;
        }
        return false;
    }

    public bool TestNotEnd()
    {
        return _waypoints.Count > 0;
    }

    public Vector3 Direction(Vector3 position)
    {
        if (!TestNotEnd())        
            return Vector3.zero;
        return (_waypoints.Peek().transform.position - position).normalized;
    }

    public float DistanceTo(Vector3 position)
    {
        if (!TestNotEnd())
            return 0.0f;
        return (_waypoints.Peek().transform.position - position).magnitude;
    }
}