using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossCon : MonoBehaviour {

    public Transform[] wayPoint;// 巡逻的四点
    public float patrolrestTime = 3f; //巡逻休息时间
    public float patrolrestTimer = 0f;// 已经休息时间
   
    private Animator anim; //动画控制器 
    public GameObject Sword;
    public List<Transform> Points;//si个目标点的链表
    private int bossindex = 0;//当前目标点的索引
  //  public  Quaternion targetRotation;         //怪物的目标朝向
    public Transform tarGetPos;   //追踪目标
    public bool boostrigger = false;
    public float defenceTime = 0;
    public int randomAttack;

    //Hp
    public float bossHp = 1000;
    public Slider bossHpSlider;
    public float Hitinterval = 0.2f;
    public float HitTime = 0;
    static public bool IsbossDeath=false;
     public bool IsDefense = false;
    public bool IsAttacked = false;
   

    public enum BossState
    {
        IDLE,      //idle状态
        PATROL,   //巡逻状态
        CHASE,    // 追击状态
        RETURN,  // 返回状态
        DEFENSE,  //防御状态
        ATTACK  // 攻击状态
    }
    public enum EnemyWeapon
    {
        Unarmed,    //空手
        Sword,      //剑
        Katana,     //武士刀
        Axe,        //斧头
        Shield      //盾
    }

    public BossState bossState = BossState.PATROL;
    public EnemyWeapon enemyWeapon = EnemyWeapon.Sword;

    int randomAttackMode;
    float randomTime = 0;
    private int reward=1;

    private void Awake()
    {
        anim = GetComponent<Animator>();


        Sword = GameObject.Find("SwordV_long");

        Sword.SetActive(true);
    
    }

    private void Update()
    {
        HitTime += Time.deltaTime;
        bossHpSlider.value = bossHp/1000;

        //print("bossHp: " + bossHp+ " bossHp/1000=  " + bossHp / 1000);

        CurrentStatus();
        switch (bossState)
        {
            case BossState.PATROL:          
                Patroling();
                break;
            case BossState.CHASE:       
                Chasing();
                break;
            case BossState.DEFENSE:
                DefenceState();
                break;
                
            case BossState.ATTACK:
                AttackPlayer();
                break;
            case BossState.IDLE:
                IdleStatus();
                break;



        }

    }


    /// <summary>
    /// 巡逻控制
    /// </summary>
    void Patroling()
    {       
       if(!boostrigger)
        {
            float dis = Vector3.Distance(transform.position, Points[bossindex].position);//获得当前玩家位置和目标点的距离
            if (dis <= 0.5f)
            {
                //如果获取的距离小于0.5米，说明已经到达，
                //已经到达目标点，更新索引
                patrolrestTimer += Time.deltaTime;
                if (patrolrestTimer > patrolrestTime)
                {
                    bossindex++;
                    bossindex %= 4;
                    patrolrestTimer = 0;
                }
                anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), 0, 1));
            }
            else
            {
                //进行移动
                anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), 3, 1));
                //  进行移动转向         
                    Quaternion rota = Quaternion.LookRotation(Points[bossindex].position - this.transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rota, 0.1f);
            }
        }
        else
        {
             bossState=BossState.CHASE;
        }

    }
    /// <summary>
    /// 追击控制
    /// </summary>
    void Chasing()
    {
       
        float dis = Vector3.Distance(transform.position, tarGetPos.position);//获得当前玩家位置和目标点的距离
      //  print("boss与玩家的距离:" + dis);

        if (dis <= 5f&&dis>1 )
        {
            anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), 3, 1));
            //  进行移动转向

            //Quaternion rota = Quaternion.LookRotation(tarGetPos.position - this.transform.position);
            //transform.rotation = Quaternion.Slerp(transform.rotation, rota, 0.1f);
            anim.SetInteger("DefenseID", 1);     // Mathf.Lerp(anim.GetInteger("DefenseID"),1, 1));
            IsDefense = true;
           // print("准备攻击或进行防御  "+ anim.GetInteger("DefenseID"));
        }
        else if(dis>5)
        {
            //进行移动
            anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), 3, 1));
            //  进行移动转向
            Quaternion rota = Quaternion.LookRotation(tarGetPos.position - this.transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rota, 0.1f);
        }
       
    }

    /// <summary>
    /// 防御状态
    /// </summary>
    void DefenceState()
    {
        //Quaternion rota = Quaternion.LookRotation(tarGetPos.position - this.transform.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rota, 0.1f);

            defenceTime += Time.deltaTime;
            anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), 0, 1));
           // anim.SetBool("IsBack", true);


            if (defenceTime < 0.5)
            {
                anim.SetInteger("DefenseID", 1);
                anim.SetInteger("AttackID", -5);                                 
            }
        else
        {
            anim.SetBool("IsBack", false);
            bossState = BossState.CHASE;
            anim.SetInteger("DefenseID", 5);
            defenceTime = 0;
        }


    }
    /// <summary>
    ///  攻击玩家
    /// </summary>

    void AttackPlayer()
    {
        anim.SetInteger("DefenseID", 5);
        IsDefense = false;

       
        randomTime += Time.deltaTime;
        // Debug.Log(randomTime);
        if (randomTime > 2)
        {
            randomTime = 0;
            //设置随机数，随机重击和轻击
            randomAttackMode = 2;///Random.Range(1, 6);  // ...........................................轻重击随机数kaiqi
            randomAttack = Random.Range(1, 4);                     //  Debug.Log("llllllllllllllllllllllllllllllllllllllllllllllll"+randomAttackMode);
        }

        if (randomAttackMode < 4)
        {
         
            anim.SetTrigger("WeakAttack");
            if (enemyWeapon == EnemyWeapon.Unarmed && anim.GetInteger("WeakAttack") == 0)
            {
                anim.SetInteger("AttackID", 0);
            }
            else if (enemyWeapon == EnemyWeapon.Sword && anim.GetInteger("WeakAttack") == 0)
            {
                // anim.SetInteger("AttackID", 1);    ................武器设置关闭
                anim.SetInteger("RandomAttack", randomAttack);
                anim.SetInteger("AttackID", 2);
            }
            else if (enemyWeapon == EnemyWeapon.Axe && anim.GetInteger("WeakAttack") == 0)
            {
                // anim.SetInteger("AttackID", 1);    ................武器设置关闭
                anim.SetInteger("AttackID", 1);
            }
            else if (enemyWeapon == EnemyWeapon.Katana && anim.GetInteger("WeakAttack") == 0)
            {
                // anim.SetInteger("AttackID", 4);................武器设置关闭
                anim.SetInteger("AttackID", 4);
            }
            else if (enemyWeapon == EnemyWeapon.Shield && anim.GetInteger("WeakAttack") == 0)
            {
                // anim.SetInteger("AttackID", 4);................武器设置关闭
                anim.SetInteger("AttackID", 1);
            }
        }
        else
        {
            anim.SetTrigger("StrongAttack");
            if (enemyWeapon == EnemyWeapon.Unarmed && anim.GetInteger("StrongAttack") == 0)
            {
                anim.SetInteger("AttackID", 0);
            }
            else if (enemyWeapon == EnemyWeapon.Sword && anim.GetInteger("StrongAttack") == 0)
            {
                //  anim.SetInteger("AttackID", 1);................武器设置关闭
                anim.SetInteger("AttackID", 0);
            }
        }
        anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), 0f, 1));              //播发idle   动画

    }
    /// <summary>
    /// 返回初始点
    /// </summary>
    ////////void ReturnInitial()
    ////////{
    ////////    //  Debug.Log("进行返回");
    ////////    //在进行追击前需要将相关数进行初始化，
    ////////    //////navAgent.isStopped = false;                              //开始进行导航,
    ////////    //////navAgent.destination = wayPoint[index].position;               //目标坐标转到巡逻点 ，
    ////////    //////navAgent.nextPosition = transform.position;               // 导航与人物坐标同步
    ////////    //////                                                          // anim.SetLayerWeight(1, 0);                                //攻击层的动画权重归0
    ////////    anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), 3f, 1));              //播发walk动画
    ////////    if (Vector3.Distance(wayPoint[index].position, transform.position) < 1)
    ////////    {
    ////////        bossState = BossState.PATROL;
    ////////    }
    ////////}


    /// <summary>
    /// 判断当前状态
    /// </summary>
    void CurrentStatus()
    {
        //// Debug.Log("进行判断当前状态为:" + enemyState);  
        if(!PlayerCon.isPlayerDeath)
        {
            if (tarGetPos != null)
            {
                Quaternion rota = Quaternion.LookRotation(tarGetPos.position - this.transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rota, 0.1f);
                float dis = Vector3.Distance(transform.position, tarGetPos.position);//获得当前玩家位置和目标点的距离
                if (bossState != BossState.DEFENSE)
                {
                    if (dis > 5 && dis > 1)
                    {
                        anim.SetInteger("DefenseID", 5);
                        anim.SetBool("IsBack", false);

                        bossState = BossState.CHASE;
                    }
                    else if (dis < 1)
                    {
                        anim.SetInteger("DefenseID", 5);
                        anim.SetBool("IsBack", false);

                        bossState = BossState.ATTACK;
                    }
                    else if (PlayerCon.isPlayerDeath)
                    {
                        anim.SetInteger("DefenseID", 5);
                        anim.SetBool("IsBack", false);

                        bossState = BossState.IDLE;
                    }
                }
                else
                    bossState = BossState.DEFENSE;

            }
            else
            {
                bossState = BossState.PATROL;

            }
        }
      
        else 
        {
            bossState = BossState.IDLE;
        }

        
    
 

    }

    public void BossDamage(float playerDamage)
    {

        print("playerDamage " + playerDamage);
        // enemyhp -= 25;
        if (IsDefense)
        {
            bossHp = Mathf.Max(bossHp = bossHp- playerDamage+20, 0);
        }
        else
        {
            bossHp = Mathf.Max(bossHp -= playerDamage, 0);
        }

        if (bossHp <= 0f)
        {
            bossHpSlider.value = 0;

            IsbossDeath = true;
            SendMessageUpwards("AirWall");
            anim.SetBool("isDead", true);

            StartCoroutine("RemoveTest");
            if (reward == 1)
            {
                PlayerCon.playerScore += 500;
                PlayerCon.exp += 450;
                reward++;
            }
            BroadcastMessage("DestroySelf");

        }
        else
            IsbossDeath = false;
    }







    void IdleStatus()
    {
        anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), 0, 1));              //播发walk动画

    }

 
    void BossTrigger()
    {
        //boss开始移向玩家，即将其目标坐标赋值，
        boostrigger = true;

        tarGetPos = GameObject.FindWithTag(Tag.Player).transform;

    }
    IEnumerator RemoveTest()
    {
       
        yield return new WaitForSeconds(4);
        Destroy(GetComponent<CapsuleCollider>());//移除游戏物体BoxCollider组件
        Destroy(GetComponent<Rigidbody>());//移除游戏物体上的脚本
        Destroy(GetComponent<Animator>());//移除游戏物体上的脚本
        Destroy(GetComponent<Rigidbody>());//移除游戏物体上的脚本
        Destroy(GetComponent<AnimatorEvent>());//移除游戏物体上的脚本
        Destroy(GetComponent<BossCon>());
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.PlayerInteractable && PlayerCon.IsPlayerAttack)
        {

            if (HitTime > 0.3)
            {
              
                print(" 被空手攻击" );
                // SendMessage("EnemyDamage");
                BossDamage(PlayerCon.playerDamage);
                Debug.Log(bossHp + "  " );
                HitTime = 0;
                GameObject.Instantiate(Resources.Load("BlockAudioSource"));
                bossState = BossState.DEFENSE;
            }

        }
        if (other.tag == Tag.BulletSkills)
        {
            // SendMessage("EnemyDamage");
            BossDamage(PlayerCon.playerDamage + 8);
            // Debug.Log(enemyhp + " 受到技能攻击 ");
            bossState = BossState.DEFENSE;
        }

        int i = 0;
        if (other.tag == Tag.Axe && PlayerCon.IsPlayerAttack)
        {
          
            //   i++;
            if (HitTime > 0.3)
            {
                i++;

                BossDamage(PlayerCon.playerDamage + 7);
                print(" 被ft攻击" + i);
                HitTime = 0;
            }
            bossState = BossState.DEFENSE;
        }
        else if (other.tag == Tag.Sword && PlayerCon.IsPlayerAttack)
        {
            print(" 被jian攻击" + i);
            //   i++;
            if (HitTime > 0.3)
            {
                HitTime = 0;
                BossDamage(PlayerCon.playerDamage + 5);

            }
            bossState = BossState.DEFENSE;
        }
        else if (other.tag == Tag.Shield && PlayerCon.IsPlayerAttack)
        {
           
            print(" 被dun攻击" + i);
            // i++;
            if (HitTime > 0.3)
            {
                HitTime = 0;
                BossDamage(PlayerCon.playerDamage + 8);

            }
            bossState = BossState.DEFENSE;
        }
        else if (other.tag == Tag.Katana && PlayerCon.IsPlayerAttack)
        {
           
            if (HitTime > 0.3)
            {
                HitTime = 0;
                BossDamage(PlayerCon.playerDamage + 6);

            }
            bossState = BossState.DEFENSE;
        }
    }




}
