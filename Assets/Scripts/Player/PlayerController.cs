using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private AudioSource audioSource;
    private int jumpCounter;
    private int currentHealth = 100;
    private int fireRate = 1;
    private float nextAttack = 0;
    private float nextFire = 0;
    [SerializeField] private GameObject healthUI;
    private HealthBar healthBar;
    private State state = State.idle;
    [SerializeField] private AudioClip[] soundEffects;
    [SerializeField] private int insightGained = 0;
    [SerializeField] private Text insightText;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpHeight;
    private int bullets = 20;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        healthBar = healthUI.GetComponent<HealthBar>();

        jumpCounter = 0;
    }

    void Update()
    {
        move();
        attack();
        anim.SetInteger("state", (int)state);
    }

    private void move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            body.velocity = new Vector2(-movementSpeed, body.velocity.y);
            state = State.running;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            body.velocity = new Vector2(movementSpeed, body.velocity.y);
            state = State.running;
        }

        else
        {
            body.velocity = new Vector2(0, body.velocity.y);
            state = State.idle;
        }

        if (Input.GetKeyDown(KeyCode.W) && jumpCounter < 2)
        {
            jumpCounter += 1;
            body.velocity = new Vector2(body.velocity.x, jumpHeight);
            state = State.jumping;
        }


        if((Input.GetKeyDown(KeyCode.A) && transform.rotation.y >= 0) || (Input.GetKeyDown(KeyCode.D) && transform.rotation.y == -1))
        {
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void attack()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextAttack)
        {
            nextAttack = Time.time + 0.8f;
            anim.SetTrigger("light_attack");
            audioSource.PlayOneShot(soundEffects[2]);
        }
        else if (Input.GetMouseButtonDown(2) && Time.time > nextFire && bullets > 0)
        {
            bullets--;
            nextFire = Time.time + fireRate;
            anim.SetTrigger("shooting");
            audioSource.PlayOneShot(soundEffects[3]);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Stairs")
        {
            jumpCounter = 2;
        }
        else if(Vector2.Angle(new Vector2(0, -1), collision.GetContact(0).normal) > 135 && collision.collider.tag == "Ground")
        {
            jumpCounter = 0;
        }
        else if(collision.collider.tag == "Spikes")
        {
            TakeDamage(20);
            body.velocity = new Vector2(body.velocity.x, 5);
        }
  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Insight")
        {
            Destroy(collision.gameObject);
            audioSource.PlayOneShot(soundEffects[0]);
            insightGained++;
            insightText.text = insightGained.ToString();
        }
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            currentHealth = 100;
            bullets = 20;
            healthBar.SetHealth(100);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        audioSource.PlayOneShot(soundEffects[1]);
        healthBar.SetHealth(currentHealth);
    }


    private enum State
    {
        idle,
        running,
        jumping
    }
}

