using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public List<NailPoint> points;

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
        for (int i = 0; i < points.Count; i++)
        {
            NailPoint point = points[i];

            float dist = Mathf.Abs((point.transform.position - enemy.transform.position).magnitude);

            if (dist > 1.5f) continue;

            point.counted = true;
            point.score = dist;
            point.sprite.enabled = false;

            if (points.Count(element => element.counted) == points.Count)
            {
                winScreen.enabled = true;
            }
        }
    }
}
