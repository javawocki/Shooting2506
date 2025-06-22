using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour, IManager
{
    private IMovement movenment;

    /// <summary>
    /// 캐릭터의 총괄메니저 역할
    /// </summary>
    /// <param name="param"></param>
    /// <param name="param2"></param>
    /// <param name="param3">유저의 입력값(이동방향)을 받아온다.</param>
    public void CustomUpdate(int param, float param2, Vector2 param3)
    {
        movenment?.Move(param3);
    }

    public void InitManager(int param, float param2, Vector2 param3)
    {
       if(!TryGetComponent<IMovement>(out movenment))
        {
            Debug.Log("PlayerController - InitManger() - movement 참조에 실패했다.");
        }
    }

    public void StartGame()
    {
       movenment?.SetEnable(true);
    }

    public void StopGame()
    {
        movenment?.SetEnable(false);
    }
}
