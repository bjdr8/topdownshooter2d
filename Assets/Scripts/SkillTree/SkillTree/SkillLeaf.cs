using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

public abstract class SkillLeaf
{
    public string skillName;
    public int xpCosts;
    public bool unlocked;
    public bool bought;
    public bool active;
    public Image image;
    public PlayerProfile profile;
    public SkilltreeSave skilltreeData;
    public List<BaseEffect> effects;

    public abstract void Unlock();

    public abstract void Buy(PlayerProfile player);

    public abstract int Reset();
    public virtual void ImageChange() 
    {
        if (bought == true)
        {
            image.color = Color.blue;
        }
        else if (profile.xp >= xpCosts && unlocked == true)
        {
            image.color = Color.green;
        }
        else
        {
            image.color = Color.red;
        }
    }
}
