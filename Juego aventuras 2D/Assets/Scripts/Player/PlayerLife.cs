using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerLife : MonoBehaviour
{
    private Animator animatorDeath;
    private Rigidbody2D rb2d;
    [SerializeField] private Text lifeTxt;

    [SerializeField] private AudioSource deathSound;

    private static int lifes = 3;

    void Start()
    {
        animatorDeath = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        lifeTxt.text = "X " + lifes;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Enemy")) && !animatorDeath.GetBool("death"))
        {
            deathSound.Play();
            Death();

            if(lifes > 0)
            {
                lifes--;
                lifeTxt.text = "X " + lifes;
            }
            else
            {
                animatorDeath.SetBool("GameOver", true);
            }
        }
    }

    private void Death()
    {
        rb2d.bodyType = RigidbodyType2D.Static;

        animatorDeath.SetBool("death",true);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void GameOver()
    {
        SceneManager.LoadScene(4);
    }
}
