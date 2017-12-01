using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferFishExplode : MonoBehaviour {


    [SerializeField] private GameObject pufferFishExplodeParticle;
    [SerializeField] private AudioClip pufferFishExplode;
    private const string FISH_TAG = "Fish";
    private const string PUFFER_FISH_TAG = "PufferFish";
    private const string PUFFER_FISH_BUBBLE_TAG = "PufferFishBubble";
    private Collider[] collidersInBlastRadius;
    private float pufferFishBlastRadius;
    private int numFishToDestroy = 5;   // Is this good?

    // The following variable is used as a reference to a BubblePop script
    // (specifically, a puffer fish bubble's BubblePop script)

    private BubblePop currPufferFishBubblePop;

    // The following variable is used as a reference to a fish's FishManager script

    private FishManager currFishManager;


    void Start ()
    {
        pufferFishBlastRadius = 0.75f;
    }


    void OnTriggerEnter (Collider other)
    {
        if (other.tag == PUFFER_FISH_BUBBLE_TAG)
        {
            // First, we get an array of all the colliders inside the puffer fish's blast area

            collidersInBlastRadius = Physics.OverlapSphere(other.gameObject.transform.position, pufferFishBlastRadius);

            // We then loop through the array we just created; if the current element is a fish that is in the tank, we destroy it

            // (I will need to modify the puffer fish spawner script so that it only spawns puffer fish if there are
            // at least 5 fish in the tank; for now, when a puffer fish enters the tank, all fish inside are destroyed)

            for (int i = 0; i < collidersInBlastRadius.Length; i++)
            {
                // Does the following conditional break anything?

                /*
                if (i >= numFishToDestroy)
                {
                    break;
                }
                */

                if (collidersInBlastRadius[i].tag == FISH_TAG || collidersInBlastRadius[i].tag == PUFFER_FISH_TAG)
                {
                    if (collidersInBlastRadius[i].gameObject.GetComponent<FishManager>().isFishInTank)
                    {
                        Destroy(collidersInBlastRadius[i].gameObject);

                        // Whenever we destroy a fish in the tank, we need to decrement the fish tank counter
                        // that keeps track of the number of fish in the tank

                        TankManager.numFishInTank -= 1;
                    }
                }
            }

            // Then, we get a reference to the puffer fish's bubble, destroy the fish inside the bubble and then destroy the bubble

            currPufferFishBubblePop = other.gameObject.GetComponent<BubblePop>();
            Instantiate(pufferFishExplodeParticle, other.gameObject.transform.position, other.gameObject.transform.rotation);
            other.gameObject.GetComponent<AudioSource>().clip = pufferFishExplode;
            other.gameObject.GetComponent<AudioSource>().Play();
            Destroy(currPufferFishBubblePop.objectInBubbleRB.gameObject);
            Destroy(other.gameObject);
        }
    }

}
