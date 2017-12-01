using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleDestroy : MonoBehaviour {

    // How do I do this without the having null refereneces from the BubblePop
    // scipt once the fish inside the fish bubble is destroyed?

    private const string EMPTY_BUBBLE_TAG = "EmptyBubble";
    private const string FISH_BUBBLE_TAG = "FishBubble";

    // The following variable is used as a reference to a BubblePop script

    private BubblePop currBubblePop;


    void OnTriggerEnter (Collider other)
    {
        if (other.tag == EMPTY_BUBBLE_TAG)
        {
            Destroy(other.gameObject);
        }

        if (other.tag == FISH_BUBBLE_TAG)
        {
            // If a bubble containing a fish enters the bubble destruction trigger area,
            // we first get a reference to the fish game object that bubble contains and destroy it

            currBubblePop = other.GetComponent<BubblePop>();
            Destroy(currBubblePop.objectInBubbleRB.gameObject);

            // Then, we destroy the bubble game object

            Destroy(other.gameObject);
        }
    }
}
