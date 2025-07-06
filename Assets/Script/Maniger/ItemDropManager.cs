using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//몬스터 사망이라는 이벤트가 발생이 되면 
//월드에 잼생성하고 버프아이템도 생성해주는 역할
public class ItemDropManager : MonoBehaviour
{
    [SerializeField] private GameObject jamPrefab;
    [SerializeField] private GameObject[] flyItems;

    private void OnEnable()
    {
        Enemy.OnMonsterDied += Handle_EnemyDiedEvent;
    }

    private void OnDisable()
    {
        Enemy.OnMonsterDied -= Handle_EnemyDiedEvent;
    }

    private void Handle_EnemyDiedEvent(Enemy enemy)
    {
        for(int i = 0; i < 7; i++)
        {
            Instantiate(jamPrefab, enemy.transform.position, Quaternion.identity);
        }
    }
}
