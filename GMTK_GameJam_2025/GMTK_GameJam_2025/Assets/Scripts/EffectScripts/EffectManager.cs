using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager effectManager;


    public delegate void Condition();
    public event Condition conditionShoot;
    public event Condition conditionRoomClear;


    public List<IEffect> effects = new List<IEffect>();


    private void Awake()
    {
        effectManager = this;
    }

    private void Start()
    {
        //    conditionSPACE += AddEffect(new FlatDamageAddEffect()).ApplyEffect;
        //    conditionSPACE += AddEffect(new FlatDamageAddEffect()).ApplyEffect;


        //    conditionSPACE += AddEffect(new IncreaseBulletSizeEffect()).ApplyEffect;

    }

    private void Update()
    {
        Debug.Log(effects.Count);
    }
    public IEffect AddEffect(IEffect effect, string condition)
    {
        SubscribeTo(condition, effect);

        effects.Add(effect);


        effects.Sort(delegate (IEffect e1, IEffect e2) 
        {
            if(e1 == null || e2 == null) return 0;

            else return e1.GetItemLevel().CompareTo(e2.GetItemLevel());

        });

        return effect;
    }

    public void OnShoot()
    {
        if (conditionShoot != null)
            conditionShoot();
    }
    public void OnRoomClear()
    {
        if (conditionRoomClear != null)
            conditionRoomClear();
    }
    public void SubscribeTo(string condition, IEffect effect)
    {
        switch (condition)
        {
            case "OnShoot":
                conditionShoot += effect.ApplyEffect;
                break;
            case "OnRoomClear":
                conditionRoomClear += effect.ApplyEffect;
                break;
            default:
                effect.ApplyEffect();
                break;
        }

    }

}


