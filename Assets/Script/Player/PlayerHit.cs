using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//피격을 검출하고, 피격이벤트를 발생시켜주는 역할
[RequireComponent(typeof(CircleCollider2D))]
public class PlayerHit : MonoBehaviour, IDamaged
{
    public static event Action<bool> OnPlayerHPIncreaded; //true 체력 회복, false 체력감소

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
       OnPlayerHPIncreaded?.Invoke(false);//체력감소
    }
}
