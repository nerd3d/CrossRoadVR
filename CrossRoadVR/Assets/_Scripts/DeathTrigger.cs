using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    public GameObject _PlayerSpawn;

    void OnEnable()
    {
        _PlayerSpawn = GameObject.FindGameObjectWithTag("Respawn");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = _PlayerSpawn.transform.position;
        }
    }
}
