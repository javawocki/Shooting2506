using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//게임에 필요한 여러 수치를 저정하고 관리하는 메니저 
//게임의 성향에 따라서 각가의 캐릭터가 수치들을 관리 할 수 있다.
//게임 내에 변화되는 수치들을 관리하는 역할.
//적 캐릭터 점수를 발생 -> 스코어 메니저가 구독 -> UI 메니저 스코어 매니저를 구독.
//플레이어 캐릭터가 피격 발생 -> 스코어매니저가 구독 -> UI 메니저 스코어 매니저를 구독.
public class ScoreManager : MonoBehaviour, IManager
{
    public static event Action<int> OnChangeScore;
    public static event Action<int> OnChangeJamCount;
    public static event Action<int> OnChangeHP;
    public static event Action<int> OnChangeBoom;
    public static event Action<int> OnChangePower;
    public static event Action OnDiedPlayer; //게임종료

    private const int MaxHP = 5;

    private int score;
    private int Score
    {
        set {
            score = value;
            OnChangeScore?.Invoke(score);
        }
    }

    private int curHP;
    private int CurHP
    {
        set
        {
            curHP = value;
            OnChangeHP?.Invoke(curHP);
        }
    }

    private int jamCount;
    private int JamCount
    {
        set
        {
            jamCount = value;
            OnChangeJamCount?.Invoke(jamCount);
        }
    }

    private int boomCount;
    private int BoomCount
    {
        set
        {
            boomCount = value;
            OnChangeBoom?.Invoke(boomCount);
        }
    }

    private int powerLevel;
    private int PowerLevel
    {
        set
        {
            powerLevel = value;
            OnChangePower?.Invoke(powerLevel);
        }
    }

    public void CustomUpdate(int param, float param2, Vector2 param3)
    {
        
    }

    public void InitManager(int param, float param2, Vector2 param3)
    {
        
    }

    public void StartGame() //게임시작시 초기 세팅
    {
        Score = 0;
        CurHP = MaxHP;
        PowerLevel = 1;
        JamCount = 0;
        BoomCount = 3;

    }

    public void StopGame()
    {
        
    }
    //수치의 변화를 발생시켜주는 객체들 구독 신청.
    private void OnEnable()
    {
        //람다식(익명함수)사용
        Enemy.OnMonsterDied += (Enemy info) => Score = score + 10;
        //Enemy.OnMonsterDied += (Enemy info) => Score = score + info이걸 이용;

        DropJam.OnPickupJam += HandleJamPickup;

        PlayerHit.OnPlayerHPIncreaded += PlayerHPChange;
    }

    private void HandleJamPickup()
    {
        Score = score + 7;
        JamCount = jamCount + 1;
    }

    public void PlayerHPChange(bool isIncreased)
    {
        if (isIncreased)
        {
            if(curHP + 1 < MaxHP)
            {
                curHP = curHP + 1;
            }
            else
            {
                CurHP = MaxHP;
            }
        }
        else
        {
            if(curHP - 1 <= 0)
            {
                CurHP = 0;
                OnDiedPlayer?.Invoke();
            }
            else
            {
                CurHP = curHP - 1;
            }
        }

       // Debug.Log($"변화된 체력 : {curHP}");
    }

    public void ChangeBoomCount(bool isIncreased)
    {
        if (isIncreased)
        {
            BoomCount = boomCount + 1;
        }
        else
        {
            BoomCount = boomCount - 1;
        }
    }
}
