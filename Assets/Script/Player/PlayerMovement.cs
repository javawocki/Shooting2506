using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovement
{
    private bool isMoving = false;

    private float moveSpeed = 5.0f;

    private readonly Vector2 minArea = new Vector2(-2.0f, -4.5f);
    private readonly Vector2 maxArea = new Vector2(2.0f, 0f);

    private Vector3 moveDelta;
    private Vector3 newPos;

    public void Move(Vector2 moveDir)
    {
        if (isMoving)
        {
            moveDelta = new Vector3(moveDir.x, moveDir.y, 0f) * moveSpeed * Time.deltaTime;

            newPos = transform.position + moveDelta;

            newPos.x = Mathf.Clamp(newPos.x, minArea.x, maxArea.x);
            newPos.y = Mathf.Clamp(newPos.y, minArea.y, maxArea.y);

            newPos.z = 0f;

            transform.position = newPos;
        }
    }

    public void SetEnable(bool enable)
    {
       isMoving = enable;
    }

    // Start is called before the first frame update
  
}
