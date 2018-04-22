using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {
    private bool readyToSpawn = true;
    public BasicWorker basicPrefab;
    public WaypointNode node;
    public float timeToSpawn = 2;
    public int population = 5;
    public int CurrentPopulation { get { return workers == null ? 0 : workers.Count; } }
    public List<BasicWorker> workers;
    public List<BasicWorker> waiting;
    public List<WaypointNode> targets;
    private int currentTargetIndex = 0;
    public List<int> workersOnTarget;
    // Use this for initialization
    void Start () {
        workers = new List<BasicWorker>();
        waiting = new List<BasicWorker>();
        targets = new List<WaypointNode>();
        workersOnTarget = new List<int>();
    }

    private void Ready(Timer timer)
    {
        readyToSpawn = true;
    }

    public void AddTarget(ObjectOfInterest target)
    {
        AddTarget(target.node);
    }

    public void AddTarget(WaypointNode target)
    {
        targets.Add(target);
        workersOnTarget.Add(0);
    }


    public void RemoveTarget(ObjectOfInterest target)
    {
        RemoveTarget(target.node);
    }

    public void RemoveTarget(WaypointNode target)
    {
        int index = targets.IndexOf(target);
        if (index != -1)
        {
            targets.Remove(target);
            workersOnTarget.RemoveAt(index);
            foreach (BasicWorker worker in workers)
            {
                if (worker.target == target)
                    worker.SetTarget(node);
            }
        }
    }

    private void SendWorker(BasicWorker worker, WaypointNode target)
    {
        worker.gameObject.SetActive(true);
        worker.SetTarget(target);
        workers.Add(worker);
    }

    public void DeactivateWorker(BasicWorker worker, WaypointNode target)
    {
        worker.gameObject.SetActive(false);
        if (targets.IndexOf(target) != -1)
            workersOnTarget[targets.IndexOf(target)]--;
        waiting.Add(worker);
        workers.Remove(worker);
    }

	private WaypointNode FindTarget()
    {
        if (currentTargetIndex >= targets.Count)
        {
            currentTargetIndex = 0;
        }
        workersOnTarget[currentTargetIndex]++;
        return targets[currentTargetIndex ++];
    }

    private BasicWorker SpawnWorker()
    {
        BasicWorker basicWorker;
        if (waiting.Count > 0)
        {
            basicWorker = waiting[waiting.Count - 1];
            waiting.RemoveAt(waiting.Count - 1);
        }
        else
        {
            basicWorker = Instantiate<BasicWorker>(basicPrefab);
            basicWorker.transform.position = transform.position;
            basicWorker.parentBase = this;
        }
        readyToSpawn = false;
        Timer.AddTimer(timeToSpawn, Ready);
        return basicWorker;
    }
	// Update is called once per frame
	void Update () {
		if (CurrentPopulation < population)
        {
            if (readyToSpawn&&targets.Count>0)
            {
                SendWorker(SpawnWorker(), FindTarget());
            }
        }
	}
}
