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
    
    //SaveState Constructor
    public SaveState(int roundNum, int creditNum, string levelName, GameObject [] towerList, List<int> towerTypeList, List<Vector3> towerListPos, List<Quaternion> towerListRot)
    {
        this.roundNum = roundNum;
        this.creditNum = creditNum;
        this.levelName = levelName;
        this.towerList = towerList;
        this.towerTypeList = towerTypeList;
        this.towerListPos = towerListPos;
        this.towerListRot = towerListRot;
    }

    


}