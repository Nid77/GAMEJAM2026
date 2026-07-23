using UnityEngine;
using System.Collections.Generic;


public class ProblemSpawner : MonoBehaviour
{
    public static ProblemSpawner Instance { get; private set; }

    [SerializeField] private GameObject problemPrefab;
    [SerializeField] private List<ProblemData> problemPool;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private int maxActiveProblems = 8;

    private List<Problem> activeProblems = new();
    private float timer;

    private void Awake() { Instance = this; }

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

    private void SpawnProblem()
    {
        var zone = ZoneManager.Instance.GetRandomZone();
        Debug.Log($"Spawning problem in zone: {zone.name}");
        var pos = zone.GetRandomPoint();
        Debug.Log($"Spawning problem at position: {pos}");
        var data = problemPool[UnityEngine.Random.Range(0, problemPool.Count-1)];
        Debug.Log($"Spawning problem with data: {data.name}");

        var go = Instantiate(problemPrefab, pos, Quaternion.identity);
        Debug.Log($"Instantiated problem prefab: {go.name}");
        var problem = go.GetComponent<Problem>();
        Debug.Log($"Got Problem component: {problem.name}");
        problem.data = data;
        activeProblems.Add(problem);
    }

    public void NotifyProblemResolved(Problem p) => activeProblems.Remove(p);
}