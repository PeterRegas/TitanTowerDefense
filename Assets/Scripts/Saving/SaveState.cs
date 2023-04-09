using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveState
{
    public int roundNum;
    public int creditNum;
    public string levelName;
    
    public GameObject [] towerList;
    public List<Vector3> towerListPos;
    public List<int> towerTypeList;
    public List<Quaternion> towerListRot;

   
    
    public SaveState()
    {
        roundNum = 1;
        creditNum = 100;
        towerList = null;
        levelName = "";
        towerTypeList = new List<int>();
        towerListPos = new List<Vector3>(); 
        towerListRot = new List<Quaternion>();
    }
    
    // public SaveState(int roundNum, int creditNum, Vector3 [] towerListPos, Quaternion [] towerListRot)
    // {
    //     // this.health = health;
    //     // this.score = score;
    //     // this.level = level;
    //     // this.experience = experience;
    // }

    


}
