using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCon : MonoBehaviour {

    public string weaponTag;





    private void Start()
    {
        weaponTag = this.gameObject.tag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag==Tag.Player)
        {
            other.SendMessage("WeaponAcquisition", weaponTag);        
            Destroy(gameObject);

        }
    }
}
