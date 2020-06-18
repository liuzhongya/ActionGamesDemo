using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDest : MonoBehaviour {



 
    void DestroySelf()
    {
        //Destroy(gameObject);
        Destroy(GetComponent< BoxCollider>());//移除游戏物体BoxCollider组件
    }
}
