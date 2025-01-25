using PixelCrushers.DialogueSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public GameObject bubble;

    public static event Action<GameState> OnGameStateChanged;

    private VideoPlayer videoPlayer;

    private GameState gameState = GameState.Menu;

    private IEnumerator FadeMenuCoroutine;

    private GameObject Menu;

    private List<int> flujoPersonajes;

    private int score;

    // Spawner logic
    public GameObject[] hexes;
    public Vector3 spawnPoint;

    private bool isHexing;
    private float hexCD;
    private GameObject currentWalker;

    [Header("Visions")]
    public VideoClip[] actor1Visions;
    public VideoClip[] actor2Visions;
    public VideoClip[] actor3Visions;
    public VideoClip[] actor4Visions;
    public VideoClip[] actor5Visions;

    private void Awake()
    {
        flujoPersonajes = new List<int> { 1, 0 };

        score = 0;

        isHexing = false;

        hexCD = 2.0f;
    }

    private void Start()
    {
        videoPlayer = bubble.GetComponentInChildren<VideoPlayer>();
    }

    private void Update()
    {
        Debug.Log(hexCD);
        startSpawnPocess();
    }

    public void ChangeGameState(GameState newGameState)
    {
        Debug.Log("Se ha cambiado el estado de juego a " + newGameState);
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

    private void startSpawnPocess()
    {
        if (!isHexing && gameState == GameState.Gameplay)
        {
            if (hexCD <= 0)
            {
                SpawnHex(UnityEngine.Random.Range(0, hexes.Length));
                isHexing = true;
                hexCD = 2.0f;
            } else
            {
                hexCD -= Time.deltaTime;
            }
            
        }
    }

    private void SpawnHex(int hexIndex)
    {
        GameObject newHex = Instantiate(hexes[hexIndex], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);

        newHex.transform.Rotate(90.0f, -0f, 0.0f);

        newHex.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

        newHex.transform.position = spawnPoint;
    }

    public void SiguienteDialogo()
    {
        DialogueManager.StartConversation("Actor" + flujoPersonajes[0] + "_" + flujoPersonajes[1]);
    }

    public void Gameplay()
    {
        ManageBuble();

        ChangeGameState(GameState.Gameplay);
        //Comienza el spawner

        //Aparece la burbuja
    }

    private void ManageBuble()
    {
        bubble.SetActive(true);
        setVision();
        videoPlayer.Play();
    }

    private void setVision()
    {
        switch (flujoPersonajes[0])
        {
            case 1:
                videoPlayer.clip = actor1Visions[flujoPersonajes[1]];
                break;
            case 2:
                videoPlayer.clip = actor2Visions[flujoPersonajes[1]];
                break;
            case 3:
                videoPlayer.clip = actor3Visions[flujoPersonajes[1]];
                break;
            case 4:
                videoPlayer.clip = actor4Visions[flujoPersonajes[1]];
                break;
            case 5:
                videoPlayer.clip = actor5Visions[flujoPersonajes[1]];
                break;
        }
    }

    public void EndGameplay()
    {
        bubble.SetActive(false);

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
