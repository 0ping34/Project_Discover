using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionArray : MonoBehaviour
{

    public GameObject[] panels;
    public GameObject focusedObject;
    public int panelLength = 12;
    public int idx = 0;
    private Canvas canvas;
    private Inventory inventory;
    // Start is called before the first frame update
    void Awake()
    {
        canvas= GetComponentInParent<Canvas>();
        inventory = GameObject.Find("Inv").GetComponent<Inventory>();
    }
    void Start()
    {

    }

    public void AssignToSquare()
    {
        Debug.Log("Asignarea a reusit");
        focusedObject.transform.position = panels[idx].transform.position;
        focusedObject.transform.SetParent(panels[idx].transform);
        idx++;
    }

    public void DeassignToSquare()
    {
        inventory.AddToInventory();
        for (int i = 0; i < idx; i++) if (panels[i].transform.childCount == 0)
                {
                    idx = i;
                    break;
                }
            for (int i = idx+1; i < panels.Length-1; i++)
            {
            if (panels[i].transform.childCount > 0)
            {
                GameObject focused = panels[i].transform.GetChild(0).gameObject;
                Debug.Log(focused);
                focused.transform.position = panels[i - 1].transform.position;
                focused.transform.SetParent(panels[i - 1].transform);
                
            }
            else {
                idx = i-1;
                break; }
            }
            //idx -= 1;
           
    }
    public void ClearAll()
    {
        inventory.AddAlltoInventory();
        idx = 0;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
