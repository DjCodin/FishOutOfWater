using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class GMScript : MonoBehaviour
{

    public ArrayList buttons;
    public Button blue;
    public Button brown;
    public Button pink;
    public Button red;
    public Button turquoise;
    public Button yellow;
    public Button gray;
    public Color currentColor;
    public Dictionary<Button, Color> colorDict;
    public Button green;
    [SerializeField]
    private GameDataSO gameDaySO;
    public AudioSource click;
    private bool dialogueFinished = false;
    private bool stuffSpawned = false;
    public GameObject dialogueBox;
    public TMP_Text dialogue;
    public TMP_Text personSpeaking;
    public GameObject shelldon;
    public GameObject micah;
    public Button skipFish;
    private int dialogueNum = 1;
    private float timer = 0;
    public AudioSource dialogueSound;
    public bool dialogueSoundPlayed = false;
    public bool speaking = false;
    public int i = 0;
    public float textDelay = .005f;
    public bool lastDialogue = false;
    public GameObject[] objectsWithTagA;
    public GameObject aTile;
    public GameObject[] objectsWithTagB;
    public GameObject bTile;
    public GameObject[] objectsWithTagC;
    public GameObject cTile;
    public string micahText = "Teacher";
    public string shelldonText = "Shelldon";
    public string dayOneDialogue1 = "Hello Shelldon, let me explain to you your assignment for today. Today your job is to color your drawing in accordance with the number it is assigned.";
    public string dayOneDialogue2 = "Each day, you will color in one of three sections of your drawing. By the third day, you will have a fully colored drawing. Does all of that make sense to you?";
    public string dayOneDialogue3 = "Yes, I can't wait to start.";
    public string dayOneDialogue4 = "Alright, I will leave you to it now.";
    public string dayTwoDialogue1 = "You wonderfully finished coloring yesterday's section of the drawing. Today, you will be painting a new section of the drawing. How does that sound?";
    public string dayTwoDialogue2 = "It sounds good. I will make this section just as beautiful as yesterday's section.";
    public string dayThreeDialogue1 = "Shelldon, today you will work on coloring the last section of your drawing. After today, you will have a completed drawing, so work hard to finish it. You got this!";
    public string dayThreeDialogue2 = "Ok, I can't wait to see how this drawing turns out.";
    public GameDataSO gameData;
    public UIManager uiManager;
    public Dictionary<GameObject, Color> filledDict  = new Dictionary<GameObject, Color>();
    public int filledNum = 0;
    public TMP_Text filledText;
    public int numTiles = 0;
    public AudioSource music;
    public Button finishButton;
    public TMP_Text finishedButtonText;
    public float areYouSureTimer = 6f;
    bool areYouSure = false;
    public AudioSource scribble;
    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        colorDict = new Dictionary<Button, Color>();
        buttons = new ArrayList();
        currentColor = Color.white;
        colorDict[blue] = RGB(30, 161, 226);
        colorDict[brown] = RGB(245, 166, 62); 
        colorDict[pink] = RGB(255, 166, 187);
        colorDict[gray] = RGB(164, 196, 199);
        colorDict[green] = RGB(166, 216, 42);
        colorDict[red] = RGB(250, 96, 90);
        colorDict[turquoise] = RGB(144, 243, 244);
        colorDict[yellow] = RGB(254, 239, 102);
        buttons.Add(blue);
        buttons.Add(brown);
        buttons.Add(pink);
        buttons.Add(red);
        buttons.Add(turquoise);
        buttons.Add(yellow);
        buttons.Add(gray);
        buttons.Add(green);

        foreach (Button btn in buttons)
        {
            btn.onClick.AddListener(() => HandleButtonClick(btn));
        }

        // Tiles
        objectsWithTagA = GameObject.FindGameObjectsWithTag("ATiles");
        // Outline
        aTile = GameObject.FindGameObjectWithTag("A");
        // Tiles
        objectsWithTagB = GameObject.FindGameObjectsWithTag("BTiles");
        // Outline
        bTile = GameObject.FindGameObjectWithTag("B");
        // Tiles
        objectsWithTagC = GameObject.FindGameObjectsWithTag("CTiles");
        // Outline
        cTile = GameObject.FindGameObjectWithTag("C");
        aTile.SetActive(false);
        bTile.SetActive(false);
        cTile.SetActive(false);
        foreach (GameObject obj in objectsWithTagA)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in objectsWithTagB)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in objectsWithTagC)
        {
            obj.SetActive(false);
        }
        foreach (Button btn in buttons)
        {
            btn.gameObject.SetActive(false);
        }

        skipFish.onClick.AddListener(() => SkipFish(skipFish));
        dialogue.text = "";
        personSpeaking.text = "";
        filledText.gameObject.SetActive(false);
        finishButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (gameDaySO.GameDay == 0 && !dialogueFinished)
        {
            if (dialogueNum == 1)
            {
                timer += Time.deltaTime;
                micah.SetActive(true);
                shelldon.SetActive(false);
                personSpeaking.text = micahText;
                if (!dialogueSoundPlayed)
                {
                    dialogueSound.Play();
                    dialogueSoundPlayed = true;
                    speaking = true;
                }
                char[] dialogueArr = dayOneDialogue1.ToCharArray();

                if (timer > textDelay && dialogueArr.Length > i && speaking)
                {
                    dialogue.text += dialogueArr[i].ToString();
                    i++;
                    timer = 0;
                    skipFish.gameObject.SetActive(false);
                }
                if (i >= dialogueArr.Length)
                {
                    i = 0;
                    speaking = false;
                    timer = 0;
                    skipFish.gameObject.SetActive(true);
                }
            }
            if (dialogueNum == 2)
            {
                timer += Time.deltaTime;
                micah.SetActive(true);
                shelldon.SetActive(false);
                personSpeaking.text = micahText;
                if (!dialogueSoundPlayed)
                {
                    dialogueSound.Play();
                    dialogueSoundPlayed = true;
                    speaking = true;
                }
                char[] dialogueArr = dayOneDialogue2.ToCharArray();

                if (timer > textDelay && dialogueArr.Length > i && speaking)
                {
                    dialogue.text += dialogueArr[i].ToString();
                    i++;
                    timer = 0;
                    skipFish.gameObject.SetActive(false);
                }
                if (i >= dialogueArr.Length)
                {
                    i = 0;
                    speaking = false;
                    timer = 0;
                    skipFish.gameObject.SetActive(true);
                }
            }
            if (dialogueNum == 3)
            {
                timer += Time.deltaTime;
                micah.SetActive(false);
                shelldon.SetActive(true);
                personSpeaking.text = shelldonText;
                if (!dialogueSoundPlayed)
                {
                    dialogueSound.Play();
                    dialogueSoundPlayed = true;
                    speaking = true;
                }
                char[] dialogueArr = dayOneDialogue3.ToCharArray();

                if (timer > textDelay && dialogueArr.Length > i && speaking)
                {
                    dialogue.text += dialogueArr[i].ToString();
                    i++;
                    timer = 0;
                    skipFish.gameObject.SetActive(false);
                }
                if (i >= dialogueArr.Length)
                {
                    i = 0;
                    speaking = false;
                    timer = 0;
                    skipFish.gameObject.SetActive(true);
                }
            }
            if (dialogueNum == 4)
            {
                timer += Time.deltaTime;
                micah.SetActive(true);
                shelldon.SetActive(false);
                personSpeaking.text = micahText;
                if (!dialogueSoundPlayed)
                {
                    dialogueSound.Play();
                    dialogueSoundPlayed = true;
                    speaking = true;
                }
                char[] dialogueArr = dayOneDialogue4.ToCharArray();

                if (timer > textDelay && dialogueArr.Length > i && speaking)
                {
                    dialogue.text += dialogueArr[i].ToString();
                    i++;
                    timer = 0;
                    skipFish.gameObject.SetActive(false);
                }
                if (i >= dialogueArr.Length)
                {
                    i = 0;
                    speaking = false;
                    timer = 0;
                    skipFish.gameObject.SetActive(true);
                    lastDialogue = true;
                }
            }
        }
        if (gameDaySO.GameDay == 1 && !dialogueFinished)
        {
            if (dialogueNum == 1)
            {
                timer += Time.deltaTime;
                micah.SetActive(true);
                shelldon.SetActive(false);
                personSpeaking.text = micahText;
                if (!dialogueSoundPlayed)
                {
                    dialogueSound.Play();
                    dialogueSoundPlayed = true;
                    speaking = true;
                }
                char[] dialogueArr = dayTwoDialogue1.ToCharArray();

                if (timer > textDelay && dialogueArr.Length > i && speaking)
                {
                    dialogue.text += dialogueArr[i].ToString();
                    i++;
                    timer = 0;
                    skipFish.gameObject.SetActive(false);
                }
                if (i >= dialogueArr.Length)
                {
                    i = 0;
                    speaking = false;
                    timer = 0;
                    skipFish.gameObject.SetActive(true);
                }
            }
            if (dialogueNum == 2)
            {
                timer += Time.deltaTime;
                micah.SetActive(false);
                shelldon.SetActive(true);
                personSpeaking.text = shelldonText;
                if (!dialogueSoundPlayed)
                {
                    dialogueSound.Play();
                    dialogueSoundPlayed = true;
                    speaking = true;
                }
                char[] dialogueArr = dayTwoDialogue2.ToCharArray();

                if (timer > textDelay && dialogueArr.Length > i && speaking)
                {
                    dialogue.text += dialogueArr[i].ToString();
                    i++;
                    timer = 0;
                    skipFish.gameObject.SetActive(false);
                }
                if (i >= dialogueArr.Length)
                {
                    i = 0;
                    speaking = false;
                    timer = 0;
                    skipFish.gameObject.SetActive(true);
                    lastDialogue = true;
                }
            }

        }
        if (gameDaySO.GameDay == 2 && !dialogueFinished)
        {
            if (dialogueNum == 1)
            {
                timer += Time.deltaTime;
                micah.SetActive(true);
                shelldon.SetActive(false);
                personSpeaking.text = micahText;
                if (!dialogueSoundPlayed)
                {
                    dialogueSound.Play();
                    dialogueSoundPlayed = true;
                    speaking = true;
                }
                char[] dialogueArr = dayThreeDialogue1.ToCharArray();

                if (timer > textDelay && dialogueArr.Length > i && speaking)
                {
                    dialogue.text += dialogueArr[i].ToString();
                    i++;
                    timer = 0;
                    skipFish.gameObject.SetActive(false);
                }
                if (i >= dialogueArr.Length)
                {
                    i = 0;
                    speaking = false;
                    timer = 0;
                    skipFish.gameObject.SetActive(true);
                }
            }
            if (dialogueNum == 2)
            {
                timer += Time.deltaTime;
                micah.SetActive(false);
                shelldon.SetActive(true);
                personSpeaking.text = shelldonText;
                if (!dialogueSoundPlayed)
                {
                    dialogueSound.Play();
                    dialogueSoundPlayed = true;
                    speaking = true;
                }
                char[] dialogueArr = dayThreeDialogue2.ToCharArray();

                if (timer > textDelay && dialogueArr.Length > i && speaking)
                {
                    dialogue.text += dialogueArr[i].ToString();
                    i++;
                    timer = 0;
                    skipFish.gameObject.SetActive(false);
                }
                if (i >= dialogueArr.Length)
                {
                    i = 0;
                    speaking = false;
                    timer = 0;
                    skipFish.gameObject.SetActive(true);
                    lastDialogue = true;
                }
            }

        }


        if (dialogueFinished && !stuffSpawned)
        {
            spawn();
            stuffSpawned = true;
            filledText.gameObject.SetActive(true);
            music.loop = true;
            music.Play();
        }

        filledText.text = filledNum + "/" + numTiles + " Tiles Colored";

        if (Input.GetMouseButton(0) && dialogueFinished)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider is PolygonCollider2D)
            {
                GameObject clickedObject = hit.collider.gameObject;
                if (filledDict[clickedObject] == Color.white)
                {
                    filledNum += 1;
                    gameData.AddSocialPoints(2);
                    uiManager.UpdatePointsUI();
                }
                filledDict[clickedObject] = currentColor;
                SpriteRenderer spriteRenderer = hit.collider.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = currentColor;
                    scribble.Play();
                }
            }
        }
        if (areYouSure && areYouSureTimer > 0)
        {
            areYouSureTimer -= Time.deltaTime;
            finishedButtonText.text = "Are You Sure? (" + (int)areYouSureTimer + ")";
            if (Input.GetMouseButtonDown(0) && IsPointerOverUIElement(finishButton.gameObject))
            {
                Debug.Log("Recess Map");
                SceneManager.LoadScene("RecessMap");
            }
        }

        if (Input.GetMouseButtonDown(0) && !areYouSure)
        {
            // Check if the mouse is over the button
            if (IsPointerOverUIElement(finishButton.gameObject))
            {
                finishedButtonText.text = "Are You Sure? (" + (int) areYouSureTimer + ")";
                areYouSure = true;
            }
        }

        
        if (areYouSure && areYouSureTimer <= 0)
        {
            areYouSure = false;
            finishedButtonText.text = "Finish Game";
            SceneManager.LoadScene("RecessMap");
        }
    }
    void HandleButtonClick(Button clickedButton)
    {
        currentColor = colorDict[clickedButton];
        GetComponent<AudioSource>().Play();
    }

    Color RGB(int r, int g, int b)
    {
        return new Color(r / 255f, g / 255f, b / 255f);
    }

    void spawn()
    {
        dialogue.gameObject.SetActive(false);
        dialogueBox.SetActive(false);
        micah.SetActive(false);
        shelldon.SetActive(false);
        personSpeaking.gameObject.SetActive(false);
        skipFish.gameObject.SetActive(false);
        finishButton.gameObject.SetActive(true);

        foreach (Button btn in buttons)
        {
            btn.gameObject.SetActive(true);
        }

        if (gameDaySO.GameDay == 0)
        {
            aTile.SetActive(true);
            foreach (GameObject obj in objectsWithTagA)
            {
                filledDict.Add(obj, Color.white);
                obj.SetActive(true);
                var image = obj.GetComponent<SpriteRenderer>();
                image.color = new Color(1f, 1f, 1f, 0f);
                numTiles = objectsWithTagA.Length;
            }
        }

        if (gameDaySO.GameDay == 1)
        {
            bTile.SetActive(true);
            foreach (GameObject obj in objectsWithTagB)
            {
                filledDict.Add(obj, Color.white);
                obj.SetActive(true);
                var image = obj.GetComponent<SpriteRenderer>();
                image.color = new Color(1f, 1f, 1f, 0f);
                numTiles = objectsWithTagB.Length;
            }
        }

        if (gameDaySO.GameDay == 2)
        {
            cTile.SetActive(true);
            foreach (GameObject obj in objectsWithTagC)
            {
                filledDict.Add(obj, Color.white);
                obj.SetActive(true);
                var image = obj.GetComponent<SpriteRenderer>();
                image.color = new Color(1f, 1f, 1f, 0f);
                numTiles = objectsWithTagC.Length;
            }
        }
    }

    void SkipFish(Button btn)
    {
        if (!speaking)
        {
            dialogueNum++;
            dialogue.text = "";
            dialogueSoundPlayed = false;
            btn.gameObject.SetActive(false);
            if (lastDialogue)
            {
                dialogueFinished = true;
                micah.SetActive(false);
                dialogueBox.SetActive(false);
                shelldon.SetActive(false);
                dialogue.gameObject.SetActive(false);
                personSpeaking.gameObject.SetActive(false);
            }
        }
    }

    bool IsPointerOverUIElement(GameObject uiElement)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject == uiElement)
                return true;
        }
        return false;
    }
}