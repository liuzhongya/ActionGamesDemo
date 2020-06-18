using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lxzidan : MonoBehaviour {

    // Use this for initialization

    public Rigidbody rig;
    public Vector3 eulerAngleVelocity = new Vector3(0, 100, 0);
    void Start () {


        //rig.AddForce(Vector3.forward);
       /// rig.velocity = new Vector3(0.0f, 0.0f, -15.0f);
    }
	
	// Update is called once per frame
	void Update () {
        Quaternion deltaRotation = Quaternion.Euler(-eulerAngleVelocity * Time.deltaTime);
        rig.MoveRotation(rig.rotation * deltaRotation);

    }
}
