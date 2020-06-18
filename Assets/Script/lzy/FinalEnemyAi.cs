using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// lzy-敌人AI控制，
/// </summary>


public class FinalEnemyAi : MonoBehaviour
{
    public Transform[] wayPoint;// 巡逻的四点
    public Transform targetPos; //追踪目标
    public float patrolrestTime = 3f; //巡逻休息时间
    public float patrolrestTimer = 0f;// 已经休息时间
    private int index = 0;
    private NavMeshAgent navAgent;   //网格巡路
    private Animator anim; //动画控制器
  
    public int randomWeapon;

    public GameObject Axe;
    public GameObject Sword;
    public GameObject Shield;
    public GameObject Katana;


    public Transform PlayerPos;
    public Vector3 InitialPos;  //初始点位置
    public float distanceToPlayer; //敌人和玩家距离
    public float distanceToInitial; //敌人和初始点距离;
    public float IsWarningRange = 5f; //触发器范围

    public float IsAttackRange = 1f; //攻击范围
    public float IsChaseRange = 4f; //触发追击范围
    public float IsMaxChaseRange=15f;


   // static public bool isPlayerDeath = false;
    static public bool isEnemyDeath = false;
     ////public bool IsenemyAttack = false;


    public enum EnemyState
    { 
        IDLE,      //idle状态
        PATROL,   //巡逻状态
        CHASE,    // 追击状态
        RETURN,  // 返回状态
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

    public  EnemyState enemyState = EnemyState.PATROL;
    public EnemyWeapon enemyWeapon = EnemyWeapon.Unarmed;

    int randomAttackMode;
    float randomTime = 0;
    




    private void Awake()
    {
        anim = GetComponent<Animator>();
        PlayerPos = GameObject.FindGameObjectWithTag(Tag.Player).transform;
        //  targetPos = GameObject.FindGameObjectWithTag(Tag.Player).transform;
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.destination = wayPoint[index].position; 
        navAgent.updatePosition = false;
        InitialPos = transform.position;
        //Axe.SetActive(false);
        //Sword.SetActive(false);
        //Katana.SetActive(false);
        //Shield.SetActive(false);
        // RandomWeapon();
      
        RandomWeapon();
      //  InitialWeapon();

    }

    private void Update()
    {
         CurrentStatus();
        switch (enemyState)
        {
            case EnemyState.PATROL:
                SendMessage("AudioManagement","PATROL");
                Patroling();
                break;
            case EnemyState.CHASE:
                SendMessage("AudioManagement", "CHASE");
                Chasing();
                break;
            case EnemyState.RETURN:

                ReturnInitial();
                break;
            case EnemyState.ATTACK:
                AttackPlayer();
                break;
            case EnemyState.IDLE:
                IdleStatus();
                break;

        }

    }


    ////void IsEnemyStartAttack()
    ////{
    ////    IsenemyAttack = true;
    ////}
    ////void IsEnemyEndAttack()
    ////{
    ////    IsenemyAttack = false;
    ////}

    void InitialWeapon()
    {
        if (Axe.activeInHierarchy == true)
        {
            enemyWeapon = EnemyWeapon.Axe;
        }
        else if(Sword.activeInHierarchy==true)
        {
            enemyWeapon = EnemyWeapon.Sword;
        }
        else if (Shield.activeInHierarchy == true)
        {
            enemyWeapon = EnemyWeapon.Shield;
        }
        else if (Katana.activeInHierarchy == true)
        {
            enemyWeapon = EnemyWeapon.Katana;
        }
        else
        {
            enemyWeapon = EnemyWeapon.Unarmed;
        }
    }



    void RandomWeapon()  //随机生成武器
    {
        randomWeapon =  Random.Range(1, 6);
       // print(randomWeapon);
       
        if (randomWeapon == 1)
        {
            enemyWeapon = EnemyWeapon.Unarmed;
        }
        else if (randomWeapon == 2)
        {
            enemyWeapon = EnemyWeapon.Axe;
            //激活斧头
            Axe.SetActive(true);
        }
        else if (randomWeapon == 3)
        {
            enemyWeapon = EnemyWeapon.Shield;
            Shield.SetActive(true);
        }
        else if (randomWeapon == 4)
        {
            enemyWeapon = EnemyWeapon.Sword;
            Sword.SetActive(true);
        }
        else
        {
            enemyWeapon = EnemyWeapon.Katana;
            Katana.SetActive(true);
        }
    }




    /// <summary>
    /// 巡逻控制
    /// </summary>
    void Patroling()
    {
         if (navAgent.remainingDistance > 1)
        {
           
            anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), 3, 1));
            navAgent.nextPosition = transform.position;
        }
        else
        {
            anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), 0, 1));
            navAgent.nextPosition = transform.position;
            navAgent.isStopped = true;
            patrolrestTimer += Time.deltaTime;
            if(patrolrestTimer > patrolrestTime)
            {
                index++;
                index %= 4;
                navAgent.destination = wayPoint[index].position;
                navAgent.updatePosition = false;
                patrolrestTimer = 0;
                navAgent.isStopped = false;

            }
            navAgent.nextPosition = transform.position;
        }

    }
    /// <summary>
    /// 追击控制
    /// </summary>
    void Chasing()
    {
        //Debug.Log("进行追击");
        //在进行追击前需要将相关数进行初始化，
        navAgent.isStopped = false;                              //开始进行导航,
        navAgent.destination = PlayerPos.position;               //赋值目标坐标 ，
        navAgent.nextPosition = transform.position;               // 导航与人物坐标同步
      //  anim.SetLayerWeight(1, 0);                                //攻击层的动画权重归0
        anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), 5.8f, 1));              //播发奔跑动画
        

    }
    /// <summary>
    ///  攻击玩家
    /// </summary>

    void AttackPlayer()
    {
      //      Debug.Log("进行攻击   ");
        //在进行追击前需要将相关数进行初始化，
        navAgent.isStopped = true;                              //停止导航,
        navAgent.destination = PlayerPos.position;               //赋值目标坐标 ，
        navAgent.nextPosition = transform.position;               // 导航与人物坐标同步
                                                                  // anim.SetLayerWeight(1, Mathf.Lerp(0,1,2));                                //攻击层的动画权重归1

        randomTime +=   Time.deltaTime;
        // Debug.Log(randomTime);
        if (randomTime>2)
        {
            randomTime = 0;
            //设置随机数，随机重击和轻击
            randomAttackMode = Random.Range(1, 6);  // ...........................................轻重击随机数kaiqi
          //  Debug.Log("llllllllllllllllllllllllllllllllllllllllllllllll"+randomAttackMode);
        }

        if (randomAttackMode < 4)
        {
            //anim.GetInteger("WeakAttack").ToString();
            anim.SetTrigger("WeakAttack");
            if (enemyWeapon == EnemyWeapon.Unarmed && anim.GetInteger("WeakAttack") == 0)
            {
                anim.SetInteger("AttackID", 0);
            }
            else if (enemyWeapon == EnemyWeapon.Sword && anim.GetInteger("WeakAttack") == 0)
            {
                // anim.SetInteger("AttackID", 1);    ................武器设置关闭
                anim.SetInteger("AttackID", 1);
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
            //  Debug.Log("轻击" + anim.GetInteger("AttackID") + "   " + anim.GetInteger("WeakAttack").ToString());

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
           // Debug.Log("重击" + anim.GetInteger("AttackID") + "   " + anim.GetInteger("StrongAttack").ToString());
         
        }
      
        //?????          3.31   15:38 轻重击切换问题，



        anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), 0f, 1));              //播发idle   动画

    }
    /// <summary>
    /// 返回初始点
    /// </summary>
    void ReturnInitial()
    {
      //  Debug.Log("进行返回");
        //在进行追击前需要将相关数进行初始化，
        navAgent.isStopped = false;                              //开始进行导航,
        navAgent.destination = wayPoint[index].position;               //目标坐标转到巡逻点 ，
        navAgent.nextPosition = transform.position;               // 导航与人物坐标同步
       // anim.SetLayerWeight(1, 0);                                //攻击层的动画权重归0
        anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), 3f, 1));              //播发walk动画
        if(Vector3.Distance(wayPoint[index].position,transform.position)<1)
        {
            enemyState = EnemyState.PATROL;
        }
    }
  
    
    /// <summary>
    /// 判断当前状态
    /// </summary>
     void  CurrentStatus()
    {
       //// Debug.Log("进行判断当前状态为:" + enemyState);  
        float disToPlayer = Vector3.Distance(transform.position, PlayerPos.position);   
          distanceToInitial= Vector3.Distance(transform.position,InitialPos);
        if ((distanceToInitial> IsMaxChaseRange|| (disToPlayer>IsMaxChaseRange)&&enemyState== EnemyState.CHASE)&&!PlayerCon. isPlayerDeath) //返回
        {
          //  Debug.Log(disToPlayer+"   lll     "+IsMaxChaseRange);
            enemyState = EnemyState.RETURN;
        }
        else if(((disToPlayer<IsChaseRange&&disToPlayer>IsAttackRange))&&!PlayerCon.isPlayerDeath ) //追击   待定加入IsChase (Bool)
        {
            enemyState = EnemyState.CHASE;
        }
        else if((disToPlayer<IsAttackRange)&& !PlayerCon.isPlayerDeath )  //攻击
        {
            enemyState = EnemyState.ATTACK;
        }
        else if( (disToPlayer > IsWarningRange&&(enemyState!= EnemyState.CHASE||enemyState!= EnemyState.RETURN)) && !PlayerCon.isPlayerDeath)  //巡逻
        {
            enemyState = EnemyState.PATROL;
        }
        else if (PlayerCon.isPlayerDeath)
        {
            enemyState = EnemyState.IDLE;
        }
        //else if (isEnemyDeath)
        //{
           
        //    anim.SetBool("isDead", true);
        //}
       

    }


    void IdleStatus()
    {
        anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), 0, 1));              //播发walk动画

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.tag==Tag.Interactable)
    //    { GameObject.Instantiate(Resources.Load("BlockAudioSource")); }
    //}


}
/* 两个方法，1.直接判断玩家和敌人的距离，由距离进行状态切换
            2. 使用大的触发器，进入触发器在进行检测
            这里使用第一种方法

       WeakAttack ， StrongAttack  以及AttackID多少时 执行那个攻击方式
       AttackID=1时 执行StrongAttack或 WeakAttack中SwordAttack
       AttackID=2时 执行WeakAttack中SwordRandomAttack

         AttackID=3时 执行

       AttackID=4时 执行 WeakAttack 中 KatanaAttack
     
        在写个 enum 激活相关武器，在执行相关武器的攻击。

  

 */
