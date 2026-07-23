using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Collections.Generic;

public class ProblemPopupUI : MonoBehaviour
{
    [Header("Refs UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private Image iconImage;

    [Header("Boutons de réponse")]
    [SerializeField] private Transform answerContainer; // parent avec un Layout Group
    [SerializeField] private GameObject answerButtonPrefab; // prefab avec un TMP_Text + Button

    private Problem currentProblem;

    private void Awake()
    {
        panel.SetActive(false);
    }

    public void Show(Problem problem)
    {
        currentProblem = problem;
        var data = problem.data;

        titleText.text = data.title;
        questionText.text = data.questionText;
        iconImage.sprite = data.icon;

        ClearAnswerButtons();
        foreach (var answer in data.answers)
            CreateAnswerButton(answer);

        panel.SetActive(true);
    }

    private void CreateAnswerButton(ProblemAnswer answer)
    {
        var go = Instantiate(answerButtonPrefab, answerContainer);
        var button = go.GetComponent<Button>();
        var label = go.GetComponentInChildren<TMP_Text>();

        label.text = BuildAnswerLabel(answer);

        bool canAfford = answer.costs.All(c => ResourceManager.Instance.CanAfford(c.type, c.amount));
        button.interactable = canAfford;

        button.onClick.AddListener(() => OnAnswerChosen(answer));
    }

    private string BuildAnswerLabel(ProblemAnswer answer)
    {
        string label = answer.answerText;
        foreach (var cost in answer.costs)
            label += $"\n-{cost.amount} {cost.type}";
        return label;
    }

    private void OnAnswerChosen(ProblemAnswer answer)
    {
        currentProblem.Resolve(answer);
        Close();
    }

    private void ClearAnswerButtons()
    {
        foreach (Transform child in answerContainer)
            Destroy(child.gameObject);
    }

    public void Close()
    {
        currentProblem = null;
        panel.SetActive(false);
    }
}