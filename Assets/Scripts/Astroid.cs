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
    //float rigidPower = 50.0f;
    //float yRange = 8.0f;
    //Vector3 target;
    //Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        //rigid = gameObject.GetComponent<Rigidbody2D>();
        //rigid.AddForce(new Vector3(Random.Range(-1f, -4f) * rigidPower, 1f));      
    }

    // Update is called once per frame
    void Update()
    {
        //target = new (-30.0f, Random.Range(-yRange, yRange), 0);
        transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.forward);

        transform.Translate(moveSpeed * Time.deltaTime * direction,Space.World);

        //transform.position = Vector3.MoveTowards(transform.position,target, speed * Time.deltaTime);

        //Debug.Log(target);

        if (transform.position.x < -9.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position,transform.position+direction*1.5f);
    }
}
