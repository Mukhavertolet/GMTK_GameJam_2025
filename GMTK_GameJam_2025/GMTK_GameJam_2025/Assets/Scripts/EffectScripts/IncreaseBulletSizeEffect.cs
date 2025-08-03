using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseBulletSizeEffect : MonoBehaviour, IEffect
{
    public string effectName;
    public string effectDesc;

    public string condition;

    public float value;
    public int loop;

    public string[] GetNameAndDesc()
    {
        return new string[] { effectName, effectDesc };
    }

    public void ApplyEffect()
    {
        GameManager.playerInstance.bulletSize += value * loop;
        Debug.Log("BULLET SIZE INCREASED");
    }

    public string GetCondition()
    {
        return condition;
    }
    public void SetItemLevel(int level)
    {
        loop = level;
    }
    public int GetItemLevel()
    {
        return loop;
    }
}
