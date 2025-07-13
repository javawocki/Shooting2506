using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ProjectileType
{
    fire01,
    fire02,
    fire03,
    fire04,
    fire05,
    fire06
}
public class ProjecfileManager : Singletone<ProjecfileManager>
{
    [SerializeField] private GameObject[] projectilePrefabs;

    private static Queue<Projectfile>[] projectileQuenes;

    private int poolSize = 10;

    protected override void DoAwake()
    {
        base.DoAwake();

        projectileQuenes = new Queue<Projectfile>[projectilePrefabs.Length];

        for(int i = 0; i < projectileQuenes.Length; i++)
        {
            projectileQuenes[i] = new Queue<Projectfile>();
            Allocate((ProjectileType)i);
        }
    }

    private GameObject obj;
    private Projectfile pro;

    //type�� ���� �ش��ϴ� ������Ʈ�� �����ؼ� �����ϴ� �� �־��ִ� �Լ� 
    private void Allocate(ProjectileType type)
    {
        for (int i = 0; i < poolSize; i++)
        {
            obj = Instantiate(projectilePrefabs[(int)type]);
            if(obj.TryGetComponent<Projectfile>(out pro))
            {
                projectileQuenes[(int)type].Enqueue(pro);
            }

            obj.SetActive(false);
        }
    }

    //�ܺο��� Ư���� ������Ʈ �߻��û 
    public void FireProjectile(ProjectileType type, 
                                Vector3 spawnPos,
                                Vector2 direction,
                                GameObject owner,
                                int damage,
                                float speed)
    {
        pro = GetProjectileFromPool(type);

        if(pro != null)
        {
            pro.transform.position = spawnPos;
            pro.gameObject.SetActive(true);

            pro.InitProjectile(type,direction, owner, damage, speed);
        }
    }
    //ť���� ����ü �������� �޼ҵ�
    private Projectfile GetProjectileFromPool(ProjectileType type)
    {
        if (projectileQuenes[(int)type].Count < 1)
        {
            Allocate(type);
        }

        return projectileQuenes[(int)type].Dequeue();
    }

    public void ReturnProjectilePool(Projectfile projectile,ProjectileType type)
    {
        projectile.gameObject.SetActive(false);
        projectileQuenes[(int)type].Enqueue(projectile);
    }
}
