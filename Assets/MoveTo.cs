using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    [SerializeField]
    public Transform target;
    [SerializeField]
    private Rigidbody bulletprefab;
    [SerializeField]
    private Transform bulletSpawn;
    [SerializeField]
    float stoppingDistance = 5f;

    private NavMeshAgent agent;
    // bool canFire = false;

    [SerializeField]
    private float shotInterval = 5f;
    private float bulletSpeed = 15f;



    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if(distance > stoppingDistance) {
            agent.destination = target.position;
            agent.updateRotation = true;

        } else {
            agent.destination = transform.position;
            agent.updateRotation = false;
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 360);
            StartCoroutine(Fire());
            //StartCoroutine(WaitToFire());
        }
    }

    // void Pow() {
    //     if(canFire) {
    //         Debug.Log("Pow!");
    //     }
    // }
    // IEnumerator WaitToFire(){
    //     canFire = false;
    //     yield return new WaitForSeconds(2);
    //     canFire = true;
    // }

    IEnumerator Fire() {
        
        yield return new WaitForSeconds(shotInterval);
        Rigidbody copy = Instantiate(bulletprefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        copy.AddRelativeForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);
        
    }
}
