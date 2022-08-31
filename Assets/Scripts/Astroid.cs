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
    GameObject explosion;
    
    public int hitPoint = 3;

    // Start is called before the first frame update
    void Start()
    {
        explosion = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.forward);

        transform.Translate(moveSpeed * Time.deltaTime * direction, Space.World);

        //if (transform.position.x < -9.0f)
        //{
        //    Destroy(gameObject);
        //}
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
                explosion.SetActive(true);//부모가 총알에 맞았을떄 익스프롤션 활성화
                explosion.transform.parent = null; // 애니메이션 재생을 위해 부모랑 종속 끊기

                Destroy(this.gameObject);
            }
            // Debug.Log(hp);
        }
    }
}
