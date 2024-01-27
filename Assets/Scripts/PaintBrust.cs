using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintBrust : MonoBehaviour
{
    public Transform TargetObject;
    public Texture2D texture2D;

    public Texture2D copyTexture2D;

    public RawImage rawImage;

    private Color[] _colors;

    private int width;

    private int height;

    private List<int> colorArea = new List<int>();

    public int brushSize = 10;
    public Color brushColor=Color.red;
    // Start is called before the first frame update
    void Start()
    {
        _colors = texture2D.GetPixels();
        width = texture2D.width;
        height = texture2D.height;
        copyTexture2D = new Texture2D(width, height);
        copyTexture2D.SetPixels(_colors);
        copyTexture2D.Apply();
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (_colors[i * width + j].a != 0)
                {
                    colorArea.Add(i * width +j);
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BrushColor(TargetObject.position);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            BrushColor(TargetObject.position);
        }


    }

    private void BrushColor(Vector2 pos)
    {
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rawImage.GetComponent<RectTransform>(), pos, null,
            out localPos);
        Debug.Log(localPos);
        if (GetComponent<RectTransform>().rect.Contains(localPos))
        {
            int x = (int)(localPos.x*(width/GetComponent<RectTransform>().rect.size.x));
            int y = (int)(localPos.y*(height/GetComponent<RectTransform>().rect.size.y));
            int index = y * width + x;
            for (int i = x - brushSize; i < x + brushSize; i++)
            {
                for (int j = y - brushSize; j < y + brushSize; j++)
                {
                    if (Vector2.SqrMagnitude(new Vector2(i, j) - new Vector2(x, y)) < brushSize * brushSize)
                    {
                        index = j * width + i;
                        _colors[index] = brushColor;
                        if (colorArea.Contains(index))
                        {
                            colorArea.Remove(index);
                            if (colorArea.Count < 7000)
                            {
                                Debug.Log("上色成功");
                            }
                        }
                    }
                }
            }
            copyTexture2D.SetPixels(_colors);
            copyTexture2D.Apply();
            rawImage.texture = copyTexture2D;
        }

    }
}
