using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PowerUP : MonoBehaviour
{
    float speed = 2.0f;
    float randRot = 0.5f;
    Rigidbody2D rigid;
    Transform trans;
    Vector2 dir;
    float lifeTime = 10.0f;

    public float dirChangeTime = 2.0f;

    WaitForSeconds waitTime;
    Player player;

    private void Start()
    {
        //    randRot = UnityEngine.Random.Range(-randRot, randRot);
        //    dir = new Vector3(-1, randRot, 0);
        //    trans = GetComponent<Transform>();
        //    rigid = GetComponent<Rigidbody2D>();
        waitTime = new WaitForSeconds(dirChangeTime);

        player = FindObjectOfType<Player>();

        SetRandomDir();
        StartCoroutine(DirChange());
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(speed * Time.deltaTime * dir, Space.World);
        //transform.position = speed * Time.deltaTime * dir;
        //rigid.velocity = (dir * speed);

    }

    //public void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.gameObject.CompareTag("Border"))
    //    {
    //        dir = dir * -1.0f;

    //        //Vector3 incomeDir = dir;
    //        //incomeDir = incomeDir.normalized;
    //        //Vector3 normalDir = col.ClosestPoint(transform.position);
    //        //dir = Vector3.Reflect(incomeDir, normalDir);

    //        Debug.Log("Border");
    //    }
    //}

    void SetRandomDir(bool allRandom = true) // 디폴트 파라메터. 값을 지정하지 않으면 디폴트 값이 들어간다
    {
        if (allRandom)
        {
            dir = UnityEngine.Random.insideUnitCircle;
            dir = dir.normalized;
        }
        else
        {
            Vector2 playerToPowerUP = trans.position - player.transform.position;
            playerToPowerUP = playerToPowerUP.normalized;
            dir = Quaternion.Euler(0,0,UnityEngine.Random.Range(-90.0f,90.0f))*playerToPowerUP;
        }
    }


    IEnumerator DirChange()
    {
        while (true)
        {
            yield return waitTime;
            SetRandomDir();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Border"))
        {
            dir = Vector2.Reflect(dir, col.contacts[0].normal);
        }
    }
}
