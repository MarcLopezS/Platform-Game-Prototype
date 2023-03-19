using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager scoreManager;

    public Text scoreText;

    public static int score = 0;

    void Start()
    {
        //Singleton para evitar que la puntuación se pierda al cambiar de escena
        if(scoreManager == null)
        {
            scoreManager = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if(scoreText == null)
        {
            scoreText = GameObject.Find("Text").GetComponent<Text>();
            scoreText.text = score + "";
        }
    }
    public void RaiseScore(int s)
    {
        score += s;
        scoreText.text = score + "";

        /*if(score == 3)
        {
            SceneManager.LoadScene("Scene2");
        }*/
    }
}
