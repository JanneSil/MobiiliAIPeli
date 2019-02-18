using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    private float speed = 5.0f;
    private float maxLifeTime = 4f;

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        Destroy(gameObject, maxLifeTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
        Debug.Log("dest");
    }

}
