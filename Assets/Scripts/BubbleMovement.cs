using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMovement : MonoBehaviour {

    // The following two variables are the bubble's speed along the x-axis and the bubble's speed along the y-axis

    // Is "speed" the right word to use?

    [SerializeField]
    private float bubbleSpeedX;
    [SerializeField]
    private float bubbleSpeedY;

    private float initialBubbleDirection;
    private float initialBubbleHeight;
    private float maxBubbleHeight;
    private float minBubbleHeight;
    private float bubbleShiftAmount;
    private bool isMovingUp;
    private bool isMovingDown;
    private float bubbleTargetHeight;


    void Start () {
        // bubbleSpeedX = 0.2f;
        // bubbleSpeedY = 0.05f;
        initialBubbleHeight = transform.position.y;
        bubbleShiftAmount = 0.2f;   // We can change this value later if it seems too small
        maxBubbleHeight = initialBubbleHeight + bubbleShiftAmount;
        minBubbleHeight = initialBubbleHeight - bubbleShiftAmount;

        // First, we randomly decide whether the bubble is moving up or down

        initialBubbleDirection = Random.Range(0, 1);

        if (initialBubbleDirection < 0.5)
        {
            isMovingUp = true;
            isMovingDown = false;

            // Now, we give the newly spawned bubble a y-position to move towards (which needs to be above its initial height and at most its max height)

            bubbleTargetHeight = Random.Range(initialBubbleHeight, maxBubbleHeight);
        }
        else
        {
            isMovingUp = false;
            isMovingDown = true;

            // Now, we give the newly spawned bubble a y-position to move towards (which needs to be at least its min height and below its initial height)

            bubbleTargetHeight = Random.Range(minBubbleHeight, initialBubbleHeight);
        }
    }
	

	void Update () {

        // First, we move the bubble along its forward trajectory

        transform.Translate(Vector3.right * bubbleSpeedX * Time.deltaTime);   // I might need to change this, depending on how our bubble mechanic evolves

        // Next, if the bubble has reached its target height, we switch its direction and give it a new target

        if (isMovingUp)
        {
            if (transform.position.y >= bubbleTargetHeight)
            {
                isMovingUp = false;
                isMovingDown = true;

                bubbleTargetHeight = Random.Range(minBubbleHeight, initialBubbleHeight);
            }

            transform.Translate(Vector3.up * bubbleSpeedY * Time.deltaTime);
        }
        else if (isMovingDown)
        {
            if (transform.position.y <= bubbleTargetHeight)
            {
                isMovingUp = true;
                isMovingDown = false;

                bubbleTargetHeight = Random.Range(initialBubbleHeight, maxBubbleHeight);
            }

            transform.Translate(Vector3.down * bubbleSpeedY * Time.deltaTime);
        }
    }
}
