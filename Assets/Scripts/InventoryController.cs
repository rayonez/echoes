using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [HideInInspector]
    public ItemGrid selectedItemGrid;
    public LetterInteraction letterInteraction;
    public GameObject letter;
    
    public ItemGrid SelectedItemGrid {
        get => selectedItemGrid;
        set {
                selectedItemGrid = value;
                inventoryHighlight.SetParent(value);
            }
        }

    InventoryItem selectedItem;
    InventoryItem overlapItem;
    RectTransform rectTransform;

    public List<ItemData> items;
    private int i = 0;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform canvasTransform;

    InventoryHighlight inventoryHighlight;

    private void Awake()
    {
        inventoryHighlight = GetComponent<InventoryHighlight>();
    }
    
    private void Start()
    {
        
    }

    private void Update()
    {
        ItemIconDrag();

        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(selectedItem == null)
            {
                //CreateRandomItem();
            }
        }

        if(selectedItemGrid != null && i <= 3)
        {
            InsertRandomItem(i);
            i++;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(itemToHighlight == null) {return;}
            if(itemToHighlight.itemData.itemID == 2)
            {
                letter.SetActive(true);
            }
            if(itemToHighlight.itemData.itemID == 3)
            {
                if(selectedItemGrid != null && i <= 9)
                {
                    InsertRandomItem(i);
                    i++;
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            RoatateItem();
        }

        if (selectedItemGrid == null)
        {
            inventoryHighlight.Show(false);
            return; 
        }

        HandleHighlight();

        if (Input.GetMouseButtonDown(0))
        {
        LeftMouseButtonPress();
        }


    }

    private void RoatateItem()
    {
        if(selectedItem == null) {return;}
        selectedItem.Rotate();
    }

    private void InsertRandomItem(int itemId)
    {
        if(selectedItemGrid == null) {return;}
        
        //CreateRandomItem();
        CreateItem(itemId);
        InventoryItem itemToInsert = selectedItem;
        selectedItem = null;
        InsertItem(itemToInsert);
    }

    public void InsertItem(int itemId)
    {
        if(selectedItemGrid == null) {return;}
        
        CreateItem(itemId);
        InventoryItem itemToInsert = selectedItem;
        selectedItem = null;
        InsertItem(itemToInsert);
    }

    public void InsertItem(InventoryItem itemToInsert)
    {
        Vector2Int? posOnGrid = selectedItemGrid.FindSpaceForObject(itemToInsert);

        if (posOnGrid == null) {return;}

        selectedItemGrid.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);
    }

    Vector2Int oldPosition;
    InventoryItem itemToHighlight;

    private void HandleHighlight()
    {
        Vector2Int positionOnGrid = GetTileGridPosition();
        if(oldPosition == positionOnGrid) {return;}

        oldPosition = positionOnGrid;
        if(selectedItem == null)
        {
            itemToHighlight = selectedItemGrid.GetItem(positionOnGrid.x, positionOnGrid.y);

            if(itemToHighlight != null)
            {
                inventoryHighlight.Show(true);
                ChangeText();
                inventoryHighlight.SetSize(itemToHighlight);
                inventoryHighlight.SetPosition(selectedItemGrid, itemToHighlight);
            }
            else
            {
                inventoryHighlight.Show(false);
                letterInteraction.script.text = "";
                letterInteraction.guide.text = "CLICK TO PICK UP/DOWN AND MOVE TO OBJECTS\nALSO CLICK AND PRESS 'R' TO ROTATE OBJECTS";
            }
        }
        else
        {
            inventoryHighlight.Show(selectedItemGrid.BoundryCheck(
                positionOnGrid.x,
                positionOnGrid.y,
                selectedItem.WIDTH,
                selectedItem.HEIGHT 
                ));

            inventoryHighlight.SetSize(selectedItem);
            inventoryHighlight.SetPosition(selectedItemGrid, selectedItem, positionOnGrid.x, positionOnGrid.y);
        }
    }

    private void CreateRandomItem()
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        selectedItem = inventoryItem;

        rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);
        rectTransform.SetAsLastSibling();

        int selectedItemID = UnityEngine.Random.Range(0, items.Count);
        inventoryItem.Set(items[selectedItemID]);
    }

    public void CreateItem(int itemId)
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        selectedItem = inventoryItem;

        rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);
        rectTransform.SetAsLastSibling();

        int selectedItemID = itemId;
        inventoryItem.Set(items[selectedItemID]);
    }

    private void LeftMouseButtonPress()
    {
        Vector2Int tileGridPosition = GetTileGridPosition();

        if (selectedItem == null)
        {
            PickUpItem(tileGridPosition);
        }
        else
        {
            PlaceItem(tileGridPosition);
        }
    }

    private Vector2Int GetTileGridPosition()
    {
        Vector2 position = Input.mousePosition;

        if (selectedItem != null)
        {
            position.x -= (selectedItem.WIDTH - 1) * ItemGrid.tileSizeWidth / 2;
            position.y += (selectedItem.HEIGHT - 1) * ItemGrid.tileSizeHeight / 2;
        }

        return selectedItemGrid.GetTileGridPosition(position);
    }

    private void PlaceItem(Vector2Int tileGridPosition)
    {
        bool complete = selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y, ref overlapItem);
        if(complete)
        {
            selectedItem = null;
            if(overlapItem != null)
            {
                selectedItem = overlapItem;
                overlapItem = null;
                rectTransform = selectedItem.GetComponent<RectTransform>();
                rectTransform.SetAsLastSibling();
            }
        }
    } 

    private void PickUpItem(Vector2Int tileGridPosition)
    {
        selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
        if (selectedItem != null)
        {
            rectTransform = selectedItem.GetComponent<RectTransform>();
        }
    }

    private void ItemIconDrag()
    {
        if (selectedItem != null)
        {
            rectTransform.position = Input.mousePosition;
        }
    }

    private void ChangeText()
    {
        switch(itemToHighlight.itemData.itemID)
        {
            case 0:
                letterInteraction.script.text = "ATTIC KEY";
                letterInteraction.guide.text = "";
                break;
            case 1:
                letterInteraction.script.text = "HOUSE KEY";
                letterInteraction.guide.text = "";
                break;
            case 2:
                letterInteraction.script.text = "MY DEAREST PEPPER";
                letterInteraction.guide.text = "PRESS 'E' TO READ";
                break;
            case 3:
                letterInteraction.script.text = "AM ENVELOPE FROM 'ASTER'";
                letterInteraction.guide.text = "What's in it...? (PRESS 'E' TO SHAKE)";
                break;
            case 4:
                letterInteraction.script.text = "BROKEN PICTRUE";
                letterInteraction.guide.text = "";
                break;
            case 5:
                letterInteraction.script.text = "PIECE OF PICTURE 1";
                letterInteraction.guide.text = "";
                break;
            case 6:
                letterInteraction.script.text = "PIECE OF PICTURE 2";
                letterInteraction.guide.text = "";
                break;
            case 7:
                letterInteraction.script.text = "PIECE OF PICTURE 3";
                letterInteraction.guide.text = "";
                break;
            case 8:
                letterInteraction.script.text = "PIECE OF PICTURE 4";
                letterInteraction.guide.text = "";
                break;
            case 9:
                letterInteraction.script.text = "PIECE OF PICTURE 5";
                letterInteraction.guide.text = "";
                break;
        }
    }
}
