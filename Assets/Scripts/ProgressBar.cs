using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private RectTransform fill;

    private float currentValue = 0f;
    private float maxWidth;

    private void Awake()
    {
        maxWidth = fill.rect.width;
        currentValue = maxWidth;
    }

    public void SetValue(float value)
    {
        currentValue = Mathf.Clamp(value, 0f, maxWidth);
        fill.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal,
            currentValue
        );
    }
}