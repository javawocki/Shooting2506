using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ư���� ��ġ�� ������ �Ǹ�
//�߷¿� ���ؼ� �Ʒ��������� �̵�.


[RequireComponent(typeof(Rigidbody2D))] //�߷¿� ���ؼ� �߶�
[RequireComponent(typeof(CircleCollider2D))]
public class DropJam : MonoBehaviour, IPickup
{
    public static event Action OnPickupJam;

    private bool isSetTarget = false; //������ ������ �Ǿ��°�?
    private GameObject target;
    private float pickupTimerPer;
    private Rigidbody2D rig;


    private void Awake()
    {
        if(TryGetComponent<Rigidbody2D>(out rig))
        {
            rig.gravityScale = 1.0f;

            Vector2 initVelocity = Vector2.zero;
            initVelocity.x = UnityEngine.Random.Range(-0.5f,0.5f);
            initVelocity.y = UnityEngine.Random.Range(3.0f, 4.0f);

            rig.AddForce(initVelocity, ForceMode2D.Impulse); //������ �ӵ��� 0���� ����� ���� ���� ���Ѵ�.
        }

        if(TryGetComponent<CircleCollider2D>(out CircleCollider2D col))
        {
            col.isTrigger = true;
            col.radius = 0.2f;
        }
    }
    //�ڱ��忡 ������� ȣ��
    //Ÿ���� �������ִ� �޼ҵ�
    public void OnPickup(GameObject picker)
    {
        rig.gravityScale = 0f; //�߷� ������ ����
        rig.velocity = Vector2.zero;

        isSetTarget = true;
        target = picker;

        pickupTimerPer = 0f;

    }

    private void Update()
    {
        if (isSetTarget && target.activeSelf) { 
            pickupTimerPer += Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position,target.transform.position,pickupTimerPer/2f);

            float sgrDist = (transform.position - target.transform.position).sqrMagnitude;

            if(sgrDist < 0.1f || pickupTimerPer >= 2f)
            {
                OnPickupJam?.Invoke(); //����Ǿ��ٴ� �̺�Ʈ �߻�
                Destroy(gameObject); //�ڱ� �ڽ� �ı�
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DestroyArea")) {
            Destroy(gameObject);
        }
    }
}
