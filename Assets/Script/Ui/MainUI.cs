using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour {

    public GameObject explainUI;
    public GameObject AuthorUI;
    public GameObject ButtonUI;


    private void Start()
    {
        ButtonUI.SetActive(true);
        AuthorUI.SetActive(false);
        explainUI.SetActive(false);
    }




    public  void ShowExplainUi()
    {
        ButtonUI.SetActive(false);
        AuthorUI.SetActive(false);
        explainUI.SetActive(true);     
    }


    public void ShowAuthorUi()
    {
        ButtonUI.SetActive(false);
        AuthorUI.SetActive(true);
        explainUI.SetActive(false);
    }
    public void EnterDemoScence()
    {
        Globe.nextSceneName = "DemoScence";
        SceneManager.LoadScene("LoadScence");
    }
    public void EnterGameScence()
    {
        Globe.nextSceneName = "Level1";
        SceneManager.LoadScene("LoadScence");

    }
    public void CloseButton()
    {
        ButtonUI.SetActive(true);
        AuthorUI.SetActive(false);
        explainUI.SetActive(false);
    }
    public void ExitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
          Application.Quit();
    }


}
