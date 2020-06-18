using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


/// <summary>
/// lzy-门打开脚本
/// </summary>
public class SimpleDoor : MonoBehaviour {
    public Transform pivot;
    public bool autoOpen = true;
    public bool autoClose = true;

    public bool isOpen = false;
    public bool isClose = false;
    public bool invertAngle;

    public float angleOfOpen = 90f;
    public float angleToInvert = 30f;
    public float speedClose = 2f;
    public float speedOpen = 2f;
    public float timeToClose = 1f;
    public float forwardDotVelocity;

    private Vector3 currentAngle;

    public NavMeshObstacle navMeshObstacle;
 
    void Start()
    {
        if (!pivot) this.enabled = false;
        navMeshObstacle = GetComponentInChildren<NavMeshObstacle>();
        if (navMeshObstacle)
        {
            navMeshObstacle.enabled = false;
            navMeshObstacle.carving = true;
        }
    }
    private void Update()
    {
        
    }


    public void Open()
    {
      //  if (!isOpen)
            StartCoroutine(_Open());
    }

    public void Close()
    {
       // if (isOpen)
            StartCoroutine(_Close());
    }

    IEnumerator _Open()
    {
        if (isOpen)
        {
           while (currentAngle.y != (invertAngle ? -angleOfOpen : angleOfOpen))
            {
                yield return new WaitForEndOfFrame();

                if (invertAngle)
                {
                    currentAngle.y -= (100 * speedOpen) * Time.deltaTime;
                    currentAngle.y = Mathf.Clamp(currentAngle.y, -angleOfOpen, 0);
                }
                else
                {
                    currentAngle.y += (100 * speedOpen) * Time.deltaTime;
                    currentAngle.y = Mathf.Clamp(currentAngle.y, 0, angleOfOpen);
  
                }

                pivot.localEulerAngles = currentAngle;
             //   Debug.Log(currentAngle + "currentAngle");
               
            }
        }

      

    }

    IEnumerator _Close()
    {
        if (isClose)
        {
            while (currentAngle.y != 0)
            {
                yield return new WaitForEndOfFrame();
                if (invertAngle)
                {
                    currentAngle.y += (100 * speedClose) * Time.deltaTime;
                    currentAngle.y = Mathf.Clamp(currentAngle.y, -angleOfOpen, 0);
                }
                else
                {
                    currentAngle.y -= (100 * speedClose) * Time.deltaTime;
                    currentAngle.y = Mathf.Clamp(currentAngle.y, 0, angleOfOpen);
                }
                pivot.localEulerAngles = currentAngle;
            }
        }
      
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag==Tag.Player)
        {
            isOpen = true;
            isClose = false;
          //  Debug.Log("打开门");
          //  invertAngle = false;
            //    pivot.localEulerAngles=new Vector3(0,90,0);
            Open();
            forwardDotVelocity = Mathf.Abs(Vector3.Angle(transform.forward, other.transform.position - transform.position));
         //   Debug.Log(forwardDotVelocity);
            if (forwardDotVelocity < 91.0f)
            {
                  invertAngle = false;            
            }
            else
            {
                 invertAngle = true;                 
            }



        }

      //      Debug.Log(forwardDotVelocity);
    }
     
    void OnTriggerExit(Collider other)
    {
        if (other.tag == Tag.Player)
        {
            isClose = true;
            isOpen = false;
            // Debug.Log("关闭门");
            Close();
            //pivot.localEulerAngles = new Vector3(0, 0, 0);

        }

    }
}
