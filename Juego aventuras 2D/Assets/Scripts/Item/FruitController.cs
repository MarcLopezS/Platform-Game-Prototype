using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    public GameObject soundPrefab;
    public AudioClip sound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Esto si tiene miles de gameobjects, este proceso ralentizaria el propio juego
        //GameObject scripter = GameObject.Find("Scripter");
        //scripter.GetComponent<ScoreManager>().RaiseScore(1);
        GetComponent<BoxCollider2D>().enabled = false;
        ScoreManager.scoreManager.RaiseScore(1);

        //gameObject.GetComponent<Animator>().SetBool("collected", true);
        gameObject.GetComponent<Animator>().Play("collect");
        AudioSource sourceSound = Instantiate(soundPrefab, Vector3.zero,new Quaternion()).GetComponent<AudioSource>();
        sourceSound.clip = sound;
        sourceSound.Play();        
        Destroy(transform.parent.gameObject, gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

}
