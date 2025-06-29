using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ü�� 1���� ������ �����鼭 ������ ��ü�� �帧�� �����Ѵ�.
//���� �غ�ܰ�,���� ����, �����ÿ����ϰ� �ִ���, ������ ����
public class GameManager : Singletone<GameManager>
{
    private ScrollManger scrollManger;
    private PlayerController playController;

    private void Start()
    {
        FindReferenceManagers();

        InitManagers();

        StartCoroutine(StartGame());
    }

    private void Update()
    {
        scrollManger?.CustomUpdate(0, 0f, Vector2.zero);
        playController?.CustomUpdate(0, 0f, new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical")));
    }

    private void FindReferenceManagers()
    {
        scrollManger = FindAnyObjectByType<ScrollManger>();
        playController = FindAnyObjectByType<PlayerController>();
    }

    private void InitManagers()
    {
        scrollManger?.InitManager(0, 2.5f, Vector2.zero);
        playController?.InitManager(0,0f, Vector2.zero);
    }

    IEnumerator StartGame()
    {
        yield return null;

        scrollManger?.StartGame();
        playController?.StartGame();
    }

    
  


   

    
}
