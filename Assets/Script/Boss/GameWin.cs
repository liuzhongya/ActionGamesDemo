using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWin : MonoBehaviour {
    public Rigidbody bossRig;

    public GameObject WinGameUI;

    void Start () {
        WinGameUI.SetActive(false);
        bossRig = GameObject.Find("Boss").GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
       
        if (bossRig == null)
        {
            WinGameUI.SetActive(true);
        }
    }
}
