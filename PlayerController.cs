using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool isDashing = false;
    public bool doubleJumpReady = true;
    public bool gameOver = false;
    public bool startSceneActive = true;
    private float score = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerAnim.SetBool("Static_b", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (startSceneActive)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 2);

            if (transform.position.x >= 0)
            {
                playerAnim.SetBool("Static_b", true);
                startSceneActive = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && (isOnGround || doubleJumpReady) && !gameOver)
            {
                if (!isOnGround)
                {
                    doubleJumpReady = false;
                }
                dirtParticle.Stop();
                playerRb.AddForce(Vector3.up * 100, ForceMode.Impulse);
                isOnGround = false;
                playerAnim.SetTrigger("Jump_trig");
                playerAudio.PlayOneShot(jumpSound, 1.0f);
            }
            if (Input.GetKeyDown(KeyCode.W) && !gameOver)
            {
                isDashing = true;
            }
            if (Input.GetKeyUp(KeyCode.W) && !gameOver)
            {
                isDashing = false;
            }

            if (isDashing)
            {
                Time.timeScale = 2;
            }
            else
            {
                Time.timeScale = 1;
            }

            if (!gameOver)
            {
                score += Time.deltaTime;
                Debug.Log(score);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            doubleJumpReady = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            explosionParticle.Play();
            dirtParticle.Stop();
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            playerAudio.PlayOneShot(crashSound, 1.0f);
        } 
    }
}
