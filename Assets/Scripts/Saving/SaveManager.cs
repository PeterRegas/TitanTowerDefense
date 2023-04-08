using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public SaveState saveStats = null;
    private static SaveManager instance;
    private string savePath;
    [SerializeField] public GameObject [] towerTypes;
    private void Awake()
    {
        
        if(instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;

        }
        
        savePath = Application.persistentDataPath + "/saveData/";
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }
        Debug.Log("Saving to path: " + savePath);

        this.saveStats = new SaveState();
    }

    [ContextMenu("Save playerStats")]
    public void saveGameStats()
    {
        Debug.Log("Saving");
        saveStats.towerTypeList.Clear();
        saveStats.towerListPos.Clear();
        saveStats.towerListRot.Clear();
        saveStats.levelName = SceneManager.GetActiveScene().name;
        
        // save the data
        saveStats.towerList = GameObject.FindGameObjectsWithTag("Tower");
        //Debug.Log(saveStats.towerList.Length);
        for (int i = 0; i < saveStats.towerList.Length; i++)
        {
            //if towerList[i] is a tower
            if(saveStats.towerList[i].GetComponent<Tower>() != null){
                saveStats.towerTypeList.Add(0);
            }
            else if (saveStats.towerList[i].GetComponent<SpinTower>() != null){
                saveStats.towerTypeList.Add(1);
            }

            
            saveStats.towerListPos.Add(saveStats.towerList[i].transform.position);
            saveStats.towerListRot.Add(saveStats.towerList[i].transform.rotation);
        }
        JSONLoaderSaver.SaveAsJSON(savePath, "playerStats.json", this.saveStats);
    }

    [ContextMenu("Load playerStats")]
    public void loadGameStats()
    {
        Debug.Log("Loading");
        // load the data
        this.saveStats = JSONLoaderSaver.LoadFromJSON(savePath, "playerStats.json");

    }
}