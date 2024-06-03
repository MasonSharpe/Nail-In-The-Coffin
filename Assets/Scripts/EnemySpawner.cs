using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Renderer render;
    public NailEnemy enemyType;
    public Transform parent;
    public float delay;
    public bool spawning = true;

    public float timer = 0;
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0 && spawning)
        {
            timer = delay;
            bool notOnScreen = !GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(Camera.main), render.bounds);
            bool close = Mathf.Abs((PlayerManagement.instance.transform.position - transform.position).magnitude) < 20;
            if (LevelManager.instance.enemiesSpawnedIn < LevelManager.instance.spawnCap && notOnScreen && close)
            {
                NailEnemy enemy = Instantiate(enemyType, parent);
                enemy.transform.position = transform.position;
                LevelManager.instance.enemiesSpawnedIn++;
            }
        }
        
    }
}
