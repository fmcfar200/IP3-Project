﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PBSpawnScript : MonoBehaviour {

    public List<GameObject> spawns = new List<GameObject>();
    public List<GameObject> arrows = new List<GameObject>();
    public List<Button> buttons = new List<Button>();

    GameObject arrow;
    float spawnDelay;
    bool spawning;
    int wave;
    int maxWave;
    int spawnAmount;
    int score;

    GameControllerScript gameController;
    GameObject gameControllerObj;

    public Text scoreText;

    int bikeTier;

    void Start()
    {
        spawning = true;
        wave = 0;
        maxWave = 3;

        gameControllerObj = GameObject.Find("GameController");
        if(gameControllerObj!=null)
        {
            gameController = gameControllerObj.GetComponent<GameControllerScript>();
            bikeTier = gameController.bikeTier;

            if (bikeTier == 0)
            {
                spawnDelay = 1.75f;
                spawnAmount = 5;

            }
            else if (bikeTier == 1)
            {
                spawnDelay = 1.5f;
                spawnAmount = 8;
            }
            else if (bikeTier > 1)
            {
                spawnDelay = 1.25f;
                spawnAmount = 10;
            }
        }

        foreach(Button button in buttons)
        {
            button.onClick.AddListener(() => ButtonClick());
        }

        StartCoroutine(Spawn());

    }

    void Update()
    {
        if (spawning == false)
        {
            SceneManager.LoadScene(1);
        }

        scoreText.text = "Score: " + score.ToString();
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(3.0f);

        while (spawning)
        {
            if (wave != maxWave)
            {
                for (int i = 0; i < spawnAmount; i++)
                {
                    Instantiate(arrows[Random.Range(0, arrows.Count)], spawns[Random.Range(0, spawns.Count)].transform.position, Quaternion.identity);

                    yield return new WaitForSeconds(spawnDelay);
                }
                wave++;

                yield return new WaitForSeconds(3.0f);
            }
            else
            {
                if (gameController.bikeTier > 0)
                {
                    gameController.healthy += 2 * gameController.bikeTier;
                    gameController.active += 2 * gameController.bikeTier;
                }
                else
                {
                    gameController.healthy += 2;
                    gameController.active += 2;
                }
                spawning = false;

            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Arrow")
        {
            arrow = coll.gameObject;


        }
    }

    void ButtonClick()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;

        if (arrow != null)
        {
            if (button.name == "UpButton")
            {
                if (arrow.name == "ArrowUp(Clone)")
                {
                    Destroy(arrow);
                    AddScore();
                    Debug.Log("Correct");

                }
                else
                {
                    Debug.Log("Wrong");
                }
            }
            else if (button.name == "DownButton")
            {
                if (arrow.name == "ArrowDown(Clone)")
                {
                    Destroy(arrow);
                    AddScore();
                    Debug.Log("Correct");

                }
                else
                {
                    Debug.Log("Wrong");
                }
            }
            else if (button.name == "LeftButton")
            {
                if (arrow.name == "ArrowLeft(Clone)")
                {
                    Destroy(arrow);
                    AddScore();
                    Debug.Log("Correct");

                }
                else
                {
                    Debug.Log("Wrong");
                }
            }
            else if (button.name == "RightButton")
            {
                if (arrow.name == "ArrowRight(Clone)")
                {
                    Destroy(arrow);
                    AddScore();
                    Debug.Log("Correct");

                }
                else
                {
                    Debug.Log("Wrong");
                }
            }


        }
    }

    void AddScore()
    {
        score += 10;
        gameController.playerScore += score;
    }

    public void Return()
    {
        SceneManager.LoadScene(1);
    }

}
