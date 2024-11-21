using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Lever : MonoBehaviour
{
	  float angle=0;
    public float forwardValue=0;
    public int maxAngleLever=33;
    public int leverDeadZone=5;

    XRSimpleInteractable Interactable;
    XRBaseInteractor HandInteractor;
    bool isSelected = false;
    // Start is called before the first frame update
    void Start()
    {
        Interactable = GetComponent<XRSimpleInteractable>();
        Interactable.onSelectEntered.AddListener(OnSelect);
        Interactable.onSelectExited.AddListener(OnDeselect);
        Debug.Log("_start_");
    }

    // Update is called once per frame
    void OnSelect(XRBaseInteractor interactor)
    {   
      //transform.LookAt(interactor.transform);
      isSelected=true;
      HandInteractor=interactor;

      //Debug.Log(Interactable);
      Debug.Log("Selected");
      //Debug.Log(HandInteractor);
    }

    void OnDeselect(XRBaseInteractor interactor){
      isSelected=false;
      HandInteractor=null;
      Debug.Log("deselected");

    }
    void Update(){
      
		if(isSelected){
			
			Interactable.transform.LookAt(HandInteractor.transform);

			Vector3 currentRotation = Interactable.transform.eulerAngles;
      angle = Mathf.Clamp(-currentRotation.y+90,-maxAngleLever,maxAngleLever);
			Interactable.transform.eulerAngles=new Vector3(
			angle,
			0,
			0
			);

      if(angle>leverDeadZone){
        forwardValue=-(angle-leverDeadZone)/(maxAngleLever-leverDeadZone);
      }
			
			else if(angle<-leverDeadZone){
        forwardValue=-(angle+leverDeadZone)/(maxAngleLever-leverDeadZone);
      }
			else{
        forwardValue=0;
      }

		}
	}

}