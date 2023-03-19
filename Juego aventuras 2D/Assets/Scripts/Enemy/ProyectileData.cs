using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileData : MonoBehaviour
{
    [SerializeField] AnimationClip destroyClip;
    //[SerializeField] float distance;
    [SerializeField] float speed;
    [SerializeField] public AudioSource audioImpulse;
    [SerializeField] AudioSource audioCollision;



    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Collider2D coll;
    

    float currentTime;
    internal float direction;
    bool isEnemyHit;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        isEnemyHit = false;
        currentTime = 0;
    }
    private void Update()
    {
        sr.flipX = direction < 0 ? true : false;
        currentTime += Time.deltaTime;
        if (!isEnemyHit && currentTime > 0.2f)
        {
            impulseProyectile();
        }
    }
    public enum ProyectileType
    {
        Ice
    };

    public ProyectileType type;

    public ProyectileType GetProyectileType()
    {
        return type;
    }

    public void impulseProyectile()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Entra");
        isEnemyHit = true;
        anim.SetTrigger("collision");
        rb.bodyType = RigidbodyType2D.Static;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Slime>().enemyState = Enemy.state.Frozen;
        }
        audioCollision.Play();
        Destroy(gameObject, destroyClip.length);
    }

}
