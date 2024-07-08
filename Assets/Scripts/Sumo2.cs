using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sumo2 : Sumo
{
    public Sumo1 sumo2;
    bool launch;
    public float rotationTorque = 3f; // Rotation torque

    void Start()
    {
        base.Start();
      
    }

    void FixedUpdate()
    {
        Vector3 direction = sumo2.transform.position - transform.position;
        direction.y = 0;

        rb.angularVelocity = Vector3.zero;
        PerformMovement();
    }

    public void PerformMovement()
    {
        // Reset movement variables
        float xAxis = 0f;
        float zAxis = 0f;

        // Check for right shift key press to apply forward force
        if (Input.GetKey(KeyCode.LeftShift))
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
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) && !launch)
        {
            xAxis = Input.GetAxis("Horizontal2");
            rb.AddTorque(0, xAxis * rotationTorque, 0, ForceMode.VelocityChange);
        }

        // Check for vertical input for forward and backward movement
        if (Input.GetKey(KeyCode.W) && !launch)
        {
            zAxis = 1f;
        }
        else if (Input.GetKey(KeyCode.S) && !launch)
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
