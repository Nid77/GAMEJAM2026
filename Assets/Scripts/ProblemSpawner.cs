using UnityEngine;
using System.Collections.Generic;


public class ProblemSpawner : MonoBehaviour
{
    public static ProblemSpawner Instance { get; private set; }

    [SerializeField] private GameObject problemPrefab;
    [SerializeField] private List<ProblemData> problemPool;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private int maxActiveProblems = 8;
    private Transform problemParent;

    private List<Problem> activeProblems = new();
    private float timer;

    private void Awake() { 
        Instance = this; 
        problemParent = this.transform;
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver) return;
        timer += Time.deltaTime;
        if (timer >= spawnInterval && activeProblems.Count < maxActiveProblems)
        {
            timer = 0f;
            SpawnProblem();
        }
    }

    public void SpawnProblem()
    {
        var zone = ZoneManager.Instance.GetRandomZone();
        var pos = zone.GetRandomPoint();
        var data = problemPool[UnityEngine.Random.Range(0, problemPool.Count)];

        var go = Instantiate(problemPrefab, pos, Quaternion.identity, problemParent);
        var problem = go.GetComponent<Problem>();
        problem.data = data;
        activeProblems.Add(problem);
    }

    public void NotifyProblemResolved(Problem p) => activeProblems.Remove(p);
}