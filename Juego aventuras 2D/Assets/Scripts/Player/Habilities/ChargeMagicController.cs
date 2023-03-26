using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeMagicController : MonoBehaviour
{
    internal GameObject player;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void finishCharge()
    {
        Rigidbody2D rb_player = player.GetComponent<Rigidbody2D>();
        rb_player.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb_player.velocity = new Vector2(rb_player.velocity.x,-1);
        player.GetComponent<Animator>().SetBool("castingSpell", false);
        player.GetComponent<PlayerMovement>().setFreezePlayer(false);
        Destroy(gameObject);
    }
}
