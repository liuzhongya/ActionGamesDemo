using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// lzy-血药旋转脚本，
/// </summary>

public class RotateAnimate : MonoBehaviour {

    public Renderer _renderer;   
    public float scrollSpeed = 0.5F;
    void Update()
    {
   
        float offset = -Time.time * scrollSpeed;   //这里的Time.time和我例子中   Time.detalTime不一样  但这句话的意思是一样的  都是不断递减的一个数  
        _renderer.material.mainTextureOffset = new Vector2( 0,offset);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.Player)
        {
            GameObject.Instantiate(Resources.Load("PickUpSource"), other.transform);
            print(PlayerCon.playerHp);
            if (PlayerCon.playerHp < 40 && PlayerCon.playerHp > 0)
            {
                PlayerCon.playerHp= Mathf.Min(PlayerCon.playerHp+ 80, PlayerCon.playerFullHp);
            }
            else if (PlayerCon.playerHp > 40)
            {
                PlayerCon.playerHp = PlayerCon.playerFullHp;
            }
            print(PlayerCon.playerHp+"csz"+ PlayerCon.playerFullHp);
            Destroy(gameObject);
        }
    }


}
