using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//FSM ���� ���� ���

//�ڵ���
//1. �õ�����
//2. �õ������ִµ� ����
//3. �õ������ְ�, ����[����]��
//4. �õ������ְ�, ����[����]��

public enum BossState
{ 
    BS_MoveToApper, //���� ����
    BS_Phase01,//���ڸ� ����
    BS_Phase02 //�¿��̵� ����
}
public class BossAI : MonoBehaviour, IMovement, IDamaged
{
    [SerializeField] private float bossAppearPointY = 2.5f;
    private BossState curState = BossState.BS_MoveToApper;
    private Vector2 moveDir = Vector2.zero;
    private bool isInit = false;
    private float moveSpeed = 3f;
    private string bossName;
    private int curHP;
    private int maxHP;

    public bool IsDead {
        get {
            return curHP <= 0;
        }
    }

    private Iwaper[] weapons;
    private Iwaper curWeapon;

    public event Action OnBossDied;

    //DI : ������ ����
    //�� ���������� DI�� ����Ǿ� ���� �ʴ�. (������ ������ �����ִ� �����̴�.)
    public void InitBoss(string newBossName,int newHP , Iwaper[] newWeapons)
    {
        weapons = newWeapons;
        curHP = maxHP = newHP;
        bossName = newBossName;

        SetEnable(true);
    }
    private void Update()
    {
        if (isInit)
        {
            Move(moveDir);
        }
    }
    public void Move(Vector2 moveDir)
    {
        transform.Translate(moveDir* (moveSpeed*Time.deltaTime));
    }

    public void SetEnable(bool enable)
    {
        isInit = enable;
        if (isInit) {
            //ai����
            ChangeState(BossState.BS_MoveToApper);//ù��° ����
        }
    }

    private void ChangeState(BossState newStae)
    {
        StopCoroutine(curState.ToString()); //�� �������� State�� ���߰�
        curState = newStae;

        StartCoroutine(curState.ToString());//����� State�� �����ϰ�
    }
    IEnumerator BS_MoveToApper()
    {
        moveDir = Vector2.down;

        while (true) { 
            yield return null; //�������� ����
            if(transform.position.y <= bossAppearPointY)
            {
                moveDir = Vector2.zero;

                ChangeState(BossState.BS_Phase01);
            }
        }
    }

    IEnumerator BS_Phase01()
    {
        curWeapon = weapons[0];
        if (curWeapon != null)
        {
            curWeapon.SetEnable(true);
        }

        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            curWeapon?.Fire();
        }
    }

    IEnumerator BS_Phase02()
    {
        curWeapon = weapons[1];

        moveDir = Vector2.right;

        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            
            if(MathF.Abs(transform.position.x) > 2.5f)
            {
                moveDir *= -1f; //���� ����
            }
            curWeapon?.Fire();
        }
    }
    public void TakeDamage(GameObject attacker, int damage)
    {
        if (curState == BossState.BS_MoveToApper)
            return;

        if (IsDead)
            return;

        curHP -= damage;

        if(curHP <= 0)
        {
            OnDied();
        }
        else
        {
            OnDamaged();
        }
    }

    private void OnDamaged()
    {
        if (curState == BossState.BS_Phase01 && curHP/(float)maxHP < 0.5f) {
            //����2������ �����Ѵ�.
            ChangeState(BossState.BS_Phase02);
        }
    }

    private void OnDied()
    {
        OnBossDied?.Invoke();

        Destroy(gameObject);
    }
}
