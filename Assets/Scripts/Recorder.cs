using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class Recorder : MonoBehaviour
{

    public Transform[] pivots;

    public int views = 8;

    void Start()
    {
        StartCoroutine(EveryFrameAScreenshot());
    }


    IEnumerator EveryFrameAScreenshot()
    {
        Vector3[] dirs = GetDirs();
        Transform t = Camera.main.transform;

        yield return null;

        for (int i = 0; i < pivots.Length; i++)
        {
            if (!pivots[i]) continue;

            t.position = pivots[i].position;

            for (int v = 0; v < views; v++)
            {
                t.rotation = Quaternion.LookRotation(dirs[v]);

                Application.CaptureScreenshot("Frames/Pivot_" + i + "_" + v + ".png");

                yield return null;
            }
        }
    }

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

            foreach (var dir in dirs)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(pivot.position, pivot.position + dir);
            }
        }
    }
}
