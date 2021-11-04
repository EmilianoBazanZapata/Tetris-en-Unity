using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool InGame;
    public bool GameOver;

    public Canvas StartGame;
    public Canvas EndGame;

    public static GameManager SharedInstance;

    private void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
        }
    }
    private void Start()
    {
        this.InGame = true;
        this.ShowCanvasGame();
    }
    public void FinishGame()
    {
        this.InGame = false;
        this.GameOver = true;
        this.ShowCanvasGame();
    }
    //reiniciar jeugo
    public void RestartGame()
    {
        SceneManager.LoadScene("TetrisScene");
    }
    //metodo para salir del juego
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


    private void ShowCanvasGame()
    {
        if(InGame == true)
        {
            StartGame.enabled = true;
            EndGame.enabled = false;
        }
        else if(GameOver == true)
        {
            EndGame.enabled = true;
            StartGame.enabled = false;
        }
    }
}
