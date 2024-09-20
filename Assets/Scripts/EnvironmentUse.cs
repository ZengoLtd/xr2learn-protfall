using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentUse : MonoBehaviour
{
    public GameObject handle;
    public Texture2D selectedTex;
    public Texture2D normalTex;

    bool isHover = false;
    public void OnTriggerEnter(Collider other)
    {
        isHover = true;
    }
    public void OnTriggerExit(Collider other)
    {
        isHover = false;
    }
    void OnDisable(){
        isHover = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHover)
        {
            if (selectedTex && handle != null)
            {
                handle.GetComponent<Renderer>().material.mainTexture = selectedTex;
            }
        }
        else
        {
            if (normalTex && handle != null)
            {
                handle.GetComponent<Renderer>().material.mainTexture = normalTex;
            }
        }
    }
}
