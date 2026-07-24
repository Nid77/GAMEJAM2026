using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Problem : MonoBehaviour, IPointerClickHandler
{
    public ProblemData data;

    public void OnPointerClick(PointerEventData eventData) // ou un Collider2D + EventTrigger si UI/2D
    {   
        //Debug.Log("Problem clicked");
        //1Debug.Log("Question : " + data.questionText);
        PopupSpawn.Instance.UpdatePopup(this);
    }

    public void Resolve(ProblemAnswer chosenAnswer)
    {   
        foreach (var cost in chosenAnswer.costs) {
            ResourceManager.Instance.Modify(cost.type, -cost.amount);
            Debug.Log("Current " + cost.type + ": " + ResourceManager.Instance.Get(cost.type));
        }

        //foreach (var effect in chosenAnswer.effects)
          //  ResourceManager.Instance.Modify(effect.type, effect.amount);

        ProblemSpawner.Instance.NotifyProblemResolved(this);
        Destroy(gameObject);
    }
}