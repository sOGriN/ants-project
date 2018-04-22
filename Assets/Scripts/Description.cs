using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Description : MonoBehaviour {
    public string objectName;
    public string objectDescription;
    private bool showed = false;
    public float timeToMessage = 2;
    public float timeToClose = 1f;
    private Timer timer;
    private void ShowMessage(Timer timer = null)
    {
        showed = true;
        if (timer == this.timer)
        {
            timer = null;
        }
    }

    private void CloseMessage(Timer timer = null)
    {
        showed = false;
        if (timer == this.timer)
        {
            timer = null;
        }
    }

    private void OnMouseEnter()
    {
        if (timer != null)
            timer.Stop();
        timer = Timer.AddTimer(timeToMessage, ShowMessage);
    }

    private void OnMouseExit()
    {
        if (timer != null)
            timer.Stop();
        timer = Timer.AddTimer(timeToClose, CloseMessage);
    }

    private void OnGUI()
    {
        if (showed)
        {
            Debug.Log(Input.mousePosition);
            GUI.Box(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, Config.MenuWidth, 50), "Муравей");
        }

    }
}
