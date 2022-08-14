using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// PickUp.cs
// Usando referencias de:
// youtube.com/watch?v=pPcYr3tL3Sc
public class PickUp : MonoBehaviour
{
    [SerializeField] private GameObject dotScreen, handScreen, player;
    [SerializeField] private Transform holdGameObject;

    private Camera myCam;
    private bool isPicked;
    private RaycastHit hit;
    private InventorySystem inventorySystemReference;
    private LayerMask interactionLayer = 1 << 6, pickUpLayer = 1 << 7, pickedUpLayer = 1 << 8;

    public float rayDistance = 3f;

    public static GameObject pickedGameObejct;
    public GameObject[] aaa;


    void Start()
    {
        myCam = GetComponent<Camera>();
        inventorySystemReference = GameObject.FindGameObjectWithTag("GameManager").GetComponent<InventorySystem>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Physics.Raycast(transform.position, myCam.transform.forward, out hit, rayDistance, interactionLayer) && GameManager.gameIsPaused == false)
        {
            if (hit.transform.tag == "CanPickUp" || hit.transform.tag == "Grillo")
            {
                dotScreen.SetActive(false);
                handScreen.SetActive(true);
            }
            else
            {
                dotScreen.SetActive(true);
                handScreen.SetActive(false);
            }
        }
        else
        {
            dotScreen.SetActive(true);
            handScreen.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(transform.position, myCam.transform.forward, out hit, rayDistance) && GameManager.gameIsPaused == false)
            {
                if (!isPicked && hit.transform.tag == "CanPickUp")
                {
                    PickUpObject(hit.transform.gameObject);
                    isPicked = true;
                }
                else if (inventorySystemReference.GetComponent<InventorySystem>().isOpen == false && Input.GetMouseButtonDown(0) && hit.transform.gameObject == pickedGameObejct)
                {
                    LeaveObject(hit.transform.gameObject);
                    isPicked = false;
                    pickedGameObejct = null;
                }
                if(hit.transform.tag == "Safe" && hit.transform.gameObject.GetComponent<Machine>().canTouch)
                {
                    StartCoroutine(hit.transform.GetComponent<Machine>().Rotation());
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E)) //Si yo presiono la E
        {
            if (Physics.Raycast(transform.position, myCam.transform.forward, out hit, rayDistance, interactionLayer) && GameManager.gameIsPaused == false) //Y si yo lanzo un rayo desde mi posicion hasta el fordward de la camara
            {
                if (!isPicked && hit.transform.tag == "CanPickUp") //Si yo no tengo nada agarrado y el tag de HIT es CanPickUp
                {
                    hit.transform.gameObject.GetComponent<ItemPickUp>().PickUp(); //Es para agarrar algo y guardarlo en el inventario
                }
                if (hit.transform.GetComponent<InteractionManager>())
                {

                    hit.transform.gameObject.GetComponent<InteractionManager>().Interaction();

                }
                switch (hit.transform.tag)
                {
                    case "Door":
                        {
                            for (int i = 0; i < inventorySystemReference.GetComponent<InventorySystem>().items.Count; i++)
                            {
                                if ((inventorySystemReference.items[i].itemType == Item.ItemType.key))
                                {
                                    inventorySystemReference.Remove(inventorySystemReference.items[i]);
                                }
                            }
                            break;
                        }
                    case "Grillo":
                        {
                            Destroy(hit.transform.gameObject);
                            StartCoroutine(GameManager.gameManagerInstance.Grillo());
                            break;
                        }
                    case "Cuadro":
                        {
                            for (int i = 0; i < inventorySystemReference.GetComponent<InventorySystem>().items.Count; i++)
                            {
                                if (inventorySystemReference.items[i].itemType == Item.ItemType.photo)
                                {
                                    aaa[i].SetActive(true);
                                }
                            }
                            break;
                        }
                }
            }
        }
        /*
        if (Physics.Raycast(transform.position, myCam.transform.forward, out hit, rayDistance, raycastLayerMask) && GameManager.gameIsPaused == false)
        {
        ]
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
        if (isPicked && Input.GetKey(KeyCode.R) && GameManager.gameIsPaused == false)
        {
            RotateObject(hit.transform.gameObject);
        }
        if(isPicked && Input.GetKeyUp(KeyCode.R) && GameManager.gameIsPaused == false)
        {
            StopRotating();
        }

    }
    void PickUpObject(GameObject objectToPick)
    {
        pickedGameObejct = objectToPick;
        pickedGameObejct.layer = LayerMask.NameToLayer("pickedUp");
        objectToPick.GetComponent<Rigidbody>().isKinematic = true;
        objectToPick.transform.position = holdGameObject.transform.position;
        objectToPick.transform.LookAt(player.transform.position);
        Physics.IgnoreCollision(objectToPick.GetComponent<Collider>(), player.transform.GetComponent<Collider>(), true);
        objectToPick.transform.parent = myCam.transform;
    }
    void RotateObject(GameObject objectToRotate)
    {
        PlayerController.mouseSensitivity = 0;
        objectToRotate.transform.localRotation = Quaternion.Euler(Input.mousePosition.y, -Input.mousePosition.x, 0);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }
    void StopRotating()
    {
        PlayerController.mouseSensitivity = Settings.currentMouseSensibility;
        Cursor.lockState = CursorLockMode.Locked;

    }
    void LeaveObject(GameObject objectToLeave)
    {
        if (hit.transform.gameObject.GetComponent<ItemPickUp>())
        {
            pickedGameObejct.layer = LayerMask.NameToLayer("InteractionLayer");
        }
        else
        {
            pickedGameObejct.layer = LayerMask.NameToLayer("PickUp");
        }
        objectToLeave.GetComponent<Rigidbody>().isKinematic = false;
        Physics.IgnoreCollision(objectToLeave.GetComponent<Collider>(), player.transform.GetComponent<Collider>(), false);
        PlayerController.mouseSensitivity = Settings.currentMouseSensibility;
        Cursor.lockState = CursorLockMode.Locked;
        objectToLeave.transform.parent = null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, player.transform.GetComponentInChildren<Camera>().transform.forward * 3);
        Gizmos.color = Color.green;
    }
}
