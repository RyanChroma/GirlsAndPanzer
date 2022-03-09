using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDrag : MonoBehaviour
{
    Camera myCam;
    
    //Graphical
    [SerializeField]
    RectTransform boxvisual;

    //Logical
    Rect selectionBox;
    Vector2 startPosition;
    Vector2 endPosition;
    void Start()
    {
        myCam = Camera.main;
        startPosition = Vector2.zero;
        endPosition = Vector2.zero;
        DrawVisual();
    }

    void Update()
    {
        //When CLicked.
        if(Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
            selectionBox = new Rect();
        }

        //When Holding Click.
        if(Input.GetMouseButton(0))
        {
            endPosition = Input.mousePosition;
            DrawVisual();
            DrawSelection();
        }

        //When Release Click.
        if(Input.GetMouseButtonUp(0))
        {
            SelectUnits();
            startPosition = Vector2.zero;
            endPosition = Vector2.zero;
            DrawVisual();
        }
    }

    void DrawVisual()
    {
        Vector2 boxStart = startPosition;
        Vector2 boxEnd = endPosition;

        Vector2 boxCenter = (boxStart + boxEnd) / 2;
        boxvisual.position = boxCenter;

        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));

        boxvisual.sizeDelta = boxSize;
    }

    void DrawSelection()
    {
        //Do x calculations.
        if(Input.mousePosition.x < startPosition.x)
        {
            //Dragging Left.
            selectionBox.xMin = Input.mousePosition.x;
            selectionBox.xMax = startPosition.x;
        }
        else
        {
            //Dragging Right.
            selectionBox.xMin = startPosition.x;
            selectionBox.xMax = Input.mousePosition.x;

        }

        //Do y calculations.
        if(Input.mousePosition.y < startPosition.y)
        {
            //Draging Down.
            selectionBox.yMin = Input.mousePosition.y;
            selectionBox.yMax = startPosition.y;
        }
        else
        {
            //Dragging Up.
            selectionBox.yMin = startPosition.y;
            selectionBox.yMax = Input.mousePosition.y;
        }
    }

    void SelectUnits()
    {
        //Loop through all of your units.
        foreach(var unit in UnitSelections.Instance.unitList)
        {
            //If unit is within the bounds of the selection rect.
            if(selectionBox.Contains(myCam.WorldToScreenPoint(unit.transform.position)))
            {
                //If any unit is within the selection add them to selection.
                UnitSelections.Instance.DragSelect(unit);
            }
        }
    }
}