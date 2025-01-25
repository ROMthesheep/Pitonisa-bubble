using UnityEngine;
using UnityEngine.Splines;

public class CursorDetector : MonoBehaviour
{
    

    private void Start() {
        
    }

    private void StartPathing() {
        GetComponent<SplineAnimate>().Play();
    }

    private void OnTriggerEnter(Collider other) {
        gameObject.GetComponent<SplineAnimate>().Play();
    }
}