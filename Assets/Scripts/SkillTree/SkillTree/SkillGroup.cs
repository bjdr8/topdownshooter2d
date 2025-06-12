using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGroup : SkillLeaf
{
    public List<SkillLeaf> children = new List<SkillLeaf>();
    public PlayerProfile profile;
    public ScriptableSkilltreeSave skilltreeData;

    public SkillGroup(PlayerProfile profile, ScriptableSkilltreeSave skilltreeData)
    {
        this.profile = profile;
        this.profile.OnXpChanged += ImageChange;
        this.skilltreeData = skilltreeData;
    }

    public void AddSkillNode(SkillLeaf skillNode) => children.Add(skillNode);
    public override void Buy(PlayerProfile player)
    {
        if (unlocked == false)
        {
            Debug.Log("This skill is still locked");
            return;
        }

        if (xpCosts <= player.xp && bought == false)
        {
            Debug.Log("aquired skill");
            active = true;
            bought = true;
            skilltreeData.AddUnlockedSkill(skillName);
            foreach (var child in children)
            {
                child.unlocked = true;
            }
            player.RemoveXp(xpCosts);
        }
        else
        {
            Debug.Log("not enough xp to buy");
        }

        //if (bought) // yes thiss does work it makes it so u can deactive a skill but i need to make a function that checks if it is active if yes then deactivate if no then activate it
        //{
        //    skillTreeData.RemoveActiveSkill(skillName);
        //}
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
