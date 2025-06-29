using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour,Iwaper
{
    private GameObject weaponOwner;
    [SerializeField] private Transform fireTrans; //총알이 발사되는 위치정보
    [SerializeField] private GameObject boomPrefab;//폭탄은 오브젝트 풀링 적용x,별도관리

    private int numOfProjectiles = 5; //한번에 발사하는 투사체의 갯수
    private float spreadAngle = 5.0f; //투사체와 투사체 사이의 간격 각도
    private float fireRate = 0.3f; //이전 발사 이후에 발사까지의 간격
    private float nextFireTime = 0f; //다음발사까지의 시간을 계산하기 위한 변수
    private bool isFiring = false; //투사체를 발사하고 있는 상태인가? 관리하는 변수

    //발사 방향을 연산하는 변수들
    private float startAngle;
    private float angle;
    private Quaternion fireRotation;
    private Projectfile proj;
    private GameObject obj;

    //PlayerController => CustomUpdate 호출.. 1프레임당 1번씩 호출
    public void Fire()
    {
        if (Time.time < nextFireTime)
        {
            return;
        }

        if (isFiring)
        {
            nextFireTime = Time.time + fireRate;

            //첫번째 투사체의 발사 방향
            startAngle = -spreadAngle * ((numOfProjectiles - 1) / 2);

            for (int i = 0; i < numOfProjectiles; i++)
            {
                //각각의 발사 방향 연산
                angle = startAngle + (i * spreadAngle);

                fireRotation = fireTrans.rotation * Quaternion.Euler(0f,0f,angle);
                Vector2 fireDir = fireRotation * Vector2.up;

                //투사체를 생성하고 발사를 해주면 됩니다.
                //프로젝타일 메너저를 만들어서 오프제특 풀링 구현 후에 작성
            }



        }
    
    }

    public void LaunchBoom()
    {
        obj = Instantiate(boomPrefab,transform.position,Quaternion.identity);
    }

    public void SetEnable(bool enable)
    {
        isFiring = enable;
    }

    public void SetOwner(GameObject owner)
    {
        weaponOwner = owner;
    }

}
