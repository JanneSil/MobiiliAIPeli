using UnityEngine;
using System.Collections;

public class Edge
{
	public Node startNode;
	public Node endNode;
    public string wallToRemove;

    // Use this for initialization
    void Start()
    {

    }

    public Edge(Node from, Node to, string removethis)
	{
		startNode = from;
		endNode = to;
        wallToRemove = removethis;
	}
}
