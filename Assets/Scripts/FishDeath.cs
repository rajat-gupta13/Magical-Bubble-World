using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDeath : MonoBehaviour {

    private const string FISH_TAG = "Fish";
    private const string PUFFER_FISH_TAG = "PufferFish";

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == FISH_TAG || collision.gameObject.tag == PUFFER_FISH_TAG)
        {
            collision.gameObject.GetComponent<Animator>().SetBool("dead",true);
            Destroy(collision.gameObject, 3.0f);
        }
    }
}
