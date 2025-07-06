using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ġ�� �Ǹ�
//ȭ���� �Ʒ� �������� �̵�
//�÷��̾� ����ü�� ������ ������ ����
//��������� ������

[RequireComponent(typeof(Rigidbody2D))]//ȭ���� ���������� ������ ���ؼ�
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

    //event : ������ ����������, ������ ������Ҹ� �� �� �ֵ��� ����.
    //action : c#�� ��������Ʈ ������ ����Ƽ��Ÿ�Ϸ� ������ ����

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
        Destroy(gameObject); //������Ʈ Ǯ�� �����ؾ� ��. �ϴ� ����
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
