using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class SubmarineNavigator : MonoBehaviour
{
    public float trueAngle;
    float radius=0.5f;
    float forwardCommand=0.0f;
    float x=0.5f,y=0.5f;
    float prex=0.5f,prey=0.5f;
    float dmg=0.0f;
    float colisionForce=0.01f;
    Vector3 SubmarinePositionV3;
    float counter;
    public Vector2 radarPosition;
    Vector3 baseH;
    public Texture2D map;
    Lever lever;
    manivelle crank;

    GameObject radarMiniSubmarine;

    HealthBar Bar;
    void Start()
    {
        lever = FindObjectOfType<Lever>();
        SubmarinePositionV3=transform.position;

        crank = FindObjectOfType<manivelle>();

        Bar = FindObjectOfType<HealthBar>();
        baseH = Bar.GetComponents<Transform>()[0].localScale;

        radarMiniSubmarine=GameObject.Find("radarMiniSubmarine");
    }
    void Update()
    {
        

        float w=map.width;
        float h=map.height;
/*COLLISON TO UNCOMMENT
        if(map.GetPixel((int)((x*w)%w),(int)((y*h)%h)).r==1){
            transform.position+=new Vector3(
                Random.Range(-colisionForce,colisionForce),
                Random.Range(-colisionForce,colisionForce),
                Random.Range(-colisionForce,colisionForce)
                );
            if(baseH.x-dmg>0){
                dmg+=0.001f;
                Bar.GetComponents<Transform>()[0].localScale = new Vector3(baseH.x-dmg,baseH.y,baseH.z);
            }
            Debug.Log("hit!");
        }
*/      


        forwardCommand = lever.forwardValue/100.0f;

        //float xx = (x*(Mathf.Cos(crank.tourGlobal*360)-Mathf.Sin(crank.tourGlobal*360)));
        //float yy = (y*(-Mathf.Sin(crank.tourGlobal*360)+Mathf.Cos(crank.tourGlobal*360)));

        /*
        x=(x%1);
        y=(y%1);
        */

        radius=Vector2.Distance(new Vector2(x,y),new Vector2(prex,prey));

        radarMiniSubmarine.transform.rotation = Quaternion.Euler(90 ,crank.tourGlobal*360,0);
        Debug.DrawRay(radarMiniSubmarine.transform.position,radarMiniSubmarine.transform.right,Color.cyan);

        x+=forwardCommand*Mathf.Cos(-2.0f*Mathf.PI*crank.tourGlobal);
        y+=forwardCommand*Mathf.Sin(-2.0f*Mathf.PI*crank.tourGlobal);
        
        

        radarPosition = new Vector2(
            x,
            y
            );

        prex=x;
        prey=y;

        // effect de secousse
        if(Time.frameCount%5==0)
            transform.position=SubmarinePositionV3;

    }
}
