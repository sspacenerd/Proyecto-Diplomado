using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Machine : MonoBehaviour
{
    [Range(0,5)]
    public int targetIndex;
    public int index;
    public bool canTouch = true;
    public IEnumerator Rotation()
    {
        canTouch = false;
        transform.Rotate(new Vector3(transform.rotation.x + 72, 0,0));
        index++;
        switch (index)
        {
            case 5:
                {
                    index = 0;
                    break;
                }
        }
        yield return new WaitForSeconds(1);
        canTouch = true;
        yield break;
    }
}
