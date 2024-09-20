   
public class FallHint : HintUI
{
    void OnEnable(){
       EventManager.OnPlayerFallingTooFast += ShowHint;
    }
    void OnDisable(){
       EventManager.OnPlayerFallingTooFast -= ShowHint;
    }
   
   
}
