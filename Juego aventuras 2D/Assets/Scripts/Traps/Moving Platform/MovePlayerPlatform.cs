using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerPlatform : MonoBehaviour
{
    GameObject player;
    Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.gameObject;

        if (player.name == "Player")
        {
            player.transform.SetParent(transform);
        }

        //Only for Falling Platforms
        if(gameObject.tag == "FallingPlatform")
        {
            rb2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player = collision.gameObject;

        if (player.name == "Player")
        {
            player.transform.SetParent(null);
        }
    }

}
