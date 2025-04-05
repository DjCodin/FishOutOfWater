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
    public Dictionary<Button, Color> colorDict;
    public Button green;
    [SerializeField]
    private GameDataSO gameDaySO;

    // Start is called before the first frame update
    void Start()
    {
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
        colorDict[yellow] = RGB(144, 243, 244);
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
        GameObject[] objectsWithTagA = GameObject.FindGameObjectsWithTag("A");
        GameObject aTile = GameObject.FindGameObjectWithTag("ATiles");
        aTile.SetActive(false);
        foreach (GameObject obj in objectsWithTagA)
        {
            obj.SetActive(false);
        }

        if (gameDaySO.GameDay == 0)
        {
            aTile.SetActive(true);
            foreach (GameObject obj in objectsWithTagA)
            {
                obj.SetActive(true);
                var image = obj.GetComponent<SpriteRenderer>();
                image.color = new Color(1f, 1f, 1f, 0f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider is PolygonCollider2D)
            {
                SpriteRenderer spriteRenderer = hit.collider.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = currentColor;
                }
            }
        }
    }

    void HandleButtonClick(Button clickedButton)
    {
        currentColor = colorDict[clickedButton];
    }

    Color RGB(int r, int g, int b)
    {
        return new Color(r / 255f, g / 255f, b / 255f);
    }
}