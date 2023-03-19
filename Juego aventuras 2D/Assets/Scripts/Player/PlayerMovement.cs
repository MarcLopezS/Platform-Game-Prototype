using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private SpriteRenderer sp2d;
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D bc2d;
    [SerializeField] private LayerMask jumpableGround;

    private float dirX;
    private bool checkground;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 15f;

    [SerializeField] private AudioSource jumpSound;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp2d = GetComponent<SpriteRenderer>();
        bc2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(rb.constraints != (RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation))
        {
            dirX = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            dirX = 0;
        }

        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        anim.SetFloat("velocity", rb.velocity.y);

        checkground = IsGround();

        if (rb.constraints != (RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation))
        {

            if (Input.GetButtonDown("Jump") && checkground)
            {
                jumpSound.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

        UpdateAnimation();
        
    }

    private void UpdateAnimationRunning()
    {
        bool stateRunning;

        if (dirX > 0f)
        {
            stateRunning = true;
            sp2d.flipX = false;

        }
        else if (dirX < 0f)
        {
            stateRunning = true;
            sp2d.flipX = true;
        }
        else //idle
        {
            stateRunning = false;

        }
        anim.SetBool("running", stateRunning);

    }

    private void UpdateAnimation()
    {

        anim.SetBool("checkGround", checkground);

        UpdateAnimationRunning();

    }

    private bool IsGround()
    {
        return Physics2D.BoxCast(bc2d.bounds.center, bc2d.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }
}
