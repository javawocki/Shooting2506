using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallteSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    ScrollManger scrollManger;

    private void Start()
    {
        FindReferenceManagers();

        InitManagers();

        StartCoroutine(StartGame());
    }

    private void Update()
    {
        scrollManger?.CustomUpdate(0,0f,Vector2.zero);
    }

    private void FindReferenceManagers()
    {
        scrollManger = FindAnyObjectByType<ScrollManger>();
    }

    private void InitManagers()
    {
        scrollManger?.InitManager(0, 2.5f, Vector2.zero);
    }

    IEnumerator StartGame() 
    {
        yield return null;

        scrollManger?.StartGame();
    }
}
