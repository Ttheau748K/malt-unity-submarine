using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class manivelle : MonoBehaviour
{
    float counter=0;
    public float tourMod=0;
    public float tourGlobal=0;
    Vector3 previousHandPos;
    Vector3 HandPos;
    Vector3 basepose; 
    XRSimpleInteractable Interactable;
    XRBaseInteractor HandInteractor;
    bool isSelected = false;
    void Start()
    {
        Interactable = GetComponent<XRSimpleInteractable>();
        Interactable.onSelectEntered.AddListener(OnSelect);
        Interactable.onSelectExited.AddListener(OnDeselect);
        Debug.Log("_start_");
        basepose = transform.position;

    }

    // Update is called once per frame
    void OnSelect(XRBaseInteractor interactor)
    {   

        isSelected=true;
        HandInteractor=interactor;

        HandPos=HandInteractor.transform.position;
        previousHandPos=HandPos;

        Debug.Log("Selected");
    }

    void OnDeselect(XRBaseInteractor interactor){
        
        isSelected=false;
        HandInteractor=null;
        Debug.Log("deselected");
        
    }
    void Update(){
      
		if(isSelected){
            
            HandPos=HandInteractor.transform.position;

            Debug.DrawRay(HandPos, HandInteractor.transform.forward, Color.green);
            Debug.DrawRay(HandPos, HandInteractor.transform.right, Color.magenta);
            Debug.DrawRay(HandPos, HandInteractor.transform.up, Color.blue);

            Vector3 FirstVect = new Vector3(
                basepose.x,
                HandPos.y,
                HandPos.z
                );

            Vector3 SecondVect = new Vector3(
                basepose.x, 
                previousHandPos.y, 
                previousHandPos.z
                );
                
            Debug.DrawLine(basepose, FirstVect, Color.cyan); 
            Debug.DrawLine(basepose, SecondVect, Color.magenta);

            Debug.DrawLine(basepose,HandPos,Color.blue);
            Debug.DrawLine(basepose,previousHandPos,Color.green);

            float radius = Vector3.Distance(basepose, FirstVect);

            Vector3 baseX = transform.right;
            float FirstAngle = Vector3.Angle(FirstVect,baseX);
            float SecondAngle = Vector3.Angle(SecondVect,baseX);          

            float DeltaAngle = (FirstAngle-SecondAngle);

            //Debug.Log(Time.frameCount);

            transform.Rotate(new Vector3(
                                        0,
                                        DeltaAngle,
                                        0));

            counter+=DeltaAngle;
            tourMod=counter%360;

            tourGlobal = ((counter-tourMod)/360)+tourMod/360;
            
            if(Time.frameCount%5==0)
                previousHandPos=HandPos;
            
		}
	}

}
