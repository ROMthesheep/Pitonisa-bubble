using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    float size = 15;
    CursorDetector splineTarget;
    MouseFollower cursor;
    float timeCounter = 0;
    Renderer rend, videoRend;

    [SerializeField]
    float sizeStep = .5f;


    private float bubbleDistorsionAmount = 0f;
    [Range(0.0f, 0.5f)]
    public float bubbleDistorsionK = 0f;
    [Range(0.0f, 0.5f)]
    public float videoDistorsionK = 0f;
    private float videoDistorsionScale = 0f;

    private void Start()
    {
        splineTarget = FindObjectOfType<CursorDetector>();
        cursor = FindObjectOfType<MouseFollower>();
        rend = GetComponent<MeshRenderer>();
        videoRend = gameObject.transform.GetChild(0).GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        timeCounter += Time.deltaTime;

        if (CursorIsFollowingTheTarget() && timeCounter > .05f)
        {
            size += sizeStep;
            timeCounter = 0;
        }
        else if (timeCounter > .05f)
        {
            size -= sizeStep;
            timeCounter = 0;
        }


        size = Math.Clamp(size, 3, 25);
        float newBubbleDistorsion = Mathf.Lerp(rend.material.GetFloat("_distortAmount"), size * bubbleDistorsionK, sizeStep);
        float newVideoDistorsion = Mathf.Lerp(videoRend.material.GetFloat("_distortScale"), size * videoDistorsionK * 0.01f, sizeStep);

        Debug.Log("BuBbledistortAmount" + newBubbleDistorsion);
        Debug.Log("VideodistortAmount" + newVideoDistorsion);
        rend.material.SetFloat("_distortAmount", newBubbleDistorsion);
        videoRend.material.SetFloat("_distortScale", newVideoDistorsion);

        gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, new Vector3(size / 10f, size / 10f, size / 10f), sizeStep);
    }

    //private void Update()
    //{
    //    timeCounter += Time.deltaTime;

    //}

    public void reset()
    {
        timeCounter = 0;
        size = 15;
        splineTarget = FindObjectOfType<CursorDetector>();
    }

    private void updateBubble()
    {


        rend.material.SetFloat("_distortAmount", bubbleDistorsionAmount);
        videoRend.material.SetFloat("_distortionScale", videoDistorsionScale);
    }

    private bool CursorIsFollowingTheTarget()
    {
        return splineTarget.SplineIsPlaying() && cursor.distanceToTarget < 1;
    }
}
