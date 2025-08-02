using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public interface IEffect
{

    public void ApplyEffect()
    {

    }

    public string[] GetNameAndDesc();

    public string GetCondition();

}
