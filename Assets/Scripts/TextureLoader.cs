using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using System.IO;

public class TextureLoader : MonoBehaviour
{
    public Image image;

    public string folderPath = "Frames/JPEG/";

    void Start()
    {
        Texture2D tex = GetTextureFromFile();

        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);

        image.sprite = sprite;
    }

    public Texture2D GetTextureFromFile()
    {
        string filePath = "";

        string fileName = folderPath + "Pivot_0_0";

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
            Debug.LogError("Texture doesn't exist");
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
