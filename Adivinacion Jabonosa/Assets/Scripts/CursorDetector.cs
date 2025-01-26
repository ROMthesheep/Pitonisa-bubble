using UnityEngine;
using UnityEngine.Splines;

public class CursorDetector : MonoBehaviour
{
    private void Update()
    {
        if (!SplineIsPlaying()&& gameObject.GetComponent<SplineAnimate>().ElapsedTime > 0)
        {
            FindAnyObjectByType<MouseFollower>().StopFollowing();
            FindAnyObjectByType<GameManager>().ChangeGameState(GameState.Dialogo);
            FindAnyObjectByType<GameManager>().EndGameplay();
            GameObject.Destroy(gameObject.transform.parent.gameObject);
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