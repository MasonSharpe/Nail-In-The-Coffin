using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;
    public int phase = 0;
    public List<string> instructions;
    public float timer;
    public bool canClick;

    public Transform enemies;
    public TextMeshProUGUI instructionsText;
    public NailPoint point;
    public EnemySpawner spawner;
    public EnemySpawner spawner2;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        point.gameObject.SetActive(false);
        point.enabled = false;
        phase = -1;
        spawner.spawning = false;
        spawner2.spawning = false;
        canClick = true;
        NextPhase();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && canClick)
        {
            NextPhase();
        }
    }

    public void NextPhase()
    {
        phase++;

        instructionsText.text = instructions[phase];
        canClick = true;

        switch (phase)
        {
            case 3:
                spawner.spawning = true;
                spawner2.spawning = true;
                canClick = false;
                break;
            case 4:
                spawner.spawning = false;
                spawner2.spawning = false;
                break;

            case 5:
                spawner.spawning = true;
                spawner2.spawning = true;
                canClick = false;
                break;

            case 6:
                point.gameObject.SetActive(true);
                point.enabled = true;
                canClick = false;
                break;

            case 7:
                canClick = false;
                break;
        }

    }
    public void ClearEnemies()
    {
        for (int i = 0; i < enemies.childCount; i++)
        {
            Destroy(enemies.GetChild(0).gameObject);
        }
    }
}
