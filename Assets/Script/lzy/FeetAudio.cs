using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// lzy-脚本声
/// </summary>
public class FeetAudio : MonoBehaviour {

    public AudioSource feetAudio; 

    private void Start()
    {
        feetAudio = GetComponent<AudioSource>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.Wall)
        {
            feetAudio.pitch = 1.2f;
            feetAudio.Play();

            Debug.Log("脚步声开始");

        }
      
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == Tag.Wall)
        {
            //feetAudio.Stop();
            Debug.Log("脚步声jieshu");
        }
    }


}
