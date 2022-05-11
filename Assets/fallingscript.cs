using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingPlatform : MonoBehaviour
{

    public AnimationCurve curve;
    bool platformIsActive = false;
    public bool randomize = true;
    public float resetInterval = 3;

    Rigidbody rb;
    Vector3 startPosition;
    Quaternion startRotation;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        startPosition= this.transform.position;

        if(randomize){
            resetInterval += Random.Range(-resetInterval/5, resetInterval/5);
        }
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        Debug.Log(other.name + " has run into us");
            StartCoroutine(WaitToFall());

        
    }

    IEnumerator WaitToFall(){
        if(!platformIsActive){
            platformIsActive = true;
            yield return new WaitForSeconds(2);
            rb.isKinematic = false;
            StartCoroutine(ResetTile());
        }
    }

     IEnumerator ResetTile(){
        yield return new WaitForSeconds(2);
        rb.isKinematic = true;

        Vector3 pointB = startPosition;
        Vector3 pointA =  this.transform.position;
        Quaternion rotA = this.transform.rotation;
        Quaternion rotB = this.transform.rotation;

        float timer = 0;
        while(timer < 1){
            this.transform.position = Vector3.Lerp(pointA, pointB, curve.Evaluate(timer));
            this.transform.rotation = Quaternion.Lerp(rotA, rotB, curve.Evaluate(timer));
            timer += Time.deltaTime;
            yield return null;
        }
        this.transform.position = startPosition;
        this.transform.rotation = startRotation;

        platformIsActive = false;
     }

}
