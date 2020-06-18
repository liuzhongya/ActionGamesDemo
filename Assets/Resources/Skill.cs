using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour {

 
    public  int Id  {      get;  set;    }
    public int Damage { get; set; }
    public string Name { get; set; }
    public  string Lang { get; set; }

    //public override string ToString()
    //{
    //    return  string.Format("id:{0},Name:{1},Lang:{2},Damage:{3}",Id,Name,Lang,Damage);
    //}
}
