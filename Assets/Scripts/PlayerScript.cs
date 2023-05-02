using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private bool canDoubleJump = false;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isGrounded == true)
            {
                RB.AddForce(Vector2.up * JumpForce);
                isGrounded = false;
                canDoubleJump = true;

                audioSource.PlayOneShot(jumpSound);
            }
            else if (canDoubleJump)
            {
                RB.velocity = new Vector2(RB.velocity.x, 0f);
                RB.AddForce(Vector2.up * JumpForce);
                canDoubleJump = false;

                audioSource.PlayOneShot(jumpSound);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (isGrounded != true)
            {
                RB.AddForce(new Vector2(0f, -JumpForce));

                audioSource.PlayOneShot(downSound);
            }
        }
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (isGrounded == false)
            {
                isGrounded = true;
                canDoubleJump = false;
                audioSource.PlayOneShot(landingSound);
            }
            Debug.Log("Player has landed on the ground.");
        }
        if (collision.gameObject.CompareTag("Spike"))
        {
            float playerY = transform.position.y;
            float spikeY = collision.transform.position.y;

            if (playerY > spikeY && RB.velocity.y > 0)
            {
                spikesJumpedOver++;
            }
            else
            {
                Time.timeScale = 0; 
                audioSource.Stop(); 
                QuitGame();
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