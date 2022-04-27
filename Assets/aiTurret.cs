using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiTurret : MonoBehaviour
{

    //[SerializeField]
    //private Transform target;

    [SerializeField]
    private float shotInterval = 1f;
    private float bulletSpeed = 30f;

    [SerializeField]
    private Rigidbody bulletprefab;

    [SerializeField]
    private Transform bulletSpawn;




    void Start()
    {
        StartCoroutine(Fire());
    }

    IEnumerator Fire() {
        while(true) {
            yield return new WaitForSeconds(shotInterval);
            Rigidbody copy = Instantiate(bulletprefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            copy.AddRelativeForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);
        }
    }
}
