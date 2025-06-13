using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SkilltreeSave
{
    [SerializeField] private List<string> unlockedSkills = new List<string>();
    [SerializeField] private List<string> activeSkills = new List<string>();

    private PassiveEffect passiveEffect;
    public SkillManager skillManager;

    public SkilltreeSave(PassiveEffect passiveEffect)
    {
        this.passiveEffect = passiveEffect;
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(this);
        File.WriteAllText(GetSavePath(), json);
    }

    public void Load()
    {
        string path = GetSavePath();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, this);
            UpdatePassiveEffects();
            foreach (SkillLeaf skill in skillManager.skillsList)
            {
                for (int i = 0; i < unlockedSkills.Count; i++)
                {
                    if (skill.skillName == unlockedSkills[i])
                    {
                        skill.unlocked = true;
                        skill.active = true;
                        skill.bought = true;
                        skill.ImageChange();

                        if (skill is SkillGroup skillGroup)
                        {
                            foreach (var child in skillGroup.children)
                            {
                                child.unlocked = true;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("Save Not Found");
        }
    }

    public void ResetSkills()
    {
        unlockedSkills.Clear();
        activeSkills.Clear();
        UpdatePassiveEffects();
        foreach (SkillLeaf skill in skillManager.skillsList)
        {
            if (skill.skillName == "SkillTreeRoot")
            {
                skill.unlocked = true;
                skill.active = false;
                skill.bought = false;
            } else {
                skill.unlocked = false;
                skill.active = false;
                skill.bought = false;
            }
            skill.ImageChange();
        }

        string path = GetSavePath();
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        Save(); // Optional: save the cleared state
    }

    public void AddUnlockedSkill(string skill)
    {
        unlockedSkills.Add(skill);
        AddActiveSkill(skill);
    }

    public void RemoveUnlockedSkill(string skill)
    {
        unlockedSkills.Remove(skill);
    }

    public void AddActiveSkill(string skill)
    {
        activeSkills.Add(skill);
        UpdatePassiveEffects();
    }

    public void RemoveActiveSkill(string skill)
    {
        activeSkills.Remove(skill);
        UpdatePassiveEffects();
    }

    public void UpdatePassiveEffects()
    {
        passiveEffect.Init(activeSkills);
    }

    public void UpdateSkills()
    {

    }

    private string GetSavePath()
    {
        return Path.Combine(Application.persistentDataPath, "skilltree.json");
    }
}