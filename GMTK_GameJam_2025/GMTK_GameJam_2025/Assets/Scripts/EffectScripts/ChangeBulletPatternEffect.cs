using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBulletPatternEffect : MonoBehaviour, IEffect
{
    public string effectName;
    public string effectDesc;

    public string condition;

    public List<GameObject> BulletPattern;

    public int stack;
    public int loop;


    public string[] GetNameAndDesc()
    {
        return new string[] { effectName, effectDesc };
    }

    public void ApplyEffect()
    {
        if(loop > BulletPattern.Count)
            loop = BulletPattern.Count;

        GameManager.playerInstance.bulletPattern[0] = BulletPattern[loop-1];
        Debug.Log($"bullet pattern changed to {BulletPattern}");
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
