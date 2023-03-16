using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        //"Player"������Ʈ �������� �ڽ� ������Ʈ��
        //"HPCharacter" ������Ʈ�� Animator ������Ʈ�� �ִ�.
        animator = GetComponentInChildren<Animator>();
    }

    public float MoveDir
    {
        set => animator.SetFloat("movementDir", value);
        get => animator.GetFloat("movementDir");
    }

    public void OnReload()
    {
        animator.SetTrigger("onReload");
    }


    public void OnShoot()
    {
        animator.SetTrigger("Shoot");
    }

    public void Play(string stateName, int layer, float normalizedTime)
    {
        animator.Play(stateName, layer, normalizedTime);
    }

    public bool CurrentAnimationIs(string name)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }
}
