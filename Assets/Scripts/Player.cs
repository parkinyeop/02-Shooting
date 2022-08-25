using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
 

public class Player : MonoBehaviour
{
    PlayerInputAction inputActions;
    //float speed = 5.0f;
    Vector3 dir;
    public float speed = 5.0f;
    //Awake -> OnEnable -> Start : 함수 실행 순서

    /// <summary>
    /// 이 스크립트가 들어있는 게임 오브젝트가 생성된 직후에 호츨
    /// </summary>
    private void Awake()
    {
        inputActions = new PlayerInputAction();
    }

    /// <summary>
    /// 이 스크립트가 들어있는 게임 오브젝트가 활성화 될때 호출
    /// </summary>
    private void OnEnable()
    {
        inputActions.Player.Enable(); //오브젝트가 생셩되면 입력을 받는다
        inputActions.Player.Move.performed += OnMove; //performed 일 때 OnMove 함수 실행
        inputActions.Player.Move.canceled += OnMove; //cancled 일 때 OnMove 함수 실행
        inputActions.Player.Fire.performed += OnFire;        
    }

    /// <summary>
    /// 이 스크립트가 들어있는 게임 오브젝트가 비활성화 될때 호출
    /// </summary>
    private void OnDisable()
    {
        inputActions.Player.Disable(); //오브젝트가 사라질때 더이상 입력을 받지 않는다
        inputActions.Player.Move.performed -= OnMove; //연결된 함수 해제
        inputActions.Player.Move.canceled -= OnMove; //연결된 함수 해제
        inputActions.Player.Fire.performed -= OnFire;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        transform.position += (speed * Time.deltaTime * dir);
    }
    private void OnMove(InputAction.CallbackContext context)
    {
        //Exception : 예외 상황(무엇을 해야 할지 지정이 안되어 있는 예외일때)
        //throw new NotImplementedException(); //NotImplementedException()을 실행해라. => 코드 구현을 알려주기 위해 강제로 죽여라
        //Debug.Log("이동입력");
        Vector2 inputDir = context.ReadValue<Vector2>();
        dir = inputDir;
    }
    private void OnFire(InputAction.CallbackContext obj)
    {
        // throw new NotImplementedException();
        Debug.Log("발사");
    }
    
}
