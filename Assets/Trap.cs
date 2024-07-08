using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trap : MonoBehaviour
{



    public float trapDuration;
    public float destroyTime;
    public static bool isTrapped;
    bool release;
    public float timer;
    Rigidbody rb;


    void Start()
    {
        //Destroy(gameObject, destroyTime);
    }

    void Update()
    {

   
        if (isTrapped )
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                release = true;
            }

        }
       

        if ( release )
        {

            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotationZ;
            rb.constraints = RigidbodyConstraints.FreezeRotationX;
            Destroy(gameObject);

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "RedPlayer" || other.tag == "BluePlayer")
        {
            isTrapped = true;
            Debug.Log("Trapped");
            rb = other.GetComponent<Rigidbody>();

            if (!release) {

                other.transform.position = transform.position + new Vector3(0, 0, 0);
                rb.constraints = RigidbodyConstraints.FreezePosition;
                rb.velocity = Vector3.zero;
            }
           
        }
    }

    

}
