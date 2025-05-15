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
    private int wrongAnswers = 0;
    public GameDataSO gameData;
	public UIManager uiManager;
    public DialogueTrigger mathGameDone;

    private TextMeshProUGUI questionText;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI timerText;

    
    private int correctAnswer;
    private int currentLevel = 1;
    public int score;
    public int totalQuestions;
    private float timeRemaining = 60f;
    private bool gameActive = false;
    private bool level1Completed = true;
    private bool level2Completed = true;
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
        wrongAnswers = 0;
        currentLevel = level;
        score = 0;
        totalQuestions = 0;

        if(level == 1){
            timeRemaining = 30f;
        }
        if(level == 2){
            timeRemaining = 50f;
        }
        if(level == 3){
            timeRemaining = 60f;
        }
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
        int num1, num2, x;
        char[] operators = { '+', '-', '*' };
        char selectedOperator = operators[Random.Range(0, operators.Length)];
        bool isSolveForX = Random.value > 0.5f; // 50% chance to ask for x

        switch (currentLevel)
        {
            case 1:
                num1 = Random.Range(1, 10);
                num2 = Random.Range(1, 10);
                break;
            case 2:
                num1 = Random.Range(10, 20);
                num2 = Random.Range(10, 20);
                break;
            case 3:
                num1 = Random.Range(1, 20);
                num2 = Random.Range(1, 20);
                break;
            default:
                num1 = 1; num2 = 1;
                break;
        }

        if (isSolveForX)
        {
            x = num1; // Let x be the first number
            correctAnswer = x;

            switch (selectedOperator)
            {
                case '+':
                    questionText.text = $"x + {num2} = {x + num2}";
                    break;
                case '-':
                    questionText.text = $"x - {num2} = {x - num2}";
                    break;
                case '*':
                    questionText.text = $"x * {num2} = {x * num2}";
                    break;
            }
        }
        else
        {
            correctAnswer = selectedOperator switch
            {
                '+' => num1 + num2,
                '-' => num1 - num2,
                '*' => num1 * num2,
                _ => num1 + num2
            };

            questionText.text = $"{num1} {selectedOperator} {num2} = ?";
        }

        totalQuestions++;

        HashSet<int> answerChoices = new HashSet<int> { correctAnswer };

        while (answerChoices.Count < answerButtons.Length)
        {
            int randomAnswer = Random.Range(correctAnswer - 10, correctAnswer + 10);
            if (randomAnswer != correctAnswer) 
            {
                answerChoices.Add(randomAnswer);
            }
        }

        // Convert HashSet to List and shuffle
        List<int> shuffledAnswers = new List<int>(answerChoices);
        shuffledAnswers.Sort((a, b) => Random.Range(-1, 2)); // Random shuffle

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int answer = shuffledAnswers[i];
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answer.ToString();
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => AnswerSelected(answer));
        }
    }


    IEnumerator ShowFeedbackAndNext(string message)
    {
        questionText.text = message;
        yield return new WaitForSeconds(0.8f);
        
        foreach (Button button in answerButtons)
        {
            button.interactable = true;
        }

        GenerateQuestion();
    }

    IEnumerator ShowFeedbackAndRestart(string message, float delay)
    {
        questionText.text = message;

        // foreach (Button button in answerButtons)
        // {
        //     button.interactable = false;
        // }

        yield return new WaitForSeconds(delay);
        StartLevel(currentLevel);
    }


    void AnswerSelected(int selectedAnswer)
    {

        if (selectedAnswer == correctAnswer)
        {
            score++;
            StartCoroutine(ShowFeedbackAndNext("Correct!"));
        }
        else
        {
            wrongAnswers++;
            if (wrongAnswers >= 3)
            {
                RestartLevel();
                return;
            }
            StartCoroutine(ShowFeedbackAndNext("Wrong!"));
        }
    }


    void RestartLevel()
    {
        questionText.text = "Too many wrong answers! Restarting...";
        StartCoroutine(RestartLevelAfterDelay(2f)); // Optional delay before restart
    }

    IEnumerator RestartLevelAfterDelay(float delay)
    {
        foreach (Button button in answerButtons)
        {
            button.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(delay);
        StartLevel(currentLevel);
    }



    void GameOver()
    {
        questionText.text = "Time's up!";
        scoreText.text = "Score: " + score + "/" + totalQuestions;
        if (currentLevel == 1 && score >= 5){
            level1Completed = true;
            gameData.AddAcademicPoints(5);
		    uiManager.UpdatePointsUI();
        } 
        if (currentLevel == 2 && score >= 5){
            level2Completed = true;
            gameData.AddAcademicPoints(5);
		    uiManager.UpdatePointsUI();
        }
        if (currentLevel == 3 && score >= 5){
            level3Completed = true;
            gameData.AddAcademicPoints(5);
		    uiManager.UpdatePointsUI();
            mathGameDone.updatingMathGame(true);
        }
        foreach (Button button in answerButtons)
        {
            button.gameObject.SetActive(false);
        }

    }
}
