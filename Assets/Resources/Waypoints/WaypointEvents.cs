using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class WaypointFinishReachedEvent : UnityEvent<MovingObject, WaypointNode> {
}


[System.Serializable]
public class WaypointNextReachedEvent : UnityEvent<MovingObject, WaypointNode>
{
}
