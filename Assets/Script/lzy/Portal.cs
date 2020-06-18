using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// lzy-传送门脚本
/// </summary>
public class Portal : MonoBehaviour {


    public int Level;
    public Renderer _renderer;
    public float scrollSpeed = 0.5F;
    //public Object playerTarget;

    void Update()
    {
       // playerTarget = GameObject.FindWithTag(Tag.Player);
        float offset = -Time.time * scrollSpeed;   //这里的Time.time和我例子中   Time.detalTime不一样  但这句话的意思是一样的  都是不断递减的一个数  
        _renderer.material.mainTextureOffset = new Vector2(0, offset);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.Player)
        {
            GameObject.Instantiate(Resources.Load("PickUpSource"), other.transform);

            //SceneManager.LoadScene(Level);
            int lx = Level + 1;
            string nextScence = "Level" + lx;
            print(nextScence);
            Globe.nextSceneName = nextScence;
            SceneManager.LoadScene("LoadScence");
         

        }
    }




}