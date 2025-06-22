using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour, IManager
{
    private IMovement movenment;

    /// <summary>
    /// ĳ������ �Ѱ��޴��� ����
    /// </summary>
    /// <param name="param"></param>
    /// <param name="param2"></param>
    /// <param name="param3">������ �Է°�(�̵�����)�� �޾ƿ´�.</param>
    public void CustomUpdate(int param, float param2, Vector2 param3)
    {
        movenment?.Move(param3);
    }

    public void InitManager(int param, float param2, Vector2 param3)
    {
       if(!TryGetComponent<IMovement>(out movenment))
        {
            Debug.Log("PlayerController - InitManger() - movement ������ �����ߴ�.");
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
