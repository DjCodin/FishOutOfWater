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
    public DialogueTrigger dialogueTrigger;
    public GameObject interactableIcons;
    public GameObject computerPrompt;
    public Button yesButton;
    public Button noButton;
    

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
            yesButton.onClick.AddListener(() => {SceneManager.LoadScene("MathClass");});
            noButton.onClick.AddListener(() => {computerPrompt.SetActive(false);});
        }
    }

    void FixedUpdate ()
    {
        rb.velocity = new Vector2(speedX * moveSpeed, speedY * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Computer" || collision.tag == "Person")
        {
            iconActive = true;
            Debug.Log("Touched");
        }
        if(collision.tag == "Computer")
        {
            compInteract = true;
            Debug.Log("comp Touched");
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision){
        if(collision.tag != "Computer" || collision.tag != "Person")
        {
            iconActive = false;
            Debug.Log("Touched");
        }
    }  
}