using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InteractionManager : MonoBehaviour
{
    bool isOpen;
    float initialPos;
    private Transform parent;
    private void Start()
    {
        initialPos = transform.localPosition.z;
    }
    public void Interaction()
    {
        switch (gameObject.tag)
        {
            case "drawer":
                {
                    if (!isOpen)
                    {
                        this.gameObject.transform.DOLocalMoveZ(initialPos * 2, 1);
                        isOpen = true;
                    }
                    else
                    {
                        this.gameObject.transform.DOLocalMoveZ(initialPos, 1);
                        isOpen = false;
                    }
                    break;
                }
            case "DoorR":
                {
                    if (!isOpen)
                    {
                        this.gameObject.transform.DOLocalRotate(new Vector3(0,-90f,0), 1);
                        isOpen = true;
                    }
                    else
                    {
                        this.gameObject.transform.DOLocalRotate(new Vector3(0, 0, 0), 1);
                        isOpen = false;
                    }
                    break;
                }
            case "DoorL":
                {
                    if (!isOpen)
                    {
                        this.gameObject.transform.DOLocalRotate(new Vector3(0, 90f, 0), 1);
                        isOpen = true;
                    }
                    else
                    {
                        this.gameObject.transform.DOLocalRotate(new Vector3(0, 0, 0), 1);
                        isOpen = false;
                    }
                    break;
                }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "CanPickUp")
        {
            collision.transform.parent = this.transform;

        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "CanPickUp" && PickUp.pickedGameObejct != null)
        {
            collision.transform.parent = PickUp.pickedGameObejct.transform.GetComponent<Transform>();
        }
        else if (collision.transform.tag == "CanPickUp" && PickUp.pickedGameObejct == null)
        {
            collision.transform.parent = null;
        }
    }
}
