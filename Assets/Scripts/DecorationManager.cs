using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationManager : MonoBehaviour {

    [SerializeField]
    private float moveSpeedInWater;

    public bool isDecorationFalling = false;


    void Update ()
    {
        if (isDecorationFalling)
        {
            transform.Translate(Vector3.down * moveSpeedInWater * Time.deltaTime);
        }
    }
}
