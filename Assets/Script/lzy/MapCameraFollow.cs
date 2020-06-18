using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// lzy-小地图显示（摄像头的跟随）
/// </summary>
public class MapCameraFollow : MonoBehaviour {

    
    public Transform targetPos;
    public float height = 5;
    public float smoothSpeed = 1f;

    private void Start()
    {
        targetPos = GameObject.FindGameObjectWithTag(Tag.Player).transform;
        transform.position = targetPos.position + Vector3.up * height;
    }
    

    void Update () {

        //if(Vector3.Distance(this.transform.position,))
        // float dis = Vector3.Distance(transform.position, targetPos.position);
        // //transform.position = targetPos.position + Vector3.up * height;
        // print(dis);

     //   print("玩家的位置" + targetPos.position);

            transform.position = targetPos.position + Vector3.up * height;

        // //重新计算位置
        //// Vector3 mPosition = mRotation * new Vector3(0.0F, 0.0F, -Distance) + Target.position;
      //  transform.position = Vector3.MoveTowards(transform.position, targetPos.position + Vector3.up * height, 0.5f);

    }
    void UpdataPos()
    {

        //float lx=Mathf.Lerp()
        //transform.position = targetPos.position + Vector3.up * lx;
        transform.position = Vector3.MoveTowards(transform.position, targetPos.position + Vector3.up * height,0.5f);


    }



}
