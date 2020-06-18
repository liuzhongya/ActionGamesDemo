using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// lzy-场景跳转脚本（传送门）
/// </summary>

public class LoadLevel: MonoBehaviour {
    public int Level=0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag==Tag.Player)
        {
            Debug.Log("进行场景跳转");
            SceneManager.LoadScene("Level2");
        }
    }
}
