using PixelCrushers.DialogueSystem;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DialogueManager.StartConversation("Actor1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum GameState
{
    Menu,
    Entrada,
    Dialogo,
    Pregunta,
    Gameplay,
    Respuesta,
    Reaccion,
    Final
}
