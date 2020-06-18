using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;



public class Weapon : MonoBehaviour
{
    public string Name { get; set; }

}


    public class PlayerCon : MonoBehaviour {


    private Animator animator;
    private Animator anim; //动画控制器
    public  Rigidbody rig;
    private float IsIsjumpheight = 0;
    private float HitTime = 0;
    private bool IsUpgrade = false;
    private float   Upgrade = 0;

    public Vector3 eulerAngleVelocity = new Vector3(0, 100, 0);
   // public Transform targetPos;

    static public bool isPlayerDeath = false;
    static public bool isEnemyDeath = false;

     static public bool IsPlayerAttack=false;
     static public float playerFullHp=1000;
    static public float playerHp = 1000;
    static public float playerDamage=25;
    static public float playerScore = 100;
    static public float exp = 0;         //经验值
    static public float nextexp = 150;         //经验值
    static  public float level = 1;        //等级 初始为1级
    public Rigidbody shell;
    public Transform muzzle;
    public Transform ShellPos;
    public  Quaternion rotation;
    public float launchForce = 10;


    static public bool IsScence=false;

    private int wallLayerMsk;
    private bool IsWall = false;


    List<Weapon> weaponlList = new List<Weapon>();
    public GameObject Axe;
    public GameObject Sword;
    public GameObject Shield;
    public GameObject Katana;

    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject foot;

    public enum PlayerWeapon
    {
        Unarmed,    //空手
        Sword,      //剑
        Katana,     //武士刀
        Axe,        //斧头
        Shield      //盾
    }

   
    public PlayerWeapon playerWeapon = PlayerWeapon.Unarmed;
 //   public PlayerState playerState = PlayerState.IDLE;

    int randomAttackMode;
    int RandomAttackMode;
    float randomTime = 0;
    int RandomAttack;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        wallLayerMsk = LayerMask.GetMask("wall");
        Object.DontDestroyOnLoad(transform.gameObject);
       // targetPos = GameObject.FindWithTag("TargetPos").transform;
    }
    private void Start()
    {
        Time.timeScale = 1;
        PlayerPos();
        Axe.SetActive(false);
        Sword.SetActive(false);
        Katana.SetActive(false);
        Shield.SetActive(false);
       // ShellPos=

    }
    private void Update()
    {
        Quaternion deltaRotation = Quaternion.Euler(-eulerAngleVelocity * Time.deltaTime);
        rig.MoveRotation(rig.rotation * deltaRotation);

        // print(playerHp);
        HitTime += Time.deltaTime;
        PlayerPos();

       // if (IsUpgrade)
       // { }


            IsUpLevel();
        MovePlayer();
        AttackPlayer();
      //  print(IsPlayerAttack + "             s");
    }
    void FixedUpdate()
    {
        MovePlayer();
       // Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
      //  rig.MoveRotation(rig.rotation * deltaRotation);

    }



    void PlayerPos()
    {
        if (IsScence)
        {
            if (SceneManager.GetActiveScene().name == "Level1")
            {
                this.transform.position = new Vector3(7, 0.2f, 20);
              //  this.transform.position = targetPos.position;
                IsScence = false;
            }
            else if (SceneManager.GetActiveScene().name == "Level2")
            {
                this.transform.position = new Vector3(4, 0.2f, 25);
              //  this.transform.position = targetPos.position;
                IsScence = false;
            }
            else if (SceneManager.GetActiveScene().name == "Level3")
            {
                this.transform.position = new Vector3(25, 0.2f, 25);
               // this.transform.position = targetPos.position;
                IsScence = false;
            }
          
        }


       
    }




    void MovePlayer()
    {
       // Debug.Log("进行移动   ");

        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift))
        {
           // Debug.Log("WWWWWWW   ");
            anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), 5.9f, 1));
        }
        else if(Input.GetKey(KeyCode.W)&& Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), 7, 1));
           // Debug.Log("冲刺跑");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("IsBack", true);
        }     
        else
        {
            anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), 0f, 0.5f));
            anim.SetBool("IsBack", false);
        }
         if (Input.GetKey(KeyCode.D) && !isPlayerDeath)
        {

            Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
            rig.MoveRotation(rig.rotation * deltaRotation);
          //  print("按下D键");
        }
        else if (Input.GetKey(KeyCode.A)&&!isPlayerDeath)
        {
            Quaternion deltaRotation = Quaternion.Euler(-eulerAngleVelocity * Time.deltaTime);
            rig.MoveRotation(rig.rotation * deltaRotation);
            //   Debug.Log("AAAAAAAAA");
          // print("按下A键");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
     
            anim.SetTrigger("IsJump"); 
          
        }

    }

    /// <summary>
    ///  攻击玩家
    /// </summary>

    void AttackPlayer()
    {
       
        //IsPlayerAttack = false;
        //  Debug.Log("进行攻击   ");
        randomTime += Time.deltaTime;
        //  Debug.Log(randomTime);
        if (randomTime > 2)
        {
            randomTime = 0;
            //设置随机数，随机重击和轻击
            randomAttackMode = 2; //Random.Range(1, 6); ///  ...........................................轻重击随机数关闭
                                  //  Debug.Log("llllllllllllllllllllllllllllllllllllllllllllllll"+randomAttackMode);
          
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.J))
        {
            RandomAttackMode = Random.Range(1, 9);
            RandomAttack = Random.Range(1, 4);
        //    print(RandomAttack + "SSSSSSSSSSSSSSSSSSSSSSS"+ RandomAttackMode);
            // IsPlayerAttack = true;
            if (randomAttackMode < 4)
            {
                //anim.GetInteger("WeakAttack").ToString();
                anim.SetTrigger("WeakAttack");
                if (playerWeapon == PlayerWeapon.Unarmed && anim.GetInteger("WeakAttack") == 0)
                {
                    anim.SetInteger("AttackID", 0);
                }
                else if (playerWeapon == PlayerWeapon.Sword && anim.GetInteger("WeakAttack") == 0)
                {
                   
                    if (RandomAttackMode > 6)
                    {
                        anim.SetInteger("AttackID", 1);
                        
                    }
                    else  
                    {
                        anim.SetInteger("AttackID", 2);

                        if (RandomAttack == 1)
                        {
                            anim.SetInteger("RandomAttack", 0);
                            print("攻击1   " + RandomAttack + "     " + RandomAttackMode);
                        }
                        else if (RandomAttack == 2)
                        {
                            anim.SetInteger("RandomAttack", 1);
                            print("攻击2 " + RandomAttack + "     " + RandomAttackMode);
                        }
                        else
                        {
                            anim.SetInteger("RandomAttack", 2);
                            print("攻击3 " + RandomAttack + "     " + RandomAttackMode);
                        }
                    }
                    //   ................武器设置关闭
                    // GameObject.Instantiate(Resources.Load("Attack1Source"), this.transform);

                    //SwordHitBox.enabled = true;
                }
                else if (playerWeapon == PlayerWeapon.Axe && anim.GetInteger("WeakAttack") == 0)
                {
                    if (RandomAttackMode > 6)
                    {
                        anim.SetInteger("AttackID", 1);
                       
                    }
                    else
                    {
                        anim.SetInteger("AttackID", 2);
                        if (RandomAttack == 1)
                        {
                            anim.SetInteger("RandomAttack", 0);
                        }
                        else if (RandomAttack == 2)
                        {
                            anim.SetInteger("RandomAttack", 1);
                        }
                        else
                        {
                            anim.SetInteger("RandomAttack", 2);
                        }
                    }
                }
                else if (playerWeapon == PlayerWeapon.Shield && anim.GetInteger("WeakAttack") == 0)
                {
                    if (RandomAttackMode >6)
                    {
                        anim.SetInteger("AttackID", 1);
                        
                    }
                    else
                    {
                        anim.SetInteger("AttackID", 2);
                        if (RandomAttack == 1)
                        {
                            anim.SetInteger("RandomAttack", 0);
                        }
                        else if (RandomAttack == 2)
                        {
                            anim.SetInteger("RandomAttack", 1);
                        }
                        else
                        {
                            anim.SetInteger("RandomAttack", 2);
                        }
                    }
                }
                else if (playerWeapon == PlayerWeapon.Katana && anim.GetInteger("WeakAttack") == 0)
                {
                    anim.SetInteger("AttackID", 4);   //................武器设置关闭

                    //检查是否正在播放reload动画.
                    AnimatorStateInfo stateinfo = anim.GetCurrentAnimatorStateInfo(1);  // # 参数表示动画层的id
                    bool play_ing_flag = stateinfo.IsName("KatanaAttack");
                    print(play_ing_flag);
                    //KanataHitBox.enabled = true;
                }
                //  Debug.Log("轻击" + anim.GetInteger("AttackID") + "   " + anim.GetInteger("WeakAttack").ToString());

            }
            else
            {
                anim.SetTrigger("StrongAttack");
                if (playerWeapon == PlayerWeapon.Unarmed && anim.GetInteger("StrongAttack") == 0)
                {
                    anim.SetInteger("AttackID", 0);
                }
                else if (playerWeapon == PlayerWeapon.Sword && anim.GetInteger("StrongAttack") == 0)
                {
                    //  anim.SetInteger("AttackID", 1);................武器设置关闭
                    anim.SetInteger("AttackID", 0);
                }
                // Debug.Log("重击" + anim.GetInteger("AttackID") + "   " + anim.GetInteger("StrongAttack").ToString());
         }

        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            anim.SetTrigger("MagicAttack");
            // GameObject.Instantiate(Resources.Load("Shell 1"));
            // Fire();
            StartCoroutine(Fire());
        }

    }

    public IEnumerator Fire()
    {

        yield return new WaitForSeconds(0.8f);
       Rigidbody shellInstance = Instantiate(shell, muzzle.position, muzzle.rotation) as Rigidbody;
       shellInstance.velocity = launchForce * muzzle.forward;
        // GameObject.Instantiate(Resources.Load("Shell"),this.transform.position+new Vector3(0,1,1),this.transform.rotation);


    }
    void IdleStatus()
    {
        anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), 0, 1));              //播发walk动画

    }

 

        void IsUpLevel() //是否进行升级
    {
        float a;
        a = exp / nextexp;    //每100经验升级 也就是4次有效攻击升一级

        if (a >= 1)
        {
            uplevel();     //调用升级函数
       //     print(level);
        }
      //  print("Level "+ level + " a " + a + " exp " + exp+"  nextexp "+nextexp);
        if (level == 1)
        {
            nextexp = 150;
        }
        else if (level == 2)
        {
            nextexp = 250;
        }
        else if (level == 3)
        {
            nextexp = 500;
        }
        else if (level == 4)
        {
            nextexp = 900;
        }
        else if (level == 5)
        {
            nextexp = 1500;
        }
        else if (level == 6)
        {
            nextexp = 2200;
        }
        else if (level == 7)
        {
            nextexp = 99999999;
        }

    }

    void uplevel()        //进行升级
    {
     //  if(Upgrade == 0)
        {
            GameObject.Instantiate(Resources.Load("UpgradeSoure"), this.transform);
            
            level = level + 1;
            print("uplevel " + level);
            playerFullHp = 75 + 25 * level;  //1级的时候血量100
               playerHp = playerFullHp;
            playerDamage = playerDamage + 5;
            //  IsUpgrade = false;
            Upgrade++;

        }


    }

    public void Death()
    {
       int randomAttackDamage = Random.Range(4, 10);
        playerHp = playerHp - randomAttackDamage;
        if (playerHp <= 0)
        {
            anim.SetBool("isDead", true);
            isPlayerDeath = true;
            print("血量为0"+ isPlayerDeath);
        }
    }

  
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.Interactable|| other.tag == Tag.Weapon)
        {
           // print("lxxxxxx");
            if (HitTime > 0.3)
            {
                HitTime = 0;
                Death();
            }
        }
    }
   //void WeaponSwitching()
   // {
   //     for(int i = 0; i < weaponlList.Count; i++)
   //     {
   //         foreach (Weapon element in weaponlList)
   //         {
   //             if (element)
   //             {

   //             }

   //         }
   //     }


       

   // }

    /// <summary>
    /// 武器的获取
    /// </summary>
    /// Acquisition
    void WeaponAcquisition(string weaponName)
    {
        Weapon possessionWeapon = new Weapon();

        print(weaponName);
        if (weaponName== Tag.Axe)
        {
            leftHand.SetActive(false);
            rightHand.SetActive(false);
            foot.SetActive(false);

            possessionWeapon.Name = weaponName;
            GameObject.Instantiate(Resources.Load("PickUpSource"), this.transform);
            //切换武器
             Axe.SetActive(true);
          //  playerDamage += 10;
            Sword.SetActive(false);
            Katana.SetActive(false);
            Shield.SetActive(false);
             playerWeapon = PlayerWeapon.Axe;

            weaponlList.Add(possessionWeapon);
        }
        else if (weaponName == Tag.Shield)
        {
            leftHand.SetActive(false);
            rightHand.SetActive(false);
            foot.SetActive(false);

            possessionWeapon.Name = weaponName;
            GameObject.Instantiate(Resources.Load("PickUpSource"), this.transform);
            //切换武器
            Shield.SetActive(true);
            Axe.SetActive(false);
            Sword.SetActive(false);
            Katana.SetActive(false);
          
            playerWeapon = PlayerWeapon.Shield;
            weaponlList.Add(possessionWeapon);
        }
        else if (weaponName == Tag.Katana)
        {
            leftHand.SetActive(false);
            rightHand.SetActive(false);
            foot.SetActive(false);

            possessionWeapon.Name = weaponName;
            GameObject.Instantiate(Resources.Load("PickUpSource"), this.transform);
            //切换武器
            Katana.SetActive(true);
            Axe.SetActive(false);
            Sword.SetActive(false);
            
            Shield.SetActive(false);
            playerWeapon = PlayerWeapon.Katana;
            weaponlList.Add(possessionWeapon);
        }
        else if (weaponName == Tag.Sword)
        {
            leftHand.SetActive(false);
            rightHand.SetActive(false);
            foot.SetActive(false);

            possessionWeapon.Name = weaponName;
            GameObject.Instantiate(Resources.Load("PickUpSource"), this.transform);
            //切换武器
            Sword.SetActive(true);
            Axe.SetActive(false);
            
            Katana.SetActive(false);
            Shield.SetActive(false);
            playerWeapon = PlayerWeapon.Sword;
            weaponlList.Add(possessionWeapon);
        }

       

    }





}
