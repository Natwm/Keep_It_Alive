using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public GameObject aim;
    private Rigidbody rigidbody;

    void Start(){
        rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        rigidbody.velocity = (aim.transform.position - transform.position) * 5;
    }
}
