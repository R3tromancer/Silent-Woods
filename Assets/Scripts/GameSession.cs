using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameSession : MonoBehaviour
{

    [SerializeField] int playerLives = 3;
    [SerializeField] GameObject[] lives;
    [SerializeField] TextMeshProUGUI scoreTMP;
    int scoreText;

    void Awake()
    {
        if(FindObjectsOfType<GameSession>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            playerLives--;

            Destroy(lives[playerLives]);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            scoreText = 0;
            scoreTMP.text = scoreText.ToString();
            FindObjectOfType<GamePersist>().SelfDestruct();
            SceneManager.LoadScene(0);
            Destroy(gameObject);
        }
    }

    public void ProcessCoin(int score)
    {
        scoreText += score;
        scoreTMP.text = scoreText.ToString();
    }


}
