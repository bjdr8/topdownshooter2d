using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEffect : ScriptableObject
{
    public string nameId;
    public abstract void ApplyEffect(PlayerControler player);
    public abstract void RevertEffect(PlayerControler player);
}
