using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro scoreText;

    GameObject player;
    PlayerScript playerScript;

    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerScript>();
    }

    void Update()
    {
        ApplyScore();
    }

    private void ApplyScore()
    {
        scoreText.text = playerScript.score.ToString();
    }
}
