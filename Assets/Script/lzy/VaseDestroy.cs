using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// lzy-罐子破摔后碎片相关组件删除
/// </summary>
public class VaseDestroy : MonoBehaviour {


	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        Destroy(GetComponent<Rigidbody>());//移除游戏物体上的rig 
        Destroy(GetComponent<MeshCollider>());
        Destroy(GetComponent<VaseDestroy>());
    }

   
}
