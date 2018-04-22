using System;
using UnityEngine;

public class Timer:MonoBehaviour {
    public delegate void SimpleMethod(Timer timer);
    public event SimpleMethod OnTimer; 
    public float time = 0;
    private bool started = false;
	// Update is called once per frame
	void Update () {
        if (started)
        {
            if (time < 0)
            {
                if (OnTimer != null)
                {
                    OnTimer(this);
                    Stop();
                }
            }
            else
                time -= Time.deltaTime;
        }
	}

    public void Run()
    {
        started = true;
    }

    public void Stop()
    {
        Destroy(gameObject);
    }

    public static Timer AddTimer(float time, SimpleMethod callback)
    {
        Timer timer = new GameObject().AddComponent<Timer>();
        timer.time = time;
        timer.OnTimer += callback;
        timer.Run();
        return timer;
    }
}
