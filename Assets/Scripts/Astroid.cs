using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Astroid : MonoBehaviour
{
    public float rotateSpeed = 360.0f;
    float speed = 3.0f;
    float yRange = 8.0f;
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target = new (-10.0f, Random.Range(-yRange, yRange), 0);
        transform.Rotate(rotateSpeed * Time.deltaTime * new Vector3(0,0,1),Space.Self);
        
        transform.position = Vector3.MoveTowards(transform.position,target, speed * Time.deltaTime);

        //Debug.Log(target);

        if (transform.position.x < -9.0f)
        {
            Destroy(gameObject);
        }
    }
        
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, target);
    }
}
