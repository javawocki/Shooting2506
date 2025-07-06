using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ����̶�� �̺�Ʈ�� �߻��� �Ǹ� 
//���忡 ������ϰ� ���������۵� �������ִ� ����
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
