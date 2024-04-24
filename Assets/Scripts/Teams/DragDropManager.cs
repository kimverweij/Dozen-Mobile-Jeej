using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using Debug = UnityEngine.Debug;

public class DragDropManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,  IDragHandler
{
    private GameObject draggedItem;
    private PlayerItemController draggedItemController;
    private RectTransform draggedItemRect;


    private GameObject clonedItem; // player item clicked and cloned
    private PlayerItemController clonedItemController;


    public Canvas canvas;
    private RectTransform canvasRectTransform;


    public GameObject playerItem;

    

    private bool isDragging = false;
    private string playerItemTag = "PlayerItemTeams";


    public CreateTeams RefineTeams;
    

    private GameObject objectBeingHit;
    public PlayerItemCollision playerItemCollision;




    private float cooldownTime = 0.2f;

    private void Awake()
    {
        objectBeingHit = null;

        canvasRectTransform = canvas.GetComponent<RectTransform>();
        InitateDraggebleItem();
    }


    private void InitateDraggebleItem()
    {
        draggedItem = Instantiate(playerItem, canvas.transform); // Assuming "canvas" is your Canvas reference
        draggedItem.GetComponent<BoxCollider2D>().isTrigger = true;

        draggedItemController = draggedItem.GetComponent<PlayerItemController>();
        draggedItem.AddComponent<PlayerItemCollision>();

        draggedItemRect = draggedItem.GetComponent<RectTransform>();
        draggedItem.SetActive(false);
        draggedItem.name = "DraggedItem";

        draggedItem.GetComponent<PlayerItemCollision>().setCallBackFunction(this);

    }

    private void Start()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // create a clone if its a playeritem
        if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.CompareTag(playerItemTag))
        {
            clonedItem = eventData.pointerCurrentRaycast.gameObject;
            if (!isDragging)
            {
                // Instantiate a new player item prefab
                draggedItem.SetActive(true);

                clonedItemController = clonedItem.GetComponent<PlayerItemController>();
                clonedItemController.AdjustRaycast(false);

                draggedItemController.SetPlayerName(clonedItemController.GetPlayerName());
                // Set the name of the new item
                draggedItem.name = clonedItem.name;

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
        Debug.Log("cloned item: " + clonedItem);
    }

    private void Update()
    {
       // Debug.Log("cloned item: " + clonedItem);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggedItem != null && isDragging)
        {
            // Convert the cursor position to the local space of the Canvas
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out Vector2 localCursor);
            // Set the position of the dragged item to the converted cursor position
            draggedItemRect.localPosition = localCursor;
        }      
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp");
        if (isDragging)
        {
            if(objectBeingHit!= null) // somehting is being hit
            {
                if (objectBeingHit.CompareTag(playerItemTag)) // its a player item
                {
                    if(clonedItem !=null)
                    {
                        SetBackgroundClonedItem(1, objectBeingHit);
                        Debug.Log("SwapPlayers " + clonedItem + " objectBeingHit " + objectBeingHit);
                        RefineTeams.SwapPlayers(clonedItem, objectBeingHit);
                    }
                }
            }
            objectBeingHit = null;
            draggedItem.SetActive(false);
            isDragging = false;
        }

        if (clonedItem != null)
        {
            SetBackgroundClonedItem(1, clonedItem);
            clonedItemController.AdjustRaycast(true);
        }
    }

    private IEnumerator StartCooldown() // add cooldown before setting cloned item to null
    {
        yield return new WaitForSeconds(cooldownTime);
        clonedItem = null;
        
    }

    public void ResetCollidedObject(GameObject _collidedObject)
    {
        if (_collidedObject.CompareTag(playerItemTag) && _collidedObject != clonedItem)
        {
            SetBackgroundClonedItem(1, _collidedObject);
        } 
    }

    public void SetCollidedObject(GameObject _collidedObject)
    {
        // first check if we start colliding or exit colliding.
        if (_collidedObject == clonedItem)
        {
            Debug.Log("its colliding with itself. do nothing");
        }
        else if(_collidedObject.CompareTag(playerItemTag))
        {
            objectBeingHit = _collidedObject;
            SetBackgroundClonedItem(3, objectBeingHit);
        }
    }

    private void SetBackgroundClonedItem(int state, GameObject _playerItem)
    {
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
}