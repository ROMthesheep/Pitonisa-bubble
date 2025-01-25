using PixelCrushers.DialogueSystem;
using System;
using System.Collections;
using System.Collections.Generic;
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

    private List<int> flujoPersonajes;

    private int score;

    private void Awake()
    {
        flujoPersonajes = new List<int> { 1, 1 };

        score = 0;
    }

    private void Start()
    {
        videoPlayer = bubble.GetComponentInChildren<VideoPlayer>();
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

        Debug.Log(flujoPersonajes);
        SiguienteDialogo();
    }

    public void SiguienteDialogo()
    {
        Debug.Log("Actor" + flujoPersonajes[0] + "_" + flujoPersonajes[1]);
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
