using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Linq;

public class ImageSwapper : MonoBehaviour
{
    public Camera viewCamera;

    public Image image;

    Dictionary<Vector3, Sprite> imageDict = new Dictionary<Vector3, Sprite>();

    void Start()
    {
        TextureLoader.e.LoadImages();

        var pivots = CameraPivot.GetAllInScene();
        Vector3[] points = pivots[0].Points;


        // Map images to positions
        for (int i = 0; i < points.Length; i++)
        {
            imageDict.Add(points[i], TextureLoader.e.sprites[i]);
        }
    }

    void Update()
    {
        Vector3[] points = imageDict.Keys.ToArray();

        Vector3 cameraPosition = viewCamera.transform.position;
        int i = MathUtils.GetClosestPointIndex(cameraPosition, points);

        image.sprite = TextureLoader.e.sprites[i];
    }
}
