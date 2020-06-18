using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Xml;
public class Xmlskill : MonoBehaviour {
    List<Skill> skillList = new List<Skill>();
    public Text lxtext;

    // Use this for initialization
    void Start() {
        lxtext.text = "";
        ReadXml();

    }

    // Update is called once per frame
    void Update() {



    }
    public void ReadXml()
    {
        XmlDocument xmlDoc = new XmlDocument();
        // xmlDoc.LoadXml("data2.xml");
        //   string path = Application.dataPath + "/Resources/data2.xml";
        // Debug.Log(Application.dataPath    );
        xmlDoc.Load(Application.dataPath + "/Resources/data2.xml");
        XmlNode rootNode = xmlDoc.FirstChild;
        XmlNodeList skillNodeList = rootNode.ChildNodes;
        foreach (XmlNode skillNode in skillNodeList)
        {
            Skill skill = new Skill();
            XmlNodeList fieldNodeList = skillNode.ChildNodes;// 获得skill下面的节点
            foreach (XmlNode fieldNode in fieldNodeList)
            {
                if (fieldNode.Name == "id")
                {
                    int id = int.Parse(fieldNode.InnerText);
                    skill.Id = id;
                }
                else if (fieldNode.Name == "name")
                {
                    string name = fieldNode.InnerText;
                    skill.Name = name;
                    string lang = fieldNode.Attributes[0].Value;
                    skill.Lang = lang;
                }
                else
                {
                    int damage = int.Parse(fieldNode.InnerText);
                    skill.Damage = damage;
                }
            }
            skillList.Add(skill);
        }
      //  Sorting();   //对list进行排序;   
     //   Debug.Log("list  " + skillList.Count);
    }
    public void Sorting()
    { 

        print("未进行排序前：");
        for (int i = 0; i < skillList.Count; i++)
        {
            print(skillList[i].Name + "  " + skillList[i].Damage);
        }


        print("进行排序后：");
        skillList.Sort((x, y) => { return -x.Damage.CompareTo(y.Damage); });  //.CompareTo(y.level); });
                                                                              //   myList.Sort((x, y) => { return -x.level.CompareTo(y.level); });
        for (int i = 0; i < skillList.Count; i++)
        {
            print(skillList[i].Name + "  " + skillList[i].Damage);
           
                lxtext.text = lxtext.text + "\n" + skillList[i].Name  +"伤害:"+ skillList[i].Damage;
        }

      
    }
    
    public void addXMLData()
    {
        string path = Application.dataPath + "/Resources/data2.xml";
        if (File.Exists(path))
        {
            Skill skill = new Skill();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            XmlNode rootNode = xmlDoc.FirstChild;
            XmlNodeList skillNodeList = rootNode.ChildNodes;
            XmlNode nodeRoot = xmlDoc.FirstChild;
            //添加一个完整的数据
            XmlElement element = xmlDoc.CreateElement("lxskill");
            XmlElement elementChild1 = xmlDoc.CreateElement("id");
            elementChild1.InnerText = "9";
            skill.Id = int.Parse(elementChild1.InnerText);

            XmlElement elementChild2 = xmlDoc.CreateElement("name");
            elementChild2.SetAttribute("lang", "cn");
            elementChild2.InnerText = "练习技能";
            skill.Name = elementChild2.InnerText;
            skill.Lang = elementChild2.Attributes[0].Value;

            XmlElement elementChild3 = xmlDoc.CreateElement("damage");      
            elementChild3.InnerText = "1111";
            skill.Damage =int .Parse( elementChild3.InnerText);


            element.AppendChild(elementChild1);
            element.AppendChild(elementChild2);
            element.AppendChild(elementChild3);
            nodeRoot.AppendChild(element);
            xmlDoc.AppendChild(nodeRoot);
            skillList.Add(skill);
            //最后保存文件
            xmlDoc.Save(path);

            //Skill skill = new Skill();
            //skill.Id = int.Parse(elementChild1.InnerText);
            //skill.name = elementChild2.InnerText;// elementChild2.InnerText;
            //skill.Damage = int.Parse(elementChild3.InnerText);
            //skill.Lang = elementChild2.Attributes[0].Value;
            //skillList.Add(skill);

           //ReadXml();

  //          Debug.Log("添加数据后list  " + skillList.Count);
             
        }
    }

   public void updateXML()
   {

    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(50, 50, 100, 50), "增加技能数据"))
        {
            addXMLData();
            Debug.Log("增加技能数据");
        }
       else if (GUI.Button(new Rect(50, 250, 100, 50), "进行排序"))
        {
           
            Sorting();
            Debug.Log("进行排序");
        }
    }


}