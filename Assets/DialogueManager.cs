using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

	public Image characterIcon;
	public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;
    public GameObject dialogueBox; 
	public AudioSource dialogueMusic;
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

		foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
		{
			dialogueMusic.Play();
			lines.Enqueue(dialogueLine);
		}
		
		DisplayNextDialogueLine();
	}

	public void DisplayNextDialogueLine()
	{
		if (lines.Count == 0)
		{
			EndDialogue();
			dialogueMusic.Stop();
			return;
		}

		DialogueLine currentLine = lines.Dequeue();

		characterIcon.sprite = currentLine.character.icon;
		characterName.text = currentLine.character.name;

		StopAllCoroutines();
		dialogueMusic.Play();
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
		dialogueMusic.Stop();
		isDialogueActive = false;
        dialogueBox.SetActive(isDialogueActive);
	}
}