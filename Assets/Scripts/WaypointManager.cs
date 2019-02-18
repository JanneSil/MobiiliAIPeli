using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Link
{
    public enum direction {UNI, BI};
    public enum wallBlock { NONE, FIRST, SECOND };
    public GameObject node1;
    public GameObject node2;
    public direction dir;
    public wallBlock wall;
}


public class WaypointManager : MonoBehaviour {

    public GameObject[] waypoints;
    public Link[] links;
    public Graph graph = new Graph();

	// Use this for initialization
	void Start ()
    {
        if (waypoints.Length > 0)
        {
            foreach (GameObject waypoint in waypoints)
            {
                graph.AddNode(waypoint);
            }

            foreach (Link l in links)
            {
                graph.AddEdge(l.node1, l.node2, l.wall.ToString());

                if (l.dir == Link.direction.BI)
                {
                    graph.AddEdge(l.node2, l.node1, l.wall.ToString());
                }
            }
        }
	}

    public void RemoveEdge(string removethis)
    {
        graph.RemoveEdge(removethis);
    }

    void Update()
    {
        graph.debugDraw();
    }

}
