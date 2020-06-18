using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/// <summary>
/// lzy-敌人的血量管理，包括死亡后相关组件的删除
/// </summary>


public class EnemyHp : MonoBehaviour {

    public float enemyhp=100;
    public Slider enemyHpslider;
   // public float IsDamageTime=0;
    public Animator Anim;
    public float HPP;
    public float HitTime = 0;
    private int reward = 1;
    public float Hitinterval = 0.5f;

    // Use this for initialization
    void Awake () {
		Anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        HitTime += Time.deltaTime;
        enemyHpslider.value = enemyhp;
        HPP = enemyhp;
    }
    public void EnemyDeath()
    {

    }


    public void EnemyDamage(float playerDamage)
    {
         print("playerDamage " + playerDamage);
        // enemyhp -= 25;
        enemyhp = Mathf.Max(enemyhp -= playerDamage, 0);
       
        if (enemyhp <= 0f)
        {     
            enemyHpslider.value = 0;
            FinalEnemyAi.isEnemyDeath = true;
            Anim.SetBool("isDead", true);
           
           StartCoroutine("RemoveTest");
             if(reward == 1)
            {
                PlayerCon.playerScore += 80;
                PlayerCon.exp += 90;
                reward++;
            }
            BroadcastMessage("DestroySelf");
            
        }
      



        else
            FinalEnemyAi.isEnemyDeath = false;
    }
    int i = 0;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == Tag.PlayerInteractable && PlayerCon.IsPlayerAttack)
        {
               
            if (HitTime > 0.3)
            {
                i++;
                print(" 被空手攻击" + i);
                // SendMessage("EnemyDamage");
                EnemyDamage(PlayerCon.playerDamage);
                Debug.Log(enemyhp + "  " + i);
                HitTime = 0;
                GameObject.Instantiate(Resources.Load("BlockAudioSource"));
            }
              
        }
        if (other.tag == Tag.BulletSkills)
        {
            // SendMessage("EnemyDamage");
            EnemyDamage(PlayerCon.playerDamage+8);
            // Debug.Log(enemyhp + " 受到技能攻击 ");
        }

        //if (other.tag == Tag.PlayerWeapon&&PlayerCon.IsPlayerAttack)
        //{

        //    i++;
        //    if(HitTime>0.3)
        //    {
        //        HitTime = 0;
        //        EnemyDamage(PlayerCon.playerDamage);
        //       // Debug.Log(enemyhp + "  " + i);
        //        //   Debug.Log(enemyhp + "  " + i);
        //    }

        //}

        if (other.tag == Tag.Axe && PlayerCon.IsPlayerAttack)
        {
           
            //   i++;
            if (HitTime > 0.3)
            {
                i++;
               
                EnemyDamage(PlayerCon.playerDamage+7);
                print(" 被ft攻击" + i);
                HitTime = 0;
            }

        }
        else  if (other.tag == Tag.Sword && PlayerCon.IsPlayerAttack)
        {
            print(" 被jian攻击" + i);
            //   i++;
            if (HitTime > 0.3)
            {
                HitTime = 0;
                EnemyDamage(PlayerCon.playerDamage+5);
               
            }

        }
        else if (other.tag == Tag.Shield && PlayerCon.IsPlayerAttack)
        {
            print(" 被dun攻击" + i);
            // i++;
            if (HitTime > 0.3)
            {
                HitTime = 0;
                EnemyDamage(PlayerCon.playerDamage + 8);
              
            }

        }
        else if (other.tag == Tag.Katana && PlayerCon.IsPlayerAttack)
        {
    
            //  i++;
            if (HitTime > 0.3)
            {
                HitTime = 0;
                EnemyDamage(PlayerCon.playerDamage + 6);
               
            }

        }


    }


    IEnumerator RemoveTest()
    {
        yield return new WaitForSeconds(5);
        Destroy(GetComponent<CapsuleCollider>());//移除游戏物体BoxCollider组件
        Destroy(GetComponent<Rigidbody>());//移除游戏物体上的脚本
        Destroy(GetComponent<Animator>());//移除游戏物体上的脚本
        Destroy(GetComponent<EnemyHp>());//移除游戏物体上的脚本
        Destroy(GetComponent<Rigidbody>());//移除游戏物体上的脚本
        Destroy(GetComponent<NavMeshAgent>());//移除游戏物体上的脚本

        Destroy(GetComponent<FinalEnemyAi>());//移除游戏物体上的脚本
        Destroy(GetComponent<AudioManage>());//移除游戏物体上的脚本
        Destroy(GetComponent<EnemyHp>());//移除游戏物体上的脚本
        Destroy(GetComponent<AnimatorEvent>());//移除游戏物体上的脚本

       
    }

}
