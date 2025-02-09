using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayTwoOpponent : MonoBehaviour
{
    public TicTacToeGameManagerScript ticTacToeGMScript;
    public float timer = 0;
    private int row;
    private int col;
    private bool opponentMove = false;
    private Button button;
    public Canvas canvas;
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

    public void dayTwoOpponentTurn()
    {
        timer += Time.deltaTime;
        if (timer >= 2f)
        {
            opponentMove = true;
        }
        timer = 0;

        for (int i = 1; i < ticTacToeGMScript.buttonMap.Length - 1; i++)
        {
            for (int j = 1; j < 4; j++)
            {
                if (ticTacToeGMScript.lastClickedButton == ticTacToeGMScript.buttonMap[i, j])
                {
                    row = i;
                    col = j;
                }
            }
        }

        if (opponentMove)
        {
            // if the button is at the center

            int randCol = Random.Range(-1, 1);
            int randRow = Random.Range(-1, 1);
            if (ticTacToeGMScript.buttonMap[row + 1, col + 1] == null && 
                ticTacToeGMScript.buttonMap[row + 1, col + 0] == null &&
                ticTacToeGMScript.buttonMap[row + 0, col + 1] == null &&
                ticTacToeGMScript.buttonMap[row - 1, col + 0] == null &&
                ticTacToeGMScript.buttonMap[row - 1, col - 1] == null &&
                ticTacToeGMScript.buttonMap[row + 1, col - 1] == null &&
                ticTacToeGMScript.buttonMap[row - 1, col + 1] == null)
            {
                button = (Button) ticTacToeGMScript.buttons[Random.Range(0, ticTacToeGMScript.buttons.Count - 1)];
                selectSpace();
                opponentMove = false;
                ticTacToeGMScript.playerTurn = true;
            }
            else if (ticTacToeGMScript.buttonMap[row + randRow, col + randCol] != null)
            {
                button = 0;
                opponentMove = false;
                selectSpace();
                ticTacToeGMScript.playerTurn = true;
            }

        }
    }

    void selectSpace()
    {
        var text = button.GetComponentInChildren<TMP_Text>();

        // Gets the RectTransform of the selected button
        RectTransform buttonRect = button.GetComponent<RectTransform>();

        // Declares a variable localPosition to store the resulting position in the local coordinate space of the canvas
        Vector2 localPosition;

        // Converts a point in screen space  to a local point relative to a specific UI element's RectTransform
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            // Specifies the rectangle (UI element) into which the screen position will be converted
            canvas.GetComponent<RectTransform>(),
            // Provides the position of the button in screen space
            buttonRect.position,
            // Specifies the camera responsible for rendering the canvas
            canvas.worldCamera,
            // Stores the calculated local position in the canvas's coordinate space
            out localPosition);

        // Runs if the player is X
        if (ticTacToeGMScript.isX)
        {
            // Text of the randomly selected button becomes "O"
            text.text = "O";
            // Make the button non-interactable
            button.interactable = false;


            // Instantiates a new GameObject "O" + the name of the randomly selected button's GameObject 
            GameObject o = new GameObject("O " + button.name);
            // Sets the parent to be the Canvas, and sets the transform to be based on the canvas
            o.transform.SetParent(canvas.transform, false);
            // Adds an Image component
            Image image = o.AddComponent<Image>();
            // Adds the O image to the GameObject 
            image.sprite = oImage;
            // Gets the RectTransform of the GameObject and sets the coordinates to be (0,0), and the size to be 200x200 pixels
            RectTransform rectTransform = o.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = localPosition;
            rectTransform.sizeDelta = new Vector2(150, 150);

            // Adds the O GameObject to the list of instantiated button GameObjects 
            instantiatedImages.Add(o);

            // Remove the randomly selected button from the ArrayList of buttons that do not have an X or O
            buttons.RemoveAt(randomSquare);
            playerTurn = true;
        }
        // Runs if the player is O
        else
        {
            // Text of the randomly selected button becomes "X" + the name of the randomly selected button's GameObject 
            text.text = "X";
            // Makes the button non-interactable
            button.interactable = false;

            // Instantiates a new GameObject "X"
            GameObject x = new GameObject("X " + button.name);
            // Sets the parent to be the Canvas, and sets the transform to be based on the canvas
            x.transform.SetParent(canvas.transform, false);
            // Adds an Image component
            Image image = x.AddComponent<Image>();
            // Adds the X image to the GameObject 
            image.sprite = xImage;
            // Gets the RectTransform of the GameObject and sets the coordinates to be (0,0), and the size to be 200x200 pixels
            RectTransform rectTransform = x.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = localPosition;
            rectTransform.sizeDelta = new Vector2(150, 150);

            // Adds the O GameObject to the list of instantiated button GameObjects 
            instantiatedImages.Add(x);

            // Remove the randomly selected button from the ArrayList of buttons that do not have an X or O
            buttons.RemoveAt(randomSquare);
            playerTurn = true;
        }
    }
}
