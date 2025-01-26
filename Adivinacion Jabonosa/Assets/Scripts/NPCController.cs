using UnityEngine;

public class NPCController : MonoBehaviour
{
    private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = gameObject.GetComponent<Animator>();
    }

    public void OnGameStateChanged(GameState newGameState)
    {
        if(newGameState == GameState.Dialogo)
        {
            myAnimator.Play("explota");
        }
        else if(newGameState == GameState.Gameplay)
        {
            myAnimator.Play("divination");
        }
    }
}
