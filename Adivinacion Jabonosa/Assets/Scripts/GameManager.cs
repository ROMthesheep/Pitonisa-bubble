using PixelCrushers.DialogueSystem;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    private GameState gameState = GameState.Menu;

    public GameObject bubble;

    private VideoPlayer videoPlayer;

    public static event Action<GameState> OnGameStateChanged;

    private IEnumerator FadeMenuCoroutine;

    private GameObject Menu;

    private int[] flujoPersonajes;

    private int score;

    private void Start()
    {
        videoPlayer = bubble.GetComponentInChildren<VideoPlayer>();

        flujoPersonajes = new int [2];
        flujoPersonajes[0] = 1;
        flujoPersonajes[1] = 1;

        score = 0;

    }

    public void ChangeGameState(GameState newGameState)
    {
        OnGameStateChanged?.Invoke(newGameState);

        gameState = newGameState;
    }

    public void StartButton()
    {
        Menu = GameObject.FindGameObjectWithTag("Menu");

        Menu.GetComponentInChildren<Button>().gameObject.SetActive(false);

        FadeMenuCoroutine = FadeMenu();

        StartCoroutine(FadeMenuCoroutine);
    }

    private IEnumerator FadeMenu()
    {
        float counter = 0.0f;

        while(counter < 0.1f)
        {
            Menu.gameObject.GetComponent<RawImage>().color = new Color(Menu.gameObject.GetComponent<RawImage>().color.r,
                                                                       Menu.gameObject.GetComponent<RawImage>().color.g,
                                                                       Menu.gameObject.GetComponent<RawImage>().color.g,
                                                                       counter);

            counter += Time.fixedDeltaTime;

            yield return new WaitForSeconds(0.1f);
        }

        Menu.gameObject.GetComponent<RawImage>().enabled = false;

        SiguienteDialogo();
    }

    public void SiguienteDialogo()
    {
        DialogueManager.StartConversation("Actor" + flujoPersonajes[0] + "_" + flujoPersonajes[1]);
    }

    public void Gameplay()
    {
        videoPlayer.clip = (VideoClip)Resources.Load("Metraje/" + flujoPersonajes[0] + "_" + flujoPersonajes[1]);

        //Comienza el spawner

        //Aparece la burbuja
    }

    public void EndGameplay()
    {
        SiguientePaso();

        SiguienteDialogo();
    }

    public void Success()
    {
        score += 1;

        SiguientePersonaje();

        SiguienteDialogo();
    }

    public void Failure()
    {
        SiguientePersonaje();
    }

    private void SiguientePersonaje()
    {
        flujoPersonajes[0] += 1;
        flujoPersonajes[1] = 1;
    }

    private void SiguientePaso()
    {
        flujoPersonajes[1] += 1;
    }
}

public enum GameState
{
    Menu,
    Dialogo,
    Gameplay,
    Respuesta,
    Reaccion,
    Salida,
    Pausa,
    Final
}
