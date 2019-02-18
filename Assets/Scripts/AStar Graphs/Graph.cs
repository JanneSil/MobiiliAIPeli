using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Graph
{
	public List<Edge>	edges = new List<Edge>();
	List<Node>	nodes = new List<Node>();
	List<Node> pathList = new List<Node>();  
	
	public Graph() {}

    // Use this for initialization
    void Start()
    {

    }

    // Add a node to the node list
    public void AddNode(GameObject id)
	{
		Node node = new Node(id);
		nodes.Add(node);
	}

    // Add an edge to the edge list
    public void AddEdge(GameObject fromNode, GameObject toNode, string wallblock)
	{
		Node from = FindNode(fromNode);
		Node to = FindNode(toNode);
		
		if(from != null && to != null)
		{
			Edge e = new Edge(from, to, wallblock);
			edges.Add(e);
			from.edgelist.Add(e);
        }
    }

    public void RemoveEdge(string wallblock)
    {
        foreach (Node node in nodes)
        {
            foreach (Edge edge in node.edgelist)
            {
                if (edge.wallToRemove == wallblock)
                {
                    node.edgelist.Remove(edge);
                }
            }
        }
    }



    Node FindNode(GameObject id)
	{
		foreach (Node n in nodes) 
		{
			if(n.getId() == id)
				return n;
		}

		return null;
	}
	
	
	public int GetPathLength()
	{
		return pathList.Count;	
	}
	
	public GameObject GetPathPoint(int index)
	{
		return pathList[index].id;
	}
	
	//Calculate path with A*
	public bool AStar(GameObject startId, GameObject endId)
	{
	  	Node start = FindNode(startId);
	  	Node end = FindNode(endId);
	  
	  	if(start == null || end == null)
	  	{
	  		return false;	
	  	}
	  	
	  	List<Node>	open = new List<Node>();
	  	List<Node>	closed = new List<Node>();

	  	float tentative_g_score= 0;
	  	bool tentative_is_better;
	  	
	  	start.g = 0;
	  	start.h = Distance(start, end);
	  	start.f = start.h;

	  	open.Add(start);
	  	
	  	while(open.Count > 0)
	  	{
	  		int i = LowestF(open);

			Node thisnode = open[i];

			if(thisnode.id == endId)  //path found
			{
				ReconstructPath(start, end);
				return true;	
			} 	
			
			open.RemoveAt(i);
			closed.Add(thisnode);
			
			Node neighbour;

			foreach(Edge e in thisnode.edgelist)
			{
				neighbour = e.endNode;
				neighbour.g = thisnode.g + Distance(thisnode, neighbour);
				
				if (closed.IndexOf(neighbour) > -1)
                {
                    continue;
                }
				
				tentative_g_score = thisnode.g + Distance(thisnode, neighbour);
				
				if( open.IndexOf(neighbour) == -1 )
				{
					open.Add(neighbour);
					tentative_is_better = true;	
				}

				else if (tentative_g_score < neighbour.g)
				{
					tentative_is_better = true;	
				}

				else
                {
                    tentative_is_better = false;
                }
					
				if(tentative_is_better)
				{
					neighbour.cameFrom = thisnode;
					neighbour.g = tentative_g_score;
					neighbour.h = Distance(thisnode, end);
					neighbour.f = neighbour.g + neighbour.h;	
				}
			}
  	
	  	}
		
		return false;	
	}
	
	public void ReconstructPath(Node startId, Node endId)
	{
		pathList.Clear();
		pathList.Add(endId);
		
		var p = endId.cameFrom;

		while(p != startId && p != null)
		{
			pathList.Insert(0, p);
			p = p.cameFrom;	
		}

		pathList.Insert(0, startId);
	}
	
    //Calculate distance between nodes
    float Distance(Node a, Node b)
    {

	  float dx = a.xPos - b.xPos;
	  float dy = a.yPos - b.yPos;
	  float dz = a.zPos - b.zPos;

	  float dist = dx*dx + dy*dy + dz*dz;

	  return (dist);

    }

    int LowestF(List<Node> l)
    {
	  float lowestf = 0;
	  int count = 0;
	  int iteratorCount = 0;
	  	  
	  for (int i = 0; i < l.Count; i++)
	  {

	  	if(i == 0)
	  	{	
	  		lowestf = l[i].f;
	  		iteratorCount = count;
	  	}

	  	else if( l[i].f <= lowestf )
	  	{
	  		lowestf = l[i].f;
	  		iteratorCount = count;	
	  	}

	  	count++;
	  }

	  return iteratorCount;
    }

    public void debugDraw()
    {
        //draw edges
        for (int i = 0; i < edges.Count; i++)
        {
            Debug.DrawLine(edges[i].startNode.id.transform.position, edges[i].endNode.id.transform.position, Color.red);

        }
        //draw directions
        for (int i = 0; i < edges.Count; i++)
        {
            Vector3 to = (edges[i].startNode.id.transform.position - edges[i].endNode.id.transform.position) * 0.05f;
            Debug.DrawRay(edges[i].endNode.id.transform.position, to, Color.blue);
        }
    }

}
