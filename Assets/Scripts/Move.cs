using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    float speed = 5.0f;
    
    Vector3 dir;
        
    private void Start()
    {
        Debug.Log("Hello Unity");
    }
    private void Update()
    {
        transform.position += (speed * Time.deltaTime * dir);
        //if(rot == true)
        //{
        ////transform.Rotate(new Vector3(0, 0 ,2 * rotSpeed * Time.deltaTime));
        ////transform.Rotate(new Vector3(0, 0 ,360));
        //}
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        ////Vector2 inputDir = context.ReadValue<Vector2>();
        //if (context.started) // 매핑된 키가 누른 직후
        //{
        //    //Debug.Log("입력들어옴 - started");
        //}
        //if (context.performed) // 매핑된 키가 확실하게 눌러졌다
        //{
        //    transform.position += (speed * Time.deltaTime * dir);
        //    Debug.Log("입력들어옴 - performed");
        //}
        //if (context.canceled)  // 매핑된 키가 떨어졌을 떄.
        //{
        //    //Debug.Log("입력들어옴 - cancled");
        //}
        Vector2 inputDir = context.ReadValue<Vector2>();
        Debug.Log(inputDir);
        dir = inputDir;

        // vector : 방향과 크리
        // vextor2 : 유니티에서 제공하는 구조체(struct) : 2차원 벡터를 표현하기 위한 구조체(x,y)
    }

    public void FireInput(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Debug.Log("발사");
        }
    }
        
    /// <summary>
    /// InputManager 사용 예시
    /// </summary>
    private void OldInputManager()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            dir = Vector3.up;
            Debug.Log("W Input");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            dir = Vector3.left;
            Debug.Log("A Input");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            dir = Vector3.down;
            Debug.Log("S Input");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            dir = Vector3.right;
            Debug.Log("D Input");
        }
    }
}
