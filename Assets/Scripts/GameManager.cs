using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] walls;

    // Start is called before the first frame update
    void Start()
    {
        walls = GameObject.FindGameObjectsWithTag("BuildableWall");
        foreach (GameObject wall in walls)
        {
            wall.SetActive(false);
        }
    }

}
