using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float JumpForce;
    public AudioSource audioSource;   
    public AudioClip jumpSound;       
    public AudioClip landingSound;   
    public AudioClip downSound;

    public int spikesJumpedOver = 0;           

    [SerializeField]
    bool isGrounded = false;

    Rigidbody2D RB;

    private bool canDoubleJump = false; // new

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(isGrounded == true)
            {
                RB.AddForce(Vector2.up * JumpForce);
                isGrounded = false;
                canDoubleJump = true; // enable double jump

                // Play the jump sound effect
                audioSource.PlayOneShot(jumpSound);     
            }
            else if (canDoubleJump) // double jump
            {
                RB.velocity = new Vector2(RB.velocity.x, 0f);
                RB.AddForce(Vector2.up * JumpForce);
                canDoubleJump = false;

                // Play the jump sound effect
                audioSource.PlayOneShot(jumpSound);
            }
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(isGrounded != true)
            {
                RB.AddForce(new Vector2(0f, -JumpForce));

                audioSource.PlayOneShot(downSound);
            }
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            if(isGrounded == false)
            {
                isGrounded = true;
                canDoubleJump = false; // disable double jump
                audioSource.PlayOneShot(landingSound); // Play the sound effect       
            }
            Debug.Log("Player has landed on the ground.");
        }
        else if (collision.gameObject.CompareTag("Spike"))
        {
            float playerY = transform.position.y;
            float spikeY = collision.transform.position.y;

            if (playerY > spikeY && RB.velocity.y > 0)
            {
                spikesJumpedOver++;
            }
        }
    }
}


/*
if (collision.gameObject.CompareTag("Spike"))
        {
            if (transform.position.y > collision.transform.position.y)
            {
                spikesJumpedOver++;
            }
        }
*/