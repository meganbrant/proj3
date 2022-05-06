using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpawner : MonoBehaviour
{

    public List<GunScript> gunPrefabs;
    public Transform target;


    private bool onCooldown = false;

    void SpawnItem() {
        if(!onCooldown) {
            int elementType = Random.Range(0,4);
            GunScript newGun = Instantiate(gunPrefabs[Random.Range(0, gunPrefabs.Count)], transform.position, transform.rotation);
            newGun.elType = (GunScript.elements) elementType;
           // newGun.Randomize();
           StartCoroutine(Cooldown());
        }
    }

    void Update() {
        
        if(Vector3.Distance(transform.position, target.position) < 4f) {
            Debug.Log("wow");
            SpawnItem(); 
        }

    }

    IEnumerator Cooldown() {
        onCooldown = true;
        yield return new WaitForSeconds(2);
        onCooldown = false;
    }

}
