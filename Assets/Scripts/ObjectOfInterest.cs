using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[RequireComponent(typeof(Description))]
[RequireComponent(typeof(Mask))]
public class ObjectOfInterest : MonoBehaviour
{
    public WaypointNode node;
    private Mask mask;
    private bool _visible;
    private Rect menuRect;
    private List<AntMenuItem> items = null;
    private Vector3 menuCoord;
    private bool _hide = false;
    private void Start()
    {
        items = new List<AntMenuItem>();
        mask = GetComponent<Mask>();
    }

    public void ShowMenu()
    {
        _visible = true;
        _hide = false;
        BuildRect();
    }

    public void HideMenu()
    {
        if (_visible)
            _hide = true;
    }

    private void BuildRect()
    {
        Interest[] interests = GetComponentsInChildren<Interest>();
        items.Clear();
        foreach (Interest interest in interests)
        {
            items.AddRange(interest.GetComponents<AntMenuItem>());
        }
        menuRect = new Rect(
                Input.mousePosition.x,
                Screen.height - Input.mousePosition.y,
                Config.MenuWidth,
                Config.MenuHeight * items.Count);
        for (int index = 0; index < items.Count; index++)
        {
            items[index].Init(new Rect(
                Input.mousePosition.x,
                Screen.height - Input.mousePosition.y + Config.MenuHeight * index,
                Config.MenuWidth,
                Config.MenuHeight));
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (mask.IsShowed())
            {
                ShowMenu();
            }
            else
            {
                HideMenu();
            }
        }
    }

    public void Select()
    {

    }

    private void OnGUI()
    {
        if (_visible)
        {
            GUI.Box(menuRect, "");
            bool selected = false;
            foreach (AntMenuItem item in items)
            {
                selected |= item.DrawMenuItem(this);
            }
            if (_hide || selected)
            {
                _visible = false;
                _hide = false;
            }
        }
    }
}
