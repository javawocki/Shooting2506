using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroller : MonoBehaviour,IScroller
{
    private float scrollSpeed = 0f;
    private Vector3 startPos = new Vector3(0f,12.75f,0f);
    private const float endPosY = -12.75f;

    public void ResetPosition()
    {
        transform.position = startPos;
    }

    public void Scroller(float deltaTime)
    {
        transform.position += Vector3.down * (scrollSpeed * deltaTime);

        if (transform.position.y < endPosY)
        {
            ResetPosition();
        }
    }

    public void SetScrollSpeed(float newSpeed)
    {
        scrollSpeed = newSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
