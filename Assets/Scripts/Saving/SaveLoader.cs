using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoader : MonoBehaviour
{
    // Start is called before the first frame update
    private SaveState saveStats;
    private SaveManager saveManager;
    

    void Start()
    {
        
        saveManager = FindObjectOfType<SaveManager>();
        saveStats = saveManager.saveStats;
        if(saveStats.towerList != null){
            Debug.Log("Towerlist exists");
            for(int i = 0; i< saveStats.towerList.Length; i++){
                GameObject temp = Instantiate(saveManager.towerTypes[saveStats.towerTypeList[i]], saveStats.towerListPos[i], saveStats.towerListRot[i]);
                if (temp.GetComponent<Tower> () != null) {
                    temp.GetComponent<Tower> ().towerLevel = saveStats.towerLevelList[i];
                }
                else if(temp.GetComponent<SpinTower>() != null){
                    temp.GetComponent<SpinTower>().towerLevel = saveStats.towerLevelList[i];
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
