using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//특정한 위치에 생성이 되면
//중력에 의해서 아래방향으로 이동.


[RequireComponent(typeof(Rigidbody2D))] //중력에 의해서 추락
[RequireComponent(typeof(CircleCollider2D))]
public class DropJam : MonoBehaviour, IPickup
{
    public static event Action OnPickupJam;

    private bool isSetTarget = false; //습득자 결정이 되었는가?
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

            rig.AddForce(initVelocity, ForceMode2D.Impulse); //기존의 속도를 0으로 만들고 나서 힘을 가한다.
        }

        if(TryGetComponent<CircleCollider2D>(out CircleCollider2D col))
        {
            col.isTrigger = true;
            col.radius = 0.2f;
        }
    }
    //자기장에 닿았을때 호출
    //타켓을 지정해주는 메소드
    public void OnPickup(GameObject picker)
    {
        rig.gravityScale = 0f; //중력 영향을 삭제
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
                OnPickupJam?.Invoke(); //습득되었다는 이벤트 발생
                Destroy(gameObject); //자기 자신 파괴
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
