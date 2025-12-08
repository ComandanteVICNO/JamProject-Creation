using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager instance;
    
    [SerializeField] private GameObject gruntPrefab;
    [SerializeField] private GameObject rangerPrefab;

    [SerializeField] private float baseInterval = 1.5f;
    [SerializeField] private float minInterval = 0.2f;
    [SerializeField] private float timeToMax = 1200f;
    float elapsedTime = 0f;
    private float currentInterval;

    public List<GameObject> enemies = new List<GameObject>();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        currentInterval = baseInterval;
    }

    private void Update()
    {
        enemies.RemoveAll(item => item == null);
        float t = Mathf.Clamp01(elapsedTime / timeToMax);
        currentInterval = Mathf.Lerp(baseInterval, minInterval, t);
        
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= currentInterval && enemies.Count < 100)
        {
            SpawnEnemy();
            elapsedTime = 0f;
        }
        
    }

    void SpawnEnemy()
    {
        int rand = UnityEngine.Random.Range(1,3);
        GameObject enemyToSpawn;
        if (rand == 1)
        {
            enemyToSpawn = gruntPrefab;
        }
        else enemyToSpawn = rangerPrefab;

        Vector2 spawnPosition = GetOffScreenSpawnPosition();
        GameObject enemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
        enemies.Add(enemy);
    }
    
    Vector2 GetOffScreenSpawnPosition()
    {
        Camera cam = Camera.main;

        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        float extra = 3f; // how far outside the screen to spawn
        int side = UnityEngine.Random.Range(0, 4); // 0=Top, 1=Bottom, 2=Left, 3=Right

        Vector2 camPos = cam.transform.position;
        Vector2 pos = Vector2.zero;

        switch (side)
        {
            case 0: // Top
                pos = new Vector2(
                    UnityEngine.Random.Range(-width / 2, width / 2),
                    height / 2 + extra
                );
                break;

            case 1: // Bottom
                pos = new Vector2(
                    UnityEngine.Random.Range(-width / 2, width / 2),
                    -(height / 2 + extra)
                );
                break;

            case 2: // Left
                pos = new Vector2(
                    -(width / 2 + extra),
                    UnityEngine.Random.Range(-height / 2, height / 2)
                );
                break;

            case 3: // Right
                pos = new Vector2(
                    width / 2 + extra,
                    UnityEngine.Random.Range(-height / 2, height / 2)
                );
                break;
        }

        // shift from "relative to camera center" to world space
        pos += camPos;

        return pos;
    }
    
    
}
