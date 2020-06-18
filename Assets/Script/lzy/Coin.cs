using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.Player)
        {
            GameObject.Instantiate(Resources.Load("PickUpSource"), other.transform);
            PlayerCon.playerScore += 1000;
            print(PlayerCon.playerScore);
            Destroy(gameObject);
        }
    }
    

}
