using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("Input KeyCode")]
    [SerializeField]
    private KeyCode keyCodeReload = KeyCode.R;           //������ Ű

    [SerializeField]
    private int dir;
    private RotateToMouse rotateToMouse;               //���콺 �̵����� ī�޶� ȸ��
    private PlayerAnimatorController animator;         //�ִϸ��̼� ��� ����
    private MovementCharacterController movement;      //Ű���� �Է����� �÷��̾� �̵�, ����

    private WeaponAssaultRifle weapon;                 //���⸦ �̿��� ���� ����

    public int Dir
    {
        get => dir; set => dir = value;
    }

    private void Awake()
    {
        rotateToMouse = GetComponent<RotateToMouse>();
        movement = GetComponent<MovementCharacterController>();
        animator = GetComponent<PlayerAnimatorController>();
        weapon = GetComponentInChildren<WeaponAssaultRifle>();
    }

    private void Update()
    {
        UpdateRotate();
        UpdateMove();
        UpdateWeaponAction();
    }

    private void UpdateRotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        //float mouseY = Input.GetAxis("Mouse Y");

        rotateToMouse.UpdateRotate(mouseX);//mouseY
    }

    private void UpdateMove()
    {
        switch (dir)
        {
            case 0:
                animator.MoveDir = 0.0f;
                //Debug.Log("����");
                break;

            case 1:
                animator.MoveDir = 0.25f;
               //Debug.Log("��");
                break;

            case 2:
                animator.MoveDir = 0.5f;
                //Debug.Log("��");
                break;

            case 3:
                animator.MoveDir = 0.75f;
                //Debug.Log("��");
                break;

            case 4:
                animator.MoveDir = 1.0f;
                //Debug.Log("��");
                break;

        }
        //animator.MoveDir = 0;
        movement.MoveTo(new Vector3(transform.position.x, 0, transform.position.z));
    }

    private void UpdateWeaponAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weapon.StartWeaponAction();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            weapon.StopWeaponAction();
        }

        if (Input.GetKeyDown(keyCodeReload))
        {
            weapon.StartReload();
        }
    }
}
