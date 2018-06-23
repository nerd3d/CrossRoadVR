using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour {

    public float spawnFrequency = 5;
    public float laneSpeed = 4;
    [Range(0.0f, 0.01f)]
    public float swerveSpeed = 0.001f;
    [Range(0.0f, 10.0f)]
    public float swerveMax = 1;
    public GameObject[] spawneable;
    public float vehicleLifeSpan = 20;
    private Queue<GameObject> vehicles;
    private float spawnStartTime = 2f;
	// Use this for initialization
	void Start () {
        vehicles = new Queue<GameObject>();
        InvokeRepeating("SpawnVehicle", spawnStartTime, spawnFrequency);
        InvokeRepeating("DestroyVehicle", vehicleLifeSpan+spawnStartTime, spawnFrequency);
    }
	
	//called once per fixed "frame" (computer frame rate will not affect gameplay speed)
	void FixedUpdate () {
	}
    private void SpawnVehicle()
    {
        GameObject newVehicle = Instantiate(spawneable[0],transform);
        //newVehicle.transform.position = transform.position;
        newVehicle.GetComponent<Drive>().speed = laneSpeed;
        newVehicle.GetComponent<Drive>().swerveSpeed = swerveSpeed;
        vehicles.Enqueue(newVehicle);
    }
    private void DestroyVehicle()
    {
        Destroy(vehicles.Dequeue());
    }
}
