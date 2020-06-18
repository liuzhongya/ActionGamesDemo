using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//注意引用命名空间
using System;
using UnityEngine.UI;

public class Globe
{
    public static string nextSceneName;
}

public class LoadScene : MonoBehaviour {

  public   Slider slider;//滑动条
   public  Text text;//文本
  // public  Button but;//按钮
    // 加载进度
    float loadPro = 0;
 
    // 用以接受异步加载的返回值
    AsyncOperation AsyncOp = null;

    void Start()
    {
        Debug.Log("开始加载");

       // slider = transform.Find("Slider").GetComponent<Slider>();
        slider.value = 0;
        //  text = transform.Find("Text").GetComponent<Text>();
        //   but = transform.Find("Button").GetComponent<Button>();
        //  but.onClick.AddListener(StartLoad);//绑定按钮事件
        StartLoad();

        PlayerCon.IsScence = true;
    }

    //点击按钮,开始加载下一场景,文本和进度条显示加载进度
    void StartLoad()
    {
        AsyncOp = SceneManager.LoadSceneAsync(Globe.nextSceneName, LoadSceneMode.Single);//异步加载场景名为"Demo Valley"的场景,LoadSceneMode.Single表示不保留现有场景
        AsyncOp.allowSceneActivation = true;//allowSceneActivation =true表示场景加载完成后自动跳转,经测,此值默认为true
    }

    void Update()
    {
        if (AsyncOp != null)//如果已经开始加载
        {
            loadPro = AsyncOp.progress; //获取加载进度,此处特别注意:加载场景的progress值最大为0.9!!!
        }
        if (loadPro >= 0.9f)//因为progress值最大为0.9,所以我们需要强制将其等于1
        {
            loadPro = 1;
        }
        slider.value = Mathf.Lerp(slider.value, loadPro, 1 * Time.deltaTime);//滑动块的value以插值的方式紧跟进度值
        if (slider.value > 0.99f)
        {
            slider.value = 1;
            AsyncOp.allowSceneActivation = true;
        }
        text.text = string.Format("{0:F0}%", slider.value * 100);//文本中以百分比的格式显示加载进度
    } 

   






}
