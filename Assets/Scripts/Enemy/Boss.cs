using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss : MonoBehaviour
{
    public float speed = 0.1f;
    public int score = 50;
    GameObject explosion;
    float hitPoint = 5.0f;

    float spawnY;
    float timeElapsed;

    float amlitude; //sin 으로 변경되는 위아래 차이.
    float frequency; //Sin 그래프가 한번 도는데 걸리는 시간

    public GameObject powerUp;

    [Range(0.1f, 0.9f)]
    public float spawnPow;

    Action<int> onDead;
    //public float spawnInterval = 2;
    //float yRange = 4;

    // Start is called before the first frame update
    void Start()
    {
        explosion = transform.GetChild(0).gameObject;
        explosion.SetActive(false);//활성화 상태 끄기

        spawnY = transform.position.y; // 생성 시 기준 높이
        timeElapsed = 0.0f;
        amlitude = 1.5f;
        frequency = 1.0f;

        Player player = FindObjectOfType<Player>();
        onDead += player.AddScore;

        Destroy(this.gameObject, 15.0f);
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += (Time.deltaTime * frequency); // 게임 시작 부터 얼마나 시간이 지났는지 확인

        float newY = spawnY + Mathf.Sin(timeElapsed) * amlitude;
        //Debug.Log(newY);
        float newX = transform.position.x - speed * Time.deltaTime;

        if (newX <= 5.5f)
        {
            newX = 5.5f;
        }
        transform.position = new Vector3(newX, newY, 0);

        //transform.Translate(speed * Time.deltaTime * new Vector3(-1, spawnY, 0), Space.World);
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
                explosion.SetActive(true);//부모가 총알에 맞았을떄 익스프롤션 활성화
                explosion.transform.parent = null; // 애니메이션 재생을 위해 부모랑 종속 끊기

                float rndPower = UnityEngine.Random.Range(0.0f, 1.0f);

                //Debug.Log(rndPower);

                if (rndPower < spawnPow)
                //if (true)
                {
                    Spawn();

                }
                onDead.Invoke(score);
                Destroy(this.gameObject);
            }


        }

        //if (collider.gameObject.CompareTag("KillZone"))
        //{
        //    Destroy(this.gameObject);
        //}
    }

    private void Spawn()
    {
        //float rand = Random.Range(0, 360.0f);
        //float angleGap = rand;
        //Instantiate(powerUp, transform.position, Quaternion.Euler(0, 0, angleGap));
        //GameObject obj = Instantiate(powerUp, transform);
        //obj.transform.Translate(Random.Range(-yRange, yRange), 0, 0);
        Instantiate(powerUp, transform.position, Quaternion.identity);
        powerUp.gameObject.SetActive(true);
        powerUp.transform.parent = null;
        //Debug.Log("Spawn");
    }
}
