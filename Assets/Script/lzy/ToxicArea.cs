using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// lzy-毒雾伤害脚本 
/// </summary>
public class ToxicArea : MonoBehaviour {
   // EnemyHp playerHp;
    
    private void OnTriggerStay(Collider other)
    {
        
        if (other.tag == Tag.Player)
        {
            if (PlayerCon.playerHp > 0)
            {
                PlayerCon.playerHp -= 0.05f;
                Debug.Log("毒雾伤害减少血量"+ PlayerCon.playerHp);
            }
           else
               other.GetComponent<PlayerCon>().Death();

        }
    }


}
