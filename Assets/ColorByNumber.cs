using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorByNumber : MonoBehaviour
{
    public List<Color> colors; // List of colors
    public List<Button> colorButtons; // Buttons for color selection
    public GameObject pixelPrefab; // Prefab for each pixel
    public Transform pixelParent; // Parent object for pixel grid
    public int gridSizeX = 10; // Grid width
    public int gridSizeY = 10; // Grid height

    private Color selectedColor = Color.white; // Default selected color
    private Dictionary<Vector2, PixelCell> pixelGrid = new Dictionary<Vector2, PixelCell>();

    void Start()
    {
        // Check if necessary objects are assigned
        if (pixelPrefab == null)
        {
            Debug.LogError("ERROR: Pixel Prefab is missing!");
            return;
        }

        if (pixelParent == null)
        {
            Debug.LogError("ERROR: Pixel Parent (Grid) is missing!");
            return;
        }

        if (colorButtons.Count == 0)
        {
            Debug.LogError("ERROR: No color buttons assigned!");
            return;
        }

        SetupColorButtons();
        GeneratePixelGrid();
    }

    void SetupColorButtons()
    {
        for (int i = 0; i < colorButtons.Count; i++)
        {
            int index = i;
            colorButtons[i].image.color = colors[i]; // Set button color
            colorButtons[i].onClick.AddListener(() => SelectColor(colors[index]));
        }
    }

    void GeneratePixelGrid()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                GameObject pixelObj = Instantiate(pixelPrefab, pixelParent);
                PixelCell cell = pixelObj.GetComponent<PixelCell>();

                if (cell == null)
                {
                    Debug.LogError("ERROR: PixelCell component is missing on PixelPrefab!");
                    continue;
                }

                cell.Setup(x, y, Random.Range(0, colors.Count), this);
                pixelGrid[new Vector2(x, y)] = cell;
                Debug.Log($"Pixel Created at ({x}, {y})");
            }
        }
    }


    public void SelectColor(Color color)
    {
        selectedColor = color;
    }

    public void TryPaintPixel(int x, int y, int requiredColorIndex)
    {
        if (colors[requiredColorIndex] == selectedColor)
        {
            pixelGrid[new Vector2(x, y)].SetColor(selectedColor);
        }
    }
}

