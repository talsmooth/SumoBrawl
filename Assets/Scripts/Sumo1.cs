using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sumo1 : Sumo
{
    public Sumo2 sumo2;
    bool launch;
    public float rotationTorque = 3f; // Rotation torque

    void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 direction = sumo2.transform.position - transform.position;
        direction.y = 0;

        // Optional: Smoothly rotate towards sumo2
        /*if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);
        }*/

        rb.angularVelocity = Vector3.zero;
        PerformMovement();
    }

    public void PerformMovement()
    {
        // Reset movement variables
        float xAxis = 0f;
        float zAxis = 0f;

        // Check for right shift key press to apply forward force
        if (Input.GetKey(KeyCode.RightShift))
        {
            launch = true;
            rb.AddForce(transform.forward * speed * 1.3f, ForceMode.Acceleration);
            launchEffect.Play();
        }

        else 
        {
            launch = false;
            launchEffect.Stop();

        }


        // Check for horizontal input for rotation
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) && !launch)
        {
            xAxis = Input.GetAxis("Horizontal");
            rb.AddTorque(0, xAxis * rotationTorque, 0, ForceMode.VelocityChange);
        }

        // Check for vertical input for forward and backward movement
        if (Input.GetKey(KeyCode.UpArrow) && !launch)
        {
            zAxis = 1f;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && !launch)
        {
            zAxis = -1f;
        }

        // Apply movement force in the direction the object is facing
        if (zAxis != 0 && rb.velocity.magnitude < 2)
        {
            rb.AddForce(transform.forward * zAxis * speed, ForceMode.Acceleration);
        }
    }
}
