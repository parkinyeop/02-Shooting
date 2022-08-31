using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float bulletSpeed = 10;
    public float lifeTime = 0.5f;
    public GameObject hitEffect;

    // Start is called before the first frame update

    private void Awake()
    {
        hitEffect = transform.GetChild(0).gameObject;
    }
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(bulletSpeed * Time.deltaTime * Vector3.right, Space.Self); // Space.Self 는 자기 기준 Space.World 화면 기준으로 오른쪽
        //if (transform.position.x < 9.0f)
        //{
        //    Destroy(gameObject);
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            hitEffect.SetActive(true);//부모가 적과 충돌했을떄 익스프롤션 활성화
            //hitEffect.transform.parent = null; // 애니메이션 재생을 위해 부모랑 종속 끊기
            Destroy(this.gameObject);
        }
    }
}
