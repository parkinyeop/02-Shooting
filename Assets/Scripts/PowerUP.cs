using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PowerUP : MonoBehaviour
{
    float speed = 2;
    float randRot = 0.5f;
    Rigidbody2D rigid;
    Transform trans;
    Vector3 dir;
    float lifeTime = 25f;

    private void Start()
    {
        randRot = UnityEngine.Random.Range(-randRot, randRot);
        dir = new Vector3(-1, randRot, 0);
        trans = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject,lifeTime);

    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(speed * Time.deltaTime*dir, Space.World);
        //transform.position = speed * Time.deltaTime * dir;
        //rigid.velocity = (dir * speed);

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Border"))
        {
            dir = dir * -1.0f;
            Debug.Log("Border");
        }
    }
}
