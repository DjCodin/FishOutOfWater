using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TempMainSceneScript : MonoBehaviour
{
    public Button ticTacToeButton;
    // Start is called before the first frame update
    void Start()
    {
        ticTacToeButton.onClick.AddListener(() => test(ticTacToeButton));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void test(Button btn)
    {
        SceneManager.LoadScene("TicTacToe");
    }
}
