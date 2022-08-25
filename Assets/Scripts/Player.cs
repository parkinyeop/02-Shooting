using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
 

public class Player : MonoBehaviour
{
    //public delegate void DelegateName(); //리턴이 없고 파라메터도 없는 함수를 저장하는 델리게이트
    //public DelegateName del; // DegegateName 타입으로 del이라는 이름의 델리게이트를 만듬
    //Action del2; //리턴타입이 void, 파라메타가 없는 델리게이트를 만듬
    //Action<int> del3; // 인자값이 int rh 리턴타입으 없는 델리게이트를 만듬
    //Func<int , float> del4;// 인자가 int 이고 리턴이 float인 델리게이트를 만듬
    
    public float speed = 5.0f;

    float boost = 1.0f;
    
    Vector3 dir;
    
    PlayerInputAction inputActions;

    Rigidbody2D rigid;
    //Awake -> OnEnable -> Start : 함수 실행 순서

    /// <summary>
    /// 이 스크립트가 들어있는 게임 오브젝트가 생성된 직후에 호츨
    /// </summary>
    private void Awake()
    {
        inputActions = new PlayerInputAction();
        rigid = GetComponent<Rigidbody2D>();//한번만 찾고 저장해서 계속 쓰기(메모리를 쓰고 성능 아끼기
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
        inputActions.Player.Booster.performed += OnBooster;
        inputActions.Player.Booster.canceled += OffBooster;
    }

    /// <summary>
    /// 이 스크립트가 들어있는 게임 오브젝트가 비활성화 될때 호출
    /// </summary>
    private void OnDisable()
    {
        inputActions.Player.Disable();//오브젝트가 사라질때 더이상 입력을 받지 않는다
        inputActions.Player.Move.performed -= OnMove; //연결된 함수 해제
        inputActions.Player.Move.canceled -= OnMove; //연결된 함수 해제
        inputActions.Player.Fire.performed -= OnFire;
        inputActions.Player.Booster.performed -= OnBooster;
        inputActions.Player.Booster.canceled -= OffBooster;      
    }

    private void Start()
    {
        
    }
    /// <summary>
    /// 일정시간 간격(물리 업데이트 시간 간격)으로 호출
    /// </summary>
    private void Update()
    {
        //transform.position += (speed * Time.deltaTime * dir);
        //transform.Translate(speed * Time.deltaTime * dir);

    }

    private void FixedUpdate()
    {
        //transform.Translate(speed * Time.fixedDeltaTime * dir);

        //이 스크립트 파일이 들어있는 게임 오브젝트에서 Rigibody2D 컴포넌트를 찾아 리턴(없으면 null)
        // Rigidbody2D 함수는 무거움 = Update() 또는 FixedUpdate 처럼 자주 호출 되는 함수 안에서는 안쓰는 것이 좋다
        //GetComponent<Rigidbody2D>();

        //rigid.AddForce(speed * Time.fixedDeltaTime * dir);//관성이 필요한 무브에서 사용
        rigid.MovePosition(transform.position + speed*boost * Time.fixedDeltaTime * dir);
    }
    private void OnMove(InputAction.CallbackContext context)
    {
        //Exception : 예외 상황(무엇을 해야 할지 지정이 안되어 있는 예외일때)
        //throw new NotImplementedException(); //NotImplementedException()을 실행해라. => 코드 구현을 알려주기 위해 강제로 죽여라
        //Debug.Log("이동입력");
        // Vector2 inputDir = context.ReadValue<Vector2>();
        dir = context.ReadValue<Vector2>();
         
    }
    private void OnFire(InputAction.CallbackContext obj)
    {
        // throw new NotImplementedException();
        Debug.Log("발사");
    }

    private void OnBooster(InputAction.CallbackContext obj)
    {
        boost = 2.0f; 
    }
    private void OffBooster(InputAction.CallbackContext obj)
    {
        boost = 1.0f;
    }
}
