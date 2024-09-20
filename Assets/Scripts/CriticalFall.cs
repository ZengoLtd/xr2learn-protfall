using System.Collections;

using UnityEngine;


public class CriticalFall : MonoBehaviour
{

    public GameObject TeleportMarker;
    public GameObject JailCollider;
    public GameObject UI;
    bool failed = false;
    void OnEnable(){
        EventManager.OnPlayerFallingTooFast += CriticalFallEvent;
    }

    private void Start()
    {
        if(UI != null){
           UI.GetComponent<Canvas>().enabled = false;
        }
       
    }

    void CriticalFallEvent(){
        if(failed){
            return;
        }
        if(RopeManagerBlock.Instance == null){
            return;
        }
        if(RopeManagerBlock.Instance.isConnected()){
            //biztosnágosan zuhant;
            Debug.Log("Safe fall");
            return;
        }
        //nem biztosnágosan zuhant;
        failed = true;
        UI.GetComponent<Canvas>().enabled = true;
        Debug.Log("Critical fall");
        if(TeleportMarker == null){
            return;
        }
        StartCoroutine(TeleportAfter());
        JailCollider.SetActive(true);
   }
    IEnumerator TeleportAfter(){
        yield return new WaitForSeconds(1);
        if(TeleportMarker.transform.parent?.GetComponent<TeleportDestination>()){
            EventManager.PlayerTeleport(TeleportMarker.transform.position,TeleportMarker.transform.rotation,false,TeleportMarker.transform.parent.gameObject);
        }else{
            EventManager.PlayerTeleport(TeleportMarker.transform.position,TeleportMarker.transform.rotation,false,TeleportMarker);
        }
    }
   void OnDisable(){
       EventManager.OnPlayerFallingTooFast -= CriticalFallEvent;
   }
}
