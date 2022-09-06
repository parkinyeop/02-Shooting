using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundPlanet : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float minRightEnd = 40.0f;
    public float maxRightEnd = 60.0f;

    public float minHeight = -8.0f;
    public float maxHeight = -5.0f;

    const float movePosisionX = -10.0f;


    private void Update()
    {

        transform.Translate(moveSpeed * Time.deltaTime * -transform.right);

        if (transform.position.x < movePosisionX)
        {
            transform.Translate(UnityEngine.Random.Range(minRightEnd, maxRightEnd) * transform.right);
            Vector3 newPos = transform.position;
            newPos.y = UnityEngine.Random.Range(minHeight, maxHeight);  
            transform.position = newPos;            
        }
    }


}
