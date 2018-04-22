using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/*
public class WaypointsSceneEditor : EditorWindow {
    static WaypointNode _rootWaypoint;
    static bool _isActive;
    static WaypointNode _start, _dest;
    static List<WaypointNode> _route;
    static bool m_editMode;
    static WaypointNode _selected;

    //[MenuItem("Ant project/Waypoints Editor")]
    static void Create()
    {
        _rootWaypoint = GameObject.Find("waypoint").GetComponent<WaypointNode>();

        // Get existing open window or if none, make a new one:
        WaypointsSceneEditor window = (WaypointsSceneEditor)EditorWindow.GetWindow(typeof(WaypointsSceneEditor));
        window.Show();
    }

    protected GameObject _contatiner = null;
    public void OnGUI()
    {
        Handles.BeginGUI();
        GUI.enabled = !_isActive;
        if (GUILayout.Button("start"))
        {
            _isActive = true;
            _start = Selection.activeGameObject.GetComponent<WaypointNode>();
        }
        GUI.enabled = _isActive;
        if (GUILayout.Button("end"))
        {
            _isActive = false;
            _dest = Selection.activeGameObject.GetComponent<WaypointNode>();
            _route = _start.GetRouteTo(_dest);
        }

        GUI.enabled = true;
        if (m_editMode)
        {
            if (GUILayout.Button("Disable Editing"))
            {
                m_editMode = false;
            }
        }
        else
        {
            if (GUILayout.Button("Enable Editing"))
            {
                m_editMode = true;
                GameObject waypoint = Resources.Load("Assets/waypoint.prefab", typeof(GameObject)) as GameObject;
                _contatiner = Instantiate(waypoint) as GameObject;
                _contatiner.transform.position = new Vector3(0,0,0);
                _contatiner.transform.localScale = new Vector3(10, 10, 10);
                // GET Current number of waypoints currently in window and start from there
                //UnityEngine.Object[] waypoints = FindObjectsOfType(typeof(WaypointNode));
                
            }
        }
        Handles.EndGUI();
    }

    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
    static void DrawGizmos(WaypointNode scr, GizmoType gizmoType)
    {
        Gizmos.color = Color.yellow;
        if (_route != null && _route.Contains(scr))
            Gizmos.DrawWireSphere(scr.transform.position, 0.5f*scr.size);

    }

    
}
*/