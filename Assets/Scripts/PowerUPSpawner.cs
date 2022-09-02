using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PowerUPSpawner : MonoBehaviour
{
    public GameObject powerUp;
    public float spawnInterval = 2;
    float yRange = 4;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Spawn()
    {

        while (true)
        {
            GameObject obj = Instantiate(powerUp, transform);
            obj.transform.Translate(Random.Range(-yRange, yRange), 0, 0);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
