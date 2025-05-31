using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
	public GameDataSO gameData;
	public UIManager uiManager;
	public Image characterIcon;
	public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;
    public GameObject dialogueBox; 
	public AudioSource dialogueMusic;
	public AudioSource backGroundMusic;
    private Queue<DialogueLine> lines;

    
	public bool isDialogueActive = false;

	public float typingSpeed = 0.2f;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;

		lines = new Queue<DialogueLine>();
    }

	public void StartDialogue(Dialogue dialogue)
	{
		isDialogueActive = true;

		dialogueBox.SetActive(isDialogueActive);

		lines.Clear();

		backGroundMusic.Stop();
	    dialogueMusic.Play();
		
		foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
		{
			lines.Enqueue(dialogueLine);
		}
		
		DisplayNextDialogueLine();
	}

	public void DisplayNextDialogueLine()
	{
		if (lines.Count == 0)
		{
			EndDialogue();
			backGroundMusic.Play();
			dialogueMusic.Stop();
			return;
		}

		DialogueLine currentLine = lines.Dequeue();

		characterIcon.sprite = currentLine.character.icon;
		characterName.text = currentLine.character.name;

		StopAllCoroutines();
		StartCoroutine(TypeSentence(currentLine));
	}

	IEnumerator TypeSentence(DialogueLine dialogueLine)
	{
		dialogueArea.text = "";
		foreach (char letter in dialogueLine.line.ToCharArray())
		{
			dialogueArea.text += letter;
			yield return new WaitForSeconds(typingSpeed);
		}
	}

	void EndDialogue()
	{
		gameData.AddSocialPoints(5);
		uiManager.UpdatePointsUI();
		backGroundMusic.Play();
		dialogueMusic.Stop();
		isDialogueActive = false;
        dialogueBox.SetActive(isDialogueActive);
	}
}