using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//enemies will drop and only the player can pick up
public class Drop : MonoBehaviour
{
    public LayerMask playerLayer;
    private void OnTriggerEnter(Collider other)
    {
        if(LayerMaskEX.IsInLayerMask(other.gameObject.layer, playerLayer))
        {
            Debug.Log("Drop Collected");
        }
    }
}
