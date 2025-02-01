using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GMScript : MonoBehaviour
{

    public Button whiteButton;
    public Button redButton;
    public Button orangeButton;
    public Button yellowButton;
    public Button greenButton;
    public Button blueButton;
    public Button purpleButton;
    public Button brownButton;
    public Button blackButton;
    public Button eraserButton;
    public Dictionary<int, string> colors;
    public float time;
    public int score;
    public int percentCompleted;
    public bool gameHasEnded;
    public Dictionary<string, string []> meshColors;
    public int level;
    public Vector2 mouseDrag;

    public Button button1;


    // Start is called before the first frame update
    void Start()
    {

        level = 1;
        if (level == 3) { time = 60f; }
        score = 0;
        colors = new Dictionary<int, string>();
        meshColors = new Dictionary<string, string []> ();
        addColorPairs();
        percentCompleted = 0;
        gameHasEnded = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameHasEnded)
        {



            whiteButton.interactable = true;
            redButton.interactable = true;
            orangeButton.interactable = true;
            yellowButton.interactable = true;
            greenButton.interactable = true;
            blueButton.interactable = true;
            purpleButton.interactable = true;
            brownButton.interactable = true;
            blackButton.interactable = true;
            eraserButton.interactable = true;
        }

        if (level == 3)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                gameHasEnded = true;
            }
        }
        
        if (gameHasEnded)
        {
            endGame();
        }
    }
    public void addColorPairs()
    {
        colors.Add(0, "White");
        colors.Add(1, "Red");
        colors.Add(2, "Orange");
        colors.Add(3, "Yellow");
        colors.Add(4, "Green");
        colors.Add(5, "Blue");
        colors.Add(6, "Purple");
        colors.Add(7, "Brown");
        colors.Add(8, "Black");
    }
    public void endGame()
    {
        score = 200 * percentCompleted;

    }
}