using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�ش� ������Ʈ�� ���忡 ������ �Ǹ� 
//5�� ���� �ִϸ��̼��� �ڵ����� ����� ��.
//5�� ��ٷȴٰ� ������ �ı��� �Ǹ鼭 �ش� ��ġ�� ���׿��� ����


public class AlertLine : MonoBehaviour
{
    [SerializeField] private GameObject meteoPrefab;

    private void Awake()
    {
        Invoke("MeteoSpawn", 5f); //����ó��
    }

    private void MeteoSpawn() {
        Vector3 spawnPos = transform.position;
        spawnPos.y = 7.0f;

        Instantiate(meteoPrefab, spawnPos, Quaternion.identity);

        Destroy(gameObject);
    }
}
