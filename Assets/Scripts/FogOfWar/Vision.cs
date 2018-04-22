using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour {
    public float radius = 10;
    public bool opened = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!opened)
        {
            if (FogOfWar.instance != null)
            {
                opened = true;
                FogOfWar.instance.Open(transform.position, radius);
            }
        }
	}
}
