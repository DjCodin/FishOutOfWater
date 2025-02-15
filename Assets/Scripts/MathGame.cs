using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;



public class MathGame : MonoBehaviour
{
    public GameObject questionObject;
    public GameObject timerObject;
    public GameObject scoreObject;
    public GameObject gameScreen;
    public GameObject mainGameScreen;
    public GameObject[] answerObjects;
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public Button goBackButton;
    public Button mainMenuButton;
    private Button[] answerButtons;

    private TextMeshProUGUI questionText;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI timerText;

    
    private int correctAnswer;
    private int currentLevel = 1;
    public int score;
    public int totalQuestions;
    private float timeRemaining = 60f;
    private bool gameActive = false;
    private bool level1Completed = false;
    private bool level2Completed = false;
    private bool level3Completed = false;


    void Start()
    {
        scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
        questionText = questionObject?.GetComponent<TextMeshProUGUI>();
        timerText = timerObject?.GetComponent<TextMeshProUGUI>();
        
        answerButtons = new Button[answerObjects.Length];
        for (int i = 0; i < answerObjects.Length; i++)
        {
            answerButtons[i] = answerObjects[i]?.GetComponent<Button>();
        }


        level1Button.onClick.AddListener(() => StartLevel(1));
        goBackButton.onClick.AddListener(() => {
            gameScreen.SetActive(false);
            mainGameScreen.SetActive(true);

            if(level1Completed){
                level2Button.interactable = true;
                level2Button.onClick.AddListener(() => {  if (level1Completed) StartLevel(2); });
                
            }
            if(level2Completed){
                level3Button.interactable = true;
                level3Button.onClick.AddListener(() => { if (level2Completed) StartLevel(3); });
            }

            if(level1Completed && level2Completed && level3Completed){
                mainMenuButton.interactable = true;
            }
            }
            );
        level2Button.onClick.AddListener(() => {  if (level1Completed) StartLevel(2); });
        level3Button.onClick.AddListener(() => { if (level2Completed) StartLevel(3); });
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("MainScene");
        }
        
        if (gameActive)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Max(0, Mathf.CeilToInt(timeRemaining)).ToString();
            
            if (timeRemaining <= 0)
            {
                gameActive = false;
                GameOver();
            }
        }
    }


    void StartLevel(int level)
    {
        currentLevel = level;
        score = 0;
        totalQuestions = 0;
        timeRemaining = 15f;
        gameActive = true;


        // Reset UI text
        scoreText.text = "";
        questionText.text = "Get ready!";


        // Clear answer button text
        foreach (Button button in answerButtons)
        {
            button.onClick.RemoveAllListeners();
            button.GetComponentInChildren<TextMeshProUGUI>().text = "";
            button.gameObject.SetActive(true); // Make sure buttons are visible
        }

        ChangeBgColor(level);
        GenerateQuestion();
}


    void ChangeBgColor(int level)
    {
        if (gameScreen != null)
        {
            Image bgImage = gameScreen.GetComponent<Image>();
            if (bgImage != null)
            {
                switch (level)
                {
                    case 1:
                        bgImage.color = new Color(0.5f, 1f, 0.5f, 1f); // Light Green
                        break;
                    case 2:
                        bgImage.color = new Color(1f, 1f, 0.5f, 1f); // Light Yellow
                        break;
                    case 3:
                        bgImage.color = new Color(1f, 0.5f, 0.5f, 1f); // Light Red
                        break;
                }
            }
        }
    }

    void GenerateQuestion()
    {
        int num1, num2;
        char[] operators = { '+', '-', '*', '/' };
        char selectedOperator = operators[Random.Range(0, operators.Length)];


        switch (currentLevel)
        {
            case 1:
                num1 = Random.Range(1, 10);
                num2 = Random.Range(1, 10);
                break;
            case 2:
                num1 = Random.Range(10, 50);
                num2 = Random.Range(10, 50);
                break;
            case 3:
                num1 = Random.Range(50, 100);
                num2 = Random.Range(50, 100);
                break;
            default:
                num1 = 1; num2 = 1;
                break;
        }


        if (selectedOperator == '/')
        {
            num2 = Random.Range(1, num1); 
            num1 = num2 * Random.Range(1, 10); 
        }


        correctAnswer = selectedOperator switch
        {
            '+' => num1 + num2,
            '-' => num1 - num2,
            '*' => num1 * num2,
            '/' => num1 / num2,
            _ => num1 + num2
        };


        questionText.text = num1 + " " + selectedOperator + " " + num2 + " = ?";
        totalQuestions++;


        int correctPosition = Random.Range(0, answerButtons.Length);
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int answer = (i == correctPosition) ? correctAnswer : Random.Range(correctAnswer - 10, correctAnswer + 10);
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answer.ToString();
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => AnswerSelected(answer));
        }
    }


    void AnswerSelected(int selectedAnswer)
    {
        if (selectedAnswer == correctAnswer)
        {
            score++;
        }
        GenerateQuestion();
    }


    void GameOver()
    {
        questionText.text = "Time's up!";
        scoreText.text = "Score: " + score + "/" + totalQuestions;
        if (currentLevel == 1 && score >= 5) level1Completed = true;
        if (currentLevel == 2 && score >= 5) level2Completed = true;
        if (currentLevel == 3 && score >= 5) level3Completed = true;
        foreach (Button button in answerButtons)
        {
            button.gameObject.SetActive(false);
        }
    }
}
