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
    //private float swerveMax = 1;
    public float swerveMax = 45f;
    //private bool swerveRight = false;
    private float swerveCounter = 0;
    private Rigidbody body;
    private Vector3 pointA;
    private Vector3 pointB;
    void Start()
    {
        body = GetComponent<Rigidbody>();
        initZ = transform.position.z;
        pointA = transform.eulerAngles + new Vector3(0f, swerveMax / 2f, 0f);
        pointB = transform.eulerAngles + new Vector3(0f, -swerveMax / 2f, 0f);
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
        /*if (transform.position.z > initZ + swerveMax)
        {
            swerveRight = false;
        }
        else if (transform.position.z < initZ -swerveMax)
        {
            swerveRight = true;
        }

        if (swerveRight)
        {
            //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + swerveSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f , -swerveMax * Mathf.Sin(Time.time * speed), 0f);
            //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - swerveSpeed);
        }
        */
        swerveCounter += .03f * swerveSpeed;
        float time = Mathf.PingPong(swerveCounter , 1);
        transform.localEulerAngles = Vector3.Lerp(pointA, pointB, time);
        gameObject.GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }
}
