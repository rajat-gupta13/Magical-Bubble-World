using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashSpawner : MonoBehaviour {

    [SerializeField] private GameObject splashPrefab;
    [SerializeField] private AudioClip[] splashSounds; 
    private const string FISH_TAG = "Fish";
    private const string DECORATION_TAG = "Decoration";
    private const string FOOD_TAG = "Food";

    public List<GameObject> foodInTank = new List<GameObject>();
    // [SerializeField] private float moveSpeedInWater = 0.2f;
    // public bool objectFalling = false;
    // private GameObject collidingObject;
    // Use this for initialization
    void Start () {
		
	}
	
	/*
	void Update () {
        if (objectFalling)
        {
            collidingObject.transform.Translate(Vector3.down * moveSpeedInWater * Time.deltaTime);
        }
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == FISH_TAG && !other.gameObject.GetComponent<FishManager>().isFishInTank) || other.tag == FOOD_TAG || other.tag == DECORATION_TAG)
        {
            Instantiate(splashPrefab, other.transform.position, other.transform.rotation);
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            if (other.tag == FISH_TAG)
            {
                //other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                other.gameObject.GetComponent<FishMover>().enabled = true;
                other.gameObject.GetComponent<Animator>().SetBool("swim", true);
            }
            if (other.tag == DECORATION_TAG)
            {
                //other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                // collidingObject = other.gameObject;

                other.GetComponent<DecorationManager>().isDecorationFalling = true;
            }
            if (other.tag == FOOD_TAG)
            {
                //other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                // collidingObject = other.gameObject;

                other.GetComponent<FishFeeder>().isFoodInWater = true;
                foodInTank.Add(other.gameObject);
            }
            PlaySplashSound();
        }
    }

    //IEnumerator MoveObject(GameObject other)
    //{
    //    while (other.gameObject.transform.position.y != minHeight.position.y)
    //    {
            
    //    }
    //    yield return null;
    //}

    void PlaySplashSound()
    {
        int randomIndex = Random.Range(0, splashSounds.Length - 1);

        GetComponent<AudioSource>().clip = splashSounds[randomIndex];
        GetComponent<AudioSource>().Play();
    }

    /*private IEnumerator StopGravity(GameObject fish)
    {
        yield return new WaitForSeconds(0.05f);
        fish.GetComponent<Rigidbody>().isKinematic = true;
    }*/
}
