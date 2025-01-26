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
    AnimationCurve distortionScale;
    [SerializeField]
    float sizeStep = .5f;


    private float bubbleDistorsionAmount = 0f;
    private float bubbleDistorsionScale = 0f;
    private float videoDistorsionAmount = 0f;
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
            rend.material.SetFloat("_distortAmount", 10f);
            videoRend.material.SetFloat("_distortionScale", distortionScale.Evaluate(1));
            timeCounter = 0;
        }
        else if (timeCounter > .05f)
        {
            size -= sizeStep;
            rend.material.SetFloat("_distortAmount", 2f);
            videoRend.material.SetFloat("_distortionScale", distortionScale.Evaluate(5));
            timeCounter = 0;
        }   

        if (size > 25)
        {
            size = 25;
        }

        gameObject.transform.localScale = new Vector3(size / 10f, size / 10f, size / 10f);
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
