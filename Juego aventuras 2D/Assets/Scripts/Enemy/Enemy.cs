using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int enemyLife;
    [SerializeField] float resetStateTime;
    [SerializeField] float speed;
    
    public state enemyState;
    public AIState ai_state;

    internal Rigidbody2D rb;
    protected float prevSpeedAnimation;

    public enum state
    {
        Normal,
        Frozen
    };

    public enum AIState
    {
        Idle,
        Attack,
        Dead
    }

    virtual internal void stateMachine() 
    {
        if (enemyState == state.Normal)
        {
            switch (ai_state)
            {
                case AIState.Idle:
                    IdleUpdate();
                    break;
                case AIState.Attack:
                    AttackUpdate();
                    break;
            }
        }
        else if (enemyState == state.Frozen)
        {   
            if(ai_state != AIState.Dead)
            {
                Debug.Log("Frozen");
                Frozen();
                changeFrozenSprite();
            }
           
        }
            
    }
    virtual internal void IdleUpdate()
    { 
    }
    virtual internal void AttackUpdate()
    {
    }
    virtual internal void DeadCall()
    {
        ai_state = AIState.Dead;
    }

    internal void Frozen()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    virtual internal void changeFrozenSprite()
    {
    }

    virtual internal void returnNormalSprite()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("proyectile"))
        {
            ProyectileData proData = collision.gameObject.GetComponent<ProyectileData>();
            ChangeState(proData);
        }
        else if(collision.gameObject.CompareTag("Player")){
            speed = 0;
        }
    }

    private void ChangeState(ProyectileData _prog)
    {
        switch (_prog.type)
        {
            case ProyectileData.ProyectileType.Ice:
                enemyState = state.Frozen;
                break;
            default:
                break;
        }
            StartCoroutine(coroutineHandleState());
           
    }

    IEnumerator coroutineHandleState()
    {
        StartCoroutine(resetEnemyState(resetStateTime));

        if (ai_state == AIState.Dead)
        {
            StopCoroutine(resetEnemyState(resetStateTime));
            yield return null;

        }
    }

    IEnumerator resetEnemyState(float time)
    { 
        yield return new WaitForSeconds(time);
        Debug.Log("Finished waiting");
        
        if(ai_state != AIState.Dead)
        {
            returnNormalSprite();
            enemyState = state.Normal;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }

    }

    private void Update()
    {
        stateMachine();
    }

    private void Start()
    {
        enemyState = state.Normal;
        ai_state = AIState.Idle;
        rb = GetComponent<Rigidbody2D>();
    }

    public float getSpeed()
    {
        return speed;
    }

}
