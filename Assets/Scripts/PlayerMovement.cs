﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private bool mouse1Clicked;
    private bool mouse2Clicked;
    private float squareMaxVelocity;
    private Transform cameraTransform;
    public float maxVelocity = 3f;
    public float playerForceScalar = 2.0f;
    public float currentMass;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        squareMaxVelocity = Mathf.Pow(maxVelocity, 2);

        rb = gameObject.GetComponent<Rigidbody>();
        currentMass = rb.mass;
    }

    // Update is called once per frame
    void Update()
    {
        //rb.mass = currentMass;
        mouse1Clicked = Input.GetButton("Fire1");
        mouse2Clicked = Input.GetButton("Fire2");
    }

    void FixedUpdate()
    {

        //Move if either mouse is clicked
        if(mouse1Clicked || mouse2Clicked){
            Vector3 direction = new Vector3(transform.position.x - cameraTransform.position.x,
                                            transform.position.y - cameraTransform.position.y,
                                            transform.position.z - cameraTransform.position.z);
            if(mouse2Clicked){
                direction *= -1;
            }
            rb.AddForce(direction * playerForceScalar * rb.mass);
        }  

        //cap velocity
        if(rb.velocity.sqrMagnitude > squareMaxVelocity){
            rb.AddForce(-rb.velocity.normalized * (rb.velocity.magnitude - maxVelocity));
        }

        //add a tiny amount of random torque
        rb.AddTorque(getRandomVector3(1.0f), ForceMode.Acceleration);
    }

    Vector3 getRandomVector3(float magnitude){
        return new Vector3(Random.Range(-magnitude, magnitude), Random.Range(-magnitude, magnitude), Random.Range(-magnitude, magnitude));
    }
}
