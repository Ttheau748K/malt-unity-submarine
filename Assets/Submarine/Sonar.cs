using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Sonar : MonoBehaviour
{
    float time;
    float threshold;
    float speed;
    [SerializeField] Material sonarMat;
    Vector2 previousPosition;
    Vector2 newPosition;

    SubmarineNavigator navigator;

    manivelle crank;
    void Start()
    {
        navigator = FindObjectOfType<SubmarineNavigator>();
        crank = FindObjectOfType<manivelle>();
        transform.rotation = Quaternion.Euler(0 ,26.5f ,0);
        
    }

    void Update()
    {
        
        //transform.rotation = Quaternion.Euler(0 ,crank.tourGlobal*360 ,0);


        threshold = sonarMat.GetFloat("_ScanFreq");
        speed = sonarMat.GetFloat("_ScanSpeed");
        time += Time.deltaTime*speed;
        //if (time >= threshold) { 
        if (true) {
            time = 0;
            previousPosition = newPosition;
            newPosition = navigator.radarPosition;
            sonarMat.SetVector("_Position", previousPosition);
            sonarMat.SetVector("_NextPosition", newPosition);
            
        }
    }
}
