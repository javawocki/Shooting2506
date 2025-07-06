using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//PickupItem 레이어와 충돌체크한 뒤 
//IPicup 인터페이스의 호출 
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class ItemColleter : MonoBehaviour
{
    private void Awake()
    {
        if(TryGetComponent<Rigidbody2D>(out Rigidbody2D rig))
        {
            rig.gravityScale = 0f;
        }

        if (TryGetComponent<CircleCollider2D>(out CircleCollider2D col))
        {
            col.radius = 1.5f;
            col.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickupItem")) { 
            if(collision.TryGetComponent<IPickup>(out IPickup pick))
            {
                pick.OnPickup(transform.root.gameObject);
            }
        }
    }
}
