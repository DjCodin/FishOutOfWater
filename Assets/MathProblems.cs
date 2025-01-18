using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MathProblems : MonoBehaviour
{
    public GameObject MathProblem;
    public GameObject OptionOne;
    public GameObject OptionTwo;
    public GameObject OptionThree;

    public List<string> easyMathList = new List<string>();

    private int correctAnswer;
    private int wrongAnswer1;
    private int wrongAnswer2;

    public GameObject rightOrWrong_Text;

    void Start()
    {
        // Check if all GameObjects are assigned
        if (MathProblem == null || OptionOne == null || OptionTwo == null || OptionThree == null || rightOrWrong_Text == null)
        {
            Debug.LogError("One or more GameObjects are not assigned in the Inspector.");
            return;
        }

        // Initialize math problems (format: "A + B = ?")
        easyMathList.Add("2 + 3");
        easyMathList.Add("5 - 2");
        easyMathList.Add("4 + 1");
        easyMathList.Add("6 - 3");
        easyMathList.Add("3 + 2");

        // Display the first math problem
        DisplayMathProblem();
    }

    public void DisplayMathProblem()
    {
        if (easyMathList.Count == 0)
        {
            Debug.LogError("No math problems available in the list.");
            return;
        }

        // Pick a random problem
        string randomMathProblem = easyMathList[Random.Range(0, easyMathList.Count)];
        var mathProblemText = MathProblem.GetComponent<TextMeshProUGUI>();
        if (mathProblemText != null)
        {
            mathProblemText.text = randomMathProblem;
        }
        else
        {
            Debug.LogError("MathProblem GameObject does not have a TextMeshProUGUI component.");
            return;
        }

        // Calculate the correct answer
        correctAnswer = EvaluateMathProblem(randomMathProblem);

        // Generate random wrong answers
        wrongAnswer1 = correctAnswer + Random.Range(1, 5);
        wrongAnswer2 = correctAnswer - Random.Range(1, 5);

        // Ensure wrong answers are unique
        while (wrongAnswer1 == correctAnswer || wrongAnswer1 == wrongAnswer2)
        {
            wrongAnswer1 = correctAnswer + Random.Range(1, 5);
        }

        while (wrongAnswer2 == correctAnswer || wrongAnswer2 == wrongAnswer1)
        {
            wrongAnswer2 = correctAnswer - Random.Range(1, 5);
        }

        // Randomly assign the correct answer to one of the options
        int correctOption = Random.Range(1, 4);
        AssignAnswerToOption(correctOption);

        // Clear feedback text
        var feedbackText = rightOrWrong_Text.GetComponent<TextMeshProUGUI>();
        if (feedbackText != null)
        {
            feedbackText.text = "";
        }
        else
        {
            Debug.LogError("rightOrWrong_Text GameObject does not have a TextMeshProUGUI component.");
        }
    }

    private void AssignAnswerToOption(int correctOption)
    {
        var optionOneText = OptionOne.GetComponent<TextMeshProUGUI>();
        var optionTwoText = OptionTwo.GetComponent<TextMeshProUGUI>();
        var optionThreeText = OptionThree.GetComponent<TextMeshProUGUI>();

        if (optionOneText == null || optionTwoText == null || optionThreeText == null)
        {
            Debug.LogError("One or more Option GameObjects do not have TextMeshProUGUI components.");
            return;
        }

        if (correctOption == 1)
        {
            optionOneText.text = correctAnswer.ToString();
            optionTwoText.text = wrongAnswer1.ToString();
            optionThreeText.text = wrongAnswer2.ToString();
        }
        else if (correctOption == 2)
        {
            optionOneText.text = wrongAnswer1.ToString();
            optionTwoText.text = correctAnswer.ToString();
            optionThreeText.text = wrongAnswer2.ToString();
        }
        else
        {
            optionOneText.text = wrongAnswer1.ToString();
            optionTwoText.text = wrongAnswer2.ToString();
            optionThreeText.text = correctAnswer.ToString();
        }
    }

    public void CheckAnswer(GameObject selectedOption)
    {
        var selectedText = selectedOption.GetComponent<TextMeshProUGUI>();
        if (selectedText == null)
        {
            Debug.LogError("Selected option does not have a TextMeshProUGUI component.");
            return;
        }

        int selectedAnswer;
        if (int.TryParse(selectedText.text, out selectedAnswer))
        {
            var feedbackText = rightOrWrong_Text.GetComponent<TextMeshProUGUI>();
            if (feedbackText != null)
            {
                feedbackText.text = selectedAnswer == correctAnswer ? "Correct!" : "Wrong!";
            }

            // Display a new math problem
            Invoke("DisplayMathProblem", 2f);
        }
        else
        {
            Debug.LogError("Selected option text is not a valid number.");
        }
    }

    private int EvaluateMathProblem(string mathProblem)
    {
        // Split the math problem into parts (e.g., "2 + 3")
        string[] parts = mathProblem.Split(' ');
        if (parts.Length != 3)
        {
            Debug.LogError("Math problem format is invalid: " + mathProblem);
            return 0;
        }

        int operand1, operand2;
        if (!int.TryParse(parts[0], out operand1) || !int.TryParse(parts[2], out operand2))
        {
            Debug.LogError("Math problem contains invalid numbers: " + mathProblem);
            return 0;
        }

        string operatorSymbol = parts[1];

        // Perform the calculation based on the operator
        switch (operatorSymbol)
        {
            case "+":
                return operand1 + operand2;
            case "-":
                return operand1 - operand2;
            default:
                Debug.LogError("Unsupported operator: " + operatorSymbol);
                return 0;
        }
    }
}
