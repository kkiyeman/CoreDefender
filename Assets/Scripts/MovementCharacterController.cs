using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCharacterController : MonoBehaviour
{
    Dictionary<KeyCode, Action> keyDictionary;

    [SerializeField] private float moveSpeed; //이동속도
    [SerializeField] private float moveDir;   //이동방향
    private Vector3 moveForce;                //이동 힘 (x, z와 y축을 별도로 계산해 실제 이동에 적용)

    Rigidbody rb;
    float power = 20f;

    [SerializeField] private float jumpForce; //점프 힘
    [SerializeField] private float gravity;   //중력 계수

    private PlayerController curDir;

    [HideInInspector] public int dir = 0;

    [Header("Move KeyCode")]

    [SerializeField]
    private KeyCode keyCodeFront = KeyCode.W;            //전진 키

    [SerializeField]
    private KeyCode keyCodeBack = KeyCode.S;             //후진 키

    [SerializeField]
    private KeyCode keyCodeRight = KeyCode.D;            //오른쪽 이동 키

    [SerializeField]
    private KeyCode keyCodeLeft = KeyCode.A;             //왼쪽 이동 키

    public float MoveDir
    {
        set => moveDir = Mathf.Max(0, value);
        get => moveDir;
    }

    private CharacterController charCon;  //플레이어 이동 제어를 위한 컴포넌트

    private void Awake()
    {
        charCon = GetComponent<CharacterController>();
        curDir = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        keyDictionary = new Dictionary<KeyCode, Action>
        {
            { keyCodeFront, MoveFront},
            { keyCodeBack, MoveBack},
            { keyCodeRight, MoveRight},
            { keyCodeLeft, MoveLeft}
        };
    }

    private void FixedUpdate()
    {
        // 1초당 moveForce 속력으로 이동
        //characterController.Move(moveForce * Time.deltaTime);
        if (Input.anyKey)
        {
            foreach (var dic in keyDictionary)
            {
                if (Input.GetKey(dic.Key))
                {
                    dic.Value();
                }
            }
        }
        else
            curDir.Dir = 0;

    }

    public void MoveFront()
    {
        //charCon.Move(transform.forward * moveSpeed * Time.deltaTime);
        //Debug.Log("앞");
        rb.AddForce(Vector3.forward * power * Time.deltaTime);
        curDir.Dir = 1;
    }
    public void MoveBack()
    {
        // charCon.Move(transform.forward * -1f * moveSpeed * Time.deltaTime);
        //Debug.Log("뒤");
        rb.AddForce(Vector3.back * power * Time.deltaTime);
        curDir.Dir = 2;
    }
    public void MoveRight()
    {
        //charCon.Move(transform.right * moveSpeed * Time.deltaTime);
        //Debug.Log("우");
        rb.AddForce(Vector3.right * power * Time.deltaTime);
        curDir.Dir = 3;
    }
    public void MoveLeft()
    {
        //charCon.Move(transform.right * -1f * moveSpeed * Time.deltaTime);
        //Debug.Log("좌");
        rb.AddForce(Vector3.left * power * Time.deltaTime);
        curDir.Dir = 4;
    }

    public void MoveTo(Vector3 direction)
    {
        //이동 방향 = 캐릭터의 회전 값 * 방향 값
        direction = transform.rotation * new Vector3(direction.x, 0, direction.z);

        //이동 힘 = 이동 방향 * 속도
        moveForce = new Vector3(direction.x * moveSpeed, moveForce.y, direction.z * moveSpeed);
    }

    public void Jump()
    {
        //플레이어가 바닥에 있을 때만 점프 가능
        if (charCon.isGrounded)
        {
            moveForce.y = jumpForce;
        }
    }
}
