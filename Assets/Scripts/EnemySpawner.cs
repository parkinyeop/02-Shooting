using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    float spawnInterval = 1.0f;
    float yRange = 4.0f;
    // float waitSpawnTime = 0.0f;

    void Start()
    {
        StartCoroutine(CreatEnemy());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        //StartCreatEnemy();
        //waitSpawnTime += Time.fixedDeltaTime;

        //if (waitSpawnTime > spawnInterval)
        //{
        //    StartCoroutine(CreatEnemy());
        //    waitSpawnTime = 0.0f;
        //}
    }

    IEnumerator CreatEnemy()
    {
        //yield return new WaitForSeconds(spawnInterval);
        //Instantiate(Enemy, transform.position, Quaternion.identity);

        while (true)
        {
            Instantiate(Enemy,new Vector3(transform.position.x,Random.Range(-yRange,yRange)),Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }

    }
}
