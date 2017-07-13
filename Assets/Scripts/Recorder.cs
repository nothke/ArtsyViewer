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

                Application.CaptureScreenshot("Frames/" + c + "_" + p + ".png");

                yield return null;
            }
        }

        /*
        for (int i = 0; i < pivots.Length; i++)
        {
            if (!pivots[i]) continue;

            for (int v = 0; v < views; v++)
            {
                t.position = pivots[i].position + dirs[v] * distance;
                t.rotation = Quaternion.LookRotation(-dirs[v]);


                Application.CaptureScreenshot("Frames/" + i + "_" + v + ".png");

                yield return null;
            }
        }*/
    }

    /*
    Vector3[] GetDirs()
    {
        Vector3[] dirs = new Vector3[views];

        float theta = (2.0f * Mathf.PI) / views;

        for (int i = 0; i < dirs.Length; i++)
        {
            float x = Mathf.Cos(theta * i);
            float y = Mathf.Sin(theta * i);

            dirs[i] = new Vector3(x, 0, y);
        }

        return dirs;
    }

    private void OnDrawGizmos()
    {
        if (views < 2) views = 2;

        Vector3[] dirs = GetDirs();

        if (pivots == null) return;

        foreach (var pivot in pivots)
        {
            if (!pivot) continue;

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(pivot.position, 0.2f);

            for (int i = 0; i < dirs.Length; i++)
            {
                Vector3 origin = pivot.position + dirs[i] * distance;
                Vector3 dir = -dirs[i];

                Gizmos.color = Color.magenta;
                Gizmos.DrawSphere(origin, 0.1f);
                Gizmos.DrawRay(origin, dir);
            }
        }
    }*/
}
