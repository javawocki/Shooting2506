using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScroller 
{
    void Scroller(float deltaTime);

    void ResetPosition();

    void SetScrollSpeed(float newSpeed);
}
