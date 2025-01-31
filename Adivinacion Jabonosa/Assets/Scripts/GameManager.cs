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
    public GameObject cursor;

    public GameObject Fin;

    public GameObject MenuPlayButton;
    public AudioManager audioManager;

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


    public GameObject[] cartas;

    public GameObject[] npcs;

    private void Awake()
    {
        flujoPersonajes = new List<int> { 1, 1 };

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
        //startSpawnPocess();
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

        MenuPlayButton.SetActive(false);

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

        audioManager.PlayDialogueMusic();
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
        GameObject newHex = Instantiate(hexes[hexIndex], spawnPoint, Quaternion.identity);

        GameObject.FindObjectOfType<MouseFollower>().SetTarget(GameObject.FindGameObjectWithTag("Walker"));

        newHex.transform.Rotate(90.0f, -0f, 0.0f);

        newHex.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        bubble.GetComponent<Bubble>().reset();
    }

    public void SiguienteDialogo()
    {
        DialogueManager.StartConversation("Actor" + flujoPersonajes[0] + "_" + flujoPersonajes[1]);
    }

    public void FinConversacion()
    {
        Debug.Log("FinConversacion");
        if (gameState == GameState.Gameplay)
        {
            ManageBuble();

            cursor.SetActive(true);

            // Cursor.visible = false;

            SpawnHex(UnityEngine.Random.Range(0, hexes.Length));

            DialogueManager.StopConversation();

            audioManager.PlayGameplayMusic();
        }
        else if(gameState == GameState.Dialogo)
        {
            audioManager.PlayDialogueMusic();

            SiguienteDialogo();
        }
        else if(gameState == GameState.Final)
        {
            audioManager.PlayEndMusic();

            Final();
        }
    }

    private void Final()
    {
        Fin.gameObject.SetActive(true);

        cartas[score].SetActive(true);
    }

    public void CargarNPCs(int cargar)
    {
        npcs[cargar - 1].SetActive(false);
        npcs[cargar].SetActive(true);
    }

    public void ReiniciarButton()
    {
        flujoPersonajes = new List<int> { 1, 1 };

        score = 0;

        npcs[4].SetActive(false);
        npcs[0].SetActive(true);

        Menu = GameObject.FindGameObjectWithTag("Menu");
        Menu.gameObject.GetComponent<RawImage>().enabled = true;
        MenuPlayButton.SetActive(true);
        Menu.gameObject.GetComponent<RawImage>().color = new Color(Menu.gameObject.GetComponent<RawImage>().color.r,
                                                                       Menu.gameObject.GetComponent<RawImage>().color.g,
                                                                       Menu.gameObject.GetComponent<RawImage>().color.g,
                                                                       1.0f);
        Fin.gameObject.SetActive(false);
    }

    public void SalirButton()
    {
        Application.Quit();
    }

    public void Gameplay()
    {
        ChangeGameState(GameState.Gameplay);
    }

    public void Success()
    {
        score += 1;

        if(flujoPersonajes[0] >= 5)
        {
            ChangeGameState(GameState.Final);
        }
        else
        {
            ChangeGameState(GameState.Dialogo);
            SiguientePersonaje();
        }
    }

    public void Failure()
    {
        if (flujoPersonajes[0] >= 5)
        {
            ChangeGameState(GameState.Final);
        }
        else
        {
            ChangeGameState(GameState.Dialogo);
            SiguientePersonaje();
        }
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
                videoPlayer.clip = actor1Visions[flujoPersonajes[1] - 1];
                break;
            case 2:
                videoPlayer.clip = actor2Visions[flujoPersonajes[1] - 1];
                break;
            case 3:
                videoPlayer.clip = actor3Visions[flujoPersonajes[1] - 1];
                break;
            case 4:
                videoPlayer.clip = actor4Visions[flujoPersonajes[1] - 1];
                break;
            case 5:
                videoPlayer.clip = actor5Visions[flujoPersonajes[1] - 1];
                break;
        }
        

        //videoPlayer.Prepare();
        //Debug.LogError(videoPlayer.clip.ToString());
        
        //Invoke("PlayVid", .5f);
    }

    void PlayVid()
    {
        if (!videoPlayer.isPrepared)
            Debug.LogError("Video not prepared");
        videoPlayer.Play();
    }
    public void EndGameplay()
    {
        audioManager.PlayDialogueMusic();
        Debug.Log("EndGameplay");
        bubble.SetActive(false);
        cursor.SetActive(false);
        Cursor.visible = true;

        SiguientePaso();

        SiguienteDialogo();
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
