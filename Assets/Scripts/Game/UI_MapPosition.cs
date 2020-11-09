using UnityEngine;

public class UI_MapPosition : MonoBehaviour
{
    [SerializeField] private Transform backgroundPrefab;
    [SerializeField] private Transform backgroundLayoutParent;
    private int segmentCount;
    private RectTransform rectTransform;
    private int currentWidth;
    private void Start()
    {
        var generator = WorldSettings.WorldGenerator;
        rectTransform = GetComponent<RectTransform>();
        segmentCount = generator.SegmentCount;
        for (int i = 0; i < segmentCount; i++)
        {
            AddSegmentImage();
        }

        generator.OnSegmentCreated += (s) => AddSegmentImage();
    }

    private void AddSegmentImage()
    {
        currentWidth += 100;
        rectTransform.SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, currentWidth);
        Instantiate(backgroundPrefab, backgroundLayoutParent);
    }

    private void Update()
    {
        
    }
}
