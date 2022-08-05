using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float jumpForce = 100;
    [SerializeField] private GameObject cam;
    [SerializeField] public bool jumped;
    [SerializeField] private GameObject deathAnimation;
    [SerializeField] private ParticleSystem jumpFX;
    [SerializeField] private PostProcessVolume volume;
    private ChromaticAberration chromaticAberration;

    private bool dodgeRight;
    private bool dodgeLeft;
    private bool canDodge = true;

    private bool jump;

    private bool stopControll;

    private float gravityScale;

    private Rigidbody2D rb;
   

    [SerializeField] public int score;
    void Start()
    {
        volume.profile.TryGetSettings(out chromaticAberration);
        rb = GetComponent<Rigidbody2D>();

        gravityScale = rb.gravityScale;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && rb.gravityScale == gravityScale && !stopControll)
        {
            jump = true;
            jumped = true;
            jumpFX.Play();
        }

        if (MapCleaner.canDodge && canDodge)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                dodgeRight = true;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                dodgeLeft = true;
            }

        }

        if (MapCleaner.dead)
        {
            DeathAnimation();
            MapCleaner.dead = false;
            stopControll = true;
            gravityScale = 0;
            rb.gravityScale = 0;
            canDodge = false;
            StartCoroutine(RestartLevel());
        }
        
    }

    private void FixedUpdate()
    {
        if (jump)
        {
            rb.gravityScale = 0;
            rb.AddForce(Vector2.up * jumpForce);

            jump = false;
        }
        else
        {
            rb.gravityScale = gravityScale;
        }


        if (dodgeRight)
        {
            StartCoroutine(SlowDownTime());
        }

        if (dodgeLeft)
        {
            StartCoroutine(SlowDownTime());
        }
    }

    private void DeathAnimation()
    {
        deathAnimation.SetActive(true);
    }

    IEnumerator SlowDownTime()
    {
        Time.timeScale = 0.5f;

        chromaticAberration.intensity.value = 0.5f;

        if (dodgeRight)
        {
            rb.AddForce(Vector2.right * jumpForce);
        }
        else if(dodgeLeft)
        {
            rb.AddForce(Vector2.left * jumpForce);
        }
        dodgeLeft = false;
        dodgeRight = false;
        MapCleaner.canDodge = false;
        yield return new WaitForSeconds(0.2f);
        chromaticAberration.intensity.value = 0;
        Time.timeScale = 1;
        print("return");
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(1);
        {
            SceneManager.LoadScene("Main");
        }
    }
}