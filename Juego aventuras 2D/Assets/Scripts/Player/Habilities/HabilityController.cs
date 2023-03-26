using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilityController : MonoBehaviour
{
    public GameObject prefabProyectile;
    public GameObject prefabChargeMagic;
    
    SpriteRenderer sr_player;
    Rigidbody2D rb;
    Animator anim;

    public bool isCooldown;
    private const float timeSpawnProyectile = 0.2f;
    float timeDurationSpell;
    

    private void Start()
    {
        sr_player = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isCooldown = false;
        
        //cooldownSkill = 5f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && !anim.GetBool("castingSpell") && !anim.GetBool("death"))
        {
            
            anim.SetBool("castingSpell", true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            GetComponent<PlayerMovement>().setFreezePlayer(true);

            float direction = sr_player.flipX ? -1 : 1;
            Vector2 position = new Vector2(transform.position.x + 2*direction, transform.position.y);
            
            GameObject chargeMagic = Instantiate(prefabChargeMagic, position,transform.rotation);
            StartCoroutine(spawnBullet(timeSpawnProyectile,position,direction));
            chargeMagic.GetComponent<AudioSource>().Play();
            chargeMagic.GetComponent<SpriteRenderer>().flipX = sr_player.flipX ? true : false;

            chargeMagic.GetComponent<ChargeMagicController>().player = gameObject;

            //when there is more than one proyectile skill
            /*switch (proyectile.GetComponent<ProyectileData>().GetProyectileType())
            {
                case ProyectileData.ProyectileType.Ice:
                    
                    break;
            }*/
        }
    }

    IEnumerator spawnBullet(float time,Vector2 position, float direction)
    {
        timeDurationSpell = 1f;

        yield return new WaitForSeconds(time);

        GameObject proyectile = Instantiate(prefabProyectile, position, transform.rotation);
        //proyectile.GetComponent<Collider2D>().enabled = false;
        proyectile.GetComponent<ProyectileData>().direction = direction;

        proyectile.GetComponent<ProyectileData>().audioImpulse.Play();
        yield return new WaitForSeconds(timeDurationSpell);

        if(proyectile != null)
        {
            Rigidbody2D rbProyectile = proyectile.GetComponent<Rigidbody2D>();

            rbProyectile.gravityScale = 1;
            rbProyectile.AddTorque(-direction * 5);
        }
        
    }
}
