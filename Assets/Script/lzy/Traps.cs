using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// lzy-陷阱脚本
/// </summary>
public class Traps : MonoBehaviour
{

  
  //  public float spikeTime = 3f;
    public float spikenowTime = 0f;
    public Transform InitialPos;
    public Transform TargetPos;
    
    // Use this for initialization
    void Start()
    {
         
    }
     
    void Update()
    {

        spikenowTime += Time.deltaTime;
        if (spikenowTime<3)
        {
            transform.position = Vector3.MoveTowards(transform.position, InitialPos.position, 0.3f);
        }
        else if(spikenowTime>3&& spikenowTime<6)
        {

            transform.position = Vector3.MoveTowards(transform.position, TargetPos.position, 0.3f);

        }
        else if(spikenowTime > 6)
        {

            spikenowTime = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.Player)
        {
            Debug.Log("收到陷阱伤害");
            //扣除玩家的血量
            PlayerCon.playerHp -= 3;
            Debug.Log(PlayerCon.playerHp);

        }
    }
    private void OnTriggerStay(Collider other)
    {
       

    }




}


