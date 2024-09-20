using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeTeleportLimiter : MonoBehaviour
{
    public List<GameObject> disableIfConnectedToCart;
    public List<GameObject> disapbeIfConnectedTolift;
    public TeleportDestinationMarker teleportDestinationMarker;
    public List<RopeConnector> liftConnectors;
    public RopeConnector cartConnector;


    bool isconnectedtoLift(){
        foreach (var connector in liftConnectors)
        {
            if((bool)connector.ropeConnected.state){
                return true;
            }
        }
        return false;
    }

    public void OnConnectToggle(){

        
        if(isconnectedtoLift()){
            Debug.Log("Connected to lift");
            foreach (var item in disableIfConnectedToCart)
            {
                item.SetActive(false);
            }
        }
        else{
            Debug.Log("Not connected to lift");
            foreach (var item in disableIfConnectedToCart)
            {
                item.SetActive(true);
            }
             teleportDestinationMarker.ShowDestinations();
        }
        if((bool)cartConnector.ropeConnected.state == true){
            Debug.Log("Connected to cart");
            foreach (var item in disapbeIfConnectedTolift)
            {
                item.SetActive(false);
            }
        }
        else{
            Debug.Log("Not connected to cart");
            foreach (var item in disapbeIfConnectedTolift)
            {
                item.SetActive(true);
            }
           
        }
    }
    


}
