using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameDataSO : ScriptableObject
{
    [SerializeField]
    private int _gameDay;

    public int GameDay
    {
        get { return _gameDay; }
        set { _gameDay = value; }
    }

}
