using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControler : MonoBehaviour
{
    Orbiter orbiter;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Vector2 inputMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(inputMouse, Vector2.zero, Mathf.Infinity);
            if(hit.collider != null && hit.transform.tag == "Orbiter"){
                orbiter = hit.transform.GetComponent<Orbiter>();
                orbiter.Select();
            }
        }
        
        if(Input.GetMouseButton(0) && orbiter !=null){
            orbiter.Drag();
        }

        if(Input.GetMouseButtonUp(0)){
            orbiter = null;
        }

        if(Input.GetKeyDown("escape")){
            Camera.main.GetComponent<mCamera>().ResetPosition();
            UIManager.instance.Return();
        }
    }
}
