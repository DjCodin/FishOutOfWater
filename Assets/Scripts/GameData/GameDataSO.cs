using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData")]
public class GameDataSO : ScriptableObject
{
    [SerializeField]
    private int _gameDay;

    public int academicPoints;
    public int socialPoints;
    public static List<string> shellJournalContent = new List<string>();

    public int GameDay
    {
        get { return _gameDay; }
        set { _gameDay = value; }
    }

    public void AddAcademicPoints(int amount)
    {
        academicPoints += amount;
    }

    public void AddSocialPoints(int amount)
    {
        socialPoints += amount;
    }

    public void ResetPoints()
    {
        academicPoints = 0;
        socialPoints = 0;
    }
}
