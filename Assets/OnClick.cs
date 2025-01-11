using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class OnClick : MonoBehaviour
{

    // Start is called before the first frame update
    public Button button;
    public Button[] buttons;
    void Start()
    {
        foreach (Button btn in buttons)
        {
            btn.onClick.AddListener(() => HandleButtonClick(btn));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableButton()
    {
        // Make the button non-interactable
        button.interactable = false;
    }

    void HandleButtonClick(Button clickedButton)
    {
        // Make the clicked button not interactable
        clickedButton.interactable = false;
    }
}
