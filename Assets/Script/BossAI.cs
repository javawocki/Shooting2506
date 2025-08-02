using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//FSM 유한 상태 기계

//자동차
//1. 시동꺼짐
//2. 시동켜져있는데 주차
//3. 시동켜져있고, 운행[전진]중
//4. 시동켜져있고, 운형[후진]중

public enum BossState
{ 
    BS_MoveToApper, //보스 등장
    BS_Phase01,//재자리 공격
    BS_Phase02 //좌우이동 공경
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

    //DI : 의존성 주입
    //현 예제에서는 DI가 적용되어 있지 않다. (개선의 여지가 남아있는 예제이다.)
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
            //ai동장
            ChangeState(BossState.BS_MoveToApper);//첫번째 등장
        }
    }

    private void ChangeState(BossState newStae)
    {
        StopCoroutine(curState.ToString()); //현 동작중인 State를 멈추고
        curState = newStae;

        StartCoroutine(curState.ToString());//변경된 State를 시작하고
    }
    IEnumerator BS_MoveToApper()
    {
        moveDir = Vector2.down;

        while (true) { 
            yield return null; //한프레임 쉬고
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
                moveDir *= -1f; //방향 반전
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
            //패턴2번으로 변경한다.
            ChangeState(BossState.BS_Phase02);
        }
    }

    private void OnDied()
    {
        OnBossDied?.Invoke();

        Destroy(gameObject);
    }
}
