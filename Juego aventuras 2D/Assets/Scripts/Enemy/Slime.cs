using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    [SerializeField] GameObject player;
    [SerializeField] float distanceDetection;
    [SerializeField] Sprite[] frozenSpriteArray;
    [SerializeField] AudioSource frozenSlimeSound;

    Rigidbody2D slimeRB;
    SpriteRenderer sp2d;
    Animator anim;

    float timeAnimation;
    float speedBouncePlayer;
    
    

    private void Start()
    {
        sp2d = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        prevSpeedAnimation = anim.speed;
        slimeRB = GetComponent<Rigidbody2D>();
        speedBouncePlayer = 15f;


    }
    internal override void stateMachine()
    {
        base.stateMachine();
    }

    internal override void IdleUpdate()
    {
        Debug.Log("idling");
        base.IdleUpdate();
        if (Vector2.Distance(transform.position, player.transform.position) < distanceDetection)
            ai_state = AIState.Attack;

    }

    internal override void AttackUpdate()
    {
        //Debug.Log("attack ");
        base.AttackUpdate();
        
        float direction = player.transform.position.x - transform.position.x;
        
        sp2d.flipX = direction > 0f ? true : false;
        
        direction = Mathf.Clamp(direction,-1,1);
        slimeRB.velocity = new Vector2(direction * getSpeed(), slimeRB.velocity.y);  


        if (Vector2.Distance(transform.position, player.transform.position) >= distanceDetection)
            ai_state = AIState.Idle;
    }

    internal override void DeadCall()
    {
        base.DeadCall();
        playerBounce(player.gameObject);
        gameObject.GetComponent<AudioSource>().Play();
        anim.speed = prevSpeedAnimation;
        Debug.Log("Muerto");
        slimeRB.constraints = RigidbodyConstraints2D.FreezeAll;
        anim.SetTrigger("death");
    }

    public void destroyEnemy()
    {
        Destroy(gameObject);
    }

    internal override void changeFrozenSprite()
    {
        base.changeFrozenSprite();
        //frozenSlimeSound.Play();
        anim.speed = 0;
        //Debug.Log(ai_state);
        timeAnimation = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        anim.Play("Freeze",0,timeAnimation);
        
        //string numberNormalSprite = sp2d.sprite.name.Substring(sp2d.sprite.name.Length - 1);
        //sp2d.sprite = frozenSpriteArray[int.Parse(numberNormalSprite)];
    }
    internal override void returnNormalSprite()
    {
        base.returnNormalSprite();
        anim.Play("Idle", 0, timeAnimation);
        anim.speed = prevSpeedAnimation;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            DeadCall();
        }
    }

    public void playerBounce(GameObject player)
    {
        Debug.Log("Entra en Bounce");
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, speedBouncePlayer);
    }

}
