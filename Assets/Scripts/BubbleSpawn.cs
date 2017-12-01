using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawn : MonoBehaviour {

    public bool isSpawningFirstDecoration;
    public bool isSpawningDecorations;
    public bool isSpawningFish;
    public bool isSpawningFood;
    public bool isEndingScene;

    [SerializeField]
    private GameObject fishBubble;
    [SerializeField]
    private GameObject emptyBubbleSmall;
    [SerializeField]
    private GameObject emptyBubbleLarge;
    [SerializeField]
    private float minSpawnHeight;
    [SerializeField]
    private float maxSpawnHeight;
    [SerializeField]
    private float minSpawnDepth;
    [SerializeField]
    private float maxSpawnDepth;
    [SerializeField]
    private GameObject decorationBubble;
    [SerializeField]
    private GameObject foodBubble;
    [SerializeField]
    private GameObject backgroundSoundManager;

    private float spawnPositionY;
    private float spawnPositionZ;
    private float emptyBubbleSmallSpawnWaitTime;
    private float emptyBubbleLargeSpawnWaitTime;
    private float fishBubbleSpawnWaitTime;
    private float foodBubbleSpawnWaitTime;
    private float decorationBubbleSpawnWaitTime;
    private float currBackgroundMusicTrackTime;
    private bool emptyBubblesCalled = false;
    private bool decorationBubblesCalled = false;
    private bool stoppedDecorations = false;
    private bool fishBubbleCalled = false;
    private bool stoppedFishes = false;
    private bool foodBubbleCalled = false;
    private bool stoppedFood = false;
    private Vector3 tankPosition;
    private AudioSource[] backgroundMusicAudioSources;


    void Start ()
    {
        tankPosition = gameObject.transform.position;
        isSpawningFirstDecoration = true;
        isSpawningDecorations = false;
        isSpawningFish = false;
        isSpawningFood = false;
        isEndingScene = false;

        emptyBubbleSmallSpawnWaitTime = 2f;
        emptyBubbleLargeSpawnWaitTime = 3.5f;
        fishBubbleSpawnWaitTime = 5f;
        decorationBubbleSpawnWaitTime = 4f;
        foodBubbleSpawnWaitTime = 5f;

        Invoke("SpawnDecorationBubble",35f);

        InvokeRepeating("SpawnEmptyBubbleSmall", emptyBubbleSmallSpawnWaitTime, emptyBubbleSmallSpawnWaitTime);
        InvokeRepeating("SpawnEmptyBubbleLarge", emptyBubbleLargeSpawnWaitTime, emptyBubbleLargeSpawnWaitTime);

        // Once we make the first wave of bubbles start spawning, we get the backgroundSoundManager's first audio source and play it

        backgroundMusicAudioSources = backgroundSoundManager.GetComponents<AudioSource>();
        backgroundMusicAudioSources[0].Play();
    }

    private void Update()
    {
        /*
        if (!isSpawningFirstDecoration && !emptyBubblesCalled)
        {
            emptyBubblesCalled = true;
            InvokeRepeating("SpawnEmptyBubbleSmall", emptyBubbleSmallSpawnWaitTime, emptyBubbleSmallSpawnWaitTime);
            InvokeRepeating("SpawnEmptyBubbleLarge", emptyBubbleLargeSpawnWaitTime, emptyBubbleLargeSpawnWaitTime);
        }
        */

        if (isSpawningDecorations && !decorationBubblesCalled)
        {
            decorationBubblesCalled = true;
            InvokeRepeating("SpawnDecorationBubble", decorationBubbleSpawnWaitTime, decorationBubbleSpawnWaitTime);

            // We now play the music that cooresponds with the decoration bubble spawn wave,
            // making sure that it plays in time with the base background music track

            backgroundMusicAudioSources[1].time = backgroundMusicAudioSources[0].time;
            backgroundMusicAudioSources[1].Play();
        }

        if (decorationBubblesCalled && (TankManager.numDecorationsInTank == TankManager.maxDecorationsInTank) && !stoppedDecorations)
        {
            stoppedDecorations = true;
            isSpawningDecorations = false;
            isSpawningFish = true;
            CancelInvoke("SpawnDecorationBubble");
        }

        if (isSpawningFish && !fishBubbleCalled)
        {
            fishBubbleCalled = true;
            InvokeRepeating("SpawnFishBubble", fishBubbleSpawnWaitTime, fishBubbleSpawnWaitTime);

            // We now play the music that cooresponds with the fish bubble spawn wave,
            // making sure that it plays in time with the base background music track

            backgroundMusicAudioSources[2].time = backgroundMusicAudioSources[0].time;
            backgroundMusicAudioSources[3].time = backgroundMusicAudioSources[0].time;
            backgroundMusicAudioSources[2].Play();
            backgroundMusicAudioSources[3].Play();
        }

        Debug.Log("Number of fish in tank: " + TankManager.numFishInTank.ToString());

        if (fishBubbleCalled && (TankManager.numFishInTank == TankManager.maxFishInTank) && !stoppedFishes)
        {
            stoppedFishes = true;
            isSpawningFood = true;
            isSpawningFish = false;
            CancelInvoke("SpawnFishBubble");
        }

        if (isSpawningFood && !foodBubbleCalled)
        {
            foodBubbleCalled = true;
            InvokeRepeating("SpawnFoodBubble", foodBubbleSpawnWaitTime, foodBubbleSpawnWaitTime);

            // We now stop one of the fish bubble spawn wave background music tracks and play the music that cooresponds with the food bubble spawn wave,
            // making sure that it plays in time with the base background music track

            backgroundMusicAudioSources[3].Stop();
            backgroundMusicAudioSources[4].time = backgroundMusicAudioSources[0].time;
            backgroundMusicAudioSources[5].time = backgroundMusicAudioSources[0].time;
            backgroundMusicAudioSources[4].Play();
            backgroundMusicAudioSources[5].Play();
        }

        if (foodBubbleCalled && (TankManager.numFoodInTank == TankManager.maxFoodInTank) && !stoppedFood)
        {
            stoppedFood = true;
            isSpawningFood = true;
            isSpawningFish = false;
            CancelInvoke("SpawnFoodBubble");
        }
    }



    IEnumerator RunSpawner ()
    {
        // Need to add code for initial teaching phase here (with single decoration bubble)

        // While we are in the decoration bubble spawning phase, we start making repeated calls to SpawnDecorationBubble

        /*while (TankManager.numDecorationsInTank < TankManager.maxDecorationsInTank)
        {
            // Spawn decoration bubble
        }*/


        // Play audio cue for finishing decoration phase

        // If we are in the fish bubble spawning phase, we cancel our repeating calls to SpawnDecorationBubble
        // and start making repeated calls to SpawnFishBubble

        while (TankManager.numFishInTank < TankManager.maxFishInTank && isSpawningFish)
        {
            // Spawn fish bubble;
            Invoke("SpawnFishBubble", fishBubbleSpawnWaitTime);
        }


        // Play audio cue for finishing fish population phase

        // If we are in the food bubble spawning phase, we cancel our repeating calls to SpawnFishBubble
        // and start making repeated calls to SpawnFoodBubble

       /* while (TankManager.numDecorationsInTank < TankManager.maxDecorationsInTank)
        {
            // Spawn food bubble
        }
        */


        // Play ending phase

        yield return null;
    }


    // InvokeRepeating("SpawnFishBubble", fishBubbleSpawnWaitTime, fishBubbleSpawnWaitTime);



    void SpawnEmptyBubbleSmall()
    {
        spawnPositionY = Random.Range(tankPosition.y + minSpawnHeight, tankPosition.y + maxSpawnHeight);
        spawnPositionZ = Random.Range(tankPosition.z + minSpawnDepth, tankPosition.z + maxSpawnDepth);

        Instantiate(emptyBubbleSmall, new Vector3(transform.position.x, spawnPositionY, spawnPositionZ), transform.rotation);
    }


    void SpawnEmptyBubbleLarge()
    {
        spawnPositionY = Random.Range(tankPosition.y + minSpawnHeight, tankPosition.y + maxSpawnHeight);
        spawnPositionZ = Random.Range(tankPosition.z + minSpawnDepth, tankPosition.z + maxSpawnDepth);

        Instantiate(emptyBubbleLarge, new Vector3(transform.position.x, spawnPositionY, spawnPositionZ), transform.rotation);
    }


    void SpawnDecorationBubble()
    {
        spawnPositionY = Random.Range(tankPosition.y + minSpawnHeight, tankPosition.y + maxSpawnHeight);
        spawnPositionZ = Random.Range(tankPosition.z + minSpawnDepth, tankPosition.z + maxSpawnDepth);

        // Instantiate the decoration bubble
        Instantiate(decorationBubble, new Vector3(transform.position.x, spawnPositionY, spawnPositionZ), transform.rotation);
    }


    void SpawnFishBubble()
    {
        spawnPositionY = Random.Range(tankPosition.y + minSpawnHeight, maxSpawnHeight);
        spawnPositionZ = Random.Range(tankPosition.z + minSpawnDepth, tankPosition.z + maxSpawnDepth);

        Instantiate(fishBubble, new Vector3(transform.position.x, spawnPositionY, spawnPositionZ), transform.rotation);
    }


    void SpawnFoodBubble()
    {
        spawnPositionY = Random.Range(tankPosition.y + minSpawnHeight, tankPosition.y + maxSpawnHeight);
        spawnPositionZ = Random.Range(tankPosition.z + minSpawnDepth, tankPosition.z + maxSpawnDepth);

        // Instantiate the food bubble
        Instantiate(foodBubble, new Vector3(transform.position.x, spawnPositionY, spawnPositionZ), transform.rotation);
    }

}
