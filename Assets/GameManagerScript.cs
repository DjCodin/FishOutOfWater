using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

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
    public GameObject x;
    public GameObject o;
    public bool instantiated = false;
    public bool isX = false;
    public bool isO = false;
    public GameObject board;
    public bool playerTurn;
    public bool gameOver = false;
    public ArrayList buttons;
    public bool buttonClicked = false;
    public float timer2 = 0;
    void Start()
    {
        infoText.text = "Your shape is:";
        timer = 0;

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

    }

    void Update()
    {

        foreach (Button btn in buttons)
        {
            btn.onClick.AddListener(() => HandleButtonClick(btn));
        }
        // Cutscene script 
        if (!cutsceneDone)
        {
            timer += Time.deltaTime;
            if (timer >= 2 && !instantiated)
            {
                timer = 0;
                int randInt = Random.Range(0, 1);
                if (randInt == 0)
                {
                    var copy = Instantiate(x, new Vector2(0, 0), Quaternion.identity);
                    Destroy(copy, 3.0f);
                    instantiated = true;
                    isX = true;
                    playerTurn = true;
                }
                else
                {
                    var copy = Instantiate(o, new Vector2(0, 0), Quaternion.identity);
                    Destroy(copy, 3.0f);
                    instantiated = true;
                    isO = true;
                    playerTurn = false;

                }
            }
            if (timer >= 3.0f)
            {
                cutsceneDone = true;
                infoText.text = "";
                infoText.gameObject.SetActive(false);
                board.SetActive(true);
            }
        }
        if (cutsceneDone)
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
        }

        if (isX && !gameOver)
        {
            if (playerTurn)
            {
                for (int i = 0; i < buttons.Count; i++)
                {
                    Button button = (Button)buttons[i];
                    button.interactable = true;
                }
            }
            if(!playerTurn)
            {
                for(int i = 0; i < buttons.Count; i++)
                {
                    Button button = (Button) buttons[i];
                    button.interactable = false;
                }
                opponentTurn();
            }
            checkForWin();
            
        }
    }

    void opponentTurn()
    {   
        timer2 += Time.deltaTime;
        if (timer2 >= 2f)
        {
            int randomSquare = Random.Range(0, buttons.Count);
            Button button = (Button) buttons[randomSquare];
            var text = button.GetComponentInChildren<TMP_Text>();
            int index = 0;
            if (isX)
            {
                text.text = "0";
                button.interactable = false;

                for (int i = 0; i < buttons.Count; i++)
                {
                    if (object.ReferenceEquals(buttons[i], button))
                    {
                        index = i;
                    }
                }
                buttons.RemoveAt(index);
                playerTurn = true;
            }
            else
            {
                text.text = "X";
                button.interactable = false;

                for (int i = 0; i < buttons.Count; i++)
                {
                    if (object.ReferenceEquals(buttons[i], button))
                    {
                        index = i;
                    }
                }
                buttons.RemoveAt(index);
                playerTurn = true;  
            }
            timer2 = 0;
        }  
    }

    void checkForWin()
    {

    }

    void HandleButtonClick(Button clickedButton)
    {
        // Make the clicked button not interactable
        if (buttons.Contains(clickedButton))
        {
            int index = 0;
            clickedButton.interactable = false;

            if (isO)
            {
                var buttonText = clickedButton.GetComponentInChildren<TMP_Text>();
                buttonText.text = "O";
            }
            else
            {
                var buttonText = clickedButton.GetComponentInChildren<TMP_Text>();
                buttonText.text = "X";
            }

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
