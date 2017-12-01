using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class BubblePop : MonoBehaviour, IInputClickHandler {

    [SerializeField]
    private Rigidbody[] fishPrefabs;
    [SerializeField]
    private Rigidbody[] decorationPrefabs;
    [SerializeField]
    private Rigidbody foodPrefab;
    [SerializeField]
    private GameObject bubblePopParticle;
    /*
    [SerializeField]
    private AudioClip[] popSounds;
    */

    public Rigidbody objectInBubbleRB;

    private GameObject bubbleSpawner;
    private bool isBubblePopped = false;

    // The variable on the following line contains the number of seconds
    // a bubble will last until it pops on its own (use only for empty bubbles)

    private float timeUntilPop;
    private float minTimeUntilPop;
    private float maxTimeUntilPop;
    private const string EMPTY_BUBBLE_TAG = "EmptyBubble";
    private const string FISH_BUBBLE_TAG = "FishBubble";
    private const string DECORATION_BUBBLE_TAG = "DecorationBubble";
    private const string FOOD_BUBBLE_TAG = "FoodBubble";
    // private AudioClip currBubblePopSound;

    // The following variable is used as a reference to another bubble's BubblePop script

    private BubblePop currBubblePop;

    // The following variable is used as a reference to a fish's FishManager script

    // private FishManager currFishManager;


    // Use this for initialization
    void Start () {
        minTimeUntilPop = 5f;
        maxTimeUntilPop = 20f;
        timeUntilPop = Random.Range(minTimeUntilPop, maxTimeUntilPop);
        bubbleSpawner = GameObject.Find("BubbleSpawner");

        //  Use Invoke to pop bubble after a randomly generated number of seconds

        if (gameObject.tag == EMPTY_BUBBLE_TAG)
        {
            Invoke("PopEmptyBubble", timeUntilPop);
        }

        // If the bubble we just spawned is either a regular fish bubble or a puffer fish bubble,
        // we instantiate a fish at the center of the bubble

        if (gameObject.tag == FISH_BUBBLE_TAG)
        {
            int randomIndex = Random.Range(0, fishPrefabs.Length);
            objectInBubbleRB = Instantiate(fishPrefabs[randomIndex], transform.position, transform.rotation) as Rigidbody;
        }

        if (gameObject.tag == DECORATION_BUBBLE_TAG)
        {
            int randomIndex = Random.Range(0, decorationPrefabs.Length);
            objectInBubbleRB = Instantiate(decorationPrefabs[randomIndex], transform.position, transform.rotation) as Rigidbody;
        }

        if (gameObject.tag == FOOD_BUBBLE_TAG)
        {
            objectInBubbleRB = Instantiate(foodPrefab, transform.position, transform.rotation) as Rigidbody;
        }

    }


    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (gameObject.tag == EMPTY_BUBBLE_TAG)
        {
            PopEmptyBubble();
        }

        if (gameObject.tag == FISH_BUBBLE_TAG || gameObject.tag == DECORATION_BUBBLE_TAG || gameObject.tag == FOOD_BUBBLE_TAG)
        {
            PopObjectBubble();
        }
    }


    // Update is called once per frame
    void Update () {
		
        if (!isBubblePopped && (gameObject.tag == FISH_BUBBLE_TAG || gameObject.tag == DECORATION_BUBBLE_TAG || gameObject.tag == FOOD_BUBBLE_TAG))
        {
            objectInBubbleRB.gameObject.transform.position = transform.position;
        }
	}


    void PopEmptyBubble()
    {
        isBubblePopped = true;
        Instantiate(bubblePopParticle, transform.position, transform.rotation);
        gameObject.GetComponent<AudioSource>().Play();
        // PlayBubblePopSound();
        StartCoroutine(WaitBeforePopping());
    }


    public void PopObjectBubble()
    {
        // If tag on game object is "DecorationBubble" and it's the first decoration bubble, start spawning more decorations

        if (gameObject.tag == DECORATION_BUBBLE_TAG && bubbleSpawner.GetComponent<BubbleSpawn>().isSpawningFirstDecoration)
        {
            bubbleSpawner.GetComponent<BubbleSpawn>().isSpawningFirstDecoration = false;
            bubbleSpawner.GetComponent<BubbleSpawn>().isSpawningDecorations = true;
        }

        isBubblePopped = true;
        Instantiate(bubblePopParticle, transform.position, transform.rotation);
        gameObject.GetComponent<AudioSource>().Play();
        // PlayBubblePopSound();
        objectInBubbleRB.isKinematic = false;
        StartCoroutine(WaitBeforePopping());
    }

    /*
    void PlayBubblePopSound ()
    {
        int randomIndex = Random.Range(0, popSounds.Length);
        currBubblePopSound = popSounds[randomIndex];
        GetComponent<AudioSource>().clip = currBubblePopSound;
        GetComponent<AudioSource>().Play();
    }
    */

    // The following coroutine waits for a set number of seconds 
    // before setting the bubble game object this script is attached to
    // to be inactive in the scene

    IEnumerator WaitBeforePopping ()
    {
        // Since we only call this coroutine after the PlayBubblePopSound function has been called,
        // we first tell the coroutine to wait for the length of the clip we assigned in the PlayBubblePopSound function

        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length / 2);

        // Now, just in case this script is attached to an empty bubble, we call CancelInvoke
        // (so that EmptyBubblePop is not called after the bubble has been set to be inactive in the scene)

        if (gameObject.tag == EMPTY_BUBBLE_TAG)
        {
            CancelInvoke("PopEmptyBubble");
        }

        gameObject.SetActive(false);
    }
}
