using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
   public int damage = 2;
   public GunScript.elements elType = GunScript.elements.Fire;

   void Start() {
      if(Random.value < .8f) {
         damage *= 2;
      }
   }

   void Update(){
      StartCoroutine(BulletDisappear());
      
   }

   IEnumerator BulletDisappear(){
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }


}
