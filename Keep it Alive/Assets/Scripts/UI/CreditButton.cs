using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditButton : MonoBehaviour
{
    public void OnClick(){
        Camera.main.GetComponent<FollowTarget>().aim.transform.position += new Vector3(-15,0,0);
    }
}
