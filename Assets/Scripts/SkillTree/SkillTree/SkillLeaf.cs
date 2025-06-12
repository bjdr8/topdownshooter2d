using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillLeaf
{
    public string skillName;
    public int xpCosts;
    public bool unlocked;
    public bool bought;
    public bool active;

    public abstract void Unlock();

    public abstract void Buy(PlayerProfile player);

    public abstract int Reset();
}
