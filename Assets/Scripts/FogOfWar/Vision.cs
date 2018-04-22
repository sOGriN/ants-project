using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour {
    public float radius = 10;
    public bool opened = false;
    private bool initiate = false;
    public CircleCollider2D visionZone;
	// Use this for initialization
	void Start () {
        //Debug.Log(GetComponentInParent<Vision>());
        if (transform.parent == null || transform.parent.GetComponent<Vision>() == null)
        {
            initiate = true;
            visionZone = new GameObject().AddComponent<CircleCollider2D>();
            visionZone.transform.parent = transform;
            visionZone.transform.localPosition = Vector3.zero;
            visionZone.gameObject.AddComponent<Vision>().radius = radius;
            visionZone.gameObject.AddComponent<Rigidbody2D>();
            visionZone.GetComponent<Rigidbody2D>().simulated = false;
            visionZone.offset = new Vector2(0, 0);
            visionZone.radius = (radius - 5)/100;
            //Debug.Log(LayerMask.NameToLayer("FogOfWar"));
            visionZone.gameObject.layer = LayerMask.NameToLayer("FogOfWar");
        }
	}
    
}
