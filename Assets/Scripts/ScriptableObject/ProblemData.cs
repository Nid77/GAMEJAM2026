using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ProblemData", menuName = "Game/ProblemData")]
public class ProblemData : ScriptableObject
{
    public string title;
    [TextArea] public string questionText;
    public Sprite icon;
    public List<ProblemAnswer> answers;
}

[System.Serializable]
public class ProblemAnswer
{
    public string answerText;
    public List<ResourceCost> costs;      // ce que ça coûte
    public List<ResourceCost> effects;    // ce que ça rapporte (peut être négatif)
}

[System.Serializable]
public class ResourceCost
{
    public ResourceType type;
    public float amount;
}