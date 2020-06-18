using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManage : MonoBehaviour {

    //............................LZY...................

    public AudioClip jumpAudio;
    public AudioSource feetaudio;
    private AudioSource audioSource;


    /// <summary>
    /// 直接用消息机制，通过发消息来确定人物当前状态然后通过协程来播发脚本声，通过pich和等待时间来播发相应的声音如在攻击时通过确定玩家攻击的
    /// 状态和动画来播发声音
    /// </summary>
    /// 
    public float feetTime = 1f;

      IEnumerator FeetAudioTime  ()
    {

        audioSource.Stop();
        yield return new  WaitForSeconds(feetTime); ;
    }


	void Start () {
        feetaudio = GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {

        // AudioManagement();
        

    }
    private void AudioManagement(string state)
    {
        //if (state == "PATROL")
        //{
        //    feetTime = 3.0f;
        //   // audioSource.volume = 1;
        //    audioSource.pitch = 1.0f;
        //    if (!audioSource.isPlaying)
        //        audioSource.Play();
        ////    Debug.Log("1.0脚本声 暂停1秒 ");
        //    StartCoroutine(FeetAudioTime());
        //}
        //else if (state == "CHASE")
        //{
        //    feetTime = 2.8f;
        // //   audioSource.volume = 1;
        //    audioSource.pitch = 1.3f;
        //    if (!audioSource.isPlaying)
        //        audioSource.Play();
        //  //  Debug.Log("1.3脚本声  暂停0.8秒");
        //    StartCoroutine(FeetAudioTime());
        //}
        //else
        //    audioSource.Stop();
    }



}
