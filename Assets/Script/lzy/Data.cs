using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 练习脚本
/// </summary>
public class Data : MonoBehaviour {

    //............................LZY...................

    public static Data Instance;
    static public float playerHp = 10f;

    static public int CoinNumber = 100;
    static public int GameScore = 100;
    void Awake()
    {
        playerHp = 10f;
        // Data.Instance = this;
        Data.Instance = this;
    }

    public void SavecoinData(int coinnum)
    {
        PlayerPrefs.SetInt("coin", coinnum);
    }
    public void SavegemData(int gemnum)
    {
        PlayerPrefs.SetInt("coin", gemnum);
    }


    //保存数据的方法
    public void SavescoreData(int scorenum, string playername)
    {
        PlayerPrefs.SetInt("score", scorenum);
        PlayerPrefs.SetString("playerName", playername);


    }
    ////读取数据的方法
    //public string GetData()
    //{
    //    return    ( PlayerPrefs.GetString("playerName")+  PlayerPrefs.GetInt("score"));
    //}

    public string GetData()
    {
        return (PlayerPrefs.GetString("playerName") + PlayerPrefs.GetInt("score"));
    }

}
