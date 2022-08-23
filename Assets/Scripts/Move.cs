using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    float speed = 5.0f;

    Vector3 dir;

    private void Start()
    {
        Debug.Log("Hello Unity");
    }
    private void Update()
    {
        //transform.position += new Vector3(1, 0, 0);
        //new Vector3(1,0,0); // Vector3.right;
        //new Vector3(-1,0,0); // Vector3.left;
        //new Vector3(0, 1, 0); // Vector3.up;
        //new Vector3 (0, -1, 0); //Vector3.down;

        //transform.position += (speed * Vector3.down);
        //Time.deltaTime : 이전 프레임에서 현재 프레임까지 걸린 시간 => 1프레임당 시간
        //transform.position += (speed * Time.deltaTime * Vector3.down);

        if(Input.GetKeyDown(KeyCode.W))
        {
            dir = Vector3.up;
            Debug.Log("W Input");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            dir = Vector3.left;
            Debug.Log("A Input");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            dir = Vector3.down;
            Debug.Log("S Input");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            dir = Vector3.right;
            Debug.Log("D Input");
        }
        transform.position += (speed * Time.deltaTime * dir);

    }
}
