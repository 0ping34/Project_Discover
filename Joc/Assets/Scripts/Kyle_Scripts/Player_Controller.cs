using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    Animator playerAnimator;
    private bool check;
    public float walktime = 2.0f;

    private GameObject thisObject;
    private GameObject other;

    Vector3 forward;
    Vector3 toOther;
    Vector3 forwardpos;
    /* IEnumerator movePlayer()
    {
        /check = false;
       // float gridstep = 1.12007f;

        forwardpos = other.transform.position;

        playerAnimator.SetBool("Move", true);
        yield return new WaitForSeconds(walktime);
        //playerAnimator.SetBool("Move", false);
        
        forwardpos = other.transform.position;
        check = true;

    }*/
    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        var fromAngle = thisObject.transform.rotation;
        var toAngle = Quaternion.Euler(thisObject.transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            thisObject.transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;        }
    }
    public void Rotate90R()
    {
        if (!playerAnimator.GetBool("Move")) 
            StartCoroutine(RotateMe(Vector3.up * 90, 1.0f));
      
    }
    public void Rotate90L()
    {
        if (!playerAnimator.GetBool("Move"))
            StartCoroutine(RotateMe(Vector3.up * -90, 1.0f));

    }
    public void moveUp()
    {
        if (!playerAnimator.GetBool("Move"))
            moveForward();
          
     }
    void moveForward()
    {
        forwardpos = other.transform.position;

        playerAnimator.SetBool("Move", true);
    }


     void checkDistance()
    {
       

        forward = thisObject.transform.TransformDirection(Vector3.forward);
        toOther = forwardpos - thisObject.transform.position;

        //Debug.Log(Vector3.Dot(forward, toOther));
        if (Vector3.Dot(forward, toOther) < 0)
        {
            playerAnimator.SetBool("Move", false);

            check = true;

            forwardpos = other.transform.position;

        }
    }

   

    private void Awake()
    {
        check = true;
        thisObject = GameObject.FindGameObjectWithTag("Player");
        other = GameObject.Find("Forward");
        playerAnimator = thisObject.GetComponent<Animator>();
       
    }
    // Start is called before the first frame update
    void Start()
    {
       // moveUp();
       // Rotate90();
    }

    // Update is called once per frame
    void Update()
    {
        if (check == true && Input.GetKey("w")) moveForward();
       // Debug.Log(check);
        checkDistance();
       

    }
}
