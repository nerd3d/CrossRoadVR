using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Drive : MonoBehaviour
{
    [Range(0.5f, 50.0f)] public float speed = 3.0f;
    //public float xLimit = 32f;
    private float initZ;
    public float swerveSpeed = 0;
    private float swerveMax = 1;
    private bool swerveRight = false;
    private Rigidbody body;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        initZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vel = transform.forward;
        vel.Scale(new Vector3(speed, speed, speed));

        body.velocity = vel;

        /*if (transform.position.x > xLimit)
        {
            transform.position = new Vector3( -xLimit, transform.position.y, transform.position.z);
        }*/
        Move();
    }
    void Move()
    {
        if (transform.position.z > initZ + swerveMax)
        {
            swerveRight = false;
        }
        else if (transform.position.z < initZ -swerveMax)
        {
            swerveRight = true;
        }

        if (swerveRight)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + swerveSpeed);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - swerveSpeed);
        }
        gameObject.GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }
}
