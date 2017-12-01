using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour {

    public static int numDecorationsInTank;
    public static int numFishInTank;
    public static int numFoodInTank;

    public static int maxDecorationsInTank;
    public static int maxFishInTank;
    public static int maxFoodInTank;

    private const string DECORATION_TAG = "Decoration";
    private const string FISH_TAG = "Fish";
    private const string FOOD_TAG = "Food";

    // The following variable is used as a reference to a fish's FishManager script

    private FishManager currFishManager;


    // Use this for initialization
    void Start () {
        numDecorationsInTank = 0;
        numFishInTank = 0;
        numFoodInTank = 0;

        maxDecorationsInTank = 4;
        maxFishInTank = 10;
        maxFoodInTank = 3;
	}
	

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == DECORATION_TAG)
        {
            numDecorationsInTank += 1;
        }

        // If the object that entered the water trigger area is a fish,
        // set its isFishInTank variable to true and add one to the fish tank counter

        if (other.tag == FISH_TAG && !other.gameObject.GetComponent<FishManager>().isFishInTank)
        {
            currFishManager = other.gameObject.GetComponent<FishManager>();
            currFishManager.isFishInTank = true;
            numFishInTank += 1;
        }

        if (other.tag == FOOD_TAG)
        {
            numFoodInTank += 1;
        }
    }
}
