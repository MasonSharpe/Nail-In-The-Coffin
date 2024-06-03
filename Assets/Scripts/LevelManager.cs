using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public int level;
    public List<NailPoint> points;
    public int spawnCap;
    public int enemiesSpawnedIn;
    public bool tutorial;

    public float totalKilled;

    public Canvas winScreen;

    public float timeScoreChallenge;
    public float cleanlinessScoreChallenge;

    public float timeScore; // starts at 0, goes up every second
    public float cleanlinessScore;


    private void Awake()
    {
        instance = this;
        timeScore = 0;
        cleanlinessScore = 1;
        Time.timeScale = 1;
        totalKilled = 0;
        GameManager.instance.currentLevel = level;
    }
    private void Update()
    {
        timeScore += Time.deltaTime;
    }
    public float CalculateEarnings()
    {
        float earnings = cleanlinessScore / 100;
        earnings += Mathf.Clamp((2 * timeScoreChallenge - timeScore) / timeScoreChallenge, 0.5f, 1);
        earnings *= GameManager.instance.currentLevel * 12;

        if (timeScore < timeScoreChallenge) earnings *= 2;
        if (cleanlinessScore > cleanlinessScoreChallenge) earnings *= 2;

        return Mathf.Round(earnings);
    }
    public void PlaceNail(NailEnemy enemy)
    {
        totalKilled++;
        enemiesSpawnedIn--;


        if (tutorial && Tutorial.instance.phase == 3) Tutorial.instance.NextPhase();

        for (int i = 0; i < points.Count; i++)
        {
            NailPoint point = points[i];

            float dist = Mathf.Abs((point.transform.position - enemy.transform.position).magnitude);

            if (!Physics2D.IsTouchingLayers(enemy.hitbox, enemy.player.boardMask))
            {
                if (tutorial && Tutorial.instance.phase == 7) Tutorial.instance.NextPhase();
                Destroy(enemy.gameObject);
            }
            else
            {
                cleanlinessScore = Mathf.Clamp(points.Count(element => element.counted) / totalKilled, 0.5f, 1) * 100;
            }

            if (dist > 1f || !point.enabled) continue;

            if (tutorial && Tutorial.instance.phase == 6) Tutorial.instance.NextPhase();
            point.counted = true;
            point.score = dist;
            point.sprite.enabled = false;
            PlayerManagement.instance.health = Mathf.Clamp(PlayerManagement.instance.health + PlayerManagement.instance.healthRegenAmount, 0, PlayerManagement.instance.maxHealth);

            float totalInPlace = points.Count(element => element.counted);
            if (totalInPlace == points.Count)
            {
                ShowWinScreen();
            }
        }
    }

    public void ShowWinScreen()
    {
        Time.timeScale = 0;
        winScreen.enabled = true;
        winScreen.GetComponent<UpgradeScreen>().Initiate();
    }
}
