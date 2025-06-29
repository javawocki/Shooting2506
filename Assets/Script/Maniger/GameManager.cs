using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//객체를 1개만 가지고 있으면서 게임의 전체의 흐름을 관리한다.
//게임 준비단계,게임 시작, 게임플에이하고 있는중, 게임을 종료
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
