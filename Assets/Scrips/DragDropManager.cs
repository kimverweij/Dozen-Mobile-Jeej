using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using Debug = UnityEngine.Debug;

public class DragAndDropManager : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    private GameObject draggedItem;
    private GameObject clonedItem; // player item clicked and cloned

    private PlayerItemController draggedItemController;
    private PlayerItemController clonedItemController;

    private RectTransform draggedItemRect;

    public GameObject playerItem;
    public Canvas canvas;
    private RectTransform canvasRectTransform;

    private bool isDragging = false;
    private string playerItemTag = "PlayerItemTeams";

    private bool isColliding = false;

    public CreateTeams RefineTeams;
    

    private GameObject currentCollisionObject;


    private void Awake()
    {
        canvasRectTransform = canvas.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.tag);
        // create a clone if its a playeritem
        if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.CompareTag(playerItemTag))
        {
            clonedItem = eventData.pointerCurrentRaycast.gameObject;
            if (!isDragging)
            {
                // Instantiate a new player item prefab
                draggedItem = Instantiate(playerItem, canvas.transform); // Assuming "canvas" is your Canvas reference

                draggedItemController = draggedItem.GetComponent<PlayerItemController>();
                draggedItemController.AdjustRaycast(false);

                clonedItemController = clonedItem.GetComponent<PlayerItemController>();
                clonedItemController.AdjustRaycast(false);

                draggedItemController.SetPlayerName(clonedItemController.GetPlayerName());

                // Set the name of the new item
                draggedItem.name = "DraggedItem";

                // Get the RectTransform of the new item
                draggedItemRect = draggedItem.GetComponent<RectTransform>();
               

                // Get the position of the mouse cursor in world space
                Vector3 worldCursorPos;
                RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRectTransform, eventData.position, eventData.pressEventCamera, out worldCursorPos);

                // Set the position of the new item to the mouse cursor position
                draggedItemRect.position = worldCursorPos;

                SetBackgroundClonedItem(2, clonedItem);
                // Set the flag to indicate that dragging has started
                isDragging = true;
            }
        }
    }

    private void Update()
    {

    }

    private void SetBackgroundClonedItem(int state, GameObject _playerItem)
    {
        Debug.Log(_playerItem);
        if (_playerItem.TryGetComponent<PlayerItemController>(out PlayerItemController playerItemController))
        {
            switch (state)
            {
                case 1:
                    playerItemController.SetRegularBG();
                    break;
                case 2:
                    playerItemController.SetTransparentBG();
                    break;
                case 3:
                    playerItemController.SetStrokeBG();
                break;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggedItem != null && isDragging)
        {
            // Convert the cursor position to the local space of the Canvas
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out Vector2 localCursor);
            // Set the position of the dragged item to the converted cursor position
            draggedItemRect.localPosition = localCursor;
            CheckCollisions();
        }      
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // item is beging dragged
        if (isDragging)
        {
            if (currentCollisionObject != null)
            {
                SetBackgroundClonedItem(1, currentCollisionObject);
                if (currentCollisionObject.CompareTag(playerItemTag))
                {
                    RefineTeams.SwapPlayers(clonedItem, currentCollisionObject);
                }
            }
            SetBackgroundClonedItem(1, clonedItem);
           
            clonedItemController.AdjustRaycast(true);

            clonedItem = null;
            Destroy(draggedItem);
            isDragging = false;
        }
        Debug.Log("OnEndDrag");
    }
    private void CheckCollisions()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        // Perform a raycast using EventSystem to check for UI elements
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        GameObject hitObject = null;

        foreach (RaycastResult result in results)
        {
            hitObject = result.gameObject;

            if (hitObject.CompareTag(playerItemTag))
            {
                break; // Stop searching if we found a player item
            }
        }

        if (hitObject != currentCollisionObject)
        {
            // Handle exit logic
            if (isColliding)
            {
                HandleExitCollision();
            }

            // Handle enter logic
            HandleEnterCollision(hitObject);
        }
        else if (hitObject == null)
        {
            // Handle exit logic when not colliding with any object
            if (isColliding)
            {
                HandleExitCollision();
            }
        }
    }

    private void HandleEnterCollision(GameObject newCollisionObject)
    {
        if (isDragging && newCollisionObject != null)
        {
            // Call your function when entering a new collision
            Debug.Log("Entered collision with: " + newCollisionObject.name);

            // Store the current collision state
            currentCollisionObject = newCollisionObject;
            SetBackgroundClonedItem(3, currentCollisionObject);
            isColliding = true;
        }
    }

    private void HandleExitCollision()
    {
        // Call your function when exiting a collision
        

        // Reset the current collision state
        SetBackgroundClonedItem(1, currentCollisionObject);


        currentCollisionObject = null;
        isColliding = false;
        Debug.Log("Exited collision");
    }

    private void ResetCollisionState()
    {
        // Reset the collision state
        SetBackgroundClonedItem(1, currentCollisionObject);
        currentCollisionObject = null;
        isColliding = false;
    }
}