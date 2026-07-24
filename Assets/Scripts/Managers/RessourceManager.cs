using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;


public enum ResourceType { Money, Time, Population, Research } // adapte selon ton jeu

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }
    public Slider sliderMoney;
    public Slider sliderPopulation;
    public Slider sliderTime;

    public int startingMoney = 1000;
    public int startingPopulation = 100;
    public int startingTime = 300;

    private Dictionary<ResourceType, float> resources = new();
    public Dictionary<ResourceType, Slider> resourceSliders = new();

    public event Action<ResourceType, float> OnResourceChanged;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        // valeurs initiales
        resources[ResourceType.Money] = startingMoney;
        resources[ResourceType.Population] = startingPopulation;
        resources[ResourceType.Time] = startingTime;

        sliderMoney.maxValue = startingMoney;
        sliderPopulation.maxValue = startingPopulation;
        sliderTime.maxValue = startingTime;
        resourceSliders[ResourceType.Money] = sliderMoney;
        resourceSliders[ResourceType.Population] = sliderPopulation;
        resourceSliders[ResourceType.Time] = sliderTime;
    }

    public float Get(ResourceType type) => resources.TryGetValue(type, out var v) ? v : 0f;

    public bool CanAfford(ResourceType type, float cost) => Get(type) >= cost;

    public void Modify(ResourceType type, float delta)
    {
        resources[type] = Get(type) + delta;
        resourceSliders[type].value = resources[type];
        OnResourceChanged?.Invoke(type, resources[type]);
    }
}