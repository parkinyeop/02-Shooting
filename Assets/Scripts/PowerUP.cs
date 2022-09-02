using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PowerUP : MonoBehaviour
{
    float speed = 2;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        transform.position += speed * Time.deltaTime * new Vector3(-1,0,0);

    }
}
