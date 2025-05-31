using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class nextDay : MonoBehaviour
{
    public GameDataSO gameData;
    public Button nextDayButton; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameData._gameDay == 3){
            nextDayButton.interactable = false;
        }
        
    }

    public void nextDayMath(){
        SceneManager.LoadScene("MathClass");
        gameData.nextDay();
    }
}
