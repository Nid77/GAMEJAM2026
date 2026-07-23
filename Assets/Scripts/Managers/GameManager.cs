using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private float totalCountdown = 300f; // durée totale de la partie
    private float currentTime;
    public bool IsGameOver { get; private set; }

    public event Action<float> OnTimeChanged;
    public event Action OnGameOver;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        currentTime = totalCountdown;
    }

    private void Update()
    {
        if (IsGameOver) return;
        currentTime -= Time.deltaTime;
        OnTimeChanged?.Invoke(currentTime);

        if (currentTime <= 0f) EndGame();
    }

    // Appelée par RandomEventManager quand on répond trop vite
    public void SubtractTime(float amount)
    {
        currentTime -= amount;
        OnTimeChanged?.Invoke(currentTime);
    }

    public void EndGame()
    {
        IsGameOver = true;
        OnGameOver?.Invoke();
    }
}
