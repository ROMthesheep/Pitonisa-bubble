using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;

public class MouseFollower : MonoBehaviour
{
    private UnityEngine.Vector3 pos;
    public float offset = 14f;

    bool isFollowing = false;
    public float distanceToTarget { get; private set; }
    [SerializeField]
    GameObject target;

    void Update()
    {
        pos = Input.mousePosition;
        pos.z = offset;
        transform.position = Camera.main.ScreenToWorldPoint(pos);

        if (isFollowing)
        {
            distanceToTarget = (transform.position - target.transform.position).magnitude;
        }
    }

    public void StartFollowing()
    {
        isFollowing = true;
    }

    public void StopFollowing()
    {
        isFollowing = false;
    }
}
