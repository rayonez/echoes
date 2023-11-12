using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private bool isDragging = false;

    // Define the target position on the canvas
    public Vector2 targetPosition;
    public float positionTolerance = 10f;

    public bool isPlaceCorectly = false;
    public bool canControl = true;
    private void Start()
    {
        // Get the RectTransform, Canvas, and CanvasGroup components
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    //public void OnPointerClick(PointerEventData eventData) {
    //    Debug.Log("ClickDetected");
    //}
    public void OnPointerDown(PointerEventData eventData)
    {
        // Set the image as the top sibling when clicked
        rectTransform.SetAsLastSibling();
        // Make the image semi-transparent while dragging
        canvasGroup.alpha = 0.6f;
        // Block raycasts to items below the dragged image
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canControl)
        {
            // Move the image with the pointer
            isDragging = true;
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    // Restore the image's transparency and raycast behavior
    //    canvasGroup.alpha = 1f;
    //    canvasGroup.blocksRaycasts = true;
    //}
    public void OnPointerUp(PointerEventData eventData)
    {
        if (canControl)
        {
            // Restore the image's transparency and raycast behavior
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;


            // Check if the click happened on the same UI element
            if (!isDragging && eventData.pointerPress == gameObject)
            {
                Debug.Log("Clicked on the UI element!");
                // Rotate the UI element by 90 degrees
                rectTransform.Rotate(new Vector3(0, 0, 90f));
                // Add your click logic here

            }
            // Check if the image is at the target position
            if (Vector2.Distance(rectTransform.anchoredPosition, targetPosition) <= positionTolerance && rectTransform.rotation.z == 0)
            {
                Debug.Log("Image is near the target position!");
                canControl = false;
                GameManager.instance.currentArrangedCounter++;
                GameManager.instance.CheckForGameFinished();
                // Add your logic for when the image is near the target position
            }
            isDragging = false;
        }
        else
            canvasGroup.alpha = 1f;


    }
}
