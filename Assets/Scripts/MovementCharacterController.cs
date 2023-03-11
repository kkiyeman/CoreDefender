using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCharacterController : MonoBehaviour
{
    Dictionary<KeyCode, Action> keyDictionary;

    [SerializeField] private float moveSpeed; //�̵��ӵ�
    [SerializeField] private float moveDir;   //�̵�����
    private Vector3 moveForce;                //�̵� �� (x, z�� y���� ������ ����� ���� �̵��� ����)

    Rigidbody rb;
    float power = 20f;

    [SerializeField] private float jumpForce; //���� ��
    [SerializeField] private float gravity;   //�߷� ���

    private PlayerController curDir;

    [HideInInspector] public int dir = 0;

    [Header("Move KeyCode")]

    [SerializeField]
    private KeyCode keyCodeFront = KeyCode.W;            //���� Ű

    [SerializeField]
    private KeyCode keyCodeBack = KeyCode.S;             //���� Ű

    [SerializeField]
    private KeyCode keyCodeRight = KeyCode.D;            //������ �̵� Ű

    [SerializeField]
    private KeyCode keyCodeLeft = KeyCode.A;             //���� �̵� Ű

    public float MoveDir
    {
        set => moveDir = Mathf.Max(0, value);
        get => moveDir;
    }

    private CharacterController charCon;  //�÷��̾� �̵� ��� ���� ������Ʈ

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
        // 1�ʴ� moveForce �ӷ����� �̵�
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
        //Debug.Log("��");
        rb.AddForce(Vector3.forward * power * Time.deltaTime);
        curDir.Dir = 1;
    }
    public void MoveBack()
    {
        // charCon.Move(transform.forward * -1f * moveSpeed * Time.deltaTime);
        //Debug.Log("��");
        rb.AddForce(Vector3.back * power * Time.deltaTime);
        curDir.Dir = 2;
    }
    public void MoveRight()
    {
        //charCon.Move(transform.right * moveSpeed * Time.deltaTime);
        //Debug.Log("��");
        rb.AddForce(Vector3.right * power * Time.deltaTime);
        curDir.Dir = 3;
    }
    public void MoveLeft()
    {
        //charCon.Move(transform.right * -1f * moveSpeed * Time.deltaTime);
        //Debug.Log("��");
        rb.AddForce(Vector3.left * power * Time.deltaTime);
        curDir.Dir = 4;
    }

    public void MoveTo(Vector3 direction)
    {
        //�̵� ���� = ĳ������ ȸ�� �� * ���� ��
        direction = transform.rotation * new Vector3(direction.x, 0, direction.z);

        //�̵� �� = �̵� ���� * �ӵ�
        moveForce = new Vector3(direction.x * moveSpeed, moveForce.y, direction.z * moveSpeed);
    }

    public void Jump()
    {
        //�÷��̾ �ٴڿ� ���� ���� ���� ����
        if (charCon.isGrounded)
        {
            moveForce.y = jumpForce;
        }
    }
}
