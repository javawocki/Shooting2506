using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Meteorite : MonoBehaviour
{
    private void Awake()
    {
        if(TryGetComponent<Rigidbody2D>(out Rigidbody2D rig))
        {
            rig.gravityScale = 1.0f;
            rig.velocity = Vector3.zero;

           
        }

        if (TryGetComponent<CircleCollider2D>(out CircleCollider2D col))
        {
            col.isTrigger = true;
            col.radius = 0.3f;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent<IDamaged>(out IDamaged damaged))
            {
                damaged.TakeDamage(gameObject, 1);
                Destroy(gameObject);
                return;
            }
        }

        if (collision.CompareTag("DestroyArea"))
        {
            Destroy(gameObject);
        }
    }
}
