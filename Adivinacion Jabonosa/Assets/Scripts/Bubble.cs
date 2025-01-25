using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    float size = 50;
    CursorDetector splineTarget;
    MouseFollower cursor;
    float timeCounter = 0;
    [SerializeField]
    float sizeStep = .5f;

    private void Start()
    {
        splineTarget = FindObjectOfType<CursorDetector>();
        cursor = FindObjectOfType<MouseFollower>();
    }

    private void Update()
    {
        timeCounter += Time.deltaTime;

        if(splineTarget.SplineIsPlaying() && cursor.distanceToTarget < 1 && timeCounter > .05f)
        {
            size += sizeStep;
            timeCounter = 0;
        }
        else if(timeCounter > .05f)
        {
            size -= sizeStep;
            timeCounter = 0;
        }

        gameObject.transform.localScale = new Vector3(size / 10f, size / 10f, size / 10f);

        if(size > 100)
        {
            size = 100;
        }
    }
}
