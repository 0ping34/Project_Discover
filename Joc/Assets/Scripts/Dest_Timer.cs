using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dest_Timer : MonoBehaviour
{
    IEnumerator dest()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
    // Start is called before the first frame updat
    void Start()
    {
        StartCoroutine("dest");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
