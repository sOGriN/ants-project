using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour {
    List<PathNode> _neighbours;
	// Use this for initialization
	void Start () {
        _neighbours = new List<PathNode>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 1.0f);
        foreach (var _node in _neighbours)
        {
            Gizmos.DrawLine(transform.position, _node.transform.position);
        }
    }


}
