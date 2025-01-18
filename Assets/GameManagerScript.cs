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
    void Start()
    {
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
            if (timer >= 2 && !instantiated)
            {
                timer = 0;
                int randInt = Random.Range(0, 2);
                if (randInt == 0)
                {
                    GameObject x = new GameObject("X");
                    x.transform.SetParent(canvas.transform, false);
                    Image image = x.AddComponent<Image>();
                    image.sprite = xImage;
                    RectTransform rectTransform = x.GetComponent<RectTransform>();
                    rectTransform.anchoredPosition = new Vector2(0,0);
                    rectTransform.sizeDelta = new Vector2(200, 200);

                    Destroy(x, 3.0f);
                    instantiated = true;
                    isX = true;
                    playerTurn = true;
                }
                else
                {
                    GameObject o = new GameObject("O");
                    o.transform.SetParent(canvas.transform, false);
                    Image image = o.AddComponent<Image>();
                    image.sprite = oImage;
                    RectTransform rectTransform = o.GetComponent<RectTransform>();
                    rectTransform.anchoredPosition = new Vector2(0, 0);
                    rectTransform.sizeDelta = new Vector2(200, 200);

                    Destroy(o, 3.0f);
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

        if (!gameOver && cutsceneDone)
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
                dayOneOpponentTurn();
            }
            checkForWin();
            
        }

        if (gameOver)
        {
            button1.gameObject.SetActive(false);
            button2.gameObject.SetActive(false);
            button3.gameObject.SetActive(false);
            button4.gameObject.SetActive(false);
            button5.gameObject.SetActive(false);
            button6.gameObject.SetActive(false);
            button7.gameObject.SetActive(false);
            button8.gameObject.SetActive(false);
            button9.gameObject.SetActive(false);
            board.SetActive(false);
            infoText.gameObject.SetActive(true);

            if(winner == "X")
            {
                infoText.text = "X won";
                for (int i = 0; i < instantiatedImages.Count; i++)
                {
                    Destroy(instantiatedImages[i]);
                }
            }
            else if (tie)
            {
                infoText.text = "It was a tie";
            }
            else
            {
                infoText.text = "O won";
                for (int i = 0; i < instantiatedImages.Count; i++)
                {
                    Destroy(instantiatedImages[i]);
                }
            }
        }
    }

    void dayOneOpponentTurn()
    {   
        timer2 += Time.deltaTime;
        if (timer2 >= 2f)
        {
            int randomSquare = Random.Range(0, buttons.Count);
            Button button = (Button) buttons[randomSquare];
            var text = button.GetComponentInChildren<TMP_Text>();
            int index = 0;

            RectTransform buttonRect = button.GetComponent<RectTransform>();

            Vector2 localPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.GetComponent<RectTransform>(),
                buttonRect.position,
                canvas.worldCamera,
                out localPosition);

            if (isX)
            {
                text.text = "O";
                button.interactable = false;

                GameObject o = new GameObject("O " + button.name);
                o.transform.SetParent(canvas.transform, false);
                Image image = o.AddComponent<Image>();
                image.sprite = oImage;
                RectTransform rectTransform = o.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = localPosition;
                rectTransform.sizeDelta = new Vector2(150, 150);

                instantiatedImages.Add(o);

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

                GameObject x = new GameObject("X " + button.name);
                x.transform.SetParent(canvas.transform, false);
                Image image = x.AddComponent<Image>();
                image.sprite = xImage;
                RectTransform rectTransform = x.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = localPosition;
                rectTransform.sizeDelta = new Vector2(150, 150);

                instantiatedImages.Add(x);

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
        }
        // All X or O on the second row
        if ((button4Text.text == "X" || button4Text.text == "O") && button4Text.text == button5Text.text && button5Text.text == button6Text.text)
        {
            winner = button4Text.text;
            gameOver = true;
        }
        // All X or O on the third row
        if ((button7Text.text == "X" || button7Text.text == "O") && button7Text.text == button8Text.text && button8Text.text == button9Text.text)
        {
            winner = button7Text.text;
            gameOver = true;
        }
        // All X or O on the first column
        if ((button1Text.text == "X" || button1Text.text == "O") && button1Text.text == button4Text.text && button4Text.text == button7Text.text)
        {
            winner = button1Text.text;
            gameOver = true;
        }
        // All X or O on the second column
        if ((button2Text.text == "X" || button2Text.text == "O") && button2Text.text == button5Text.text && button5Text.text == button8Text.text)
        {
            winner = button2Text.text;
            gameOver = true;
        }
        // All X or O on the third column
        if ((button3Text.text == "X" || button3Text.text == "O") && button3Text.text == button6Text.text && button6Text.text == button9Text.text)
        {
            winner = button3Text.text;
            gameOver = true;
        }
        // All X or O diagonally down from left to right
        if ((button1Text.text == "X" || button1Text.text == "O") && button1Text.text == button5Text.text && button5Text.text == button9Text.text)
        {
            winner = button3Text.text;
            gameOver = true;
        }
        // All X or O diagonally up from left to right
        if ((button7Text.text == "X" || button7Text.text == "O") && button7Text.text == button5Text.text && button5Text.text == button3Text.text)
        {
            winner = button7Text.text;
            gameOver = true;
        }
        if(buttons.Count == 0)
        {
            gameOver = true;
            tie = true;
        }
    }

    void HandleButtonClick(Button clickedButton)
    {
        // Make the clicked button not interactable
        if (buttons.Contains(clickedButton))
        {
            int index = 0;
            clickedButton.interactable = false;

            RectTransform buttonRect = clickedButton.GetComponent<RectTransform>();

            Vector2 localPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.GetComponent<RectTransform>(),
                buttonRect.position,
                canvas.worldCamera,
                out localPosition);

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
