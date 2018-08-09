using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour {

    public float spawnFrequency = 5;
    public float laneSpeed = 4;
    //[Range(0.0f, 0.01f)]
    public float swerveSpeed = 0.001f;
    //[Range(0.0f, 10.0f)]
    public float swerveMax = 1;
    public GameObject[] spawneable;
    public float vehicleLifeSpan = 20;
    private Queue<GameObject> vehicles;
    private float spawnStartTime = 2f;
    private int ranMax;
    static System.Random random = new System.Random();
    // Use this for initialization
    void Start () {
        vehicles = new Queue<GameObject>();
        GameObject temp = SpawnVehicle();
        temp.transform.position = new Vector3(temp.transform.position.x + 4 +5*laneSpeed, temp.transform.position.y, temp.transform.position.z);
        temp = SpawnVehicle();
        temp.transform.position = new Vector3(temp.transform.position.x + 4 +10*laneSpeed, temp.transform.position.y, temp.transform.position.z);
        temp = SpawnVehicle();
        temp.transform.position = new Vector3(temp.transform.position.x + 4 +15*laneSpeed, temp.transform.position.y, temp.transform.position.z);
        InvokeRepeating("SpawnVehicle", spawnStartTime, spawnFrequency);
        InvokeRepeating("DestroyVehicle", vehicleLifeSpan+spawnStartTime, spawnFrequency);
        ranMax = spawneable.Length;
    }
	
	//called once per fixed "frame" (computer frame rate will not affect gameplay speed)
	void FixedUpdate () {
	}
    private GameObject SpawnVehicle()
    {
        GameObject newVehicle = Instantiate(spawneable[random.Next(ranMax)],transform); //spawn random vehicle from list of spawnables
        //newVehicle.transform.position = transform.position;
        newVehicle.GetComponent<Drive>().speed = laneSpeed;
        newVehicle.GetComponent<Drive>().swerveSpeed = swerveSpeed;
        newVehicle.GetComponent<Drive>().swerveMax = swerveMax;
        vehicles.Enqueue(newVehicle);
        return newVehicle;
    }
    private void DestroyVehicle()
    {
        Destroy(vehicles.Dequeue());
    }
}
