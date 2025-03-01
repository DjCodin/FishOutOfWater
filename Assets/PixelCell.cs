using UnityEngine;
using UnityEngine.UI;

public class PixelCell : MonoBehaviour
{
    public Image image; // Reference to the UI Image
    private int x, y, requiredColorIndex;
    private ColorByNumber gameManager;

    public void Setup(int x, int y, int requiredColorIndex, ColorByNumber gameManager)
    {
        this.x = x;
        this.y = y;
        this.requiredColorIndex = requiredColorIndex;
        this.gameManager = gameManager; 
        image = GetComponent<Image>();

        if (image == null)
        {
            Debug.LogError("ERROR: Image component missing on PixelCell!");
        }

        if (gameManager == null)
        {
            Debug.LogError("ERROR: gameManager is NULL in PixelCell.Setup!");
        }

        image.color = Color.white; // Default color
    }

    public void SetColor(Color color)
    {
        image.color = color;
    }

    public void OnClick()
    {
        if (gameManager == null)
        {
            Debug.LogError("ERROR: gameManager is NULL in PixelCell.OnClick!");
            return;
        }

        gameManager.TryPaintPixel(x, y, requiredColorIndex);
    }
}

