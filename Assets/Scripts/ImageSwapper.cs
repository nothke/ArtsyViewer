using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Linq;

public class ImageSwapper : MonoBehaviour
{
    public Camera viewCamera;

    public Image image;
    public SpriteRenderer spriteRenderer;

    CameraPivot pivot;

    PointTowardsCamera spritePointer;

    void Start()
    {
        TextureLoader.e.LoadImages();

        var pivots = CameraPivot.GetAllInScene();
        pivot = pivots[0];
        //Vector3[] points = pivots[0].Points;

        spritePointer = spriteRenderer.GetComponent<PointTowardsCamera>();


    }

    int prevIndex;

    void Update()
    {
        Vector3[] points = (Vector3[])pivot.Points.Clone();

        for (int p = 0; p < points.Length; p++)
        {
            points[p] = pivot.transform.TransformPoint(points[p]);
        }

        Vector3 cameraPosition = viewCamera.transform.position;
        int index = MathUtils.GetClosestPointIndex(cameraPosition, points);

        if (prevIndex != index)
        {
            if (spriteRenderer)
            {
                Sprite _sprite = TextureLoader.e.sprites[index];
                spriteRenderer.sprite = _sprite;
                spritePointer.UpdateLook();
            }
            else
                image.sprite = TextureLoader.e.sprites[index];
        }

        prevIndex = index;
    }
}
