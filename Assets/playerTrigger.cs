using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTrigger : MonoBehaviour
{
    public GunScript gun;
    public Health health;

    void Start() {
        if(gun == null) {
            Debug.LogError("you forgot to assign the gun here.");
            gun = GameObject.Find("gun").GetComponent<GunScript>();
        }
    }

    void OnTriggerEnter(Collider other){
        Debug.Log("I have hit " + other.gameObject.name);
        if(other.gameObject.CompareTag("ammo pickup")) {
            gun.PickupAmmo();
            Destroy(other.gameObject);
        } 
        if(other.gameObject.CompareTag("coin")){
           health.CollectCoin();
           Destroy(other.gameObject);
        }
    }

   
  

}
