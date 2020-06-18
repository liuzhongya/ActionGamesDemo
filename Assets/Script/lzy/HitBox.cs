using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// lzy-攻击检测点
/// </summary>
public class HitBox : MonoBehaviour {
    public float  HitTime = 0;
    private bool IsAttack = false;
    //public Animator Anim;

    //private void Start()
    //{
    //    Anim = GetComponent<Animator>();
    //}
    private void Update()
    {
        
    }


    void IsStartAttack()
    {
        IsAttack = true;
    }
    void IsEndAttack()
    {
        IsAttack = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        HitTime += Time.deltaTime;
        if (other.tag == Tag.Player)
        {
            //other.SendMessage("Death");
         //   Debug.Log("pz");
        }
        if (other.tag == Tag.Player && IsAttack)
        {
            
            if (HitTime > 0.3)
            {      
                other.SendMessage("Death");
                HitTime = 0;
            }
        }

    }

  



}
