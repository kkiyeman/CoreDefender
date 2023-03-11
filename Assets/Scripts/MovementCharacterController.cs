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

    private CharacterController characterController;  //�÷��̾� �̵� ��� ���� ������Ʈ

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        curDir = GetComponent<PlayerController>();
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

    private void Update()
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
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        //Debug.Log("��");
        curDir.Dir = 1;
    }
    public void MoveBack()
    {
        transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
        //Debug.Log("��");
        curDir.Dir = 2;
    }
    public void MoveRight()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        //Debug.Log("��");
        curDir.Dir = 3;
    }
    public void MoveLeft()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        //Debug.Log("��");
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
        if (characterController.isGrounded)
        {
            moveForce.y = jumpForce;
        }
    }
}
