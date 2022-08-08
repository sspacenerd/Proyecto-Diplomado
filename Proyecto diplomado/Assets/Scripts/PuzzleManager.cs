using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public Machine[] machines;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(machines[0].index == machines[0].targetIndex && machines[1].index == machines[1].targetIndex && machines[2].index == machines[2].targetIndex)
        {
            Debug.Log("aaaaa");
        }
    }
}
