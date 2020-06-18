using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {


    public GameObject airWall;
    public Rigidbody bossRig;
    public GameObject lxboos;


    private void Start()
    {
        airWall = GameObject.Find("Wall");
        lxboos = GameObject.Find("Boss");
        airWall.SetActive(false);
        bossRig = lxboos.GetComponent<Rigidbody>(); //lxboos.GetComponent<Rigidbody>();
        print(bossRig.ToString());


    }

    private void Update()
    {
        if (bossRig == null)
        {
            airWall.SetActive(false);
            Destroy(GetComponent<BoxCollider>());//移除游戏物体BoxCollider组件
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.Player)
        {
            BroadcastMessage("BossTrigger");
            airWall.SetActive(true);
        }
    }
    void AirWall()
    {
        airWall.SetActive(false);
    }
    


}
