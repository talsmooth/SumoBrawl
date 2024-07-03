using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;


    public Sumo1 sumo1;
    public Sumo2 sumo2;

    public int scoreToWin;
    public float roundTime;
    float time;

    public int blueScore;
    public int redScore;

    public TextMeshProUGUI roundTimeText;
    public TextMeshProUGUI blueScoreText;
    public TextMeshProUGUI redScoreText;

    public GameObject gameOver;
    public GameObject redWinsText;
    public GameObject blueWinsText;

    bool blueWins;



    public Vector3 startingPos;


  /* private void Awake()
    {
        // Check if instance already exists
        if (instance == null)
        {
            // If not, set instance to this
            instance = this;
        }
        // If instance already exists and it's not this
        else if (instance != this)
        {
            // Destroy this to enforce singleton pattern
            Destroy(gameObject);
            return;
        }

        // Set GameManager to not be destroyed on scene load
        DontDestroyOnLoad(gameObject);
    }*/


    void Start()
    {
        time = roundTime;
        roundTimeText.text = roundTime.ToString();
        gameOver.SetActive(false);
        
    }

    void Update()
    {

        time -= Time.deltaTime;
        roundTimeText.text = time.ToString("F0");

        if (sumo1.transform.position.y < -3 && sumo1.tag == "BluePlayer")
        {
          
            redScore++;
            redScoreText.text = redScore.ToString();
            ResetRound();
            

        }

        if (sumo2.transform.position.y < -3 && sumo2.tag == "RedPlayer")
        {
            blueScore++;
            blueScoreText.text = blueScore.ToString();
            ResetRound();
        }

        if (blueScore == scoreToWin || redScore == scoreToWin)
        {
            GameOver();
         


        }

        if (time <= 0 )
        {

            ResetRound();

        }


    }


    void ResetRound()
    {
        time = roundTime;
        sumo1.transform.rotation = Quaternion.Euler(transform.rotation.x,-90, transform.rotation.z);
        sumo2.transform.rotation = Quaternion.Euler(transform.rotation.x, 90, transform.rotation.z);
        sumo1.boost = false;
        sumo2.boost = false;
        sumo1.rb.velocity = Vector3.zero;
        sumo2.rb.velocity = Vector3.zero;
        sumo1.ResetBoost();
        sumo2.ResetBoost();
        sumo1.transform.position = startingPos;
        sumo2.transform.position = new Vector3(startingPos.x * -1, startingPos.y, startingPos.z);

    }

 


    void GameOver()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0;

        if (blueScore == scoreToWin)
        {
            blueWinsText.SetActive(true);

        }

        else
        {
            redWinsText.SetActive(true);

        }


        if (Input.anyKey)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);

        }
    }

  

}
