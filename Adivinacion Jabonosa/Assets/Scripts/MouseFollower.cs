using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;

public class MouseFollower : MonoBehaviour
{
    

    private UnityEngine.Vector3 pos;
    public float offset = 14f;


    // Update is called once per frame
    void Update()
    {
        pos = Input.mousePosition;
        pos.z = offset;
        transform.position = Camera.main.ScreenToWorldPoint(pos);
    }

}
