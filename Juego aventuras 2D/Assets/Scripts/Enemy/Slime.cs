using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    [SerializeField] Transform player;
    [SerializeField] float distanceDetection;
    [SerializeField] Sprite[] frozenSpriteArray;
    SpriteRenderer sp2d;
    Animator anim;
    float prevSpeedAnimation;
    

    private void Start()
    {
        sp2d = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        prevSpeedAnimation = anim.speed;
    }
    internal override void stateMachine()
    {
        base.stateMachine();
    }

    internal override void IdleUpdate()
    {
        Debug.Log("idling");
        base.IdleUpdate();
        if (Vector2.Distance(transform.position, player.position) < distanceDetection)
            ai_state = AIState.Attack;

    }

    internal override void AttackUpdate()
    {
        Debug.Log("attack ");
        base.AttackUpdate();
        
        float direction = player.position.x - transform.position.x;
        
        sp2d.flipX = direction > 0f ? true : false;
        
        direction = Mathf.Clamp(direction,-1,1);
        rb.velocity = new Vector2(direction * getSpeed(), rb.velocity.y);  


        if (Vector2.Distance(transform.position, player.position) >= distanceDetection)
            ai_state = AIState.Idle;
    }

    internal override void DeadUpdate()
    {
        base.DeadUpdate();
    }

    internal override void changeFrozenSprite()
    {
        base.changeFrozenSprite();
        anim.speed = 0;
        //string numberNormalSprite = sp2d.sprite.name.Substring(sp2d.sprite.name.Length - 1);
        //sp2d.sprite = frozenSpriteArray[int.Parse(numberNormalSprite)];
    }
    internal override void returnNormalSprite()
    {
        base.returnNormalSprite();
        anim.speed = prevSpeedAnimation;
    }

}
