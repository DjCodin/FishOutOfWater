using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

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

    // Start is called before the first frame update
    void Start()
    {
        buttons.Add(blue);
        buttons.Add(brown);
        buttons.Add(pink);
        buttons.Add(red);
        buttons.Add(turquoise);
        buttons.Add(yellow);
        buttons.Add(gray);

        foreach (Button btn in buttons)
        {
            btn.onClick.AddListener(() => HandleButtonClick(btn));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HandleButtonClick(Button clickedButton)
    {

    }


}