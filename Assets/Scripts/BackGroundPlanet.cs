using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundPlanet : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float minRightEnd = 40.0f;
    public float maxRightEnd = 60.0f;

    const float movePosisionX = -10.0f;


    private void Update()
    {
        //float minusX = transform.position.x - movePosisionX;

        transform.Translate(moveSpeed * Time.deltaTime * -transform.right);

        if (transform.position.x < movePosisionX)
        {
            //transform.Translate(UnityEngine.Random.Range(minRightEnd,maxRightEnd) * transform.right);
            transform.Translate(UnityEngine.Random.Range(minRightEnd,maxRightEnd)*transform.right);
        }
    }


}
