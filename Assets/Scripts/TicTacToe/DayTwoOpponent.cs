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
    public Sprite xImage;
    public Sprite oImage;
    public int randRow;
    public int randCol;
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
            timer = 0;
        }
        

        for (int i = 1; i < 4; i++)
        {
            for (int j = 1; j < 4; j++)
            {
                if (ticTacToeGMScript.lastClickedButton == ticTacToeGMScript.buttonMap[i, j])
                {
                    row = i;
                    col = j;
                    ticTacToeGMScript.buttonMap[i, j] = null;
                }
            }
        }

        if (opponentMove)
        {
            // if the button is at the center

            randCol = Random.Range(-1, 2);
            randRow = Random.Range(-1, 2);
            if (ticTacToeGMScript.buttonMap[row + 1, col + 1] == null && 
                ticTacToeGMScript.buttonMap[row + 1, col + 0] == null &&
                ticTacToeGMScript.buttonMap[row + 0, col + 1] == null &&
                ticTacToeGMScript.buttonMap[row - 1, col + 0] == null &&
                ticTacToeGMScript.buttonMap[row - 1, col - 1] == null &&
                ticTacToeGMScript.buttonMap[row + 1, col - 1] == null &&
                ticTacToeGMScript.buttonMap[row - 1, col + 1] == null &&
                ticTacToeGMScript.buttonMap[row + 0, col - 1] == null)
            {
                button = (Button) ticTacToeGMScript.buttons[Random.Range(0, ticTacToeGMScript.buttons.Count - 1)];
                for (int k = 1; k < 4; k++)
                {
                    for (int l = 1; l < 4; l ++)
                    {
                        if (button == ticTacToeGMScript.buttonMap[k, l])
                        {
                            ticTacToeGMScript.buttonMap[k, l] = null;
                        }
                    }
                }
                selectSpace();
                opponentMove = false;
            }
            else if (row + randRow > 0 && row + randRow < 4 && col + randCol > 0 && col + randCol < 4 && ticTacToeGMScript.buttonMap[row + randRow, col + randCol] != null)
            {
                button = ticTacToeGMScript.buttonMap[row + randRow, col + randCol];
                opponentMove = false;
                ticTacToeGMScript.buttonMap[row + randRow, col + randCol] = null;
                selectSpace();
               
            }

        }
    }

    void selectSpace()
    {

        int index = ticTacToeGMScript.buttons.IndexOf(button);
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
            ticTacToeGMScript.instantiatedImages.Add(o);

            // Remove the randomly selected button from the ArrayList of buttons that do not have an X or O
            ticTacToeGMScript.buttons.RemoveAt(index);
            ticTacToeGMScript.playerTurn = true;
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
            ticTacToeGMScript.instantiatedImages.Add(x);

            // Remove the randomly selected button from the ArrayList of buttons that do not have an X or O
            ticTacToeGMScript.buttons.RemoveAt(index);
            ticTacToeGMScript.playerTurn = true;
        }
    }
}
