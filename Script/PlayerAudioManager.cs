using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip impact;
    [SerializeField] private AudioClip lobbySound;
    AudioSource audioSource;

    private bool inLobby;

    PlayerScript playerScript;
    void Start()
    {
        playerScript = GetComponent<PlayerScript>();
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(lobbySound);
    }

    void Update()
    {
        if (playerScript.jumped)
        {
            audioSource.PlayOneShot(impact);
            playerScript.jumped = false;
        }

        if(Time.timeScale == 1 && inLobby)
        {
            audioSource.Stop();
            inLobby = false;
        }
        if (Time.timeScale == 0)
        {
            if(!audioSource.isPlaying)
                audioSource.PlayOneShot(lobbySound);
            inLobby = true;
        }
    }


}
