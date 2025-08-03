using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffect : MonoBehaviour, IEffect
{
    public string effectName;
    public string effectDesc;

    public string condition;

    public int value;

    public int loop;

    public string[] GetNameAndDesc()
    {
        return new string[] { effectName, effectDesc };
    }

    public void ApplyEffect()
    {
        GameManager.playerInstance.currentHP += value * loop;
        Debug.Log("heal");
    }

    public string GetCondition()
    {
        return condition;
    }
    public void SetItemLevel(int level)
    {
        loop = level;
    }
}
