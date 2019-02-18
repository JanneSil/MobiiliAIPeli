using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody projectile;
    public Transform projectileSpawn;

    private Transform target;
    private float speed = 6f;
    //Vector2 targetPos;
    private Vector3 targetPos;


    private void Start()
    {
        targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;

        if (Input.GetMouseButtonUp(0))
        {
            Fire();
        }

        Vector3 mouse_pos = Input.mousePosition;
        mouse_pos.z = -20;
        Vector3 object_pos = Camera.main.WorldToScreenPoint(transform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        float angle = Mathf.Atan2(mouse_pos.x, mouse_pos.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));


        //if (Physics.Raycast(ray, out hit, 100))
        //{
        //    Vector3 lookatGoal = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z);

        //    Vector3 direction = lookatGoal - transform.position;

            //direction.z = 0.0f; // Only needed if objects don't share 'z' value



            //if (direction != Vector3.zero)
            //{
            //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.up), 5f * Time.deltaTime);
            //}

            // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(Quaternion.LookRotation(direction), Vector3.forward), Time.deltaTime * 5f);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction, transform.TransformDirection(Vector3.back)), Time.deltaTime * 5f);
            //transform.rotation.eulerAngles.x = 0;

            //targetPos = new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z);
        //}

        //transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * 5);

    }

    private void Fire()
    {
        Rigidbody shellInstance = Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation) as Rigidbody;

    }


}
