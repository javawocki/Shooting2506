using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class ProjectileInfo {
    private float moveSpeed = 10.0f;
    private int damage = 0;
    private Vector2 moveDir = Vector2.zero;
    private GameObject owner;
    private bool isInit = false;

    public ProjectileInfo(float moveSpeed, int damage, Vector2 moveDir, GameObject owner, bool isInit)
    {
        this.moveSpeed = moveSpeed;
        this.damage = damage;
        this.moveDir = moveDir;
        this.owner = owner;
        this.isInit = isInit;
    }
}
*/
//���� ��ü�鿡�� �ش� ����ü�� ������ ���ָ� ������ �ӵ�,������ �������� �̵��ϸ鼭
//���ʰ� �ƴ� �ٸ� ���� ���� �浹������ ���¿��� �������� �����ϴ� ����
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Projectfile : MonoBehaviour,IMovement
{
    private CircleCollider2D col;
    public CircleCollider2D Col
    {

        get
        {
            if(col == null)
            {
                col = GetComponent<CircleCollider2D>();
            }
            return col;
        }
    }

    private Rigidbody2D rig;

    public Rigidbody2D Rig
    {
        get
        {
            if (rig == null)
            {
                rig = GetComponent<Rigidbody2D>();
            }
            return rig;
        }
    }

    //DI(�ܺ�����): ���߿� õõ�� ã�ƺ���.
    //����ü�� ���� �Ӽ���
    private float moveSpeed = 10.0f;
    private int damage = 0;
    private Vector2 moveDir = Vector2.zero;
    private GameObject owner;
    private bool isInit = false;
    private string ownerTag;
    //������Ÿ���� ���� : prjectileManager�� ������ �ڿ� �߰�

    public void InitProjectile(Vector2 newDir,GameObject newOwner,int newDamage, float newSpeed)
    {
        this.moveDir = newDir;
        this.owner = newOwner;
        this.damage = newDamage;
        this.moveSpeed = newSpeed;

        ownerTag = newOwner.tag;
        Col.isTrigger = true;
        Col.radius = 0.1f;

        Rig.gravityScale = 0f;
        SetEnable(true);
    }
    //����ü�� ������ Controller��� ��ü������ update�� ����
    public void Move(Vector2 moveDir)
    {
        if (isInit)
        {
            transform.Translate(moveDir * (moveSpeed * Time.deltaTime));
        }
    }

    private void Update()
    {
        Move(moveDir);
    }

    public void SetEnable(bool enable)
    {
        isInit = enable;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�� ����: �ش� �żҵ尡 ������ �Ǹ� �ȵǴ� ��Ȳ�� ���� üũ return
        //�ڵ� �ۼ��ϴ� ������߿� �ϳ�....
        if (!isInit)
        {
            return;
        }
        if (owner == null) {
            return;
        }
        if (collision.gameObject == owner)
        {
            return;
        }
        if (collision.CompareTag(ownerTag))
        {
            return;
        }

        if (collision.CompareTag("DestroyArea"))
        {
            //����ü �ı�
        }
        else
        {
            if(collision.TryGetComponent<IDamaged>(out IDamaged damaged))
            {
                damaged.TakeDamage(owner, damage);
                //����ü �ı�
            }
        }
    }

}
