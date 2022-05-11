using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    enum healthType{Player, Enemy, Object};

    [SerializeField]
    healthType hType = healthType.Object;

    public GunScript.elements elType = GunScript.elements.Fire;

    public AudioClip death, coinCollect; //kildt for dead player sound
    private AudioSource aud;

    public int health = 10;
    private bool isDying = false;
   

    void Start(){
        aud = this.gameObject.GetComponent<AudioSource>();
    }

    void Update(){
        if(health <= 0 && !isDying){
            //aud.PlayOneShot(kildt);    --doesn't work because the scene restarts before audio can play
            Death();
        }
        if(hType== healthType.Player){
            UIManager.playerHealthText.text = "Health: " + health.ToString();
        }
    }

    // LOOK AT ME LEARNING AND WRITING THIS ON MY OWN WOW
    public void CollectCoin(){
        health += 30;
        aud.PlayOneShot(coinCollect);
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.CompareTag("Bullet")){

            health -= other.gameObject.GetComponent<bullet>().damage;

            if(health <=0){
                Death();

            //health -=2;
            //--
            //Debug.Log("Magnitude: " + other.relativeVelocity.magnitude);
            //health -= (int)(other.relativeVelocity.magnitude * 0.05f);
            }
            
        }
    }


    void Death(){
        isDying = true;
        if(hType == healthType.Object){
            Destroy(this.GetComponent<Collider>());
            Destroy(this.GetComponent<Renderer>());
            for(int i = 0; i <4; i++){
                GameObject part = GameObject.CreatePrimitive(PrimitiveType.Cube);
                part.transform.localScale = Vector3.one * Random.Range(0.5f, 0.8f);
                part.transform.position = this.transform.position;
                part.transform.Translate(Random.Range(-.5f, -.5f),Random.Range(-.5f, -.5f),Random.Range(-.5f, -.5f));
                part.AddComponent<Rigidbody>();
            }
            Destroy(this.gameObject, 1);
            aud.PlayOneShot(death);
        } else if(hType == healthType.Enemy){
            this.gameObject.AddComponent<Rigidbody>();
            UIManager.KilledEnemy();
            //Destroy(this.gameObject, 5);
            StartCoroutine(GetSmallAndDie());
            aud.PlayOneShot(death);
        } else if(hType == healthType.Player) {
            aud.PlayOneShot(death);
            Application.LoadLevel(0);
        }
    }

    

    IEnumerator GetSmallAndDie() {
        float time= 4;
        float ObjectStartSize = this.transform.localScale.y;
        float objectSize = this.transform.localScale.y;
        while(objectSize > 0.1f) {
            this.transform.localScale -= Vector3.one * (ObjectStartSize / time) * Time.deltaTime;
            yield return new WaitForEndOfFrame();
            objectSize = this.transform.localScale.y;
        }
        Destroy(this.gameObject);
    }

}
