using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCleaner : MonoBehaviour
{
    public static bool dead;
    public static bool canDodge;

    private void Start()
    {
        canDodge = false;
        dead = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            dead = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dead = true;
        }
    }
}
