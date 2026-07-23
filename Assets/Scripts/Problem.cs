using UnityEngine;
using System.Collections.Generic;

public class Problem : MonoBehaviour
{
    public ProblemData data;

    private void OnMouseDown() // ou un Collider2D + EventTrigger si UI/2D
    {
        UIManager.Instance.OpenProblemPopup(this);
    }

    public void Resolve(ProblemAnswer chosenAnswer)
    {
        foreach (var cost in chosenAnswer.costs)
            ResourceManager.Instance.Modify(cost.type, -cost.amount);

        foreach (var effect in chosenAnswer.effects)
            ResourceManager.Instance.Modify(effect.type, effect.amount);

        ProblemSpawner.Instance.NotifyProblemResolved(this);
        Destroy(gameObject);
    }
}