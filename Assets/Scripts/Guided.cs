using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guided : MonoBehaviour
{
    public static Guided instance;
    void Awake(){
        if(instance != this){
            Destroy(instance);
        }
        
        DontDestroyOnLoad(gameObject);
        instance = this;
        
    }
    public bool guided = true;
    public void EnableGuided(){
       guided = true;
    }
    public void DisableGuided(){
        guided = false;
    }
}
