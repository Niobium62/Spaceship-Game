using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject follow;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (follow != null)
        {
            transform.position = follow.transform.position;
        }
    }
}
