using UnityEngine;
using System.Collections;

/// <summary>
/// A Killing zone resets or kills all actor objects that enter it.
/// It can be used to create for instance lava, pits or thorny 
/// obstacles in a platform game.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class KillingZoneController : MonoBehaviour
{
    public RespawnController RespawnPoint;
    public LayerMask AffectedLayer = -1;

	// Use this for initialization
	void Start () 
    {
	    // Make sure attached collider is a trigger.
        collider2D.isTrigger = true;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (AffectedLayer.value != -1 && isLayerInLayerMAsk(other.gameObject.layer, AffectedLayer) == false)
            return;

        if (RespawnPoint == null || RespawnPoint.Respawn(other.gameObject) == false)
        {
            Destroy(other.gameObject);
        }
    }

    private bool isLayerInLayerMAsk(int layer, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << layer)) > 0);
    }
}
