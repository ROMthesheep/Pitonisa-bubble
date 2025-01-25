using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    float size = 50;
    CursorDetector splineTarget;
    MouseFollower cursor;
    float timeCounter = 0;
    Renderer rend, videoRend;

    [SerializeField]
    float sizeStep = .5f;

    private void Start()
    {
        splineTarget = FindObjectOfType<CursorDetector>();
        cursor = FindObjectOfType<MouseFollower>();
        rend = GetComponent<MeshRenderer>();
        videoRend = gameObject.GetComponentInChildren<MeshRenderer>();
    }

    private void Update()
    {
        timeCounter += Time.deltaTime;

        if (CursorIsFollowingTheTarget() && timeCounter > .05f)
        {
            size += sizeStep;
            rend.material.SetFloat("_distortAmount", 10f);
            videoRend.material.SetFloat("_distortionScale", 10f);
            timeCounter = 0;
        }
        else if (timeCounter > .05f)
        {
            size -= sizeStep;
            rend.material.SetFloat("_distortAmount", 2f);
            videoRend.material.SetFloat("_distortionScale", 1f);
            timeCounter = 0;
        }

        gameObject.transform.localScale = new Vector3(size / 10f, size / 10f, size / 10f);

        if (size > 100)
        {
            size = 100;
        }
    }

    private bool CursorIsFollowingTheTarget()
    {
        return splineTarget.SplineIsPlaying() && cursor.distanceToTarget < 1;
    }
}
