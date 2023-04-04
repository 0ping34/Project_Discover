using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Pick_Up : MonoBehaviour
{


    public List<GameObject> buttonContainer = new List<GameObject>();
    private Inventory inv;
    // Start is called before the first frame update


    public void AddButtons()
    {
        foreach (GameObject button in buttonContainer)
        {
            GameObject obj = Instantiate(button, new Vector3(-721.0f, -25.0f, 0.0f), Quaternion.identity);
           
            inv.AddToInventorySpecific(obj);
            obj.transform.localScale = Vector3.one;
        }
        Destroy(gameObject);
    }
    private void Awake()
    {
        inv = GameObject.Find("Inv").GetComponent<Inventory>();
    }
    private void Update()
    {
        if(Input.GetKeyUp("r")) AddButtons();
    }
}