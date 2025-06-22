using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IManager 
{
    void InitManager(int param, float param2, Vector2 param3);

    void StartGame();

    void StopGame();

    public void CustomUpdate(int param,float param2, Vector2 param3);
}
