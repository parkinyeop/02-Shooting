using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemySpawner : MonoBehaviour
{
    public GameObject spawnPrefab;
    public float spawnInterval = 3.0f;
    protected float yRange = 3.0f;
    // float waitSpawnTime = 0.0f;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        
    }

    protected virtual IEnumerator Spawn()
    {
        //yield return new WaitForSeconds(spawnInterval);
        //Instantiate(Enemy, transform.position, Quaternion.identity);

        while (true)
        {
            GameObject obj = Instantiate(spawnPrefab, transform);
            obj.transform.Translate(0, Random.Range(-yRange, yRange), 0);
            //spawnInterval = Random.Range(0.5f, 1.5f);
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
