using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    GameObject explosion;

    float spawnY;
    float timeElapsed;

    public float amlitude; //sin 으로 변경되는 위아래 차이.
    public float frequency; //Sin 그래프가 한번 도는데 걸리는 시간
    // Start is called before the first frame update
    void Start()
    {
        explosion = transform.GetChild(0).gameObject;
        explosion.SetActive(false);//활성화 상태 끄기

        spawnY = transform.position.y; // 생성 시 기준 높이
        timeElapsed = 0.0f;
        amlitude = 2.0f;
        frequency = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += (Time.deltaTime*frequency); // 게임 시작 부터 얼마나 시간이 지났는지 확인

        float newY = spawnY + Mathf.Sin(amlitude*timeElapsed);
        //Debug.Log(newY);
        float newX = transform.position.x - speed * Time.deltaTime;

        transform.position = new Vector3(newX, newY, 0);

        //transform.Translate(speed * Time.deltaTime * new Vector3(-1, spawnY, 0), Space.World);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collider)
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
