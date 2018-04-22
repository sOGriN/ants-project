using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWaypoints : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnWaypoint(MovingObject obj, WaypointNode node)
    {
        Debug.Log("Объект " + obj.gameObject.name + " достиг точки " + node.gameObject.name);

    }
    public void OnFinalWaypoint(MovingObject obj, WaypointNode node)
    {
        Debug.Log("Объект " + obj.gameObject.name + " достиг конечной точки " + node.gameObject.name);

    }
}
