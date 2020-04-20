using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mCamera : MonoBehaviour
{
    Vector3 initPosition;

    void Start()
    {
        initPosition = transform.position;
    }

    public void ResetPosition(){
        GetComponent<FollowTarget>().aim.transform.position = initPosition;
    }
}
