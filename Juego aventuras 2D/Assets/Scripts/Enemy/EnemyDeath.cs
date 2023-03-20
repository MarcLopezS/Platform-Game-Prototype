using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private float speed;
    private Animator animEnemyDeath;
    private Rigidbody2D enemy_rb;
    private GameObject enemyParent;

    private void Start()
    {
        enemy_rb = transform.parent.gameObject.GetComponent<Rigidbody2D>();
        animEnemyDeath = transform.parent.gameObject.GetComponent<Animator>();
        enemyParent = transform.parent.gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            enemy_rb.constraints = RigidbodyConstraints2D.FreezeAll;
            animEnemyDeath.SetTrigger("death");
            gameObject.GetComponent<AudioSource>().Play();
            playerBounce(collider.gameObject);
            Destroy(transform.parent.gameObject, animEnemyDeath.GetCurrentAnimatorStateInfo(0).length);

        }
    }

    public void playerBounce(GameObject player)
    {
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, speed);
    }

}
