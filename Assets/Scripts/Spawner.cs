using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject food;
    public float circleRadius = 5f;
    public float spawnRate;
    float timer;

    float rate;

    private void Start()
    {
        timer = 0;

        rate = Random.Range(0, spawnRate);
    }
    private void Update()
    {
        timer += Time.deltaTime;
       

        if (timer > rate)
        {

         SpawnFood();
         rate = Random.Range(0, spawnRate);
            Debug.Log(rate);
         timer = 0;

        }
    }


    void SpawnFood()
    {
        // Generate random position inside circle
        float angle = Random.Range(0f, Mathf.PI * 2); // Random angle in radians
        float distance = Mathf.Sqrt(Random.Range(0f, 1f)) * circleRadius; // Random distance from center

        // Calculate position
        float x = Mathf.Cos(angle) * distance;
        float z = Mathf.Sin(angle) * distance;

        Vector3 position = new Vector3(x, transform.position.y, z);

        // Instantiate object at calculated position
        Instantiate(food, position, Quaternion.identity);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }
}
