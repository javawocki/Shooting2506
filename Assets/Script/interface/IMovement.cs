using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement 
{
    void SetEnable(bool enable);

    void Move(Vector2 moveDir);


}
