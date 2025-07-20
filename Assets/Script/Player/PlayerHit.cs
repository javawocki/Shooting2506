using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�ǰ��� �����ϰ�, �ǰ��̺�Ʈ�� �߻������ִ� ����
[RequireComponent(typeof(CircleCollider2D))]
public class PlayerHit : MonoBehaviour, IDamaged
{
    public static event Action<bool> OnPlayerHPIncreaded; //true ü�� ȸ��, false ü�°���

    private void Awake()
    {
        if(TryGetComponent<CircleCollider2D>(out CircleCollider2D col))
        {
            col.isTrigger = true;
            col.radius = 0.2f;
        }
    }
    public void TakeDamage(GameObject attacker, int damage)
    {
       OnPlayerHPIncreaded?.Invoke(false);//ü�°���
    }
}
