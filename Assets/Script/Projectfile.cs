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
//여러 객체들에서 해당 투사체를 생성을 해주면 지정된 속도,지정된 방향으로 이동하면서
//오너가 아닌 다른 팀의 대상과 충돌했을때 상태에게 데미지를 전달하는 역할
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

    //DI(외부주입): 나중에 천천히 찾아보자.
    //투사체의 고유 속성값
    private float moveSpeed = 10.0f;
    private int damage = 0;
    private Vector2 moveDir = Vector2.zero;
    private GameObject owner;
    private bool isInit = false;
    private string ownerTag;
    //프로젝타일의 종류 : prjectileManager를 구현한 뒤에 추가

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
    //투사체는 별도의 Controller없어서 자체적으로 update를 수행
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
        //얼리 리턴: 해당 매소드가 실행이 되면 안되는 상황을 먼제 체크 return
        //코드 작성하는 방법론중에 하나....
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
            //투사체 파괴
        }
        else
        {
            if(collision.TryGetComponent<IDamaged>(out IDamaged damaged))
            {
                damaged.TakeDamage(owner, damage);
                //투사체 파괴
            }
        }
    }

}
