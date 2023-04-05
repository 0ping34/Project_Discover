using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevCheck : MonoBehaviour
{
    public GameObject gate;
    public GameObject[] sensors;
    public Material mat;
    public int currentRev = -2;
    public bool gateCrash = false;
    private Animation anim;
    // Start is called before the first frame update
    private void Awake()
    {
        anim = GameObject.Find("Gate").GetComponent<Animation>();
    }
    void Start()
    {
        
    }
    public void updateSensor()
    {
        sensors[currentRev / 2].GetComponent<MeshRenderer>().material = mat;
        if (currentRev == 4)
            gateCrash = true;

    }
    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyUp("o")) updateSensor();
        if (gateCrash == true) {
            anim.Play();
            gateCrash = false;
        }
    }
}
