using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SquareMove : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float mapSpeed;
    [SerializeField] public bool moveStraight;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private AudioClip impactNormal;
    [SerializeField] private AudioClip impactDodge;
    AudioSource audioSource;
    public TextMeshPro text;

    private GameObject[] squares;
    private GameObject player;
    private bool followTarget = true;
    private float timeRemaining = 5;
    private bool anotherSide;
    private bool redKiller;
    private bool dodge;
    private bool thereIsDodge;

    private Rigidbody2D rb;

    private int randomInt;
    private void Start()
    {
        squares = GameObject.FindGameObjectsWithTag("Square");
        foreach(GameObject square in squares)
        {
            SquareMove squareMove = square.GetComponent<SquareMove>();
            if (squareMove.dodge)
            {
                thereIsDodge = true;
            }
        }
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();

        if(text.color != Color.red && text.text != "i" && text.text != "3")
        {
            randomInt = Random.Range(-5, 5);
            if (transform.position.y == 4)
            {
                text.text = "1";
            }
            else if (transform.position.y == 3)
            {
                text.text = "2";
            }
            else if (transform.position.y == 1)
            {
                text.text = "4";
            }
            else if (transform.position.y == 0)
            {
                text.text = "5";
            }
            else if (transform.position.y == -4)
            {
                text.text = "9";
            }
            else if (transform.position.y == -3)
            {
                text.text = "8";
            }
            else if (transform.position.y == -2)
            {
                text.text = "7";
            }
            else if (transform.position.y == -1)
            {
                text.text = "6";
            }
        }
        
        int randomChance = Random.Range(0, 101);
        if (randomChance <= 10 && text.color != Color.green && text.text != "3")
        {
            text.color = Color.red;
        }
        else if(randomChance >= 95 && text.text != "3" && text.color != Color.green)
        {
            text.text = "i";
        }
        else if(randomChance> 10 && randomChance <= 20)
        {
            text.text = "3";
        }

        if(text.color == Color.red && !dodge)
        {
            redKiller = true;
        }
        else if(text.text == "i" && text.text != "3")
        {
            mapSpeed *= 3;
            rb.mass *= 2;
        }
        else if(text.text == "3" && !redKiller && !MapCleaner.canDodge && !thereIsDodge)
        {
            text.color = Color.green;
            dodge = true;
        }
    }
    void Update()
    {
        if (moveStraight && followTarget)
        {
            MoveStraight();
            return;
        }
        if (!anotherSide && followTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(-9, randomInt), mapSpeed * Time.deltaTime);
            if (transform.position.x == -9)
            {
                Destroy(gameObject);
            }
            else
            {
                StartTimerToGoBack();
            }
        }
        else if(followTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(9, randomInt), mapSpeed * Time.deltaTime);
            if (transform.position.x == 9)
            {
                Destroy(gameObject);
            }
        }
        if (!followTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, player.transform.position.y), mapSpeed * Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.1f, 0.1f, 0.1f), Time.deltaTime * 2);
            if(transform.position == player.transform.position)
            {
                Destroy(gameObject);
            }
        }

        Destroy(gameObject, 15);
    }


    private void StartTimerToGoBack()
    {
        if (timeRemaining <= 0)
        {
            anotherSide = true;
        }
        else
        {
            timeRemaining -= Time.deltaTime;
        }

    }

    private void MoveStraight()
    {
        if (!anotherSide)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(-9, transform.position.y), mapSpeed * Time.deltaTime);
            if (transform.position.x == -9)
            {
                Destroy(gameObject);
            }
            else
            {
                StartTimerToGoBack();
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(9, randomInt), mapSpeed * Time.deltaTime);
            if (transform.position.x == 9)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && redKiller)
        {
            MapCleaner.dead = true;
            audioSource.PlayOneShot(impactDodge);
        }

        if (collision.gameObject.tag == "Player" && dodge)
        {
            MapCleaner.canDodge = true;
            _collider.isTrigger = true;
            followTarget = false;
            
            audioSource.PlayOneShot(impactDodge);
        }

        if (collision.gameObject.tag == "Player" && !redKiller && !dodge)
        {
            audioSource.PlayOneShot(impactNormal);
        }
    }
}
