using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class Recorder : MonoBehaviour
{
    public Camera recordingCamera;

    CameraPivot[] cameraPivots;

    public int views = 8;
    public float distance = 5;

    void Start()
    {
        cameraPivots = FindObjectsOfType<CameraPivot>();

        StartCoroutine(EveryFrameAScreenshot());
    }

    IEnumerator EveryFrameAScreenshot()
    {
        //Vector3[] dirs = GetDirs();
        Transform t = recordingCamera.transform;

        yield return null;

        if (cameraPivots == null || cameraPivots.Length == 0)
        {
            Debug.LogError("No camera pivots found in scene");
            yield break;
        }

        for (int c = 0; c < cameraPivots.Length; c++)
        {
            Vector3[] points = cameraPivots[c].Points;

            for (int p = 0; p < points.Length; p++)
            {
                Vector3 point = cameraPivots[c].transform.TransformPoint(points[p]);

                t.position = point;
                t.rotation = Quaternion.LookRotation(cameraPivots[c].transform.position - point);

                string filePath = "Frames/" + c + "_" + p + ".png";
                

                yield return new WaitForEndOfFrame();

                GL.Clear(true, true, Color.clear);

                yield return null;

                yield return new WaitForEndOfFrame();

                //Application.CaptureScreenshot(filePath);
                CaptureTransparentScreenshot(filePath);

                yield return null;
            }
        }
    }

    public void CaptureTransparentScreenshot(string filePath)
    {

        Texture2D sshot = new Texture2D(Screen.width, Screen.height);
        sshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        sshot.Apply();

        byte[] pngShot = sshot.EncodeToPNG();
        Destroy(sshot);
        File.WriteAllBytes(filePath, pngShot);
    }
}
