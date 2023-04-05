using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private ActionArray actionArray;
    private ExecutionOrder executionOrder;
    private Pick_Up pickup;
    private RevCheck revCheck;
        
    Animator playerAnimator;
    GameObject cubeFall;
    private bool check;
    public bool makeCheckpointRev = false;
    public float walktime = 2.0f;
    public GameObject placeCube;

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
            yield return null; }
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
    public void Rotate180()
    {
        if (!playerAnimator.GetBool("Move"))
            StartCoroutine(RotateMe(Vector3.up * 180, 1.2f));
    }
    public void moveUp()
    {
        if (!playerAnimator.GetBool("Move"))
        {
            transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
            moveForward();
            transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
        }
    }
    void moveForward()
    {
        forwardpos = other.transform.position;
       

        playerAnimator.SetBool("Move", true);


    }

   /* IEnumerator MoveObject(Vector3 startingPos, Vector3 endingPos, float timeToMove)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Slerp(startingPos, endingPos, t);
            yield return null;
        }
    }*/
    public void AddBlock()
    {
        Vector3 placePos = new Vector3(other.transform.position.x, -0.669f, other.transform.position.z);
        Instantiate(placeCube, placePos, Quaternion.identity);

    }

    void CheckGround()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 250.0f, ~2))
        {
            Debug.DrawRay(transform.position, Vector3.down, Color.blue, 600);
            switch (hit.collider.gameObject.tag)
            {
                case "Finish":
                    Debug.Log("Ai castigat nivelul!");
                    executionOrder.StopExec();
                    winPopup.localScale = Vector3.one;
                    break;
                case "Falling":
                    Debug.Log("We've hit the falling cube!");
                    cubeFall = hit.collider.gameObject;
                    executionOrder.StopExec();
                    cubeFall.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    cubeFall.gameObject.GetComponent<BoxCollider>().enabled = false;
                    StartCoroutine("DelayedReset");
                    break;
                case "Pick_Up":
                    Debug.Log("We've hit a Pick_up");
                    pickup = hit.collider.gameObject.GetComponent<Pick_Up>();
                    pickup.AddButtons();
                    break;
                case "Checkpoint":
                    Debug.Log("We've hit a checkpoint!");
                    firstpos = transform.position;
                    firstrot = transform.rotation;
                    hit.collider.gameObject.tag = "Untagged";
                    if (makeCheckpointRev) hit.collider.gameObject.tag = "Revolution";
                    //  executionOrder.StopExec();
                    //  actionArray.ClearAll();
                    break;
                case "Revolution":
                    Debug.Log("Sitting on Revolution square!");
                    if (executionOrder.CheckRev()) {
                        Debug.Log("We have completed one revolution");
                        revCheck.currentRev= revCheck.currentRev+1;
                        revCheck.updateSensor();
                            }
                    break;
            }

            /*if (hit.collider.gameObject.tag == "Finish")
            {
                Debug.Log("Ai castigat nivelul!");
                executionOrder.StopExec();
                winPopup.localScale = Vector3.one;
            }
            else if (hit.collider.gameObject.tag == "Falling")
            {
                Debug.Log("We've hit the falling cube!");
                cubeFall = hit.collider.gameObject;
                executionOrder.StopExec();
                cubeFall.gameObject.GetComponent<MeshRenderer>().enabled = false;
                cubeFall.gameObject.GetComponent<BoxCollider>().enabled = false;
                StartCoroutine("DelayedReset");
            }
            else if (hit.collider.gameObject.tag == "Pick_Up")
            {
                Debug.Log("We've hit a Pick_up");
                pickup = hit.collider.gameObject.GetComponent<Pick_Up>();
                pickup.AddButtons();

            }
            else if (hit.collider.gameObject.tag == "Checkpoint")
            {
                Debug.Log("We've hit a checkpoint!");
                firstpos = transform.position;
                firstrot = transform.rotation;
                hit.collider.gameObject.tag = "Untagged";
                if (makeCheckpointRev) hit.collider.gameObject.tag = "Revolution";
              //  executionOrder.StopExec();
              //  actionArray.ClearAll();
            }*/

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
    public void resetPosition()
    {
        transform.position = firstpos;
        transform.rotation = firstrot;
    }
    IEnumerator DelayedReset()
    {
        yield return new WaitForSeconds(2);
        cubeFall.gameObject.GetComponent<MeshRenderer>().enabled = true;
        cubeFall.gameObject.GetComponent<BoxCollider>().enabled = true;
        transform.position = firstpos;
        transform.rotation = firstrot;
        actionArray.ClearAll();
       
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
        revCheck = GameObject.Find("Rev").GetComponent<RevCheck>();
       // cubeFall = GameObject.FindGameObjectWithTag("Falling");
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
        //if (check == true && Input.GetKey("w")) moveForward();
       // Debug.Log(check);
        checkDistance();
        if (Input.GetKeyUp("a")) AddBlock();
       // CheckGround();
       

    }
}
