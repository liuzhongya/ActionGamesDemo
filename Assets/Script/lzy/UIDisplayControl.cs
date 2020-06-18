using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// lzy-玩家血条显示
/// </summary>
public class UIDisplayControl : MonoBehaviour {

    public Slider slider;
    public Text playerlevel;
    public Text playerexp;
    public Text playerScore;

    public GameObject EscUI;
    public GameObject HelpUI;
    public GameObject ButtonUI;

    // public renwu2 playerHp;
    //public float plhealth;
    private void Start()
    {
        //Object.DontDestroyOnLoad(transform.gameObject);   
     
        EscUI.SetActive(false);
        HelpUI.SetActive(false);
    }

    private void Update()
    {

        SliderUpdata();
        TextShow();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            EscUI.SetActive(true);
            ButtonUI.SetActive(true);
        }

       // print(Time.timeScale);


    }
    public void SliderUpdata()
    {
       
           slider.value = PlayerCon.playerHp / PlayerCon.playerFullHp;
    
    }
    public void TextShow()
    {
        playerlevel.text = "Level: " +PlayerCon.level;
        playerexp.text = "exp: " + PlayerCon.exp + "/" + PlayerCon.nextexp;
        playerScore.text = "Score: " + PlayerCon.playerScore;

    }

    public void ExitGame()
    {
       // UnityEditor.EditorApplication.isPlaying = false;
          Application.Quit();
    }
    public void ReturnMainUI()
    {
        Globe.nextSceneName = "MainScence";
        SceneManager.LoadScene("LoadScence");
    }
    public void ShowHelpText()
    {
        ButtonUI.SetActive(false);
        HelpUI.SetActive(true);

    }
    public void CloseHelpText()
    {
        ButtonUI.SetActive(true);
        HelpUI.SetActive(false);

    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        EscUI.SetActive(false);

    }


}
