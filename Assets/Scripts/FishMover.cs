using UnityEngine;
using System.Collections;
 
public class FishMover: MonoBehaviour {

    private GameObject currFood;
    private GameObject target;
    public GameObject newTarget;
    private GameObject movePosition;
    private bool isEating = false;
    private bool isMoving = false;
    public float speed = 0.5f;


    public bool isMovingTowardsFood = false;
    public bool canEatFood = false;

    private float waterX, waterY, waterZ;

    void Start()
    {
        target = GameObject.Find("WaterBasicDaytime");
        //newTarget = GameObject.Find("Target");
        movePosition = Instantiate(newTarget, target.transform.position, target.transform.rotation);

        waterX = target.transform.position.x;
        waterY = target.transform.position.y;
        waterZ = target.transform.position.z;
    }

    void Update () {
        if (isMoving == false && !isMovingTowardsFood) {
            movePosition.transform.position = new Vector3 (Random.Range(waterX - 0.4f, waterX + 0.4f), Random.Range(waterY - 0.3f, waterY - 0.05f), Random.Range(waterZ - 0.15f, waterZ + 0.15f));
            isMoving = true;
        }
        if (target.GetComponent<SplashSpawner>().foodInTank.Count > 0 && canEatFood && !isMovingTowardsFood)
        {
            movePosition.transform.position = target.GetComponent<SplashSpawner>().foodInTank[0].transform.position;
            isMoving = true;
            isMovingTowardsFood = true;
        }

        if (target.GetComponent<SplashSpawner>().foodInTank.Count == 0 && (isMovingTowardsFood || isEating))
        {
            isMoving = false;
            isMovingTowardsFood = false;
            isEating = false;
        }

        transform.position = Vector3.MoveTowards(transform.position, movePosition.transform.position, speed * Time.deltaTime);
 
        if (transform.position == movePosition.transform.position) {
            isMoving = false;
            if (isMovingTowardsFood && !isEating)
            {
                isMovingTowardsFood = false;
                isEating = true;
                gameObject.GetComponent<Animator>().SetBool("eating", true);
                Invoke("StopEating", 2.0f);
            }
        }
 
        transform.LookAt(movePosition.transform);
    }

    void StopEating()
    {
        gameObject.GetComponent<Animator>().SetBool("eating", true);
        currFood = target.GetComponent<SplashSpawner>().foodInTank[0];
        // target.GetComponent<SplashSpawner>().foodInTank[0].SetActive(false);
        //Destroy(target.GetComponent<SplashSpawner>().foodInTank[0]);
        target.GetComponent<SplashSpawner>().foodInTank.RemoveAt(0);
        currFood.SetActive(false);
        isEating = false;
        isMovingTowardsFood = false;
    }
}