using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//���ӿ� �ʿ��� ���� ��ġ�� �����ϰ� �����ϴ� �޴��� 
//������ ���⿡ ���� ������ ĳ���Ͱ� ��ġ���� ���� �� �� �ִ�.
//���� ���� ��ȭ�Ǵ� ��ġ���� �����ϴ� ����.
//�� ĳ���� ������ �߻� -> ���ھ� �޴����� ���� -> UI �޴��� ���ھ� �Ŵ����� ����.
//�÷��̾� ĳ���Ͱ� �ǰ� �߻� -> ���ھ�Ŵ����� ���� -> UI �޴��� ���ھ� �Ŵ����� ����.
public class ScoreManager : MonoBehaviour, IManager
{
    public static event Action<int> OnChangeScore;
    public static event Action<int> OnChangeJamCount;
    public static event Action<int> OnChangeHP;
    public static event Action<int> OnChangeBoom;
    public static event Action<int> OnChangePower;
    public static event Action OnDiedPlayer; //��������

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

    public void StartGame() //���ӽ��۽� �ʱ� ����
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
    //��ġ�� ��ȭ�� �߻������ִ� ��ü�� ���� ��û.
    private void OnEnable()
    {
        //���ٽ�(�͸��Լ�)���
        Enemy.OnMonsterDied += (Enemy info) => Score = score + 10;
        //Enemy.OnMonsterDied += (Enemy info) => Score = score + info�̰� �̿�;

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

       // Debug.Log($"��ȭ�� ü�� : {curHP}");
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
