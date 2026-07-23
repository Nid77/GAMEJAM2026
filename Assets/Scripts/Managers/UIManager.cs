using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private ProblemPopupUI problemPopup;
    [SerializeField] private EventPopupUI eventPopup;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        RandomEventManager.Instance.OnNewEvent += eventPopup.Show;
    }

    public void OpenProblemPopup(Problem problem) => problemPopup.Show(problem);
}