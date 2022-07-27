using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    [SerializeField] private GameObject dotScreen, handScreen;
    public float rayDistance = 3f;
    public GameObject player, grabbedObject;
    private Camera myCam;
    public Transform pickUpGameObject;
    bool isGrabbed;
    RaycastHit hit;
    GameObject inventorySystemReference;
    private LayerMask interactionLayer = 1 << 6, pickUpLayer = 1 << 7, pickedUpLayer = 1 << 8;
    // Start is called before the first frame update
    void Start()
    {
        myCam = GetComponent<Camera>();
        inventorySystemReference = GameObject.FindGameObjectWithTag("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(transform.position, myCam.transform.forward, out hit, rayDistance) && GameManager.gameIsPaused == false)
            {
                if (!isGrabbed && hit.transform.tag == "CanPickUp")
                {
                    PickUpObject(hit.transform.gameObject);
                    isGrabbed = true;
                }
                else if (inventorySystemReference.GetComponent<InventorySystem>().isOpen == false && Input.GetMouseButtonDown(0) && hit.transform.gameObject == grabbedObject)
                {
                    LeaveObject(hit.transform.gameObject);
                    isGrabbed = false;
                    grabbedObject = null;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(transform.position, myCam.transform.forward, out hit, rayDistance) && GameManager.gameIsPaused == false)
            {
                if (!isGrabbed && hit.transform.tag == "CanPickUp")
                {
                    hit.transform.gameObject.GetComponent<ItemPickUp>().PickUp();
                }
                if (hit.transform.GetComponent<InteractionManager>())
                {

                    hit.transform.gameObject.GetComponent<InteractionManager>().Interaction();

                }
            }
        }
        /*
        if (Physics.Raycast(transform.position, myCam.transform.forward, out hit, rayDistance, raycastLayerMask) && GameManager.gameIsPaused == false)
        {
            if (hit.transform.tag == "CanPickUp")
            {
                dotScreen.SetActive(false);
                handScreen.SetActive(true);
            }
            else
            {
                dotScreen.SetActive(true);
                handScreen.SetActive(false);
            }
            if (!isGrabbed)
            {
                if (Input.GetKeyDown(KeyCode.E) && hit.transform.tag == "CanPickUp")
                {
                    hit.transform.gameObject.GetComponent<ItemPickUp>().PickUp();
                }

                if (Input.GetMouseButtonDown(0) && hit.transform.tag == "CanPickUp")
                {
                    PickUpObject(hit.transform.gameObject);
                    isGrabbed = true;
                }
                if (Input.GetKeyDown(KeyCode.E) && hit.transform.GetComponent<InteractionManager>())
                {

                     hit.transform.gameObject.GetComponent<InteractionManager>().Interaction();

                }
            }
            else if (inventorySystemReference.GetComponent<InventorySystem>().isOpen == false && Input.GetMouseButtonDown(0) && hit.transform.gameObject == grabbedObject)
            {
                LeaveObject(hit.transform.gameObject);
                isGrabbed = false;
                grabbedObject = null;
            }
        }
        else
        {
            dotScreen.SetActive(true);
            handScreen.SetActive(false);
        }
        */
        if (isGrabbed && Input.GetKey(KeyCode.R) && GameManager.gameIsPaused == false)
        {
            RotateObject(hit.transform.gameObject);
        }
        if(isGrabbed && Input.GetKeyUp(KeyCode.R) && GameManager.gameIsPaused == false)
        {
            StopRotating();
        }

    }
    void PickUpObject(GameObject objectToPick)
    {
        grabbedObject = objectToPick;
        grabbedObject.layer = LayerMask.NameToLayer("pickedUp");
        objectToPick.GetComponent<Rigidbody>().isKinematic = true;
        objectToPick.transform.position = pickUpGameObject.transform.position;
        objectToPick.transform.LookAt(player.transform.position);
        Physics.IgnoreCollision(objectToPick.GetComponent<Collider>(), player.transform.GetComponent<Collider>(), true);
        objectToPick.transform.parent = myCam.transform;
    }
    void RotateObject(GameObject objectToRotate)
    {
        player.transform.GetComponent<PlayerController>().mouseSensitivity = 0;
        objectToRotate.transform.localRotation = Quaternion.Euler(Input.mousePosition.y, -Input.mousePosition.x, 0);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }
    void StopRotating()
    {
        player.transform.GetComponent<PlayerController>().mouseSensitivity = 3;
        Cursor.lockState = CursorLockMode.Locked;

    }
    void LeaveObject(GameObject objectToLeave)
    {
        grabbedObject.layer = LayerMask.NameToLayer("PickUp");
        objectToLeave.GetComponent<Rigidbody>().isKinematic = false;
        Physics.IgnoreCollision(objectToLeave.GetComponent<Collider>(), player.transform.GetComponent<Collider>(), false);
        player.transform.GetComponent<PlayerController>().mouseSensitivity = 3;
        Cursor.lockState = CursorLockMode.Locked;
        objectToLeave.transform.parent = null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, player.transform.GetComponentInChildren<Camera>().transform.forward * 3);
        Gizmos.color = Color.green;
    }
}
