using UnityEngine;
using UnityEngine.Splines;

public class CursorDetector : MonoBehaviour
{
    private void Update()
    {
        if (!SplineIsPlaying())
        {
            FindAnyObjectByType<MouseFollower>().StopFollowing();
        }
    }

    public bool SplineIsPlaying()
    {
        return gameObject.GetComponent<SplineAnimate>().IsPlaying;
    }

    private void OnTriggerEnter(Collider other) {        
        FindAnyObjectByType<MouseFollower>().StartFollowing();
        gameObject.GetComponent<SplineAnimate>().Play();
    }
}