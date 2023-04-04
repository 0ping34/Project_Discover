using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject[] panels;
    public GameObject[] panels2;
    public ActionArray actionArray;
    public GameObject focuseditem;
    int i;
    public int panelLength = 12;
    public void AddToInventory()
    {
        focuseditem = actionArray.focusedObject;
        Debug.Log(focuseditem.name);
        for (i = 0; i < panels.Length; i++)
        {
            if (panels[i].transform.childCount == 0)
            {

                focuseditem.transform.SetParent(panels[i].transform);
                focuseditem.transform.position = panels[i].transform.position;
                break;

            }
        }
    }
    public void AddToInventorySpecific(GameObject item)
    {
        Debug.Log(item.name);
        for (i = 0; i < panels.Length; i++)
        {
            if (panels[i].transform.childCount == 0)
            {

                item.transform.SetParent(panels[i].transform);
                item.transform.position = panels[i].transform.position;
                break;

            }
        }
    }
    public void AddAlltoInventory()
    {// = actionArray.panels;
        for (i = 0; i < panels2.Length; i++)
        {
            for (int j = 0; j < panels.Length; j++)
            {
                if (panels[i].transform.childCount == 0)
                {
                    if (panels2[j].transform.childCount > 0)
                    {
                        panels2[j].transform.GetChild(0).position = panels[i].transform.position;
                        panels2[j].transform.GetChild(0).SetParent(panels[i].transform);

                    }
                   // else break;
                }
            }
        }
        // Start is called before the first frame update
        void Awake()
        {
            actionArray = GameObject.Find("Action Array").GetComponent<ActionArray>();
        }
    }
}