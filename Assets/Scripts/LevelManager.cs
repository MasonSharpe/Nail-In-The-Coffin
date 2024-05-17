using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public List<NailPoint> points;
    public int spawnCap;
    public int enemiesSpawnedIn;
    public bool tutorial;

    public Canvas winScreen;

    public float precisionScoreChallenge;
    public float timeScoreChallenge;
    public float cleanlinessScoreChallenge;

    public float precisionScore;
    public float timeScore;
    public float cleanlinessScore;


    private void Awake()
    {
        instance = this;
    }
    public void PlaceNail(NailEnemy enemy)
    {
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

            if (dist > 1.5f || !point.enabled) continue;

            if (tutorial && Tutorial.instance.phase == 6) Tutorial.instance.NextPhase();
            point.counted = true;
            point.score = dist;
            point.sprite.enabled = false;
            PlayerManagement.instance.health = Mathf.Clamp(PlayerManagement.instance.health + 0.5f, 0, PlayerManagement.instance.maxHealth);

            if (points.Count(element => element.counted) == points.Count)
            {
                //winScreen.enabled = true;
            }
        }
    }
}
