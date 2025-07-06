using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class MeteoSpwanManager : MonoBehaviour, IManager
{
    [SerializeField] private GameObject alertLinePrefab;

    private float spawnDelta = 3f;
    private GameObject obj;

    private Vector3 spawnPos = Vector3.zero;
    private bool isInit = false;

    public void CustomUpdate(int param, float param2, Vector2 param3)
    {

    }

    // Start is called before the first frame update
    public void InitManager(int param, float param2, Vector2 param3)
    {
       
    }
    



    public void StartGame()
    {
        StartCoroutine("SpawnMeteo");
    }

    public void StopGame()
    {
        StopCoroutine("SpawnMeteo");
    }

    IEnumerator SpawnMeteo()
    {
        yield return null;

        while (true)
        {
            spawnPos = Vector3.zero;
            spawnPos.x = UnityEngine.Random.Range(-2.3f, 2.3f);

            Instantiate(alertLinePrefab, spawnPos, Quaternion.identity);

            yield return new WaitForSeconds(spawnDelta);
        }
    }
}
