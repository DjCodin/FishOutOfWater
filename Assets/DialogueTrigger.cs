using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueCharacter
{
    public string name;
    public Sprite icon;
}

[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool collidedIcon;
    public bool mathGameDone = false;

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collidedIcon = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collidedIcon = false;
        Debug.Log("Not Touched");
    }

    public void updatingMathGame(bool status){
        mathGameDone = status;
    }

    void Update()
    {
         if(mathGameDone){
            TriggerDialogue();
        }
        
        if (Input.GetKeyDown(KeyCode.I) && collidedIcon)
        {
            TriggerDialogue();
        }

    }

}