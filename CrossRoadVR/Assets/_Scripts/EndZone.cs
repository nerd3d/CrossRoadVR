﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("ENDZONE COLLISION @ " + DateTime.Now);
        if(collision.gameObject.tag == "Player")
        {
            var em = GetComponentInChildren<ParticleSystem>().emission;
            em.rateOverTime = 50;
            var m = GetComponentInChildren<ParticleSystem>().main;
            m.startColor = Color.blue;
            var s = GetComponentInChildren<ParticleSystem>().shape;
            s.shapeType = ParticleSystemShapeType.Hemisphere;
            GetComponent<Collider>().enabled = false;
            
        }
    }

}
