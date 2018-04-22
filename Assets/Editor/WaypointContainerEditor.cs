using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WaypointContainer))]
public class WaypointContainerEditor : Editor {
    static bool m_editMode;
    static int m_count = 0;
    static WaypointNode _selected;

    const int EVENT_MBTN_LEFT = 0;
    const int EVENT_MBTN_RIGHT = 1;
    const int EVENT_MBTN_MIDDLE = 2;

    void OnSceneGUI()
    {
        int controlId = GUIUtility.GetControlID(FocusType.Passive);
        
        if (m_editMode)
        {
            Debug.Log(Event.current.button.ToString());
            if ((Event.current.GetTypeForControl(controlId) == EventType.MouseUp))
            {
                Debug.Log("UP"+ Event.current.button.ToString());

                Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                RaycastHit hitInfo;


                if (Physics.Raycast(worldRay, out hitInfo, 1000.0f, LayerMask.GetMask("Waypoints")))
                {
                    Debug.Log("Raycast");
                    if (Event.current.button == EVENT_MBTN_RIGHT)
                    {
                        GameObject waypoint = Resources.Load("Waypoints/waypoint", typeof(GameObject)) as GameObject;
                        GameObject waypointInstance = Instantiate(waypoint) as GameObject;
                        if (_selected != null)
                        {
                            waypointInstance.transform.parent = _selected.transform;
                            waypointInstance.GetComponent<WaypointNode>().SetParent(_selected);
                        }
                        waypointInstance.transform.position = hitInfo.point;
                        _selected = waypointInstance.GetComponent<WaypointNode>();
                         waypointInstance.name = "Waypoint" + m_count.ToString("00");
                         //waypointInstance.transform.parent = m_container.transform;

                        EditorUtility.SetDirty(waypointInstance);
                        Event.current.Use();
                    }
                    if (Event.current.button == EVENT_MBTN_LEFT)
                    {
                        Debug.Log(hitInfo.collider);
                        WaypointNode w = hitInfo.collider.GetComponent<WaypointNode>();
                        if (w != null)
                        {
                            _selected = w;
                            EditorUtility.SetDirty(w);
                        }
                        Event.current.Use();
                    }
                    m_count++;
                }

            }
            

        }

    }
    static WaypointNode _rootWaypoint;
    static bool _isActive;
    static WaypointNode _start, _dest;
    static List<WaypointNode> _route;


    [MenuItem("Ant project/Waypoints Editor")]
    static void Create()
    {
        _rootWaypoint = GameObject.Find("waypoint").GetComponent<WaypointNode>();
        StartEditMode();
    }

    static protected GameObject _contatiner = null;
    public override void OnInspectorGUI()
    {
        Handles.BeginGUI();
        GUI.enabled = !_isActive;
        if (GUILayout.Button("start"))
        {
            _isActive = true;
            _start = _selected;
        }
        GUI.enabled = _isActive;
        if (GUILayout.Button("end"))
        {
            _isActive = false;
            _dest = _selected;
            _route = _start.GetRouteTo(_dest);
        }

        GUI.enabled = true;
        if (m_editMode)
        {
            if (GUILayout.Button("Disable Editing"))
                EndEditMode();
        }
        else if (GUILayout.Button("Enable Editing"))
            StartEditMode();


        Handles.EndGUI();
    }

    static void StartEditMode()
    {
        m_editMode = true;
    }

    static void EndEditMode()
    {
        m_editMode = false;        
    }

    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
    static void DrawGizmos(WaypointNode scr, GizmoType gizmoType)
    {
        if (m_editMode)
        {
            Gizmos.color = Color.blue;
            if (_route != null && _route.Contains(scr))
                Gizmos.DrawWireSphere(scr.transform.position, 0.5f * scr.size);
            Gizmos.color = Color.red;
            if (scr == _selected)
            {
                Gizmos.DrawWireSphere(scr.transform.position, 1.1f * scr.size);
            }
        }
    }
}
