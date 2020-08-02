using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class Border : MonoBehaviour
{    
    public LayerMask destroy;
    //public LayerMask contain;

    private void OnTriggerExit(Collider other)
    {
        if (LayerMaskEX.IsInLayerMask(other.gameObject.layer, destroy))
        {
            //convert to recycle, need to create pool for bullets
            Destroy(other.gameObject);
        }
    }

    //check player pos, keep in bounds
    public Transform player;
    public float gameBoundsX = 10f;
    public float gameBoundsY = 10f;

    private void CheckPlayerBounds()
    {
        float x = Mathf.Clamp(player.position.x, -gameBoundsX, gameBoundsX);
        float y = Mathf.Clamp(player.position.y, - gameBoundsY, gameBoundsY);
        player.position = new Vector3(x, y, 0f);
    }

    private void Update()
    {
        CheckPlayerBounds();
    }


    //DEBUG
    public Color gizmosColour = Color.white;
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColour;
        Vector3 bounds = new Vector3(gameBoundsX * 2, gameBoundsY * 2, 1f);
        Gizmos.DrawWireCube(transform.position, bounds);
    }
}
