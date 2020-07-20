using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//when player comes close turns on the UI for that IKbase
public class UITrigger : MonoBehaviour
{
    public IK_Base thisBase;
    public LayerMask playerLayer;    

    public static event Action<IK_Base> Enter;
    public static event Action Exit;


    private void OnTriggerEnter(Collider other)
    {
        if(LayerMaskEX.IsInLayerMask(other.gameObject.layer, playerLayer))
        {
            Enter(thisBase);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (LayerMaskEX.IsInLayerMask(other.gameObject.layer, playerLayer))
        {
            Exit();
        }
    }
}
