using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using UnityEngine.UI;


/// <summary>
/// lzy-练习得分脚本xml的存储
/// </summary>
public class ScoreXml : MonoBehaviour
{
    List<Score> scoreList = new List<Score>();
   // public Text lxSorting;

    // Use this for initialization
    void Start()
    {
      //  lxSorting.text = "积分榜"+ "\n"+"ID   姓名  积分";
        ReadXmlScore();

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(50, 50, 100, 50), "ReadXml"))
        {
            ReadXmlScore();
            Debug.Log("增加技能数据");
        }
        else if (GUI.Button(new Rect(50, 190, 100, 50), "进行排序"))
        {

            ScoreSorting();
            Debug.Log("进行排序");
        }
    }

    public void ReadXmlScore()
    {
        string path = Application.dataPath + "/Resources/score.xml";
        XmlDocument xmlDoc = new XmlDocument();

        if (File.Exists(path))   //判断文件是否存在
        {
            xmlDoc.Load(path);
            XmlNode rootNode = xmlDoc.FirstChild;
            XmlNodeList scoreNodeList =rootNode.ChildNodes;
            foreach (XmlNode scoreNode in scoreNodeList)
            {
                Score score = new Score();
                XmlNodeList fielNodeList = scoreNode.ChildNodes;
                foreach (XmlNode fieldNode in fielNodeList)
                {
                    if (fieldNode.Name == "id")
                    {
                        int id = int.Parse(fieldNode.InnerText);
                        score.Id = id;
                    }
                    else if (fieldNode.Name == "name")
                    {
                        string name = fieldNode.InnerText;
                        score.Name = name;
                        string lang = fieldNode.Attributes[0].Value;
                        score.Lang = lang;
                    }
                    else if (fieldNode.Name == "damage")
                    {
                        int damage = int.Parse(fieldNode.InnerText);
                        score.Damage = damage;
                    }
                    else
                    {
                        int scorenum = int.Parse(fieldNode.InnerText);
                        score.ScoreNum = scorenum;
                    }
                }
                scoreList.Add(score);
            }

        }
        else
        {
            Score score = new Score();
            XmlElement root = xmlDoc.CreateElement("info");
            XmlElement element = xmlDoc.CreateElement("playerinfo");

            XmlElement elementChild1 = xmlDoc.CreateElement("id");
            elementChild1.InnerText = "001";
            score.Id = int.Parse(elementChild1.InnerText);

            XmlElement elementChild2 = xmlDoc.CreateElement("name");
            elementChild2.SetAttribute("lang", "cn");
            elementChild2.InnerText = "ilrass";
            score.Name = elementChild2.InnerText;

            XmlElement elementChild3 = xmlDoc.CreateElement("damage");
            elementChild3.InnerText = "666";
            score.Damage = int.Parse(elementChild3.InnerText);

            XmlElement elementChild4 = xmlDoc.CreateElement("score");
            elementChild4.InnerText = "100";
            score.ScoreNum = int.Parse(elementChild4.InnerText);

            element.AppendChild(elementChild1);
            element.AppendChild(elementChild2);
            element.AppendChild(elementChild3);
            element.AppendChild(elementChild4);
            root.AppendChild(element);
            xmlDoc.AppendChild(root);

            xmlDoc.Save(path);
            Debug.Log("xml创建成功");

        }

    }
    public void ScoreSorting()
    {
        print("未进行排序前：");
        for (int i = 0; i < scoreList.Count; i++)
        {
            print(scoreList[i].Id + "  " + scoreList[i].Name + "  " + scoreList[i].ScoreNum);
        }
        print("进行排序后：");


        scoreList.Sort((x, y) => { return -x.ScoreNum.CompareTo(y.ScoreNum); });

        for (int i = 0; i < scoreList.Count; i++)
        {
            print(scoreList[i].Id + "  " + scoreList[i].Name + "  " + scoreList[i].ScoreNum);
          //  lxSorting.text = lxSorting.text + "\n" + scoreList[i].Id + "  " + scoreList[i].Name + "  " + scoreList[i].ScoreNum;

        }




    }









}
