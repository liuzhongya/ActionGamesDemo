using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// lzy-玩家攻击检测点
/// </summary>
public class PlayerHit : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        print(PlayerCon.IsPlayerAttack);

        //if (other.tag == Tag.Enemy&& PlayerCon.IsPlayerAttack)
        //{
        //    print("空手攻击");
        //    other.GetComponent<EnemyHp>().EnemyDamage(PlayerCon.playerDamage);

        //    GameObject.Instantiate(Resources.Load("BlockAudioSource"));
        //    //todo 敌人血量减少
        //    //  SendMessage("EnemyDamage");
        //   // lx.EnemyDamage();
        //}

    }
 



}
