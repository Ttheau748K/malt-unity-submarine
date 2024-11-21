using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class SubmarineNavigator : MonoBehaviour
{
    float x=0.5f,y=0.5f;
    float dmg=0.0f;
    float colisionForce=0.01f;
    Vector3 SubmarinePositionV3;
    float counter;
    public Vector2 radarPosition;
    Vector3 baseH;
    public Texture2D map;
    Lever lever;
    manivelle crank;

    HealthBar Bar;
    void Start()
    {
        lever = FindObjectOfType<Lever>();
        SubmarinePositionV3=transform.position;

        crank = FindObjectOfType<manivelle>();

        Bar = FindObjectOfType<HealthBar>();
        baseH = Bar.GetComponents<Transform>()[0].localScale;

    }
    void Update()
    {
        

        float w=map.width;
        float h=map.height;

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
        
        //Debug.Log(map.GetPixel((int)((x*w)%w),(int)((h/2+y*h)%h)));
        //Debug.Log("x: "+(int)((x*w)%w)+" y: "+(int)((y*h)%h));

        x -= lever.forwardValue/100.0f;
        //float xx = (x*(Mathf.Cos(crank.tourGlobal*360)-Mathf.Sin(crank.tourGlobal*360)));
        //float yy = (y*(-Mathf.Sin(crank.tourGlobal*360)+Mathf.Cos(crank.tourGlobal*360)));

        
        x=(x%1);
        y=(y%1);

        radarPosition = new Vector2(
            x,
            y
            );

        if(Time.frameCount%5==0)
            transform.position=SubmarinePositionV3;
    }
}
