using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public List<GameObject> Foods = new List<GameObject>(); 
    void Start()
    {
        int rand = Random.Range(0,Foods.Count);
        GameObject tempFood = Instantiate(Foods[rand],transform.position,Quaternion.identity);
        tempFood.transform.parent = transform;
    }

    void Update()
    {
        
    }
}
