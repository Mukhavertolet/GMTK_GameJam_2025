using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthEffect : MonoBehaviour, IEffect
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
        GameManager.playerInstance.maxHP += value * loop;
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
    public int GetItemLevel()
    {
        return loop;
    }
}
