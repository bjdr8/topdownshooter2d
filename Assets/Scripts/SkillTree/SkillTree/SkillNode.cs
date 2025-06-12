using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillNode : SkillLeaf
{
    public PlayerProfile profile;
    public ScriptableSkilltreeSave skilltreeData;
    public SkillNode(PlayerProfile profile, ScriptableSkilltreeSave skilltreeData)
    {
        this.profile = profile;
        this.profile.OnXpChanged += ImageChange;
        this.skilltreeData = skilltreeData;
    }
    public override void Buy(PlayerProfile player)
    {
        if (unlocked == false) 
        {
            Debug.Log("This skill is still locked");
            return; 
        }

        if (xpCosts <= player.xp)
        {
            Debug.Log("u bought the item");
            active = true;
            skilltreeData.AddUnlockedSkill(skillName);
            player.RemoveXp(xpCosts);
        }
        else
        {
            Debug.Log("not enough xp to buy");
        }
    }

    public override int Reset()
    {
        active = false;
        Debug.Log("u sold the item");
        return xpCosts;
    }

    public override void Unlock()
    {
        Debug.Log("item got unlocked now u can buy it with xp");
    }
    public void ImageChange()
    {

    }
}
