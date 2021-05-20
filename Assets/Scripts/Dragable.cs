using UnityEngine;
using System.Collections;

//mine
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Dragable : MonoBehaviour
{
    //mine
    public DotPointer center;

    public float catchingDistance = 3f;
    bool isDragging = false;
    GameObject draggingObject;

    //mine
    private void Start()
    {

        center = FindObjectOfType<DotPointer>();

    }

    void Update()
    {

        if (Input.GetMouseButton(0))
        {

            if (!isDragging)
            {

                draggingObject = GetObjectFromMouseRaycast();
                if (draggingObject)
                {

                    draggingObject.GetComponent<Rigidbody>().isKinematic = true;
                    isDragging = true;

                }

            }
            else if (draggingObject != null)
            {

                draggingObject.GetComponent<Rigidbody>().MovePosition(CalculateMouse3DVector());

            }

        }
        else
        {

            if (draggingObject != null)
            {

                draggingObject.GetComponent<Rigidbody>().isKinematic = false;

            }

            isDragging = false;

        }

    }

    private GameObject GetObjectFromMouseRaycast()
    {

        GameObject gmObj = null;
        RaycastHit hitInfo = new RaycastHit();

        //bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(center.transform.position), out hitInfo);

        if (hit)
        {

            if (hitInfo.collider.gameObject.GetComponent<Rigidbody>() && Vector3.Distance(hitInfo.collider.gameObject.transform.position, transform.position) <= catchingDistance)
            {

                gmObj = hitInfo.collider.gameObject;

            }

        }

        return gmObj;

    }

    private Vector3 CalculateMouse3DVector()
    {

        //Vector3 v3 = Input.mousePosition;
        Vector3 v3 = center.transform.position;

        v3.z = catchingDistance;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        return v3;

    }

}

/*using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Dragable : MonoBehaviour
{

    public int normalCollisionCount = 1;
    public float moveLimit = .5f;
    public float collisionMoveFactor = .01f;
    public float addHeightWhenClicked = 0.0f;
    public bool freezeRotationOnDrag = true;
    public Camera cam;
    public Rigidbody myRigidbody;
    private Transform myTransform;
    private bool canMove = false;
    private float yPos;
    private bool gravitySetting;
    private bool freezeRotationSetting;
    private float sqrMoveLimit;
    private int collisionCount = 0;
    private Transform camTransform;

    void Start()
    {
        myTransform = transform;
        if (!cam)
        {
            cam = Camera.main;
        }
        if (!cam)
        {
            Debug.LogError("Can't find camera tagged MainCamera");
            return;
        }
        camTransform = cam.transform;
        sqrMoveLimit = moveLimit * moveLimit;   // Since we're using sqrMagnitude, which is faster than magnitude
    }

    void OnMouseDown()
    {
        canMove = true;
        myTransform.Translate(Vector3.up * addHeightWhenClicked);
        gravitySetting = myRigidbody.useGravity;
        freezeRotationSetting = myRigidbody.freezeRotation;
        myRigidbody.useGravity = false;
        myRigidbody.freezeRotation = freezeRotationOnDrag;
        yPos = myTransform.position.y;
    }

    void OnMouseUp()
    {
        canMove = false;
        myRigidbody.useGravity = gravitySetting;
        myRigidbody.freezeRotation = freezeRotationSetting;
        if (!myRigidbody.useGravity)
        {
            Vector3 pos = myTransform.position;
            pos.y = yPos - addHeightWhenClicked;
            myTransform.position = pos;
        }
    }

    void OnCollisionEnter()
    {
        collisionCount++;
    }

    void OnCollisionExit()
    {
        collisionCount--;
    }

    void FixedUpdate()
    {
        if (!canMove)
        {
            return;
        }

        myRigidbody.velocity = Vector3.zero;
        myRigidbody.angularVelocity = Vector3.zero;

        Vector3 pos = myTransform.position;
        pos.y = yPos;
        myTransform.position = pos;

        Vector3 mousePos = Input.mousePosition;
        Vector3 move = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, camTransform.position.y - myTransform.position.y)) - myTransform.position;
        move.y = 0.0f;
        if (collisionCount > normalCollisionCount)
        {
            move = move.normalized * collisionMoveFactor;
        }
        else if (move.sqrMagnitude > sqrMoveLimit)
        {
            move = move.normalized * moveLimit;
        }

        myRigidbody.MovePosition(myRigidbody.position + move);
    }
}*/