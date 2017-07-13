using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using System.IO;

public class TextureLoader : MonoBehaviour
{
    public Camera renderCamera;

    public Image image;

    public string folderPath = "Frames/JPEG/";

    Sprite[] sprites;

    public int views = 16;

    void Start()
    {
        sprites = new Sprite[views];

        for (int i = 0; i < sprites.Length; i++)
        {
            Texture2D tex = GetTextureFromFile("Pivot_0_" + i);

            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);

            sprites[i] = sprite;
        }
    }


    void Update()
    {
        int dir = Mathf.FloorToInt(renderCamera.transform.eulerAngles.y / 360 * views);

        image.sprite = sprites[dir];
    }


    public Texture2D GetTextureFromFile(string fileName)
    {
        string filePath = "";

        fileName = folderPath + fileName;

        if (!Directory.Exists(folderPath))
        {
            Debug.LogError("Directiory doesn't exist");
            return null;
        }

        if (File.Exists(fileName + ".jpg"))
            filePath = fileName + ".jpg";

        if (File.Exists(fileName + ".png"))
            filePath = fileName + ".png";

        if (string.IsNullOrEmpty(filePath))
        {
            Debug.LogError("Texture " + filePath + " doesn't exist");
            return null;
        }

        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(File.ReadAllBytes(filePath));

        /*
        float t = Time.realtimeSinceStartup;
        tex.LoadImage(File.ReadAllBytes(filePath));
        t = Time.realtimeSinceStartup - t;
        Debug.Log("Loading image from file completed in: " + t);*/

        return tex;
    }
}
