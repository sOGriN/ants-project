using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    public float MotionSpeed = 3.0f;
    public float RotationSpeed = 120.0f;

    public WaypointNode _destination;
    public WaypointNode Destination { get { return _destination; } set { _destination = value; onValidate(); } }
    public WaypointNextReachedEvent NextWaypointReachedEvent;
    public WaypointFinishReachedEvent FinalWaypointReachedEvent;
    private WaypointRoute route;

    void onValidate()
    {
        //Debug.Log("Validate");
        if (route==null||route.Destination != Destination)
        {
            WaypointNode closest = WaypointNode.GetClosest(transform.position);
            if (closest == null)
                route = new WaypointRoute(this,Destination);
            else
                route = new WaypointRoute(this,closest.GetRouteTo(Destination));
            route.NextWaypointReachedEvent = NextWaypointReachedEvent;
            route.FinalWaypointReachedEvent = FinalWaypointReachedEvent;
        }
    }

	// Use this for initialization
	void Start () {
        if (route == null || route.Destination != Destination)
        {
            WaypointNode closest = WaypointNode.GetClosest(transform.position);
            if (closest == null)
                route = new WaypointRoute(this, Destination);
            else
                route = new WaypointRoute(this, closest.GetRouteTo(Destination));
            route.NextWaypointReachedEvent = NextWaypointReachedEvent;
            route.FinalWaypointReachedEvent = FinalWaypointReachedEvent;

        }
    }
	
	// Update is called once per frame
	void Update () {
        route.TestNext(transform.position);
        if (route.TestNotEnd()) { 
            Vector3 moveDirection = route.Direction(transform.position);
            moveDirection *= MotionSpeed * Time.deltaTime ;
            

            float rotStep = Mathf.Deg2Rad * RotationSpeed * Time.deltaTime;

            Vector3 rotDir = moveDirection;
            /*rotDir.z = rotDir.y;
            rotDir.y = 0.0f;*/
            Vector3 newDir = Vector3.RotateTowards(transform.up, moveDirection, rotStep, 0.0f);
            if (Vector3.Angle(newDir,moveDirection)< RotationSpeed * 0.4f)
                transform.position += moveDirection;
            transform.rotation = Quaternion.LookRotation(transform.forward, newDir);
        }
	}
}
