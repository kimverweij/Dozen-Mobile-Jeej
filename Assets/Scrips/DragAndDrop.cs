using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private GameObject clone;

    // Start is called before the first frame update
    void Start()
    {
        //  mainCamera = Camera.main;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup   = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        clone = Instantiate(gameObject, transform.parent);
        clone.name = "DragClone";
        RectTransform cloneRectTransform = clone.GetComponent<RectTransform>();
        cloneRectTransform.anchoredPosition = rectTransform.anchoredPosition;

        // Disable raycasting on the original and clone during drag
        canvasGroup.blocksRaycasts = false;
        CanvasGroup cloneCanvasGroup = clone.GetComponent<CanvasGroup>();
        cloneCanvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransform cloneRectTransform = clone.GetComponent<RectTransform>();
        cloneRectTransform.anchoredPosition += eventData.delta / GetComponentInParent<Canvas>().scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        Debug.Log(eventData);

        /*
        // Check if we are hovering over another player item
        GameObject droppedOnObject = eventData.pointerCurrentRaycast.gameObject;

        if (droppedOnObject != null && droppedOnObject != gameObject)
        {
            // Swap positions if dropped on another player item
            RectTransform otherRectTransform = droppedOnObject.GetComponent<RectTransform>();

            if (otherRectTransform != null)
            {
                Vector3 tempPosition = rectTransform.anchoredPosition;
                rectTransform.anchoredPosition = otherRectTransform.anchoredPosition;
                otherRectTransform.anchoredPosition = tempPosition;
            }
        }
        */
    }
}
