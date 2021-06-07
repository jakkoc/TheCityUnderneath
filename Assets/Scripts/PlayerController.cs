using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D body;
    public Animator anim;
    private int jumpCounter;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        jumpCounter = 0;
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("running", true);
            body.velocity = new Vector2(-4, body.velocity.y);
            transform.localScale = new Vector2(-0.2f, transform.localScale.y);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("running", true);
            body.velocity = new Vector2(4, body.velocity.y);
            transform.localScale = new Vector2(0.2f, transform.localScale.y);
        }

        else
        {
            anim.SetBool("running", false);
        }

        if (Input.GetKeyDown(KeyCode.W) && jumpCounter < 2)
        {
            jumpCounter += 1;
            body.velocity = new Vector2(body.velocity.x, 8f);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            jumpCounter = 0;
        }
        else if (collision.collider.tag == "Stairs")
        {
            jumpCounter = 2;
            //Wektor kolizji
            //collision.contacts[0].normal
        }
    }
}

