using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AstroidSpawner : EnemySpawner
{
    //public GameObject Astroid;
    //public float astroindDSawnInterval;
    //float yRange = 4.0f;
    //Vector3 target;
    //float astroidSpeed = 3.0f;

    Transform destination;

    private void Awake()
    {
        // 오브젝트가 생성 직후
        // 이 오브젝트 안에 있는 모든 컴포넌트가 생성이 완료되었다
        // 그리고 이 오브젝트의 자식 오브젝트들도 모두 생성이 완료되었다.

        //destination = transform.Find("DestinationArea");
        destination = transform.GetChild(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        //첫번쨰 Update() 실행 직전
        StartCoroutine(CreatEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override IEnumerator CreatEnemy()
    {
        //yield return new WaitForSeconds(spawnInterval);
        //Instantiate(Enemy, transform.position, Quaternion.identity);

        while (true)
        {
            //spawnInterval = Random.Range(0.5f, 1.5f);
            GameObject obj = Instantiate(spawnPrefab, transform);
            obj.transform.Translate(0, Random.Range(-yRange, yRange),0);

            Vector3 destPosition = new Vector3(destination.position.x, Random.Range(-yRange, yRange), 0); //

            Astroid astroid =obj.GetComponent<Astroid>();
            if(astroid != null)
            {
                //운석이 destiPosition로 가는 방향벡터를 구함
                //direction의 방향벡터로 만들어준다
                astroid.direction = ((destPosition - astroid.transform.position).normalized);
            }

            //Instantiate(spawnPrefab, new Vector3(transform.position.x, Random.Range(-yRange, yRange)), Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if(destination == null)
        {
            destination = transform.GetChild(0);
        }
        Gizmos.DrawLine(destination.position, new Vector3(1, 8, 0));
    }
}
