using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBubbleDestroy : MonoBehaviour {

    private const string DECORATION_BUBBLE_TAG = "DecorationBubble";

    //private BubblePop bubblePopReference;
    public GameObject bubbleSpawner;


	void OnTriggerEnter (Collider other)
    {
        if ((other.tag == DECORATION_BUBBLE_TAG) && bubbleSpawner.GetComponent<BubbleSpawn>().isSpawningFirstDecoration)
        {
            other.gameObject.GetComponent<BubblePop>().PopObjectBubble();
        }
    }
}
