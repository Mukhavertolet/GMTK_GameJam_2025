using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseBulletSizeEffect : IEffect
{

    public void ApplyEffect()
    {
        GameManager.gameManager.bulletSize += 5;
        Debug.Log("BULLET SIZE INCREASED");
    }
}
