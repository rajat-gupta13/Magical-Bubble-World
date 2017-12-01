using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStep : MonoBehaviour {

    [SerializeField]private GameObject bubbleSpawner;
    [SerializeField]private GameObject splash;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Decoration")
        {
            // Since the object that just collided with the tank's floor is a decoration,
            // we access the object's DecorationManager script and tell it to stop falling

            collision.gameObject.GetComponent<DecorationManager>().isDecorationFalling = false;

            if (bubbleSpawner.GetComponent<BubbleSpawn>().isSpawningFirstDecoration)
            {
                bubbleSpawner.GetComponent<BubbleSpawn>().isSpawningFirstDecoration = false;
                bubbleSpawner.GetComponent<BubbleSpawn>().isSpawningDecorations = true;
            }
        }
    }
}
