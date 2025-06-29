using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Iwaper 
{
    void SetOwner(GameObject owner);

    void Fire();

    void SetEnable(bool enable);

    void LaunchBoom();
}
