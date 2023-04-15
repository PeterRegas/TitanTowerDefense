using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControls : MonoBehaviour
{
    public int roundNum;
    public int Money;
    public int Lives;
    // Start is called before the first frame update
    void Start()
    {
        Money = 0;;
        Lives = 100;
    }

    // Update is called once per frame
    void Update()
    {
        roundNum = GetComponent<EnemySpawn>().round;
    }

}
