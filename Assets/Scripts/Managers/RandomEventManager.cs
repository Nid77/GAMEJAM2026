using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "EventData", menuName = "Game/RandomEventData")]
public class RandomEventData : ScriptableObject
{
    [TextArea] public string questionText;
    public List<ResourceCost> yesEffects; // bonus/malus si "oui"
    // "non" = rien ne se passe, pas besoin de champ
}

public class RandomEventManager : MonoBehaviour
{
    public static RandomEventManager Instance { get; private set; }

    [SerializeField] private List<RandomEventData> eventPool;
    [SerializeField] private float cooldownDuration = 15f;

    private float cooldownTimer;
    private float timeWhenQuestionAppeared;
    private RandomEventData currentEvent;

    public event Action<RandomEventData> OnNewEvent;

    private void Awake() { Instance = this; }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver) return;
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0f) TriggerNewEvent();
    }

    private void TriggerNewEvent()
    {
        currentEvent = eventPool[UnityEngine.Random.Range(0, eventPool.Count)];
        cooldownTimer = cooldownDuration;
        timeWhenQuestionAppeared = Time.time;
        OnNewEvent?.Invoke(currentEvent);
    }

    // Appelé par l'UI quand le joueur répond
    public void Answer(bool yes)
    {
        float elapsed = Time.time - timeWhenQuestionAppeared;
        float penalty = cooldownDuration - elapsed; // "temps total max - temps mis"

        if (penalty > 0f)
            GameManager.Instance.SubtractTime(penalty);

        if (yes)
        {
            foreach (var effect in currentEvent.yesEffects)
                ResourceManager.Instance.Modify(effect.type, effect.amount);
        }
        // "non" → rien

        currentEvent = null;
    }
}