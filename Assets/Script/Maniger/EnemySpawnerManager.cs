using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//게임 진행중에 
//2초에 한번씩 적기를 5마리씩 생성 => 웨이브
//총 10번의 웨이브가 진행되면 웨이브를 멈추고, 보스 생성
//보스가 파괴가 되면 난이도 증가하여 웨이브를 시작

//위의 내용을 반복
public class EnemySpawnerManager : MonoBehaviour,IManager
{

    [SerializeField] private Transform[] spawnTrans;
    [SerializeField] private GameObject[] spawnEnemyPrefabs;
    [SerializeField] private GameObject[] spawnBossPrefabs;

    public static Action OnSpawnFinish; //일반몬스터의 스폰이 완료가 된 타이밍.
    
    private int spawnLevel = 0; //난이도
    private float spawnDelta = 2f;//스폰 간격
    private int waveCount = 1; //웨이브 횟수 카운팅

    private GameObject obj;
    //private 보스 스크립트 작성후 작업

    public void CustomUpdate(int param, float param2, Vector2 param3)
    {
       
    }

    public void InitManager(int param, float param2, Vector2 param3)
    {

        spawnLevel = param;
        spawnDelta = param2;
        waveCount = 2;
    }

    public void StartGame()
    {
        StartCoroutine("SpawnEnemys");
    }

    public void StopGame()
    {
        StopCoroutine("SpawnEnemys");
    }

    IEnumerator SpawnEnemys()
    {
        yield return null;

        while (waveCount > 0)
        {
            for(int i = 0; i < spawnTrans.Length; i++)
            {
                obj = Instantiate(spawnEnemyPrefabs[spawnLevel], spawnTrans[i].position, Quaternion.identity);

                if(obj.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    enemy.SetEnable(true);
                }
            }
            waveCount--;
            yield return new WaitForSeconds(spawnDelta);

        }

        OnSpawnFinish?.Invoke();

        yield return new WaitForSeconds(5f);

        //보스몬스터 스폰....
        Debug.Log("=====================");
        Debug.Log(spawnLevel);
        Debug.Log("=====================");
        obj = Instantiate(spawnBossPrefabs[spawnLevel]
                                ,new Vector3(0f,8f,0f)
                                ,Quaternion.identity);
        //무기객체 생성하고,
        //이름 설정하고,
        //체력 설정하고.
        //데이터 테이블....
        Iwaper[] newWeapoons = new Iwaper[] { new Bow(), new Sword() };

        foreach(var weapon in newWeapoons)
        {
            weapon.SetOwner(obj);
        }

        if(obj.TryGetComponent<BossAI>(out  bossAi))
        {
            bossAi.InitBoss($"무지막지한 보스{spawnLevel}",
                            500 * (spawnLevel+1),
                            newWeapoons);
            bossAi.OnBossDied += HandBossDied;
        }
        //다음 웨이브를 준비

        waveCount = 2;
        spawnLevel++;

    }

    private BossAI bossAi;

    private void HandBossDied()
    {
        bossAi.OnBossDied -= HandBossDied;

        StartCoroutine("SpawnEnemys");
    }
}
