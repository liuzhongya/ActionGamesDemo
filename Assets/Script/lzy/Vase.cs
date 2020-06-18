using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// lzy-罐子控制脚本
/// </summary>

public class Vase : MonoBehaviour
{
    public Transform brokenObject;
     
    public  bool isBroken;
    private Collider _collider;
    private Rigidbody _rigidBody;

    
    void Start()
    {
        _collider = GetComponent<Collider>();
        _rigidBody = GetComponent<Rigidbody>();
       
        
    }
    private void Update()
    {
        if (isBroken)
            StartCoroutine(BreakObjet());
       // Debug.Log(_rigidBody.velocity.magnitude > 2);
    }
    IEnumerator BreakObjet()
    {
        if (_rigidBody) Destroy(_rigidBody);
        if (_collider) Destroy(_collider);
        yield return new WaitForEndOfFrame();
        brokenObject.transform.parent = null;
        brokenObject.gameObject.SetActive(true);
        Destroy(gameObject);
    }
  
    //private void OnTriggerStay(Collider other)
    //{
    //    if(other.gameObject.tag== Tag.PlayerInteractable || other.tag==Tag.BulletSkills)
    //    {
    //        isBroken = true;
    //        StartCoroutine(BreakObjet());
    //        PlayerCon.playerScore += 10;
    //     //   print(PlayerCon.playerScore);

    //    }

      
    //}
    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == Tag.Axe|| other.tag == Tag.Shield|| other.tag == Tag.Sword|| other.tag == Tag.Katana) && PlayerCon.IsPlayerAttack)
        {
            isBroken = true;
            StartCoroutine(BreakObjet());
            PlayerCon.playerScore += 10;
            PlayerCon.exp += 20;
            //  print(PlayerCon.playerScore);

        }
        if ((other.gameObject.tag == Tag.PlayerInteractable&& PlayerCon.IsPlayerAttack)|| other.tag == Tag.BulletSkills)
        {
            isBroken = true;
            StartCoroutine(BreakObjet());
            PlayerCon.playerScore += 10;
            //   print(PlayerCon.playerScore);

        }

    }


}


//还需要调整，包括破碎时间和破碎后碎片的Rigidbody的清除