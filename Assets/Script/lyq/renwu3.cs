using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class renwu3 : MonoBehaviour {
    CharacterController cc;
    Animator animtor;
    float turnSpeed = 20;
    float speed = 10;
    // Use this for initialization
    public ParticleSystem explosionParticles;

    public Rigidbody shell;
    public Transform muzzle;

    public float launchForce = 10;
    public AudioSource shootAudioSource;

    static public float hp = 100; //血量
    static public float att = 25; //攻击力
    public float level = 1;        //等级 初始为1级
    public float exp = 0;         //经验值

    public float health;
    float healthMax;
    bool isAlive;

    public Slider healthSlider;                             // The slider to represent how much health the tank currently has.
    public Image healthFillImage;                           // The image component of the slider.
    public Color healthColorFull = Color.green;
    public Color HealthColorNull = Color.red;
    public bool back;
    public float backid;
    bool attacking = false;
    public float attackTime = 1;

    Animator animator;
    void Start() {
        level = 1;
        backid = 0;
        cc = GetComponent<CharacterController>();
        animtor = GetComponent<Animator>();
        healthMax = health;
        isAlive = true;
     //   RefreshHealthHUD();
        back = false;
        // explosionParticles.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        //if (!isAlive) return;
        if (attacking) return;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        animtor.SetFloat("turn", h);
        animtor.SetFloat("speed", System.Math.Abs(v));
        if (v * backid < 0) {
            animtor.SetTrigger("back");
        }
        if (v != 0)
        {
            backid = v;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            //if (attacking) return;
            animtor.SetTrigger("attack1");
            Fire();
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            //if (attacking) return;
            animtor.SetTrigger("attack2");
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up * 0.3f, transform.forward, out hit, 2))
            {
                print("******************");
                print(hit.collider.tag);
                if (hit.collider.tag == "isbox")
                {
                    animtor.SetTrigger("jumpwall");
                }

            } else
            {
                animtor.SetTrigger("jump");
            }
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            // if (attacking) return;
            animtor.SetTrigger("jumproll");
        }


        // cc.SimpleMove(new Vector3(h, 0, v) * speed);
    }


    public void Rotate(Vector3 lookDir)
    {
        var targetPos = transform.position + lookDir;
        var characterPos = transform.position;

        characterPos.y = 0;
        targetPos.y = 0;

        Vector3 faceToDir = targetPos - characterPos;

        Quaternion faceToQuat = Quaternion.LookRotation(faceToDir);
        Quaternion slerp = Quaternion.Slerp(transform.rotation, faceToQuat, turnSpeed * Time.deltaTime);
        transform.rotation = slerp;
    }

    public void Fire()
    {
        print("******");
        if (!isAlive) return;
        //attacking = false;
        Rigidbody shellInstance = Instantiate(shell, muzzle.position, muzzle.rotation) as Rigidbody;
        shellInstance.velocity = launchForce * muzzle.forward;
        //shootAudioSource.Play();

        //attacking = true;
        //Invoke("RefreshAttack", attackTime);
    }

    void addexp()         //造成有效攻击增加经验
    {
        exp = exp + 25;   //每次增加25
        float a;
        a = exp / 100;    //每100经验升级 也就是4次有效攻击升一级
        if (a > level)
        {
            uplevel();     //调用升级函数
        }
    }

    void uplevel()        //升级
    {
        print("uplevel");
        level = level + 1;
        hp = 75 + 25 * level;  //1级的时候血量100
        att = att + 25;
    }
    void RefreshAttack()
    {
        attacking = false;
    }

    public void Death()
    {
        hp = hp - 25;
        if (hp <= 0)
        {
            isAlive = false;
            animtor.SetBool("die", false);
        }

    }
    

}
