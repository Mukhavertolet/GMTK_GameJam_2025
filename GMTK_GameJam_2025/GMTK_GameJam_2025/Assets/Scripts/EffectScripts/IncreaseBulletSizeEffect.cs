using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseBulletSizeEffect : MonoBehaviour, IEffect
{
    public string effectName;
    public string effectDesc;

    public string condition;

    public string[] GetNameAndDesc()
    {
        return new string[] { effectName, effectDesc };
    }

    public void ApplyEffect()
    {
        GameManager.playerInstance.bulletSize += 0.5f;
        Debug.Log("BULLET SIZE INCREASED");
    }

    public string GetCondition()
    {
        return condition;
    }

}
