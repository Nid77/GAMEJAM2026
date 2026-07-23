using UnityEngine;
using System.Collections.Generic;
using System;


public enum ResourceType { Money, Time, Population, Research } // adapte selon ton jeu

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    private Dictionary<ResourceType, float> resources = new();

    public event Action<ResourceType, float> OnResourceChanged;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        // valeurs initiales
        resources[ResourceType.Money] = 1000f;
        resources[ResourceType.Population] = 100f;
    }

    public float Get(ResourceType type) => resources.TryGetValue(type, out var v) ? v : 0f;

    public bool CanAfford(ResourceType type, float cost) => Get(type) >= cost;

    public void Modify(ResourceType type, float delta)
    {
        resources[type] = Get(type) + delta;
        OnResourceChanged?.Invoke(type, resources[type]);
    }
}