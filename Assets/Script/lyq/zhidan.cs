using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zhidan : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update () {
		
	}
	private void OnTriggerEnter(Collider collision)
	{
        ///.......................lzy..................
       // StartCoroutine("AutomaticDestroy");
        ///.......................lzy..................

        switch (collision.tag)
		{
			case "Boss":
				print("*******************************************************");
				Destroy(gameObject);
				collision.SendMessage("Death");
				break;
			case "box":
			    Destroy(collision.gameObject);
				Destroy(gameObject);
				break;
			case "wall":
				Destroy(gameObject);
				break;
		}
		
	}
    ///.......................lzy..................
 
    IEnumerator AutomaticDestroy()
    {
        //Debug.Log("子弹自动销毁");
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
       
    }
    ///.......................lzy..................
}
