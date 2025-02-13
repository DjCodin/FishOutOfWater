using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TempMainSceneScript : MonoBehaviour
{
    public Button ticTacToeButton;
    public Button mathClassButton;
    // Start is called before the first frame update
    void Start()
    {
        ticTacToeButton.onClick.AddListener(() => loadTicTacToe(ticTacToeButton));
        mathClassButton.onClick.AddListener(() => loadMathClass(mathClassButton));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void loadTicTacToe(Button btn)
    {
        SceneManager.LoadScene("TicTacToe");
    }

    void loadMathClass(Button btn)
    {
        SceneManager.LoadScene("MathClass");
    }
}
