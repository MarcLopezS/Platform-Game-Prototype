using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int itemsCollected = 0;

    [SerializeField] private Text pointsTxt;
    [SerializeField] private AudioSource collectSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject item = collision.gameObject;

        if(item.CompareTag("CollectableItems"))
        {
            collectSound.Play();
            item.GetComponent<Animator>().SetBool("collected", true);
            collision.enabled = false;
            Destroy(item, item.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            itemsCollected++;
            pointsTxt.text = "Points: " + itemsCollected;

        }
    }
}
