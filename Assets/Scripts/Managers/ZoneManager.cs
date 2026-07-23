using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ZoneManager : MonoBehaviour
{
    public static ZoneManager Instance { get; private set; }
    [SerializeField] private List<ProblemZone> zones;

    private void Awake() { Instance = this; }

    public ProblemZone GetRandomZone()
    {
        int totalWeight = zones.Sum(z => z.weight);
        int roll = UnityEngine.Random.Range(0, totalWeight);
        int acc = 0;
        foreach (var z in zones)
        {
            acc += z.weight;
            if (roll < acc) return z;
        }
        return zones[0];
    }
}