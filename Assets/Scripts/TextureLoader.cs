using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class TextureLoader : MonoBehaviour
{
    public static TextureLoader e;
    void Awake() { e = this; }

    public string folderPath = "Frames/JPEG/";

    public Sprite[] sprites;

    public void LoadImages()
    {
        int fileCount = Directory.GetFiles(folderPath).Length;

        Debug.Log("Files in folder " + fileCount);

        sprites = new Sprite[fileCount];

        for (int i = 0; i < fileCount; i++)
        {
            Texture2D tex = GetTextureFromFile("0_" + i);

            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            sprites[i] = sprite;
        }
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
            Debug.LogError("Texture " + fileName + " doesn't exist");
            return null;
        }

        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(File.ReadAllBytes(filePath));

        return tex;
    }
}
