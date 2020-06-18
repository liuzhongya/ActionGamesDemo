using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvent : MonoBehaviour {

    //............................LZY...................
    public BoxCollider leftHand;
    public BoxCollider rightHand;
     public BoxCollider SworHitBox;
    public BoxCollider AxeHitBox;
    public BoxCollider KanataHitBox;
    public BoxCollider ShieldHitBox;

    //............................LZY...................
    private void Start()
    {
        //............................LZY...................
       // leftHand.enabled = false;
       // rightHand.enabled = false;
        //............................LZY...................

    }
    void LxAtt1Start()
    {
        GameObject.Instantiate(Resources.Load("Attack1Source"),this.transform);
        //............................LZY................... BoxCollier组件激活
     //   leftHand.enabled = true;
        //rightHand.enabled = true;
      //  Debug.Log("组件已经激活");
        //............................LZY...................


    } 
      void LxAtt1End()
    {
        //............................LZY................... BoxCollier组件禁用
        leftHand.enabled = false;
        rightHand.enabled = false;
      //  Debug.Log("组件已经禁用");
        //............................LZY...................

    }
   

    void LxMagicSource()
    {
        GameObject.Instantiate(Resources.Load("magicSource"), this.transform);
    }
    void IsAttackStart(int att)
    {
        if (att == 1)
        {
            // Debug.Log("播放声音1");
            GameObject.Instantiate(Resources.Load("Slash1"), this.transform);

        }
        else if (att == 2)
        {
            //Debug.Log("播放声音2");
            GameObject.Instantiate(Resources.Load("Slash2"), this.transform);

        }
        else if(att==3)
        {
            //Debug.Log("播放声音3"); 
            GameObject.Instantiate(Resources.Load("Slash3"), this.transform);
        }
        else if (att == 4)
        {
            //Debug.Log("播放声音3"); 
           GameObject.Instantiate(Resources.Load("Boxing"), this.transform);
        }
        else if (att == 5)
        {
            //Debug.Log("播放声音3"); 
            GameObject.Instantiate(Resources.Load("Boxing"), this.transform);
        }
        else if (att == 6)
        {
            //Debug.Log("播放声音3"); 
               GameObject.Instantiate(Resources.Load("Boxing"), this.transform);
        }
        if (gameObject.tag == Tag.Player)
        {
            PlayerCon.IsPlayerAttack = true;
        }
        if (gameObject.tag == Tag.Enemy)
        {
            // FinalEnemyAi.IsEnemyAttack = true;
            BroadcastMessage("IsStartAttack");
        }
          

    }


    void IsAttackEnd()
    {
        
        if (gameObject.tag == Tag.Player)
        {
            PlayerCon.IsPlayerAttack = false;
        }

        if (gameObject.tag == Tag.Enemy)
        {
            //FinalEnemyAi.IsEnemyAttack = false;
            BroadcastMessage("IsEndAttack");
        }

    }


}
