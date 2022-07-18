using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public float rayDistance = 3f;
    public GameObject player, grabbedObject;
    private Camera myCam;
    public Transform pickUpGameObject;
    bool isGrabbed;
    RaycastHit hit;
    GameObject inventorySystemReference;
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
            if (!isGrabbed)
            {
                if (Physics.Raycast(transform.position, myCam.transform.forward, out hit, rayDistance))
                {
                    if (hit.transform.tag == "CanPickUp")
                    {
                        PickUpObject(hit.transform.gameObject);
                        isGrabbed = true;
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
        }
        if (isGrabbed && Input.GetKey(KeyCode.R))
        {
            RotateObject(hit.transform.gameObject);
        }
        if(isGrabbed && Input.GetKeyUp(KeyCode.R))
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
