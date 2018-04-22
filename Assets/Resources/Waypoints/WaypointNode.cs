using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WaypointNode : MonoBehaviour {
    [SerializeField]
    protected WaypointNode Parent;
    public int Level
    {
        get;
        protected set;
    }
    public float size
    {
        get { return transform.lossyScale.x/2.0f; }
        private set
        {
            /*if (value > 0.0f)
                transform.localScale = Vector3.one * value;*/
        }
    }
    #region Handlers
    void OnValidate()
    {
        if (Parent != this)
        {
            RecalculateLevel();
            Debug.Log("updated");
        }
        else
        {
            Parent = null;
            UnityException E = new UnityException("WARN: Ошибка присваивания родительского узла элементу: " + this.ToString());
            Debug.LogException(E);
        }
    }
    void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        Gizmos.DrawSphere(transform.position, size);
        UnityEditor.Handles.BeginGUI();
        Handles.Label(transform.position - new Vector3(size, size, 0), Level.ToString());
        UnityEditor.Handles.EndGUI();
        if (Parent != null)
        {
            Gizmos.DrawLine(transform.position, Parent.transform.position);
        }
    }

    #endregion

    [ContextMenu("Перейти к родителю",false,0)]
    public void ShowParent()
    {
        if (Parent != null)
        {
            EditorGUIUtility.PingObject(Parent);
            //var currentlActive = Selection.activeGameObject;
            Selection.activeGameObject = Parent.gameObject;
            SceneView.lastActiveSceneView.FrameSelected();
            //Selection.activeGameObject = currentlActive;
        }
    }
    public void SetParent(WaypointNode parent)
    {
        Parent = parent;
        OnValidate();
    }

    public int RecalculateLevel()
    {
        if (Parent == null)
            Level = 0;
        else
            Level = Parent.RecalculateLevel() + 1;

        return Level;
    }

    private WaypointNode PushTo(int index, List<WaypointNode> list)
    {
        list.Insert(index, this);
        return this.Parent;
    }
    public List<WaypointNode> GetRouteTo(WaypointNode dest)
    {
        List<WaypointNode> route = new List<WaypointNode>();
        int insertIndex = 0;
        WaypointNode begin = this;
        WaypointNode end = dest;
        while (begin.RecalculateLevel() > end.RecalculateLevel())
        {
            begin = begin.PushTo(insertIndex, route);
            insertIndex++;
        }
        while (begin.RecalculateLevel() < end.RecalculateLevel())
        {
            end = end.PushTo(insertIndex, route);
        }
        while (begin.RecalculateLevel() >= 0 && begin != end)
        {
            begin = begin.PushTo(insertIndex, route);
            insertIndex++;
            end = end.PushTo(insertIndex, route);
        }
        if (begin == null)
            return null;
        else
        {
            begin.PushTo(insertIndex, route);
            return route;
        }

    }

    public static WaypointNode GetClosest(Vector3 pos)
    {
        WaypointNode[] waypoints = GameObject.FindObjectsOfType<WaypointNode>();
        WaypointNode closest = waypoints[0];
        float minDist = Vector3.Distance(closest.transform.position,pos);
        float curDist = 0.0f;
        foreach (var wayp in waypoints)
        {
            curDist = Vector3.Distance(wayp.transform.position, pos);
            if (curDist < minDist)
            {
                minDist = curDist;
                closest = wayp;
            }
        }
        return closest;
    }
}
