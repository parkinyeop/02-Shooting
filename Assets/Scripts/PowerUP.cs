using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PowerUP : MonoBehaviour
{
    public float dirChangeTime = 2.0f; //이동 방향이 바뀌는 시간

    float speed = 2.0f; // 이동 속도
    float lifeTime = 10.0f; // 스스로 없어지는 시간


    WaitForSeconds waitTime; //코루틴에서 사용하는 대기 시간
    Player player; // 파워업 아이템의 이동방향에 영향을 주는 플레이어
    Vector2 dir; // 현재 이동 방향

    private void Start()
    {
        waitTime = new WaitForSeconds(dirChangeTime);

        player = FindObjectOfType<Player>();//플레이어 오브젝트를 찾기

        SetRandomDir();
        StartCoroutine(DirChange());
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * dir, Space.World);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="allRandom">true 일 경우 완전 랜점/false일 경우 플레이어 반대 방향을 지향</param> 
    void SetRandomDir(bool allRandom = true) // 디폴트 파라메터. 값을 지정하지 않으면 디폴트 값이 들어간다
    {
        if (allRandom)
        {
            dir = UnityEngine.Random.insideUnitCircle;
            dir = dir.normalized;
        }
        else
        {
            Vector2 playerToPowerUP = transform.position - player.transform.position;
            //플레이어에서 파워업 아이템 까지의 방향 벡터
            playerToPowerUP = playerToPowerUP.normalized;
            //단위 백터로 변경
            if (UnityEngine.Random.value < 0.6f)
            {
                dir = Quaternion.Euler(0, 0, UnityEngine.Random.Range(-90.0f, 90.0f)) * -playerToPowerUP;
            }
            else
            {
                dir = Quaternion.Euler(0, 0, UnityEngine.Random.Range(-90.0f, 90.0f)) * playerToPowerUP;
            }

        }
    }

    /// <summary>
    /// 이동방향을 주기적으로 변경하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator DirChange()
    {
        while (true)
        {
            yield return waitTime;
            SetRandomDir(false);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Border"))
        {
            //보더와 충돌하면 dir의 반사
            dir = Vector2.Reflect(dir, col.contacts[0].normal);
        }
    }
}
