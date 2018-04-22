using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MovingObject))]
public class BasicWorker : MonoBehaviour {
    public bool onReturn = false;
    public Base parentBase;
    private WaypointNode execTarget;
    public WaypointNode target;
    private Vector3 _speed;
	// Use this for initialization
	void Start () {
		
	}

    public void SetTarget(WaypointNode target)
    {
        this.target = target;
        GetComponent<MovingObject>().Destination = target;
    }

    private void NextTarget()
    {
        //currentTarget = Path(currentPosition, target);
    }

    public void Finish(MovingObject worker, WaypointNode node)
    {
        //Debug.Log("FINISH");
        if (node != target)
            return;
        if (node == parentBase.node)
        {
            target = null;
            parentBase.DeactivateWorker(this, execTarget);
        }
        else
        {
            execTarget = node;
            onReturn = true;
            SetTarget(parentBase.node);
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //if (target != null)
        //{
        //    Vector3 deltaCurrent = _speed * Time.fixedDeltaTime;
        //    if ((currentTarget.position - transform.position).magnitude < deltaCurrent.magnitude)
        //    {
        //        currentPosition = currentTarget;
        //        transform.position = currentPosition.position;
        //        Finish();
        //    }
        //    else
        //    {
        //        transform.position += deltaCurrent;
        //    }
        //    FogOfWar.instance.Open(transform.position, 100);
        //}
	}
}
