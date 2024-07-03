using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sumo : MonoBehaviour
{
    public float speed;
    public float startMass;
    [HideInInspector] public Rigidbody rb;

    float size;
    public float boostTime;
    float timer;
    public bool boost;
    public TextMeshProUGUI boostTimer;
    public Camera cam;

    public ParticleSystem launchEffect;
    public GameObject hitEffect;


    AudioSource audio;


    

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = startMass;
        timer = boostTime;
        boostTimer.gameObject.SetActive(false);
        cam = Camera.main;


        audio = cam.GetComponent<AudioSource>();
    }

    public void Update()
    {

        boostTimer.text = timer.ToString("F0");
        boostTimer.transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
        


        if (boost)
        {
            boostTimer.gameObject.SetActive(true);
            timer -= Time.deltaTime;

            if (timer < 0)
            {

                transform.localScale = Vector3.one;
                ResetBoost();
                boost = false;


            }

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Food"))
        {
            boost = true;

            timer = boostTime;

            rb.mass += 2f;
            transform.localScale += new Vector3(0.25f, 0.25f, 0.25f);

            size += 0.25f;

            Destroy(other.gameObject);
        }



        if (other.tag == ("RedPlayer") || other.tag == ("BluePlayer"))
        {
            GameObject tempHitEffect = Instantiate(hitEffect,transform.position,transform.rotation);
            Destroy(tempHitEffect,2);

            audio.Play();
        }


        }



    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.tag == ("RedPlayer") || collision.collider.tag == ("BluePlayer"))
        {

            if (rb.velocity.magnitude > 3f)
            {

            CameraShake.Instance.isShaking = true;

            GameObject tempHitEffect = Instantiate(hitEffect, transform.position + Vector3.forward * 0.5f, transform.rotation);

            Destroy(tempHitEffect, 2);

            //audio.Play();

            }    


           
        }

    }

    public void ResetBoost()
    {
        transform.position += new Vector3(0,size,0);
        size = 0;
        transform.localScale = Vector3.one;
        timer = boostTime;
        rb.mass = startMass;
        boostTimer.gameObject.SetActive(false);

    }
}
