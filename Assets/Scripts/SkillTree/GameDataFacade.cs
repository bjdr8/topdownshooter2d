//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;
//public class GameDataFacade
//{
//    public ScriptableSkilltreeSave skillTreeData;

//    public GameDataFacade()
//    {
//        skillTreeData = ScriptableObject.CreateInstance<ScriptableSkilltreeSave>();
//    }

//    public void SaveAll()
//    {
//        skillTreeData.Save();
//        Debug.Log("Saving. . .");
//    }
//    public void LoadAll()
//    {
//        skillTreeData.Load();
//        Debug.Log("loading. . .");
//    }

//    public void OnDestroy()
//    {
//        skillTreeData.Save();
//    }

//    //public void 
//}