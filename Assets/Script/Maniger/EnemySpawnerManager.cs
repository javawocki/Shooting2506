using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//���� �����߿� 
//2�ʿ� �ѹ��� ���⸦ 5������ ���� => ���̺�
//�� 10���� ���̺갡 ����Ǹ� ���̺긦 ���߰�, ���� ����
//������ �ı��� �Ǹ� ���̵� �����Ͽ� ���̺긦 ����

//���� ������ �ݺ�
public class EnemySpawnerManager : MonoBehaviour,IManager
{

    [SerializeField] private Transform[] spawnTrans;
    [SerializeField] private GameObject[] spawnEnemyPrefabs;
    [SerializeField] private GameObject[] spawnBossPrefabs;

    public static Action OnSpawnFinish; //�Ϲݸ����� ������ �Ϸᰡ �� Ÿ�̹�.
    
    private int spawnLevel = 0; //���̵�
    private float spawnDelta = 2f;//���� ����
    private int waveCount = 1; //���̺� Ƚ�� ī����

    private GameObject obj;
    //private ���� ��ũ��Ʈ �ۼ��� �۾�

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

        //�������� ����....
        Debug.Log("=====================");
        Debug.Log(spawnLevel);
        Debug.Log("=====================");
        obj = Instantiate(spawnBossPrefabs[spawnLevel]
                                ,new Vector3(0f,8f,0f)
                                ,Quaternion.identity);
        //���ⰴü �����ϰ�,
        //�̸� �����ϰ�,
        //ü�� �����ϰ�.
        //������ ���̺�....
        Iwaper[] newWeapoons = new Iwaper[] { new Bow(), new Sword() };

        foreach(var weapon in newWeapoons)
        {
            weapon.SetOwner(obj);
        }

        if(obj.TryGetComponent<BossAI>(out  bossAi))
        {
            bossAi.InitBoss($"���������� ����{spawnLevel}",
                            500 * (spawnLevel+1),
                            newWeapoons);
            bossAi.OnBossDied += HandBossDied;
        }
        //���� ���̺긦 �غ�

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
