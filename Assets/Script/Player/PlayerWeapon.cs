using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour,Iwaper
{
    private GameObject weaponOwner;
    [SerializeField] private Transform fireTrans; //�Ѿ��� �߻�Ǵ� ��ġ����
    [SerializeField] private GameObject boomPrefab;//��ź�� ������Ʈ Ǯ�� ����x,��������

    private int numOfProjectiles = 5; //�ѹ��� �߻��ϴ� ����ü�� ����
    private float spreadAngle = 5.0f; //����ü�� ����ü ������ ���� ����
    private float fireRate = 0.3f; //���� �߻� ���Ŀ� �߻������ ����
    private float nextFireTime = 0f; //�����߻������ �ð��� ����ϱ� ���� ����
    private bool isFiring = false; //����ü�� �߻��ϰ� �ִ� �����ΰ�? �����ϴ� ����

    //�߻� ������ �����ϴ� ������
    private float startAngle;
    private float angle;
    private Quaternion fireRotation;
    private Projectfile proj;
    private GameObject obj;

    //PlayerController => CustomUpdate ȣ��.. 1�����Ӵ� 1���� ȣ��
    public void Fire()
    {
        if (Time.time < nextFireTime)
        {
            return;
        }

        if (isFiring)
        {
            nextFireTime = Time.time + fireRate;

            //ù��° ����ü�� �߻� ����
            startAngle = -spreadAngle * ((numOfProjectiles - 1) / 2);

            for (int i = 0; i < numOfProjectiles; i++)
            {
                //������ �߻� ���� ����
                angle = startAngle + (i * spreadAngle);

                fireRotation = fireTrans.rotation * Quaternion.Euler(0f,0f,angle);
                Vector2 fireDir = fireRotation * Vector2.up;

                //����ü�� �����ϰ� �߻縦 ���ָ� �˴ϴ�.
                //������Ÿ�� �޳����� ���� ������Ư Ǯ�� ���� �Ŀ� �ۼ�
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
