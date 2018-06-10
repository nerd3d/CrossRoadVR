using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Drive : MonoBehaviour
{
    [Range(0.5f, 50.0f)] public float speed = 3.0f;
    public float xLimit = 32f;

    private Rigidbody body;

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vel = transform.forward;
        vel.Scale(new Vector3(speed, speed, speed));

        body.velocity = vel;

        if (transform.position.x > xLimit)
        {
            transform.position = new Vector3( -xLimit, transform.position.y, transform.position.z);
        }
    }
}
