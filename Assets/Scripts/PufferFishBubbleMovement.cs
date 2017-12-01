using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferFishBubbleMovement : MonoBehaviour {

    // The following two variables are the bubble's speed along the x-axis and the bubble's speed along the y-axis

    // Is "speed" the right word to use?

    private float bubbleSpeedX;
    private float bubbleSpeedY;

    // The variable on the following line is the currently instantiated bubble's y-axis velocity

    private float initialBubbleDirection;
    private float initialBubblePositionX;
    private float maxBubblePositionX;
    private float minBubblePositionX;
    private float bubbleShiftAmount;
    private bool isMovingLeft;
    private bool isMovingRight;
    private float bubbleTargetPositionX;


    void Start()
    {
        bubbleSpeedX = 0.2f;  // Change back to 0.1f
        bubbleSpeedY = 0.15f;
        initialBubblePositionX = transform.position.x;
        bubbleShiftAmount = 0.4f;   // We can change this value later if it seems too small
        maxBubblePositionX = initialBubblePositionX + bubbleShiftAmount;
        minBubblePositionX = initialBubblePositionX - bubbleShiftAmount;

        // First, we randomly decide whether the bubble is moving left or right

        initialBubbleDirection = Random.Range(0, 1);

        if (initialBubbleDirection < 0.5)
        {
            isMovingRight = false;
            isMovingLeft = true;

            // Now, we give the newly spawned bubble an x-position to move towards 
            // (which needs to be to the left of its initial x-position and at least its min x-position)

            bubbleTargetPositionX = Random.Range(minBubblePositionX, initialBubblePositionX);
        }
        else
        {
            isMovingRight = true;
            isMovingLeft = false;

            // Now, we give the newly spawned bubble an x-position to move towards 
            // (which needs to be to the right of its initial x-position and at most its max x-position)

            bubbleTargetPositionX = Random.Range(initialBubblePositionX, maxBubblePositionX);
        }
    }


    void Update()
    {

        // First, we move the bubble along its downward trajectory

        transform.Translate(Vector3.down * bubbleSpeedY * Time.deltaTime);   // I might need to change this, depending on how our bubble mechanic evolves

        // Next, if the bubble has reached its target height, we switch its direction and give it a new target

        if (isMovingRight)
        {
            if (transform.position.x >= bubbleTargetPositionX)
            {
                isMovingRight = false;
                isMovingLeft = true;

                bubbleTargetPositionX = Random.Range(minBubblePositionX, initialBubblePositionX);
            }

            transform.Translate(Vector3.right * bubbleSpeedX * Time.deltaTime);
        }
        else if (isMovingLeft)
        {
            if (transform.position.x <= bubbleTargetPositionX)
            {
                isMovingRight = true;
                isMovingLeft = false;

                bubbleTargetPositionX = Random.Range(initialBubblePositionX, maxBubblePositionX);
            }

            transform.Translate(Vector3.left * bubbleSpeedX * Time.deltaTime);
        }
    }
}
