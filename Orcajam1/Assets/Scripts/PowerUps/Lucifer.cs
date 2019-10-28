using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Lucifer : MonoBehaviour
{
    //temporary
    public GameObject indicator;

    public GameObject array,fireBall;
    public SteamVR_Action_Boolean cast;
    public SteamVR_Input_Sources handType;
    public float distanceThreshold = 0.5f;

    private GameObject controller;
    private bool casting,tracing;
    private float angle;
    private int pointControl;
    private GameObject[] points;
    private GameObject clone;
    private float timer;

    private void Start()
    {
        controller = GameObject.Find("LeftHand");
        casting = false;
        tracing = false;
        angle = 0f;
        pointControl = 0;
        points = new GameObject[5];
        timer = 0f;
    }

    void Update()
    {
        //this entire update thing because doing stuff while holding a button is hard
        if (cast.GetStateDown(handType))
        {
            casting = true;
            //the moment the cast start is where the first point is located
            CreateP1();
        }
        if (cast.GetStateUp(handType))
        {
            casting = false;
        }
        if (casting)
        {
            //Debug.Log("casting");
            LuciferControl();
        }
        else
        {
            //Debug.Log("clearing");
            ClearCache();
        }
    }

    //main control
    private void LuciferControl()
    {
        timer += Time.deltaTime;
        if (pointControl < 5)
        {
            StartCoroutine(UpdateAngle());
            CreateAnglePoints();
            //you only have 3 second to finished drawing the star
            if(timer >= 3)
            {
                casting = false;
            }
        }
        else
        {
            CastComplete();
        }
    }

    //after the drawing process is done
    public void CastComplete()
    {
        //cast is successful
        if (CheckShape())
        {
            if (!tracing)
            {
                float px = 0, py = 0, pz = 0;
                for (int i = 0; i < 5; i++)
                {
                    px += points[i].transform.position.x;
                    py += points[i].transform.position.y;
                    pz += points[i].transform.position.z;
                }
                px /= 5; py /= 5; pz /= 5;
                Vector3 arrayPosition = new Vector3(px, py, pz);
                clone = Instantiate(array, arrayPosition, controller.transform.rotation);
                tracing = true;

                GameObject[] fireBalls = new GameObject[8];

                for(int bx = -1;bx <= 1; bx++)
                {
                    for (int by = -1;by <= 1;by++)
                    {
                        if(!(bx==0&&by==0))
                        {
                            for(int i = 0; i < fireBalls.Length; i++)
                            {
                                if (fireBalls[i] == null)
                                {
                                    fireBalls[i] = Instantiate(fireBall, clone.transform.position,clone.transform.rotation);
                                    Destroy(fireBalls[i], 10f);
                                    fireBalls[i].transform.Translate(new Vector3(bx * 0.15f, by * 0.15f, 0), Space.Self);
                                    break;
                                }
                            }
                        }
                    }
                }


            }
            else
            {
                clone.transform.rotation = controller.transform.rotation;
            }
        }
        //failed the cast
        else
        {
            //Debug.Log("cast falied");
            casting = false;
        }
    }

    private bool CheckDistance(float a,float b)
    {
        float result = a - b;
        //take abs
        if (result < 0)
            result /= -1;
        return result < distanceThreshold;
    }

    private bool CheckShape()
    {
        bool checker = true;
        //P1 and P2 cannot be in the wrong position

        //check P4
        Vector3 L1 = points[0].transform.position - points[1].transform.position;
        Vector3 L2 = points[1].transform.position - points[3].transform.position;
        float angle = Vector3.Angle(L1, L2);
        bool condition = (angle > 90 && angle < 150) && CheckDistance(L2.magnitude, L1.magnitude);
        if (!condition)
        {
            //Debug.Log("C1 failed");
            checker = false;
        }

        //check P3
        L1 = points[3].transform.position - points[0].transform.position;
        L2 = points[0].transform.position - points[2].transform.position;
        angle = Vector3.Angle(L1, L2);
        condition = (angle > 30 && angle < 90) && CheckDistance(L2.magnitude, L1.magnitude);
        if (!condition)
        {
            //Debug.Log("C2 failed");
            checker = false;
        }

        //check P5
        L1 = points[3].transform.position - points[1].transform.position;
        L2 = points[1].transform.position - points[4].transform.position;
        angle = Vector3.Angle(L1, L2);

        condition = (angle > 30 && angle < 90) && CheckDistance(L2.magnitude, L1.magnitude);
        if (!condition)
        {
            //Debug.Log("C3 failed angle: "+angle);
            checker = false;
        }
        return checker;
    }

    //reset everything
    private void ClearCache()
    {
        //Debug.Log("clear");
        casting = false;
        pointControl = 0;
        tracing = false;
        for(int i = 0;i < 5; i++)
        {
            Destroy(points[i]);
        }
        points = new GameObject[5];
        angle = 0f;
        timer = 0f;
        Destroy(clone);
    }

    //create p1
    private void CreateP1()
    {
        if (points[0] == null)
        {
            points[0] = Instantiate(indicator, controller.transform.position, Quaternion.identity) as GameObject;
        }
        pointControl++;
    }

    //create P2-P5
    private void CreateAnglePoints()
    {
        //if the controller is changing direction <testAngles>
        if (angle > 40 )
        {
            //to clean the spike of data the turning point has
            Vector3 pos1 = controller.transform.position;
            Vector3 pos2 = points[pointControl-1].transform.position;
            float distance = (pos1 - pos2).magnitude;
            if(distance > 0.2)
            {
                points[pointControl] = Instantiate(indicator, pos1, Quaternion.identity) as GameObject; ;
                pointControl++;
            }
        }
    }
    //udpate the angle once 4 fixed update <bugs?>
    private IEnumerator UpdateAngle()
    {
        //take 3 points
        Vector3 position0 = controller.transform.position;
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        Vector3 position1 = controller.transform.position;
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        Vector3 position2 = controller.transform.position;

        //connect the points
        Vector3 L0 = position1 - position0;
        Vector3 L1 = position2 - position1;
        //find and update the angle
        angle = Vector3.Angle(L0, L1);
        //Debug.Log(angle);
    }
}
