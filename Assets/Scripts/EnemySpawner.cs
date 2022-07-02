using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("The Player")]
    [SerializeField]
    GameObject player = null;

    [Tooltip("The Space Fox Enemy to Spawn")]
    [SerializeField]
    GameObject spaceFox = null;

    [Tooltip("How long for initial spawn delay")]
    [SerializeField]
    float SpawnTime = 3f;

    [Tooltip("How far away from the player the enemies must be to spawn")]
    [SerializeField]
    float SpawnDistance = 3f;

    [Tooltip("Maximum Spawn Time (Enemies per Second)")]
    [SerializeField]
    int maxSpawnRate = 3;

    Vector2 TopRight, SpawnLocation;
    float halfWidth, halfHeight, actualSpawnDistance, spawnRange;
    bool spawning;

    AsteroidSpawner aSpawner;

    // Start is called before the first frame update
    void Start()
    {
        aSpawner = FindObjectOfType<AsteroidSpawner>();
        spawnRange = aSpawner.minBoundsSpawnRange;
        TopRight = new Vector2(1, 1);
        Vector2 SizeVector = Camera.main.ViewportToWorldPoint(TopRight);
        halfHeight = spawnRange * 0.75f;
        halfWidth = spawnRange * 0.75f;

        // Make sure the SpawnDistance translates between different screen sizes
        actualSpawnDistance = (Vector2.Distance(TopRight * SizeVector, Vector2.zero) / SizeVector.magnitude * SpawnDistance);
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawning)
        {
            GenerateSpawnPoint();
            while (Vector2.Distance(SpawnLocation, player.transform.position) < actualSpawnDistance)
            {
                GenerateSpawnPoint();
            }
            StartCoroutine(SpawnCountDown());
            Debug.Log(SpawnTime);
            if (SpawnTime * 0.9f > (1.0f / maxSpawnRate))
            {
                SpawnTime *= 0.9f;
            }
            else
            {
                SpawnTime = (1.0f / maxSpawnRate);
            }
            spawning = true;
        }
    }

    void GenerateSpawnPoint()
    {
        float x, y;
        x = Random.Range(-halfWidth, halfWidth);
        y = Random.Range(-halfHeight, halfHeight);
        SpawnLocation = new Vector2(x, y);
    }

    void SpawnEnemy()
    {
        var enemy = Instantiate(spaceFox,SpawnLocation,Quaternion.identity);
		enemy.transform.SetParent( transform );
    }

    IEnumerator SpawnCountDown()
    {
        SpawnEnemy();
        yield return new WaitForSeconds(SpawnTime);
        spawning = false;
    }
}
