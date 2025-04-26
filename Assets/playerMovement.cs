using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class playerMovement :MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private float speedX;
    private float speedY;
    private bool iconActive = false;
    public bool compInteract = false;
    public bool tictacInteract = false;

    public DialogueTrigger dialogueTrigger;
    public GameObject interactableIcons;
    public GameObject computerPrompt;
    public Button yesButton;
    public Button noButton;
    public TMP_Text promptQuestion;

    

    void Start ()
    {
        dialogueTrigger = FindObjectOfType<DialogueTrigger>();
        rb = GetComponent<Rigidbody2D>();
        interactableIcons.SetActive(iconActive);
    }

    void Update ()
    {
        speedX = Input.GetAxisRaw("Horizontal");
        speedY = Input.GetAxisRaw("Vertical");

        if (iconActive)
        {
            interactableIcons.SetActive(true);
        }
        else{
      
            interactableIcons.SetActive(false);
        }

        if(compInteract && Input.GetKeyDown(KeyCode.I)){
            computerPrompt.SetActive(true);
            promptQuestion.text = "Do you want to start math class now?";
            yesButton.onClick.AddListener(() => {SceneManager.LoadScene("MathClass");});
            noButton.onClick.AddListener(() => {computerPrompt.SetActive(false);});
        }

        if(tictacInteract && Input.GetKeyDown(KeyCode.I)){
            computerPrompt.SetActive(true);
            promptQuestion.text = "Do you want to start playing TicTacToe?";
            yesButton.onClick.AddListener(() => {SceneManager.LoadScene("TicTacToe");});
            noButton.onClick.AddListener(() => {computerPrompt.SetActive(false);});
        }
    }

    void FixedUpdate ()
    {
        rb.velocity = new Vector2(speedX * moveSpeed, speedY * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Computer" || collision.tag == "Person" || collision.tag == "tictactoe")
        {
            iconActive = true;
            Debug.Log("Touched");
        }
        if(collision.tag == "Computer")
        {
            compInteract = true;
            Debug.Log("comp Touched");
        }
        if(collision.tag == "tictactoe")
        {
            tictacInteract = true;
            Debug.Log("tictactoe Touched");
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision){
        
        iconActive = false;
        compInteract = false;
        tictacInteract = false;
           
        
    }  
}