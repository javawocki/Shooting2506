using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//해당 오브젝트가 월드에 생성이 되면 
//5초 동안 애니메이션이 자동으로 재생이 됨.
//5초 기다렸다가 스스로 파괴가 되면서 해당 위치에 메테오를 생성


public class AlertLine : MonoBehaviour
{
    [SerializeField] private GameObject meteoPrefab;

    private void Awake()
    {
        Invoke("MeteoSpawn", 5f); //지연처리
    }

    private void MeteoSpawn() {
        Vector3 spawnPos = transform.position;
        spawnPos.y = 7.0f;

        Instantiate(meteoPrefab, spawnPos, Quaternion.identity);

        Destroy(gameObject);
    }
}
