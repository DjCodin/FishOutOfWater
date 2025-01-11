using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;
    public Button button6;
    public Button button7;
    public Button button8;
    public Button button9;
    public TMP_Text infoText;
    public bool isPlayerTurn;
    public float timer;
    public bool cutsceneDone = false;
    public GameObject x;
    public GameObject o;
    public bool instantiated = false;

    void Start()
    {
        infoText.text = "Your shape is:";
        timer = 0;

        // Hide the buttons for the opening cutscene
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);
        button4.gameObject.SetActive(false);
        button5.gameObject.SetActive(false);
        button6.gameObject.SetActive(false);
        button7.gameObject.SetActive(false);
        button8.gameObject.SetActive(false);
        button9.gameObject.SetActive(false);
    }

    void Update()
    {
        // Cutscene script 
        if (!cutsceneDone)
        {
            timer += Time.deltaTime;
            if (timer >= 2 && !instantiated)
            {
                timer = 0;
                int randInt = Random.Range(0, 2);
                if (randInt == 0)
                {
                    var copy = Instantiate(x, new Vector2(0, 0), Quaternion.identity);
                    Destroy(copy, 3.0f);
                    instantiated = true;
                }
                else
                {
                    var copy = Instantiate(o, new Vector2(0, 0), Quaternion.identity);
                    Destroy(copy, 3.0f);
                    instantiated = true;

                }
            }
            if (timer >= 3.0f)
            {
                cutsceneDone = true;
                infoText.text = "";
            }
        }
        if (cutsceneDone)
        {
            // Show the buttons after the cutscene finishes
            button1.gameObject.SetActive(true);
            button2.gameObject.SetActive(true);
            button3.gameObject.SetActive(true);
            button4.gameObject.SetActive(true);
            button5.gameObject.SetActive(true);
            button6.gameObject.SetActive(true);
            button7.gameObject.SetActive(true);
            button8.gameObject.SetActive(true);
            button9.gameObject.SetActive(true);


        }
    }
}
