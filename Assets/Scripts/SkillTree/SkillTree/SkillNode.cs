using UnityEngine;

public class SkillNode : SkillLeaf
{
    public SkillNode(PlayerProfile profile, SkilltreeSave skilltreeData)
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

        if (xpCosts <= player.xp && bought == false)
        {
            Debug.Log("aquired skill");
            active = true;
            bought = true;
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
    public override void ImageChange()
    {
        base.ImageChange();
    }
}
