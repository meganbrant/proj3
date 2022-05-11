using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator : MonoBehaviour
{
    public float min=2f;
    public float max=3f;
        
    void Start () {
           
        min=transform.position.y;
        max=transform.position.y+12.5f;
       
    }
       
    void Update () {
           
        //transform.position.x, transform.position =new Vector3(Mathf.PingPong(Time.time*2,max-min)+min, transform.position.z);
        transform.position = new Vector3(transform.position.x, (Mathf.PingPong(Time.time*2,max-min)+min), transform.position.z);
    }

}

