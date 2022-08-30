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
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.forward);

        transform.Translate(moveSpeed * Time.deltaTime * direction,Space.World);
                
        //if (transform.position.x < -9.0f)
        //{
        //    Destroy(gameObject);
        //}
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position,transform.position+direction*1.5f);
    }
}
