using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferFishBubbleSpawn : MonoBehaviour {

    // Okay, bubble spawning works! Do I need to decrease the max heigh at which bubble can be spawned?

    // We have made the following variable public so we can change it depending on
    // how many fish are currently in the tank

    public float pufferFishBubbleSpawnWaitTime;

    [SerializeField]
    private GameObject pufferFishBubble;
    [SerializeField]
    private float minSpawnPositionX;
    [SerializeField]
    private float maxSpawnPositionX;
    [SerializeField]
    private float minSpawnDepth;
    [SerializeField]
    private float maxSpawnDepth;

    private float spawnPositionX;
    private float spawnPositionZ;


    void Start()
    {
        pufferFishBubbleSpawnWaitTime = 5f;

        InvokeRepeating("SpawnPufferFishBubble", pufferFishBubbleSpawnWaitTime, pufferFishBubbleSpawnWaitTime);
    }

    void SpawnPufferFishBubble()
    {
        spawnPositionX = Random.Range(minSpawnPositionX, maxSpawnPositionX);
        spawnPositionZ = Random.Range(minSpawnDepth, maxSpawnDepth);

        Instantiate(pufferFishBubble, new Vector3(spawnPositionX, transform.position.y, spawnPositionZ), transform.rotation);
    }
}
