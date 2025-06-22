using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollManger : MonoBehaviour, IManager
{

    private List<IScroller> scrollers = new();
    private float scrollerSpeed = 0f;
    private bool isInit = false;



    public void InitManager(int param, float param2, Vector2 param3)
    {
        scrollers = InterfaceFinder.FindObjectsOfInterface<IScroller>();
        scrollerSpeed = param2;
    }
    public void CustomUpdate(int param, float param2, Vector2 param3)
    {
        if (isInit) {
            foreach (var scroll in scrollers)
            {
              
              scroll?.Scroller(Time.deltaTime);
            }
        }
    }

   

    public void StartGame()
    {
        if (scrollerSpeed > 0f)
        {
            isInit = true;

            foreach (var scroll in scrollers)
            {
                scroll?.SetScrollSpeed(scrollerSpeed);
            }
        }
        else
        {
                isInit = false;
        }
    }

    public void StopGame()
    {
       
    }
}
