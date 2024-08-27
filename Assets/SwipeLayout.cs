using UnityEngine;
using UnityEngine.UI;

public class SwipeLayout : MonoBehaviour
{
    [SerializeField][Range(1, 10)] int transitionSpeed = 5;
    [SerializeField][Range(0, 1)] float neighbourReductionPercentage = 0.5f;
    [SerializeField] bool scrollWhenReleased = true;
    [SerializeField][Range(0.05f, 1)] float scrollStopSpeed = 0.1f;
    [SerializeField] Sprite[] knobs;
    [SerializeField] GameObject knob;
    [SerializeField] Scrollbar scrollbar;
    [SerializeField] Transform knobContainer;

    Vector2 neighbourScale;
    Vector2 mainScale;
    private float scrollbarValue = 0;
    float[] attractionPoints;
    float subdivisionDistance;
    float attractionPoint;
    int childCount;
    bool knobClicked = false;

    void Start()
    {
        attractionPoints = new float[transform.childCount];
        childCount = attractionPoints.Length;
        subdivisionDistance = 1f / (childCount - 1f);

        for (int i = 0; i < childCount; i++)
        {
            attractionPoints[i] = subdivisionDistance * i;
            Instantiate(knob, knobContainer);
        }

        foreach (Transform child in transform)
        {
            child.localScale = new Vector2(neighbourReductionPercentage, neighbourReductionPercentage);
        }

        if (childCount > 0)
        {
            knobContainer.GetChild(0).GetComponent<Image>().sprite = knobs[0];
            transform.GetChild(0).localScale = Vector2.one;
        }
    }

    void Update()
    {
        if (!knobClicked && (Input.GetMouseButton(0) || (scrollWhenReleased && GetScrollSpeed() > scrollStopSpeed)))
        {
            scrollbarValue = scrollbar.value;
            FindAttractionPoint();
            UpdateUI();
        }
        else if (IsBeingScaled())
        {
            scrollbar.value = Mathf.Lerp(scrollbar.value, attractionPoint, transitionSpeed * Time.deltaTime);
            UpdateUI();
        }
        else
        {
            knobClicked = false;
        }
    }

    void FindAttractionPoint()
    {
        if (scrollbarValue < 0)
        {
            attractionPoint = 0;
        }
        else
        {
            for (int i = 0; i < childCount; i++)
            {
                if (scrollbarValue < attractionPoints[i] + (subdivisionDistance / 2) && scrollbarValue > attractionPoints[i] - (subdivisionDistance / 2))
                {
                    attractionPoint = attractionPoints[i];
                    break;
                }
                if (i == childCount - 1)
                {
                    attractionPoint = attractionPoints[i];
                }
            }
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.localScale = new Vector2(neighbourReductionPercentage, neighbourReductionPercentage);
        }

        transform.GetChild(0).localScale = Vector2.one;
    }

    float GetScrollSpeed()
    {
        // Implement your method to get the scroll speed here
        return 0.0f;
    }

    bool IsBeingScaled()
    {
        // Implement your method to determine if something is being scaled here
        return false;
    }
}
