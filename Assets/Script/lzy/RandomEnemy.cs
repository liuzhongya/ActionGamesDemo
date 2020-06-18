using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// lzy-触发生成敌人控制
/// </summary>

public class RandomEnemy : MonoBehaviour { //随机生成敌人

    public GameObject[] enemyPrefabs; //生成敌人
    public Transform[] spawnPosArray; //生成位置
    public float time = 0;//表示多少秒生成
    public float repeateRate=0; //生成的速率
    public bool isSpawned = false;




     
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag==Tag.Player&&isSpawned==false)
        {
            isSpawned = true;
            StartCoroutine(SpawnEnemy());
            Debug.Log("生成敌人");
            
        }
    }
    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(time);
        foreach (GameObject go in enemyPrefabs)
        {
            foreach (Transform t in spawnPosArray)
            {
                GameObject.Instantiate(go, t.position, Quaternion.identity);

            }
            yield return new WaitForSeconds(repeateRate);
        }



    }

}
