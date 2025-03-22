using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameDataSO : ScriptableObject
{
    [SerializeField]
    private int _gameDay;

    private int academicScore;
    private int socialScore;





    public int GameDay
    {
        get { return _gameDay; }
        set { _gameDay = value; }
    }


    public int AcademicScores
    {
        get { return academicScore; }
        set { academicScore = value; }
    }

    public int SocialScores
    {
        get { return socialScore;  }
        set { socialScore = value; }
    }
}
