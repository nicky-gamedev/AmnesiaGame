﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavenManager : MonoBehaviour
{
    public float speed;
    public float life;
    public GameObject cloudsPrefab;


     void Awake()
    {
        transform.position = new Vector3 (0, 0, 0);
        StartCoroutine(SpawnClouds());
    }

    void Update()
    {
        cloudsPrefab.transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    IEnumerator SpawnClouds()
    {
        Instantiate(cloudsPrefab);

        Destroy(gameObject, life);
        yield return new WaitForSeconds(1f);
        StartCoroutine(SpawnClouds());
    }


public class InstantiateObjectsConfig : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject[] spawnedPrefabs;
    [SerializeField] private float groundLength;

    private List<GameObject> activeTiles = new List<GameObject>();

    private float spawnZ = 0;
    private int groundsOnScreen = 6;
    private bool first;
    private int index;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i = 0; i < groundsOnScreen; i++)
        {
            if (i <= 1)
            {
                first = true;
                SpawnGround(0);
            }
            else
            {
                int raffle = Random.Range(-10000000, 100000000);
                if (raffle < 0)
                    SpawnGround(Random.Range(0, spawnedPrefabs.Length));
                else
                    SpawnGround(Random.Range(1, spawnedPrefabs.Length));
            }
        }
    }

    void Update()
    {
        if (playerTransform.position.z > spawnZ - (groundsOnScreen * groundLength))
        {
            SpawnGround(Random.Range(0, spawnedPrefabs.Length));
            if (!first)
                DeleteTile();
        }
    }

    void SpawnGround(int groundIndex)
    {
        GameObject ground = Instantiate(spawnedPrefabs[groundIndex], transform.forward * spawnZ, transform.rotation);
        activeTiles.Add(ground);
        spawnZ += groundLength;
    }

    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}






}
