
using MEM;


public class ToggleOnRopeConnection : ModuleEventListenerBase
{

    void OnEnable()
    {
        ModuleEventManager.OnEvent += OnEvent;  
        OnEvent("RopeConnected", ModuleEventManager.GetEventLastValue("RopeConnected"));
    }

    protected override void OnEvent(string eventName, object value) {
        if(eventName != "RopeConnected"){
            return;
        }
            try{
                if(int.Parse(value.ToString()) > 0){
                transform.GetChild(0).gameObject.SetActive(false);
            }else{
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }catch{
           
        }
       
    }
    void OnDisable(){
        ModuleEventManager.OnEvent -= OnEvent;  
    }
}
