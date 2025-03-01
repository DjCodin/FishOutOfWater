using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

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
    public Dictionary<string, int> colors;
    public float time;
    public int score;
    public int percentCompleted;
    public bool gameHasEnded;
    public Dictionary<string, string []> meshColors;
    public int level;
    public Vector2 mouseDrag;
    
    public Button button1;

    public int iteration;
    public int max = 0;

    public string currentColor;


    // Start is called before the first frame update
    void Start()
    {

        level = 1;
        if (level == 3) { time = 60f; }
        score = 0;
        colors = new Dictionary<string, int>();
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
        if (level == 1)
        {
            max = 33;
        }
        else if (level == 2)
        {
            max = 67;
        
        }    

            
            
        if (level == 3)
        {
            max = 100;
            time -= Time.deltaTime;
            if (time < 0)
            {
                gameHasEnded = true;
            }
        }
        
        if (gameHasEnded || percentCompleted >= max)
        {
            endGame();
        }
    }
    public void addColorPairs()
    {
        colors.Add("White", 0);
        colors.Add("Red", 1);
        colors.Add("Orange", 2);
        colors.Add("Yellow", 3);
        colors.Add("Green", 4);
        colors.Add("Blue", 5);
        colors.Add("Purple", 6);
        colors.Add("Brown", 7);
        colors.Add("Black", 8);
        List<string> keys = new List<string>(colors.Keys);
    }
    public void endGame()
    {
        score = 200 * percentCompleted;
        iteration += 1;

    }
    public void colorCheck(string clicked)
    {

    }
       // Checks to see if the color can be changed and matches with the color required for said change. 


}