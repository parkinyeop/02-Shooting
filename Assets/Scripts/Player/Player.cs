using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    //public delegate void DelegateName(); //리턴이 없고 파라메터도 없는 함수를 저장하는 델리게이트
    //public DelegateName del; // DegegateName 타입으로 del이라는 이름의 델리게이트를 만듬
    //Action del2; //리턴타입이 void, 파라메타가 없는 델리게이트를 만듬
    //Action<int> del3; // 인자값이 int rh 리턴타입으 없는 델리게이트를 만듬
    //Func<int , float> del4;// 인자가 int 이고 리턴이 float인 델리게이트를 만듬
    [Header("플레이어 변수")]
    public float speed = 5.0f;
    public float fireInterval = 0.5f;
    public int life;
    public float invinsibleTime = 5.0f;

    float boost = 1.0f;
    float fireAngle = 30.0f;
    bool isDead = false;
    int power = 0;
    bool isInvisiableMode = false;
    float timeElapsed = 0.0f;
    public int totalScore = 0;

    [Header("게임프리팹")]
    public GameObject bullet;
    public GameObject explotionPrefab;
    GameObject flash;

    Vector3 dir;
    Transform[] firePosition;
    PlayerInputAction inputActions;
    Rigidbody2D rigid;
    Animator anim;
    Transform firePositionRoot;
    SpriteRenderer playerRender;
    Collider2D bodyCollider; // Collider2D는 CapsuleCollider2D를 상속 받았음
    //CapsuleCollider2D bodyCollider;

    public Action<int> onLifeChnage;
    public Action<int> onScoreChange;

    int Power
    {
        get => power;

        set
        {
            power = value;
            if (power > 3)
            {
                power = 3;
            }
            //기존의 firePosition을 제거
            while (firePositionRoot.childCount > 0)
            {
                Transform temp = firePositionRoot.GetChild(0); // firePosionRoot의 첫번째 자식
                temp.parent = null; // 부모 관계를 끊고
                Destroy(temp.gameObject); // 삭제 시키기기
            }

            //파워의 값에 따라 새로운 포지션 배치
            for (int i = 0; i < power; i++)
            {
                GameObject firePos = new GameObject(); // 빈 오브젝트 생성
                firePos.name = $"FirePosition_{i}";
                firePos.transform.parent = firePositionRoot; // firePositionRoot의 자식으로 등록
                firePos.transform.localPosition = Vector3.zero; // 로컬 위치를 0,0,0 으로 만듬
                //firePos.transform.position = firePositionRoot.transform.position;


                //파워가 1 일때 : 0도
                //파워가 2 일때 : -15도  15도
                //파워가 3 일때 : -30도 30도
                firePos.transform.rotation = Quaternion.Euler(0, 0, (power - 1) * (fireAngle * 0.5f) + i * -fireAngle);
                firePos.transform.Translate(1.0f, 0, 0);

            }
        }
    }
    int PlayerLife
    {
        //get
        //{
        //return playerLife;
        //}
        get => life;

        set
        {
            if (life != value && !isDead)
            {

                if (life > value)
                {
                    StartCoroutine(Invisiabla());
                }

                life = value;

                if (life <= 0) // "<"을 때 경우와 "=" 때 경우 두번 검색하지만 코드의 안정성을 위해 
                {
                    Dead();
                }

                //(변수명)?. 왼쪽변수가 null이면 null. 아니면 맴버에 접근
                onLifeChnage?.Invoke(life); //라이프가 변경될때 onLifeChange 델리게이트에 등록된 함수들을 실행시킨다
            }

        }
        //int i = playerLife; //PlayerLife의 get이 실행된다. i=playerLife; 같은 결과
        //PlayerLife = 3; // playerLife 에 3을 넣어라 => playerLife의 set이 실행된다.
    }

    IEnumerator fireCoroutine;


    //Awake -> OnEnable -> Start : 함수 실행 순서

    /// <summary>
    /// 이 스크립트가 들어있는 게임 오브젝트가 생성된 직후에 호츨
    /// </summary>
    private void Awake()
    {

        inputActions = new PlayerInputAction();
        rigid = GetComponent<Rigidbody2D>();//한번만 찾고 저장해서 계속 쓰기(메모리를 쓰고 성능 아끼기
        anim = GetComponent<Animator>();

        firePositionRoot = transform.GetChild(0);
        flash = transform.GetChild(1).gameObject;
        flash.SetActive(false);

        fireCoroutine = Fire();


        playerRender = GetComponent<SpriteRenderer>();
        bodyCollider = GetComponent<Collider2D>();
    }

    /// <summary>
    /// 이 스크립트가 들어있는 게임 오브젝트가 활성화 될때 호출
    /// </summary>
    private void OnEnable()
    {
        inputActions.Player.Enable(); //오브젝트가 생셩되면 입력을 받는다
        inputActions.Player.Move.performed += OnMove; //performed 일 때 OnMove 함수 실행
        inputActions.Player.Move.canceled += OnMove; //cancled 일 때 OnMove 함수 실행
        inputActions.Player.Fire.performed += OnFireStart;
        inputActions.Player.Fire.canceled += OnFireStop;
        inputActions.Player.Booster.performed += OnBooster;
        inputActions.Player.Booster.canceled += OffBooster;
    }

    /// <summary>
    /// 이 스크립트가 들어있는 게임 오브젝트가 비활성화 될때 호출
    /// </summary>
    private void OnDisable()
    {
        InputDisable();
    }

    void InputDisable()
    {
        inputActions.Player.Disable();//오브젝트가 사라질때 더이상 입력을 받지 않는다
        inputActions.Player.Move.performed -= OnMove; //연결된 함수 해제
        inputActions.Player.Move.canceled -= OnMove; //연결된 함수 해제
        inputActions.Player.Fire.performed -= OnFireStart;
        inputActions.Player.Fire.canceled -= OnFireStop;
        inputActions.Player.Booster.performed -= OnBooster;
        inputActions.Player.Booster.canceled -= OffBooster;
    }



    private void Start()
    {
        Power = 1;
        PlayerLife = life;
        totalScore = 0;
        AddScore(0);
    }

    /// <summary>
    /// 일정시간 간격(물리 업데이트 시간 간격)으로 호출
    /// </summary>
    private void Update()
    {
        if (isInvisiableMode)
        {
            timeElapsed += Time.deltaTime * 30.0f;
            float alpha = (Mathf.Cos(timeElapsed) + 1.0f) * 0.5f;
            playerRender.color = new Color(1, alpha, 1, 1);
            //Debug.Log(alpha);
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            //이 스크립트 파일이 들어있는 게임 오브젝트에서 Rigibody2D 컴포넌트를 찾아 리턴(없으면 null)
            //Rigidbody2D 함수는 무거움 = Update() 또는 FixedUpdate 처럼 자주 호출 되는 함수 안에서는 안쓰는 것이 좋다
            //GetComponent<Rigidbody2D>();
            //rigid.AddForce(speed * Time.fixedDeltaTime * dir);//관성이 필요한 무브에서 사용

            rigid.MovePosition(transform.position + speed * boost * Time.fixedDeltaTime * dir);

        }
        else
        {
            rigid.AddForce(Vector2.left * 0.1f, ForceMode2D.Impulse);
            rigid.AddTorque(10.0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUP"))
        {
            Power++;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            //플레이어 HP
            //스프라이트 렌더러 키고 끄기

            //if (isDead == false)
            //{
            //    Dead();
            //}

            PlayerLife--;
            //StartCoroutine(Invinsible());


        }
    }

    IEnumerator Invisiabla()
    {
        bodyCollider.enabled = false;
        gameObject.layer = LayerMask.NameToLayer("Invinisiable");
        isInvisiableMode = true;


        yield return new WaitForSeconds(invinsibleTime);

        bodyCollider.enabled = !isDead;
        playerRender.color = Color.white;
        isInvisiableMode = false;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    void Dead()
    {
        isDead = true;
        bodyCollider.enabled = false;
        Instantiate(explotionPrefab, transform.position, Quaternion.identity);
        InputDisable();
        rigid.gravityScale = 1.0f;
        rigid.freezeRotation = false;
        StopCoroutine(fireCoroutine);
        gameObject.layer = LayerMask.NameToLayer("Invinisiable");
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        //Exception : 예외 상황(무엇을 해야 할지 지정이 안되어 있는 예외일때)
        //throw new NotImplementedException(); 
        //NotImplementedException()을 실행해라. => 코드 구현을 알려주기 위해 강제로 죽여라

        // Vector2 inputDir = context.ReadValue<Vector2>();
        dir = context.ReadValue<Vector2>();
        //dir.y>0 //w(위)를 눌렀다
        //dir.y == 0 // w,s누르지 않음
        //dir.y<0 // s(아래)를 눌렀다

        anim.SetFloat("InputY", dir.y);

    }
    private void OnFireStart(InputAction.CallbackContext _)
    {
        StartCoroutine(fireCoroutine);
    }
    private void OnFireStop(InputAction.CallbackContext _)
    {
        StopCoroutine(fireCoroutine);
    }
    IEnumerator Fire()
    {
        //yield return null; // 다음 프레임에 이어서 시작해라 
        //yield return new WaitForSeconds(1.0f); // 1초후에 이어서 시작해라

        while (true)
        {
            for (int i = 0; i < firePositionRoot.childCount; i++)
            {
                //fireposition[i]번째의 회전값을 사용
                //bulletInstatne.transform.rotation = firePosition[i].rotation;

                GameObject bulletInstatne = Instantiate(bullet,
                    firePositionRoot.GetChild(i).position, firePositionRoot.GetChild(i).rotation);
            }

            flash.SetActive(true);
            StartCoroutine(FlashOff());

            yield return new WaitForSeconds(fireInterval);
        }
    }

    IEnumerator FlashOff()
    {
        yield return new WaitForSeconds(0.1f);
        flash.SetActive(false);
    }
    private void OnBooster(InputAction.CallbackContext obj)
    {
        boost = 2.0f;
    }
    private void OffBooster(InputAction.CallbackContext obj)
    {
        boost = 1.0f;
    }
    /// <summary>
    /// 플레이어가 화면 바깥으로 나가면 포지션을 고정해서 갱신함
    /// </summary>
    //private void CheckBound()
    //{
    //    if (transform.position.x < -xBound)
    //    {
    //        transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
    //    }
    //    else if (transform.position.x > xBound)
    //    {
    //        transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
    //    }

    //    if (transform.position.y < -yBound)
    //    {
    //        transform.position = new Vector3(transform.position.x, -yBound, transform.position.z);
    //    }
    //    else if (transform.position.y > yBound)
    //    {
    //        transform.position = new Vector3(transform.position.x, yBound, transform.position.z);
    //    }
    //}
    public void AddScore(int score)
    {
        totalScore += score;
        onScoreChange.Invoke(totalScore);
    }
}
