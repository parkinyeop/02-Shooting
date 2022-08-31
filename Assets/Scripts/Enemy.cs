using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        explosion = transform.GetChild(0).gameObject;
       // explosion.SetActive(false);//활성화 상태 끄기
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.left, Space.Self);
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Bullet"))
        {
            //GameObject obj = Instantiate(explosion,transform.position, Quaternion.identity);
            //Destroy(obj, 0.5f);

            explosion.SetActive(true);//부모가 총알에 맞았을떄 익스프롤션 활성화
            explosion.transform.parent = null; // 애니메이션 재생을 위해 부모랑 종속 끊기

            Destroy(this.gameObject);
        }
    }
}
