using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapGenerator : MonoBehaviour
{
    public static int waveNum = 0;


    [SerializeField] private GameObject square;
    [SerializeField] private Collider2D barrier;
    private GameObject[] squares;

    private bool enableNormalSpawn = true;
    private bool enableAnySpawn = true;

    private int maxSquareOnTheMap = 5;

    void Start()
    {
        waveNum = 0;
    }

    void Update()
    {
        StartCoroutine(GenerateMap());
    }


    IEnumerator GenerateMap()
    {
        squares = GameObject.FindGameObjectsWithTag("Square");

        if(squares.Length <= maxSquareOnTheMap && enableNormalSpawn && enableAnySpawn)
        {
            GenerateSquareBlock();
            yield return null;
        }
        else if(squares.Length <= maxSquareOnTheMap && enableAnySpawn)
        {
            GenerateSquareLine();
        }
    }

    private void GenerateSquareBlock()
    {
        int randomInt = Random.Range(-5, 5);

        Instantiate(square, new Vector2(9, randomInt), Quaternion.identity);

        waveNum++;
        WaveDifficulty();
    }

    private void GenerateSquareLine()
    {

        int randomInt = Random.Range(-4, 4);
        int secRandomInt = Random.Range(-4, 4);
        for (int x = -4; x<4; x++)
        {
            if(x != randomInt && x != secRandomInt)
            {
                GameObject newSquare = Instantiate(square, new Vector2(9, x), Quaternion.identity);
                SquareMove squareMove = newSquare.GetComponent<SquareMove>();
                squareMove.moveStraight = true;
                waveNum++;
                WaveDifficulty();
            }
        }
    }

    public void WaveDifficulty()
    {
        if(waveNum == 20)
        {
            BossFightScript.bossFight = true;
            enableAnySpawn = false;
            barrier.isTrigger = true;
        }
        if (waveNum == 50)
        {
            enableAnySpawn = true;
            barrier.isTrigger = false;
            enableNormalSpawn = false;
            maxSquareOnTheMap += 2;
        }
        if (waveNum == 120)
        {
            enableNormalSpawn = true;
            maxSquareOnTheMap += 3;
            GenerateLine();
        }
        if (waveNum == 150)
        {
            maxSquareOnTheMap += 3;
            GenerateLine();
        }
        if (waveNum == 200)
        {
            maxSquareOnTheMap += 2;
            GenerateLine();
        }
    }

    private void GenerateLine()
    {
        for(int i = 0; i < 2; i++)
        {
            int randomInt = Random.Range(-4, 4);
            for (int x = 9; x < 25; x++)
            {
                GameObject newSquare = Instantiate(square, new Vector2(x, randomInt), Quaternion.identity);
                SquareMove squareMove = newSquare.GetComponent<SquareMove>();
                squareMove.moveStraight = true;
            }
        }
    }
}
