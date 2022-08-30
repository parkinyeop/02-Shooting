using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemySpawner : MonoBehaviour
{
    public GameObject spawnPrefab;
    protected float spawnInterval = 1.0f;
    protected float yRange = 4.0f;
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
        
    }

    protected virtual IEnumerator CreatEnemy()
    {
        //yield return new WaitForSeconds(spawnInterval);
        //Instantiate(Enemy, transform.position, Quaternion.identity);

        while (true)
        {
            spawnInterval = Random.Range(0.5f, 1.5f);
            Instantiate(spawnPrefab,new Vector3(transform.position.x,Random.Range(-yRange,yRange)),Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    protected virtual void OnDrawGizmos()
    {   
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position,new Vector3(1,8,0)); 
    }
    /// <summary>
    /// 선택한 오브젝트의 Gizmo를 보여준다
    /// </summary>
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.DrawWireCube(transform.position, new Vector3(1, 8, 0));
    //}
}
