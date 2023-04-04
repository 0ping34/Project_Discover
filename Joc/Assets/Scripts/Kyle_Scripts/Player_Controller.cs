using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private ActionArray actionArray;
    private ExecutionOrder executionOrder;

    Animator playerAnimator;
    private bool check;
    public float walktime = 2.0f;

    private GameObject thisObject;
    private GameObject other;
    private RectTransform winPopup;
    private Vector3 firstpos;
    private Quaternion firstrot;


    Vector3 forward;
    Vector3 toOther;
    Vector3 forwardpos;

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

    void CheckGround()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 250.0f, ~2))
        {
            Debug.DrawRay(transform.position, Vector3.down, Color.blue, 600);
            if (hit.collider.gameObject.tag == "Finish")
            {
                Debug.Log("Ai castigat nivelul!");
                executionOrder.StopExec();
                winPopup.localScale = Vector3.one;
            }
        }
        else
        {
            Debug.Log("You are not walking on ground!");
            actionArray.ClearAll();
            executionOrder.StopExec();
            transform.position = firstpos;
            transform.rotation = firstrot;
        }
        
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

            CheckGround();
            
        }
    }

   

    private void Awake()
    {
        check = true;
        thisObject = GameObject.FindGameObjectWithTag("Player");
        other = GameObject.Find("Forward");
        playerAnimator = thisObject.GetComponent<Animator>();
        actionArray = GameObject.Find("Action Array").GetComponent<ActionArray>();
        executionOrder = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<ExecutionOrder>();
        winPopup = GameObject.FindGameObjectWithTag("Finish_Popup").GetComponent<RectTransform>();
       
       
    }
    // Start is called before the first frame update
    void Start()
    {
        firstpos = transform.position;
        firstrot = transform.rotation;
        // moveUp();
        // Rotate90();
    }

    // Update is called once per frame
    void Update()
    {
        if (check == true && Input.GetKey("w")) moveForward();
       // Debug.Log(check);
        checkDistance();
       // CheckGround();
       

    }
}
