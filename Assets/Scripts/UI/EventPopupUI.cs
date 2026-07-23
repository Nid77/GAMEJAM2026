using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventPopupUI : MonoBehaviour
{
    [Header("Refs UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private Slider cooldownSlider; // optionnel, pour visualiser le temps restant
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    [SerializeField] private float cooldownDuration = 15f; // doit matcher RandomEventManager

    private bool eventActive;
    private float timeRemaining;

    private void Awake()
    {
        panel.SetActive(false);
        yesButton.onClick.AddListener(() => Answer(true));
        noButton.onClick.AddListener(() => Answer(false));
    }

    private void Update()
    {
        if (!eventActive) return;

        timeRemaining -= Time.deltaTime;
        if (cooldownSlider != null)
            cooldownSlider.value = timeRemaining / cooldownDuration;

        if (timeRemaining <= 0f)
            Close(); // le joueur n'a pas répondu à temps, pas de pénalité, pas de bonus
    }

    // Branché sur RandomEventManager.OnNewEvent dans UIManager
    public void Show(RandomEventData data)
    {
        questionText.text = data.questionText;
        timeRemaining = cooldownDuration;
        eventActive = true;
        panel.SetActive(true);
    }

    private void Answer(bool yes)
    {
        RandomEventManager.Instance.Answer(yes);
        Close();
    }

    private void Close()
    {
        eventActive = false;
        panel.SetActive(false);
    }
}