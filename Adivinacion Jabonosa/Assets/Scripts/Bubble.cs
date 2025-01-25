using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    int size = 50;
    CursorDetector splineTarget;
    MouseFollower cursor;
    float timeCounter = 0;

    private void Start()
    {
        splineTarget = FindObjectOfType<CursorDetector>();
        cursor = FindObjectOfType<MouseFollower>();
    }

    private void Update()
    {
        timeCounter += Time.deltaTime;

        if(splineTarget.SplineIsPlaying() && cursor.distanceToTarget < 1 && timeCounter > .2f)
        {
            size++;
            timeCounter = 0;
        }
        else if(timeCounter > .2f)
        {
            size--;
            timeCounter = 0;
        }

        gameObject.transform.localScale = new Vector3(size / 10f, size / 10f, size / 10f);

        if(size > 100)
        {
            size = 100;
        }
    }
}
