using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//배치가 되면
//화면의 아래 방향으로 이동
//플레이어 투사체에 닿으면 데이지 받음
//사망했을때 리워드

[RequireComponent(typeof(Rigidbody2D))]//화면을 벗었났을때 삭제를 위해서
[RequireComponent(typeof(CircleCollider2D))]
public class Enemy : MonoBehaviour,IMovement,IDamaged
{
    private Vector2 moveDir = Vector2.down;
    private float moveSpeed = 3f;

    private bool isInit = false;
    private int curHp = 10;

    public bool IsDead
    {
        get => curHp <= 0;
    }

    //event : 접근은 가능하지만, 구독과 구독취소만 할 수 있도록 제한.
    //action : c#의 델리게이트 문법을 유니티스타일로 변형한 문법

    public static event Action<Enemy> OnMonsterDied;


    private void Awake()
    {
        if(TryGetComponent<CircleCollider2D>(out CircleCollider2D col))
        {
            col.isTrigger = true;
            col.radius = 0.25f;
        }
        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rig))
        {
            rig.gravityScale = 0f;
        }
    }

    private void Update()
    {
        if (isInit && !IsDead)
        {
            Move(moveDir);
        }
    }

    public void Move(Vector2 moveDir)
    {
        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
    }

    public void SetEnable(bool enable)
    {
      isInit = enable;
    }

    public void TakeDamage(GameObject attacker, int damage)
    {
        if (!IsDead) { 
            curHp -= damage;
            if (curHp <= 0)
            {
                OnDied();
            }
            else
            {
                OnDemaged();
            }
        }
    }

    private void OnDied()
    {
        OnMonsterDied?.Invoke(this);
        Destroy(gameObject); //오브젝트 풀링 적용해야 됨. 일단 생략
    }

    private void OnDemaged()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DestroyArea")) {
            Destroy(gameObject);
        }
    }
}
