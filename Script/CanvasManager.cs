using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI bestScoreTxt;
    [SerializeField] private GameObject stickyNote;

    [SerializeField] private GameObject StartGamePanel;

    private int bestScore;

    void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore");
        bestScoreTxt.text = bestScore.ToString();

        Time.timeScale = 0f;
        StartGamePanel.SetActive(true);
    }

    void Update()
    {
        SpaceStart();
        if (Time.timeScale == 1f)
        {
            scoreTxt.text = MapGenerator.waveNum.ToString();

            if (MapGenerator.waveNum > bestScore)
            {
                bestScore = MapGenerator.waveNum;
                PlayerPrefs.SetInt("BestScore", bestScore);
                bestScoreTxt.text = bestScore.ToString();
            }
        }
    }

    private void SpaceStart()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartButton();
        }
    }

    public void StartButton()
    {
        Time.timeScale = 1f;
        StartGamePanel.SetActive(false);
        stickyNote.SetActive(false);
    }
}
