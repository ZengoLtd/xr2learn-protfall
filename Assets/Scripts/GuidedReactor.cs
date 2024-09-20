using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedReactor : MonoBehaviour
{
    void Awake(){
        gameObject.SetActive( Guided.instance.guided);
       
    }
}
