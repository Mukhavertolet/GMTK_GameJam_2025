using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastBulletEffect : MonoBehaviour, IEffect
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
        GameManager.playerInstance.bulletSpd += value * loop;

        if (GameManager.playerInstance.attackSpd > 0.2f)
            GameManager.playerInstance.attackSpd -= value * 0.1f * loop;
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
