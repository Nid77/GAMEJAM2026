using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupSpawn : MonoBehaviour
{   
    public ProblemData problemData;
    public static PopupSpawn Instance { get; private set; }
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private Transform answers;
    public GameObject answerButtonPrefab;
    public GameObject buttonLinePrefab;

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

        foreach(Transform line in answers)
        {
            for (int i = line.childCount - 1; i >= 0; i--)
        {   
            //Debug.Log("Destroying button " + i + " in line " + line.GetSiblingIndex());
            Destroy(line.GetChild(i).GetChild(0).gameObject);
            Destroy(line.GetChild(i).gameObject);
        }
            Destroy(line.gameObject);
        }
        
        
        problemData = data;
        questionText.text = problemData.questionText;
        costText.text = "";
        gameObject.SetActive(true);

        GameObject currentLine = Instantiate(buttonLinePrefab, answers);


        for (int i = 0; i < problemData.answers.Count; i++)
        {
            if (i == 2)
            {
                currentLine = Instantiate(buttonLinePrefab, answers);
            }
            //Debug.Log("Button:" + i + " Line: " + currentLine.GetSiblingIndex());

            GameObject answerButton = Instantiate(answerButtonPrefab, currentLine.transform);
            answerButton.GetComponent<Image>().enabled = true;
            //Debug.Log("Setting button text for answer " + i + ": " + problemData.answers[i].answerText);
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

    public void OnAnswerHovered(int index)
    {   
        if (index < 0 || index >= problemData.answers.Count)
        {
            costText.text = "";
            return;
        }       
        ProblemAnswer selectedAnswer = problemData.answers[index];
        string newText = "";
        foreach (var cost in selectedAnswer.costs)
        {
            newText += $"{cost.amount} {cost.type}\n";
            //Debug.Log(newText);
        }
        costText.text = newText;
    }
}
