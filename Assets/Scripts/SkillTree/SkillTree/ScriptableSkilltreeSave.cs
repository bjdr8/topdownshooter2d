using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillTreeData", menuName = "Game/Skill Tree Data")]
public class ScriptableSkilltreeSave : ScriptableObject
{
    [SerializeField] private List<string> unlockedSkills = new List<string>();
    [SerializeField] private List<string> activeSkills = new List<string>();

    public void Save()
    {
        string json = JsonUtility.ToJson(this);
        File.WriteAllText(GetSavePath(), json);
    }

    public List<string> Load()
    {
        string path = GetSavePath();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, this);
            return unlockedSkills;
        }
        else
        {
            return null;
        }
    }

    public void ResetSkills()
    {
        unlockedSkills.Clear();
        activeSkills.Clear();

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

    public void RemoveAllUnlockedSkills()
    {
        unlockedSkills.Clear();
    }

    public void AddActiveSkill(string skill)
    {
        activeSkills.Add(skill);
    }

    public void RemoveActiveSkill(string skill)
    {
        activeSkills.Remove(skill);
    }

    private string GetSavePath()
    {
        return Path.Combine(Application.persistentDataPath, "skilltree.json");
    }
}