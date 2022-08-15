using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PuzzleManager : MonoBehaviour
{
    public Machine[] machines;
    public GameObject cajetin;
    private bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (machines[0].index == machines[0].targetIndex && machines[1].index == machines[1].targetIndex && machines[2].index == machines[2].targetIndex && !isOpen)
        {
            cajetin.transform.DOLocalMoveZ(-0.211f, 0.5f);
            isOpen = true;
        }
    }
}
