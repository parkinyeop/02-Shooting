using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Hello Unity");
    }
    private void Update()
    {
        transform.position += Vector3.down;
    }
}
