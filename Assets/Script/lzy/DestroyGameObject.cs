using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// lzy-控制声音的自动销毁，
/// </summary>

public class DestroyGameObject : MonoBehaviour {

    public float delay;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
