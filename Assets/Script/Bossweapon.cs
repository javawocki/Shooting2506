using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bossweapon : Iwaper
{
    protected GameObject owner;
    protected abstract void SetFire();//��������,�Ļ�Ŭ�������� �̹������� ����
    
    public void Fire()
    {
        if (owner == null)
        {
            return;
        }
            SetFire();
        
    }

    public void LaunchBoom()
    {
        
    }

    public void SetEnable(bool enable)
    {
        
    }

    public void SetOwner(GameObject owner)
    {
       
    }
}

public class Bow : Bossweapon
{
    //������ �������� �Ӽ��� ����
    protected override void SetFire()
    {
        Vector3 firePos = owner.transform.position;

        int numProjectiles = 5;
        float spreadAngle = 15f;
        Debug.Log("���� ���� 1�� �߻�1");

        for (int i = 0; i < numProjectiles; i++)
        {
            float angle = spreadAngle * (i - ((numProjectiles - 1) / 2));
            Vector2 fireDirection = Quaternion.Euler(0f, 0f, angle) * Vector2.down;
            ProjecfileManager.Inst.FireProjectile(ProjectileType.fire01
                                                    , firePos
                                                    , fireDirection
                                                    , owner
                                                    , 1
                                                    , 6f);
        }
    }
}

public class Sword : Bossweapon
{
    //������ �������� �Ӽ��� ����
    protected override void SetFire()
    {
       
        Debug.Log("���� ���� 2�� �߻�1");

        
    }
}