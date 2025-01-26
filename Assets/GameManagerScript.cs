using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;
    public Button button6;
    public Button button7;
    public Button button8;
    public Button button9;
    public TMP_Text infoText;
    public bool isPlayerTurn;
    public float timer;
    public bool cutsceneDone = false;
    public Sprite xImage;
    public Sprite oImage;
    public Canvas canvas;
    public bool instantiated = false;
    public bool isX = false;
    public bool isO = false;
    public GameObject board;
    public bool playerTurn;
    public bool gameOver = false;
    public ArrayList buttons;
    public bool buttonClicked = false;
    public float timer2 = 0;
    public string winner;
    public bool tie = false;
    private List<GameObject> instantiatedImages = new List<GameObject>();
    public GameObject winSound;
    public GameObject loseSound;
    public GameObject tieSound;
    public GameObject background;
    public bool endScene = false;
    public bool endSceneAudioPlayed = false;
    void Start()
    {
        // Background without tic tac toe board
        background = GameObject.FindGameObjectWithTag("Background");

        // Gets the 3 gameobjects where the sounds are stored
        winSound =  GameObject.FindGameObjectWithTag("Win");
        loseSound = GameObject.FindGameObjectWithTag("Lose");
        tieSound = GameObject.FindGameObjectWithTag("Tie");

        infoText.text = "Your shape is:";
        timer = 0;

        winner = "";

        // Hide the buttons for the opening cutscene
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);
        button4.gameObject.SetActive(false);
        button5.gameObject.SetActive(false);
        button6.gameObject.SetActive(false);
        button7.gameObject.SetActive(false);
        button8.gameObject.SetActive(false);
        button9.gameObject.SetActive(false);
        board = GameObject.FindGameObjectWithTag("Board");
        // Hide the buttons
        board.SetActive(false);

        // Create arraylist with all the buttons
        buttons = new ArrayList();
        buttons.Add(button1);
        buttons.Add(button2);
        buttons.Add(button3);
        buttons.Add(button4);
        buttons.Add(button5);
        buttons.Add(button6);
        buttons.Add(button7);
        buttons.Add(button8);
        buttons.Add(button9);

        // Adds a listener to each button (waits for an event to occur and then runs a script accordingly)
        foreach (Button btn in buttons)
        {
            btn.onClick.AddListener(() => HandleButtonClick(btn));
        }
    }

    void Update()
    {

        // Cutscene script 
        if (!cutsceneDone)
        {
            timer += Time.deltaTime;
            // Waits for timer to be at least 2 seconds before showing the player whether they are X or O
            if (timer >= 2 && !instantiated)
            {
                timer = 0;

                // Generates a random integer  from 0 (inclusive) to 2 (exclusive). 0 = X, 1 = O
                int randInt = Random.Range(0, 2);
                if (randInt == 0)
                {
                    // Instantiates a new GameObject "X"
                    GameObject x = new GameObject("X");
                    // Sets the parent to be the Canvas, and sets the transform to be based on the canvas
                    x.transform.SetParent(canvas.transform, false);
                    // Adds an Image component
                    Image image = x.AddComponent<Image>();
                    // Adds the X image to the GameObject
                    image.sprite = xImage;
                    // Gets the RectTransform of the GameObject and sets the coordinates to be (0,0), and the size to be 200x200 pixels
                    RectTransform rectTransform = x.GetComponent<RectTransform>();
                    rectTransform.anchoredPosition = new Vector2(0,0);
                    rectTransform.sizeDelta = new Vector2(200, 200);

                    // After 3 seconds, destroys the "X" GameObject and sets the appropriate conditions for the game
                    Destroy(x, 3.0f);
                    instantiated = true;
                    isX = true;
                    playerTurn = true;
                }
                else
                {
                    // Instantiates a new GameObject "O"
                    GameObject o = new GameObject("O");
                    // Sets the parent to be the Canvas, and sets the transform to be based on the canvas
                    o.transform.SetParent(canvas.transform, false);
                    // Adds an Image component
                    Image image = o.AddComponent<Image>();
                    // Adds the O image to the GameObject
                    image.sprite = oImage;
                    // Gets the RectTransform of the GameObject and sets the coordinates to be (0,0), and the size to be 200x200 pixels
                    RectTransform rectTransform = o.GetComponent<RectTransform>();
                    rectTransform.anchoredPosition = new Vector2(0, 0);
                    rectTransform.sizeDelta = new Vector2(200, 200);

                    // After 3 seconds, destroys the "O" GameObject and sets the appropriate conditions for the game
                    Destroy(o, 3.0f);
                    instantiated = true;
                    isO = true;
                    playerTurn = false;

                }
            }
            if (timer >= 3.0f)
            {

                // Once the timer exceeds 3 seconds, starts the game and changes the backdrop to the tic tac toe board
                cutsceneDone = true;
                infoText.text = "";
                infoText.gameObject.SetActive(false);
                board.SetActive(true);
            }
        }
        if (cutsceneDone && !gameOver)
        {
            // Show the buttons after the cutscene finishes
            button1.gameObject.SetActive(true);
            button2.gameObject.SetActive(true);
            button3.gameObject.SetActive(true);
            button4.gameObject.SetActive(true);
            button5.gameObject.SetActive(true);
            button6.gameObject.SetActive(true);
            button7.gameObject.SetActive(true);
            button8.gameObject.SetActive(true);
            button9.gameObject.SetActive(true);
            background.SetActive(false);
        }

        // Runs while the game is in progress
        if (!gameOver && cutsceneDone)
        {
            // Runs if it is the player's turn
            if (playerTurn)
            {
                // Makes all buttons that aren't occupied interactable
                for (int i = 0; i < buttons.Count; i++)
                {
                    Button button = (Button)buttons[i];
                    button.interactable = true;
                }
            }
            if(!playerTurn)
            {
                // Makes all buttons that aren't occupied not interactable
                for(int i = 0; i < buttons.Count; i++)
                {
                    Button button = (Button) buttons[i];
                    button.interactable = false;
                }
                // Calls the day 1 opponent 
                dayOneOpponentTurn();
            }

            // After each turn check for a win
            checkForWin();
            
        }
        // Runs once the game is over
        if (gameOver)
        {
            // Removes all the buttons from the scene
            button1.gameObject.SetActive(false);
            button2.gameObject.SetActive(false);
            button3.gameObject.SetActive(false);
            button4.gameObject.SetActive(false);
            button5.gameObject.SetActive(false);
            button6.gameObject.SetActive(false);
            button7.gameObject.SetActive(false);
            button8.gameObject.SetActive(false);
            button9.gameObject.SetActive(false);
            infoText.gameObject.SetActive(true);

            // If the winner is X and the end scene is plapying this runs
            if(winner == "X" && endScene)
            {
                // If the player is X, plays a winning sound
                if (isX && !endSceneAudioPlayed)
                {
                    winSound.GetComponent<AudioSource>().Play();
                    endSceneAudioPlayed = true;
                }
                // If the player is O, plays a losing sound
                else if (!endSceneAudioPlayed)
                {
                    loseSound.GetComponent<AudioSource>().Play();
                    endSceneAudioPlayed = true;
                }
                timer += Time.deltaTime;
                // Destroys all instantiated X and O objects once the timer is at least 4 seconds. Changes the background to the 
                // board without the tic tac toe 
                if (timer >= 4f)
                {
                    timer = 0;
                    board.SetActive(false);
                    background.SetActive(true);
                    infoText.text = "X won";
                    for (int i = 0; i < instantiatedImages.Count; i++)
                    {
                        Destroy(instantiatedImages[i]);
                    }
                    endScene = false; 
                }
            }
            // If it is a tie and the end scene is plapying this runs
            else if (tie && endScene)
            {
                timer += Time.deltaTime;
                // Plays a sound for a tie 
                if (!endSceneAudioPlayed)
                {
                    tieSound.GetComponent<AudioSource>().Play();
                    endSceneAudioPlayed = true;
                }
                // Destroys all instantiated X and O objects once the timer is at least 4 seconds. Changes the background to the 
                // board without the tic tac toe 
                if (timer >= 4f)
                {
                    timer = 0;
                    board.SetActive(false);
                    infoText.text = "It was a tie";
                    background.SetActive(true);
                    for (int i = 0; i < instantiatedImages.Count; i++)
                    {
                        Destroy(instantiatedImages[i]);
                    }
                    endScene = false;
                }
            }
            // If O wins and the end scene is plapying this runs
            else if (winner == "O" && endScene)
            {
                timer += Time.deltaTime;
                // If the player is O, plays a winning sound
                if (isO && !endSceneAudioPlayed)
                {
                    winSound.GetComponent<AudioSource>().Play();
                    endSceneAudioPlayed = true;
                }
                // If the player is X, plays a losing sound
                else if (!endSceneAudioPlayed)
                {
                    loseSound.GetComponent<AudioSource>().Play();
                    endSceneAudioPlayed = true;
                }
                // Destroys all instantiated X and O objects once the timer is at least 4 seconds. Changes the background to the 
                // board without the tic tac toe 
                if (timer >= 4f)
                {
                    timer = 0;
                    board.SetActive(false);
                    background.SetActive(true);
                    infoText.text = "O won";
                    for (int i = 0; i < instantiatedImages.Count; i++)
                    {
                        Destroy(instantiatedImages[i]);
                    }
                    endScene = false;
                }
            }
        }
    }

    // Function for the opponent behavior
    void dayOneOpponentTurn()
    {   
        timer2 += Time.deltaTime;
        // Waits 2 seconds from player turn to run 
        if (timer2 >= 2f)
        {
            // Picks a random unoccupied square to put the X or O on
            int randomSquare = Random.Range(0, buttons.Count);
            // Stores the random square in a local Button variable 
            Button button = (Button) buttons[randomSquare];
            // Stores the TMP_Text component in the button's child in a var
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
            if (isX)
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
            timer2 = 0;
        }  
    }

    // Win checking method. The invisible buttons have text on them once they are occupied, either and X or O
    void checkForWin()
    {
        // Instantiate the text components of each button 
        TMP_Text button1Text = button1.GetComponentInChildren<TMP_Text>();
        TMP_Text button2Text = button2.GetComponentInChildren<TMP_Text>();
        TMP_Text button3Text = button3.GetComponentInChildren<TMP_Text>();
        TMP_Text button4Text = button4.GetComponentInChildren<TMP_Text>();
        TMP_Text button5Text = button5.GetComponentInChildren<TMP_Text>();
        TMP_Text button6Text = button6.GetComponentInChildren<TMP_Text>();
        TMP_Text button7Text = button7.GetComponentInChildren<TMP_Text>();
        TMP_Text button8Text = button8.GetComponentInChildren<TMP_Text>();
        TMP_Text button9Text = button9.GetComponentInChildren<TMP_Text>();

        // All X or O on the first row
        if((button1Text.text == "X" || button1Text.text == "O") && button1Text.text == button2Text.text && button2Text.text == button3Text.text)
        {
            winner = button1Text.text;
            gameOver = true;
            endScene = true;
            timer = 0;
        }
        // All X or O on the second row
        if ((button4Text.text == "X" || button4Text.text == "O") && button4Text.text == button5Text.text && button5Text.text == button6Text.text)
        {
            winner = button4Text.text;
            gameOver = true;
            endScene = true;
            timer = 0;
        }
        // All X or O on the third row
        if ((button7Text.text == "X" || button7Text.text == "O") && button7Text.text == button8Text.text && button8Text.text == button9Text.text)
        {
            winner = button7Text.text;
            gameOver = true;
            endScene = true;
            timer = 0;
        }
        // All X or O on the first column
        if ((button1Text.text == "X" || button1Text.text == "O") && button1Text.text == button4Text.text && button4Text.text == button7Text.text)
        {
            winner = button1Text.text;
            gameOver = true;
            endScene = true;
            timer = 0;
        }
        // All X or O on the second column
        if ((button2Text.text == "X" || button2Text.text == "O") && button2Text.text == button5Text.text && button5Text.text == button8Text.text)
        {
            winner = button2Text.text;
            gameOver = true;
            endScene = true;
            timer = 0;
        }
        // All X or O on the third column
        if ((button3Text.text == "X" || button3Text.text == "O") && button3Text.text == button6Text.text && button6Text.text == button9Text.text)
        {
            winner = button3Text.text;
            gameOver = true;
            endScene = true;
            timer = 0;
        }
        // All X or O diagonally down from left to right
        if ((button1Text.text == "X" || button1Text.text == "O") && button1Text.text == button5Text.text && button5Text.text == button9Text.text)
        {
            winner = button3Text.text;
            gameOver = true;
            endScene = true;
            timer = 0;
        }
        // All X or O diagonally up from left to right
        if ((button7Text.text == "X" || button7Text.text == "O") && button7Text.text == button5Text.text && button5Text.text == button3Text.text)
        {
            winner = button7Text.text;
            gameOver = true;
            endScene = true;
            timer = 0;
        }
        if(buttons.Count == 0)
        {
            gameOver = true;
            tie = true;
            endScene = true;
            timer = 0;
        }
    }

    // Function that runs once the player selects an open spot 
    void HandleButtonClick(Button clickedButton)
    {
        // Make the clicked button not interactable 
        if (buttons.Contains(clickedButton))
        {
            // Intiailizes an local int variable to get the index of the clicked button
            int index = 0;
            clickedButton.interactable = false;

            // Gets the RectTransform of the selected button
            RectTransform buttonRect = clickedButton.GetComponent<RectTransform>();

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
            if (isX)
            {
                var buttonText = clickedButton.GetComponentInChildren<TMP_Text>();
                buttonText.text = "X";

                GameObject x = new GameObject("X " + clickedButton.name);
                x.transform.SetParent(canvas.transform, false);
                Image image = x.AddComponent<Image>();
                image.sprite = xImage;
                RectTransform rectTransform = x.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = localPosition ;
                rectTransform.sizeDelta = new Vector2(150, 150);

                instantiatedImages.Add(x);
            }
            // Runs if player is O
            else
            {
                var buttonText = clickedButton.GetComponentInChildren<TMP_Text>();
                buttonText.text = "O";

                GameObject o = new GameObject("O " + clickedButton.name);
                o.transform.SetParent(canvas.transform, false);
                Image image = o.AddComponent<Image>();
                image.sprite = oImage;
                RectTransform rectTransform = o.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = localPosition;
                rectTransform.sizeDelta = new Vector2(150, 150);

                instantiatedImages.Add(o);

            }
            
            // Finds the button in buttons that is the clicked button and then removes that button
            for(int i  = 0; i < buttons.Count; i++)
            {
                if (object.ReferenceEquals(buttons[i], clickedButton))
                {
                    index = i;
                }
            }
            playerTurn = false;
            buttons.RemoveAt(index);
        }
        
    }

}
