using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Astroid : MonoBehaviour
{
    public float rotateSpeed = 360.0f;
    public float moveSpeed = 3.0f;
    public Vector3 direction = Vector3.left;
    public int hitPoint = 3;

    public float minMoveSpeed = 2.0f;
    public float maxMoveSpeed = 4.0f;
    public float minRotateSpeed = 30.0f;
    public float maxRotateSpeed = 360.0f;

    public GameObject small;
    public GameObject explosion;
    int splitCount = 3;
    [Range(1, 16)]
    float lifetime;
    float minLifeTime = 3.0f;
    float maxLifeTime = 5.0f;



    // Start is called before the first frame update

    private void Awake()
    {
        SpriteRenderer sprRender = GetComponent<SpriteRenderer>();
        Transform asteroidTransform = GetComponent<Transform>();

        //비트 연산을 사용하여 랜드값 설정
        int rand = Random.Range(0, 4); // 0000,0001,0010,0011 

        //& 비트 연산 (rand의 제일 오른쪽 비트가 0인지를 판별)
        sprRender.flipX = ((rand & 0b_01) != 0);

        //rand의 제일 오른쪽에서 두번재 비트가 1이면 truem 0이면 false
        sprRender.flipY = ((rand & 0b_10) != 0);

        rotateSpeed = Random.Range(30, 360);
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);

        float rotRand = (moveSpeed - minMoveSpeed) / (maxMoveSpeed - minMoveSpeed);
        rotateSpeed = rotRand * (maxRotateSpeed - minRotateSpeed) + minRotateSpeed;
        //rotateSpeed = Mathf.Lerp (minRotateSpeed, maxRotateSpeed, rotRand);//Lerp() 를 사용하여 구현

        lifetime = Random.Range(minLifeTime, maxLifeTime);
    }

    void Start()
    {
        explosion = transform.GetChild(0).gameObject;
        StartCoroutine(SelfCrush());
    }

    IEnumerator SelfCrush()
    {
        yield return new WaitForSeconds(lifetime);
        Crush();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.forward);
        transform.Translate(moveSpeed * Time.deltaTime * direction, Space.World);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + direction * 1.5f);
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Bullet"))
        {
            //GameObject obj = Instantiate(explosion,transform.position, Quaternion.identity);
            //Destroy(obj, 0.5f);
            hitPoint--;
            if (hitPoint <= 0)
            {
                //explosion.SetActive(true);//부모가 총알에 맞았을떄 익스프롤션 활성화
                //explosion.transform.parent = null; // 애니메이션 재생을 위해 부모랑 종속 끊기
                Crush();
                //Destroy(this.gameObject);
            }
            // Debug.Log(hp);
        }
    }

    void Crush()
    {
        explosion.SetActive(true);//부모가 총알에 맞았을떄 익스프롤션 활성화
        explosion.transform.parent = null; // 애니메이션 재생을 위해 부모랑 종속 끊기

        //5% 확률
        if(Random.Range(0.0f, 1.0f) < 0.05f)
        {
            splitCount = 20;
        }
        else
        {
            splitCount = Random.Range(3, 6);
        }

        
        float rand = Random.Range(0, 360.0f);
        float angleGap = rand / (float)splitCount;


        for (int i = 0; i < splitCount; i++)
        {
            Instantiate(small, transform.position, Quaternion.Euler(0, 0,(angleGap * i)));
        }
        Destroy(this.gameObject);
    }
}
