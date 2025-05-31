using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameDataSO gameData;

    public TMP_Text academicPointsText;
    public TMP_Text socialPointsText;
    public TMP_Text dayText;


    void Start()
    {
        UpdatePointsUI();
    }

    public void UpdatePointsUI()
    {
        academicPointsText.text = "Academic Points: " + gameData.academicPoints.ToString(); 
        socialPointsText.text = "Social Points: " + gameData.socialPoints.ToString();
        dayText.text = "Day: " + gameData._gameDay.ToString();

    }
}

