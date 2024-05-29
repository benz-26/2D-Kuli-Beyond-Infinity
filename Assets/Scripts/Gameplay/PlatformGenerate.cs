using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerate : MonoBehaviour
{
    public GameObject[] platformPrefab;
    public float[] platformProbabilities; // Probabilities for each platform prefab
    public Transform playerTransform;
    public float spawnDistanceThreshold;

    public int initialNumberOfPlatforms;
    public int platformsToSpawn;
    public float levelWidth;
    public float minY;
    public float maxY;

    public Vector3 startPosition;

    private List<GameObject> platforms = new List<GameObject>();

    private void Start()
    {
        SpawnInitialPlatforms();
    }

    private void Update()
    {
        if (NeedToSpawnMorePlatforms())
        {
            SpawnMorePlatforms();
        }
    }

    private void SpawnInitialPlatforms()
    {
        Transform platformSpawned = GameObject.Find("PlatformSpawned").transform;
        Vector3 spawnPosition = startPosition;
        for (int i = 0; i < initialNumberOfPlatforms; i++)
        {
            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);

            // Select a random platform prefab based on probabilities
            GameObject selectedPlatformPrefab = SelectPlatformPrefab();

            GameObject platform = Instantiate(selectedPlatformPrefab, spawnPosition, Quaternion.identity);
            platforms.Add(platform);
            platform.transform.SetParent(platformSpawned);
        }
    }

    private GameObject SelectPlatformPrefab()
    {
        float totalProbability = 0f;
        foreach (float prob in platformProbabilities)
        {
            totalProbability += prob;
        }

        float randomValue = Random.value * totalProbability;
        float sum = 0f;
        for (int i = 0; i < platformProbabilities.Length; i++)
        {
            sum += platformProbabilities[i];
            if (randomValue <= sum)
            {
                return platformPrefab[i];
            }
        }

        // If no platform is selected, return the last one
        return platformPrefab[platformPrefab.Length - 1];
    }

    private bool NeedToSpawnMorePlatforms()
    {
        if (platforms.Count > 0)
        {
            float distanceToLastPlatform = Vector3.Distance(playerTransform.position, platforms[platforms.Count - 1].transform.position);
            return distanceToLastPlatform < spawnDistanceThreshold;
        }
        return false;
    }

    private void SpawnMorePlatforms()
    {
        Transform platformSpawned = GameObject.Find("PlatformSpawned").transform;

        Vector3 spawnPosition = platforms[platforms.Count - 1].transform.position;
        for (int i = 0; i < platformsToSpawn; i++)
        {
            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);

            GameObject selectedPlatformPrefab = SelectPlatformPrefab();
            GameObject platform = Instantiate(selectedPlatformPrefab, spawnPosition, Quaternion.identity);
            platforms.Add(platform);

            platform.transform.SetParent(platformSpawned);
        }
    }

    public void IsMainstreamTabletRes()
    {
        levelWidth = 2.6f;
    }

    public void IsAppleIpadRes()
    {
        levelWidth = 3.4f;
    }

    public void IsMainstreamPhoneRes()
    {
        levelWidth = 2f;
    }


}
