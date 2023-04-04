using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Arrange : MonoBehaviour
{

    // private GameObject obj;
    // Start is called before the first frame update
    private bool isClicked = false;
    public GameObject panel;
    private RectTransform selfRectTransform;
    private RectTransform rectTransform;
    private ActionArray actionArray;
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = panel.GetComponent<RectTransform>();
        selfRectTransform = gameObject.GetComponent<RectTransform>();

    }
    void Start()
    {
        //panel = GameObject.Find("Instruction Constructor");
        rectTransform = panel.GetComponent<RectTransform>();
        selfRectTransform = gameObject.GetComponent<RectTransform>();

        actionArray = panel.GetComponent<ActionArray>();
    }
    public void changePos()
    {

        if (!isClicked)
        {
            isClicked = true;
            actionArray.focusedObject = gameObject;
            gameObject.transform.SetParent(canvas.transform);
        }
        else
        {
            isClicked = false;
            if (rectOverlaps(selfRectTransform, rectTransform))
            {
                Debug.Log("Merge ba");
                actionArray.focusedObject = gameObject;
                actionArray.AssignToSquare();
            }
            else
            {
                actionArray.DeassignToSquare();
            }
        }



    }
    bool rectOverlaps(RectTransform rectTrans1, RectTransform rectTrans2)
    {
        Rect rect1 = new Rect(rectTrans1.localPosition.x, rectTrans1.localPosition.y, rectTrans1.rect.width, rectTrans1.rect.height);
        Rect rect2 = new Rect(rectTrans2.localPosition.x, rectTrans2.localPosition.y, rectTrans2.rect.width, rectTrans2.rect.height);

        return rect1.Overlaps(rect2);
    }


        // Update is called once per frame
        void Update()
        {
            if (isClicked) transform.position = Input.mousePosition;
        }
    }
