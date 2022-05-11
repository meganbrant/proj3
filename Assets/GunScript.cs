using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    
    public Rigidbody bulletPrefab;
    public Transform bulletSpawn;


    [Range (10,100)]
    public float bulletSpeed = 50;
    public float fireRate = 0.1f;    

    public bool debug = false;

    [Header("Audio")]
    public AudioClip fire, clipUp, emptyAmmo;

    [Header("Ammo Manager")]
    public int totalAmmo = 30;
    public int clipSize = 10;
    public int clip = 0;
    

    private AudioSource aud;
    private Rigidbody rb;


    bool canShoot = true;
    //----------------------combo script ugh

    public enum elements {Fire, Ice, Earth, Wind};
    public elements elType = elements.Fire;

    public string name = "Gun";
    public int damage = 2;
    

    [Header("Randomization")]
    public List<string> names;
    public Vector2 damageRange;
    public Vector2 rateOfFireRange;

    void Start(){
      aud = this.gameObject.GetComponent<AudioSource>();
      rb = this.GetComponent<Rigidbody>();

    }

    public void Randomize() {
      name = names[(int)elType];
      damage = (int)Random.Range(damageRange.x, damageRange.y);
      fireRate = Random.Range(rateOfFireRange.x, rateOfFireRange.y);
    }

    public void Reload(){
      if(clip == clipSize){
        if(debug) Debug.Log("Clip is already full");
        return;
      }


      if(totalAmmo + clip >= clipSize){
        totalAmmo -= (clipSize - clip);
        clip = clipSize;
      } else{
        clip = totalAmmo +clip;
        totalAmmo = 0;
      }

        UIManager.ammoInClipText.text = "Clip: " + clip.ToString();
        UIManager.ammoInReserveText.text = "Ammo: " + totalAmmo.ToString();
    }

    public void Fire(){
      if(canShoot){
        if(debug) Debug.Log("Pow");
        if(clip>0){
          clip-=1;
          Rigidbody bullet = Instantiate(bulletPrefab,bulletSpawn.position, bulletSpawn.rotation);
          bullet.transform.Translate(0,0,1);
          bullet.AddRelativeForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);
          bullet bulletStuff = bullet.GetComponent<bullet>();
          bulletStuff.elType = this.elType;
          bulletStuff.damage = this.damage;
          aud.PlayOneShot(fire);

          UIManager.ammoInClipText.text = "Clip: " + clip.ToString();
          StartCoroutine(Cooldown());
      } else{
          if(debug) Debug.Log("Out of Ammo");
          aud.PlayOneShot(emptyAmmo);
        }
      }
    }

    public void PickupAmmo(){
      totalAmmo += 90;
      UIManager.ammoInReserveText.text = "Ammo: " + totalAmmo.ToString();
      aud.PlayOneShot(clipUp);    
    }

    // public void Pickup(){
    //   this.transform.SetParent(hand);
    //   rb.isKinematic = true;
    //   this.transform.position = hand.position;
    //   this.transform.rotation = hand.rotation;
    // }

    // public void Drop(){
    //   Debug.Log("Trying to drop()");
    //   this.transform.SetParent(null);
    //   this.transform.Translate(-Vector3.forward *2);
    //   rb.isKinematic = false;
    //   rb.AddRelativeForce(-Vector3.forward * 20, ForceMode.Impulse);
    // }


    IEnumerator Cooldown(){
       canShoot = false;
       yield return new WaitForSeconds(fireRate);
       canShoot = true;
    }
  
    
    }

  

