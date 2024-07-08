using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float forceMagnitude;
    public GameObject explosion;
    public float timeToDestroy;
    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RedPlayer" || collision.gameObject.tag == "BluePlayer")
        {  //Debug.Log("bla");
            // Calculate the collision normal
            Vector3 collisionNormal = collision.contacts[0].normal;

            // Calculate the opposite direction
            Vector3 oppositeDirection = -collisionNormal;

            // Apply force to ObjectA in the opposite direction of the collision normal
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Debug.Log("bla");
                rb.velocity = Vector3.zero;
                rb.AddForce(oppositeDirection * forceMagnitude, ForceMode.VelocityChange);
            }

            GameObject TempExplosion = Instantiate(explosion,transform.position,Quaternion.identity);
            Destroy(TempExplosion,2);
            Destroy(gameObject);

        }
    }

}
