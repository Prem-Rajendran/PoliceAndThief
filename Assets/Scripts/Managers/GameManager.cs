using UnityEngine;
using System;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool m_IsGameActive = false;

    [SerializeField] private float m_WinScreenDelay;
    [SerializeField] private float m_LoseScreenDelay;
   
    // 1. Define the Events
    public event Action OnGameStart;
    public event Action OnGameWin;
    public event Action OnGameLose;

    // We pass a boolean to GameOver to know who triggered it (optional but helpful)
    public event Action<bool> OnGameOver;

    public bool IsGameActive => m_IsGameActive;


    private void Awake()
    {
        // Singleton pattern for easy access
        Instance = this;
    }

    private void Start()
    {
        //StartGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            TriggerWin();
        }
        else if(Input.GetKeyDown(KeyCode.L))
        {
            TriggerLose();
        }
    }

    public void StartGame()
    {
        m_IsGameActive = true;
        OnGameStart?.Invoke();
       
    }


    // 2. Public methods to trigger the states
    public void TriggerWin(float additionalWinScreenDelay = 0)
    {
        if (!m_IsGameActive) return;

        StartCoroutine(WinCoroutine(m_WinScreenDelay + additionalWinScreenDelay));

    }

    IEnumerator WinCoroutine(float winScreenDelay)
    {
        // Specific Win Logic
        Debug.Log("Game Won!");
        // Transition to common GameOver logic
        EndGame(true);

        yield return new WaitForSecondsRealtime(winScreenDelay);

        OnGameWin?.Invoke();

        Debug.Log("Win Screen Shown");
    }

    public void TriggerLose(float additionLoseScreenDelay = 0)
    {
        if (!m_IsGameActive) return;

        StartCoroutine(LoseCoroutine(m_LoseScreenDelay + additionLoseScreenDelay));
    }

    IEnumerator LoseCoroutine(float loseScreenDelay)
    {
        // Specific Lose Logic
        Debug.Log("Game Lost!");
        // Transition to common GameOver logic
        EndGame(false);

        yield return new WaitForSecondsRealtime(loseScreenDelay);
        OnGameLose?.Invoke();
        Debug.Log("Lose Screen Shown");
    }

    // 3. The Shared Logic
    private void EndGame(bool isWin)
    {
        m_IsGameActive = false;

        // This event handles cleanup shared by both outcomes
        OnGameOver?.Invoke(isWin);
    }

}