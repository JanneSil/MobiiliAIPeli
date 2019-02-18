using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour {

    Transform goal;
    float speed = 2.0f;
    float accuracy = 0.3f;
    float rotSpeed = 5.0f;
    public GameObject waypointManager;
    GameObject[] waypoints;
    GameObject currentNode;
    int currentWaypoint = 0;
    Graph graph;

    private GameManager gameManager;

    private bool moving;

    // Use this for initialization
    void Start ()
    {
        waypoints = waypointManager.GetComponent<WaypointManager>().waypoints;
        graph = waypointManager.GetComponent<WaypointManager>().graph;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentNode = waypoints[1];
        EnemyAI();
	}

    public void EnemyAI()
    {
        //int temp = Random.Range(0, waypoints.Length);
        //int temp = waypoints.Length - 1;
        Debug.Log("heee");
        if (currentNode == waypoints[1])
        {
            graph.AStar(currentNode, waypoints[7]);
            currentWaypoint = 0;
        }
        else if (currentNode == waypoints[7])
        {
            foreach (GameObject wall in gameManager.walls)
            {
                if (wall.name == "FirstWall1")
                {
                    wall.SetActive(true);
                    break;
                }
            }
            graph.AStar(currentNode, waypoints[11]);
            currentWaypoint = 0;
        }
        else if (currentNode == waypoints[11])
        {
            foreach (GameObject wall in gameManager.walls)
            {
                if (wall.name == "SecondWall2")
                {
                    wall.SetActive(true);
                    break;
                }
            }
            graph.AStar(currentNode, waypoints[17]);
            currentWaypoint = 0;
        }
        else if (currentNode == waypoints[17])
        {
            foreach (GameObject wall in gameManager.walls)
            {
                if (wall.name == "ThirdWall2")
                {
                    wall.SetActive(true);
                    break;
                }
            }
            graph.AStar(currentNode, waypoints[20]);
            currentWaypoint = 0;
        }

    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        if (graph.GetPathLength() == 0 || currentWaypoint == graph.GetPathLength())
        {
            EnemyAI();
            return;
        }

        //moving = true;
        currentNode = graph.GetPathPoint(currentWaypoint);

        if (Vector3.Distance(graph.GetPathPoint(currentWaypoint).transform.position, transform.position) < accuracy)
        {
            currentWaypoint++;
        }

        if (currentWaypoint < graph.GetPathLength())
        {
            goal = graph.GetPathPoint(currentWaypoint).transform;

            Vector3 lookatGoal = new Vector3(goal.position.x, transform.position.y, goal.position.z);
  
            Vector3 direction = lookatGoal - transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);

            transform.Translate(0, 0, speed * Time.deltaTime);
        }
	}
}
