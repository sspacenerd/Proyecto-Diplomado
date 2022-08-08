using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Machine : MonoBehaviour
{
    [Range(0,6)]
    public int targetIndex;
    public int index;
    public bool canTouch = true;

   public IEnumerator Rotation()
    {
        canTouch = false;
        gameObject.transform.DORotate(transform.eulerAngles + new Vector3(0, 0, 60), 1);
        index++;
        switch (index)
        {
            case 6:
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
