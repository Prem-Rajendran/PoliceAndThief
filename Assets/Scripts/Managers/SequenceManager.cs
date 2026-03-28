using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SequenceManager : MonoBehaviour
{
    public static SequenceManager Instance;

    [SerializeField] private GameObject m_MainMenuCamera;

    private string m_CurrentScene;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.OnGameWin += OnGameWin;
        GameManager.Instance.OnGameLose += OnGameLose;
        GameManager.Instance.OnGameOver += OnGameOver;
        UIManager.Instance.Show<MainMenuScreen>();
    }

    public void LoadGameScene(string sceneName)
    {
        m_CurrentScene = sceneName;
        SceneManager.LoadScene(m_CurrentScene, LoadSceneMode.Additive);
        UIManager.Instance.Show<HUD>();
        m_MainMenuCamera.gameObject.SetActive(false);
        GameManager.Instance.StartGame();
    }

    public void GoToMainMenu()
    {
        UnloadGameScene();
        UIManager.Instance.Show<MainMenuScreen>();
        m_MainMenuCamera.gameObject.SetActive(true);
    }

    public void UnloadGameScene()
    {
        SceneManager.UnloadSceneAsync(m_CurrentScene);
        
    }
    private void OnEnable()
    {
      
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameWin -= OnGameWin;
        GameManager.Instance.OnGameLose -= OnGameLose;
        GameManager.Instance.OnGameOver -= OnGameOver;
    }


    private void OnGameLose()
    {
        
    }

    private void OnGameWin()
    {
        
    }

    private void OnGameOver(bool isWon)
    {
        UIManager.Instance.Show<GameOverScreen>();
    }
}
