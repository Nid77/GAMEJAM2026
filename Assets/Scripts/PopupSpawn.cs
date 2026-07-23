using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupSpawn : MonoBehaviour
{   
    public ProblemData problemData;
    public static PopupSpawn Instance { get; private set; }
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private Transform answers;
    public GameObject answerButtonPrefab;

    private Problem currentProblem;
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this; 
        gameObject.SetActive(false);

    }

    // Update is called once per frame
    public void UpdatePopup(Problem problem)

    {   
        currentProblem = problem;
        ProblemData data = problem.data;
        gameObject.SetActive(false);

        for (int i = answers.childCount - 1; i >= 0; i--)
        {   
            Destroy(answers.GetChild(i).GetChild(0).gameObject);
            Destroy(answers.GetChild(i).gameObject);
        }
        
        problemData = data;
        questionText.text = problemData.questionText;
        gameObject.SetActive(true);

        for (int i = 0; i < problemData.answers.Count; i++)
        {
            GameObject answerButton = Instantiate(answerButtonPrefab, answers);
            answerButton.GetComponent<Image>().enabled = true;
            answerButton.transform.GetChild(0).GetComponent<TMP_Text>().text = problemData.answers[i].answerText;
            answerButton.transform.GetChild(0).GetComponent<TMP_Text>().enabled = true;
        }
    }

    public void OnAnswerSelected(int index)
    {
        ProblemAnswer selectedAnswer = problemData.answers[index];
        currentProblem.Resolve(selectedAnswer);
        gameObject.SetActive(false);
    }
}
