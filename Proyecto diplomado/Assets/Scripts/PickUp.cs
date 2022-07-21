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
    private LayerMask layerMask = 1 << 6;
    // Start is called before the first frame update
    void Start()
    {
        myCam = GetComponent<Camera>();
        inventorySystemReference = GameObject.FindGameObjectWithTag("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            if (!isGrabbed)
            {
                if (Physics.Raycast(transform.position, myCam.transform.forward, out hit, rayDistance))
                {
                    if (hit.transform.tag == "CanPickUp")
                    {
                        PickUpObject(hit.transform.gameObject);
                        dotScreen.GetComponent<Image>().sprite = hand;
                        isGrabbed = true;
                    }
                    else
                    {
                        dotScreen.GetComponent<Image>().sprite = dot;
                    }
                }
            }
            else if(inventorySystemReference.GetComponent<InventorySystem>().isOpen == false)
            {
                LeaveObject(hit.transform.gameObject);
                isGrabbed = false;
            }
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isGrabbed)
            {
                if (Physics.Raycast(transform.position, myCam.transform.forward, out hit, rayDistance))
                {
                    if (hit.transform.tag == "CanPickUp")
                    {
                        hit.transform.gameObject.GetComponent<ItemPickUp>().PickUp();
                    }
                }
            }
        }*/

        if (Physics.Raycast(transform.position, myCam.transform.forward, out hit, rayDistance, layerMask) && GameManager.gameIsPaused == false)
        {
            if(hit.transform.tag == "CanPickUp")
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
            }
            else if (inventorySystemReference.GetComponent<InventorySystem>().isOpen == false && Input.GetMouseButtonDown(0))
            {
                LeaveObject(hit.transform.gameObject);
                isGrabbed = false;
            }
        }
        else
        {
            dotScreen.SetActive(true);
            handScreen.SetActive(false);
        }
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
        objectToLeave.GetComponent<Rigidbody>().isKinematic = false;
        Physics.IgnoreCollision(objectToLeave.GetComponent<Collider>(), player.transform.GetComponent<Collider>(), false);
        player.transform.GetComponent<PlayerController>().mouseSensitivity = 3;
        Cursor.lockState = CursorLockMode.Locked;
        objectToLeave.transform.parent = null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, player.transform.GetComponentInChildren<Camera>().transform.forward * 100);
        Gizmos.color = Color.green;
    }
}
