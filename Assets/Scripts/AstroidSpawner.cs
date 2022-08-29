using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AstroidSpawner : MonoBehaviour
{
    public GameObject Astroid;
    public float spawnInterval = 5.0f;
    float yRange = 4.0f;
    Vector3 target;
    float astroidSpeed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAstroid());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnAstroid()
    {
        //yield return new WaitForSeconds(spawnInterval);
        //Instantiate(Enemy, transform.position, Quaternion.identity);

        while (true)
        {
            spawnInterval = Random.Range(0.5f, 1.5f);
            Instantiate(Astroid, new Vector3(transform.position.x, Random.Range(-yRange, yRange)), Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
