using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MathGame : MonoBehaviour
{
    public GameObject questionObject;
    public GameObject timerObject;
    public GameObject scoreObject;
    public GameObject[] answerObjects;
    private TextMeshProUGUI questionText;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI timerText;
    private Button[] answerButtons;
    private int correctAnswer;
    public int score;
    public int totalQuestion;
    private float timeRemaining = 60f;
    private bool gameActive = true;

    void Start()
    {
        scoreText = scoreObject.GetComponent<TextMeshProUGUI>();

        if (questionObject != null)
            questionText = questionObject.GetComponent<TextMeshProUGUI>();

        else
            Debug.LogError("Question object is not assigned!");
        
        if (timerObject != null)
            timerText = timerObject.GetComponent<TextMeshProUGUI>();
        else
            Debug.LogError("Timer object is not assigned!");
        
        if (answerObjects != null && answerObjects.Length > 0)
        {
            answerButtons = new Button[answerObjects.Length];
            for (int i = 0; i < answerObjects.Length; i++)
            {
                if (answerObjects[i] != null)
                    answerButtons[i] = answerObjects[i].GetComponent<Button>();
                else
                    Debug.LogError("Answer object at index " + i + " is not assigned!");
            }
        }
        else
        {
            Debug.LogError("Answer objects are not assigned!");
        }

        if (questionText != null && timerText != null && answerButtons != null)
            GenerateQuestion();
    }

    void Update()
    {
        if (gameActive)
        {
            timeRemaining -= Time.deltaTime;
            if (timerText != null)
                timerText.text = "Time: " + Mathf.Max(0, Mathf.CeilToInt(timeRemaining)).ToString();
            
            if (timeRemaining <= 0)
            {
                gameActive = false;
                GameOver();
            }
        }
    }

    void GenerateQuestion()
    {
        int num1 = Random.Range(1, 10);
        int num2 = Random.Range(1, 10);
        correctAnswer = num1 + num2;
        
        if (questionText != null)
            questionText.text = num1 + " + " + num2 + " = ?";
            totalQuestion ++;
        
        int correctPosition = Random.Range(0, answerButtons.Length);
        
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (answerButtons[i] != null)
            {
                answerButtons[i].onClick.RemoveAllListeners();
                int answer;
                
                if (i == correctPosition)
                {
                    answer = correctAnswer;
                }
                else
                {
                    do
                    {
                        answer = Random.Range(correctAnswer - 3, correctAnswer + 3);
                    } while (answer == correctAnswer);
                }
                
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answer.ToString();
                answerButtons[i].onClick.AddListener(() => AnswerSelected(answer));
            }
        }
    }

    void AnswerSelected(int selectedAnswer)
    {
        if (selectedAnswer == correctAnswer)
        {
            Debug.Log("Correct!");
            score ++;
        }
        else
        {
            Debug.Log("Wrong! Try Again.");
        }
        
        GenerateQuestion();
    }

    void GameOver()
    {
        if (questionText != null)
            questionText.text = "Time's up!";
            scoreText.text = "Score: " + score + "/" + totalQuestion;

        
        foreach (Button button in answerButtons)
        {
            if (button != null)
                button.gameObject.SetActive(false);
        }
    }
}
