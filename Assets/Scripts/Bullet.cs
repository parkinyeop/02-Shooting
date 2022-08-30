using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float bulletSpeed = 10;
    public float lifeTime = 0.5f;
    // Start is called before the first frame update
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
}
