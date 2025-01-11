using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public TMP_Text infoText;
    public GameObject x;
    public GameObject o;
    public bool cutsceneDone = false;
    public bool shapeInstantiated = false;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        infoText.text = "Your shape is:";
        timer = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (!cutsceneDone)
        {
            timer += Time.deltaTime;
            if (timer >= 2 && !shapeInstantiated)
            {
                timer = 0;
                int randInt = Random.Range(0, 2);
                if (randInt == 0)
                {
                    var copy = Instantiate(x, new Vector2(0, 0), Quaternion.identity);
                    Destroy(copy, 3.0f);
                    shapeInstantiated = true;
                }
                else
                {
                    var copy = Instantiate(o, new Vector2(0, 0), Quaternion.identity);
                    Destroy(copy, 3.0f);
                    shapeInstantiated = true;
                }
            }
            if(shapeInstantiated && timer >= 3.0f)
            {
                SceneManager.LoadScene("TicTacToe");
            }
        }
    }
        
    
}
