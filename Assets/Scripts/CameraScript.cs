using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject target;

    
    void Start()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
          {
             Application.Quit();
          } 
    }

    void LateUpdate()
    {
        this.transform.position = new Vector3(target.transform.position.x, this.transform.position.y, this.transform.position.z);
        if (Input.GetKeyDown(KeyCode.Escape))
          {
             Application.Quit();
          } 
    }
}