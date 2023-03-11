using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("Input KeyCode")]
    [SerializeField]
    private KeyCode keyCodeReload = KeyCode.R;           //재장전 키

    [SerializeField]
    private int dir;
    private RotateToMouse rotateToMouse;               //마우스 이동으로 카메라 회전
    private PlayerAnimatorController animator;         //애니메이션 재생 제어
    private MovementCharacterController movement;      //키보드 입력으로 플레이어 이동, 점프

    public int Dir
    {
        get => dir; set => dir = value;
    }

    private void Awake()
    {
        rotateToMouse = GetComponent<RotateToMouse>();
        movement = GetComponent<MovementCharacterController>();
        animator = GetComponent<PlayerAnimatorController>();
    }

    private void Update()
    {
        UpdateRotate();
        UpdateMove();
        Debug.Log(dir);
    }

    private void UpdateRotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotateToMouse.UpdateRotate(mouseX, mouseY);
    }

    private void UpdateMove()
    {
        switch (dir)
        {
            case 0:
                animator.MoveDir = 0.0f;
                Debug.Log("정지");
                break;

            case 1:
                animator.MoveDir = 0.25f;
                Debug.Log("앞");
                break;

            case 2:
                animator.MoveDir = 0.5f;
                Debug.Log("뒤");
                break;

            case 3:
                animator.MoveDir = 0.75f;
                Debug.Log("우");
                break;

            case 4:
                animator.MoveDir = 1.0f;
                Debug.Log("좌");
                break;

            default:
                animator.MoveDir = 0.0f;
                break;
        }
        //animator.MoveDir = 0;
        movement.MoveTo(new Vector3(transform.position.x, 0, transform.position.z));
    }
}
