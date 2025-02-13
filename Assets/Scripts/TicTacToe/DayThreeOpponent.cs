using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayThreeOpponent : MonoBehaviour
{
    public TicTacToeGameManagerScript ticTacToeGMScript;
    // Start is called before the first frame update
    void Start()
    {
        GameObject tttGMGameObject = GameObject.FindGameObjectWithTag("Tic Tac Toe Game Manager");
        ticTacToeGMScript = tttGMGameObject.GetComponent<TicTacToeGameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
