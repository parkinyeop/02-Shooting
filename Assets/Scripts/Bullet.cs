using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float bulletSpeed = 10;
    public float lifeTime = 3f;
    public GameObject hitEffect;

    // Start is called before the first frame update

    private void Awake()
    {
        //hitEffect.SetActive(false);
    }
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        // Space.Self 는 자기 기준 Space.World 화면 기준으로 오른쪽
        transform.Translate(bulletSpeed * Time.deltaTime * Vector3.right, Space.Self);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("쾅");
            hitEffect.transform.position = collision.contacts[0].point;
            hitEffect.SetActive(true);
            hitEffect.transform.parent = null; // 애니메이션 재생을 위해 부모랑 종속 끊기

            // collision.contact[0].normal : 법선 백터 (노멀 벡터)
            // 노멀 벡터 : 특정 평면에 수직인 벡터
            // 노멀 벡터는 반사를 계산하기 위해 반드시 필요하다 => 반사를 이용해서 그림자를 계산한다. 물리적인 반사도 계산한다.
            // 노멀 벡터를 구하기 위해 백터의 외적을 사용한다

            
            Destroy(this.gameObject);
        }
    }
}
