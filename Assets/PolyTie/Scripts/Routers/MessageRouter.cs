using UnityEngine;
using System.Collections;

public class MessageRouter : MonoBehaviour 
{
    private System.Action<Collision2D> _onCollisionStay2DRouted;

    public void OnCollisionStay2D(Collision2D collider)
    {
        
        if (_onCollisionStay2DRouted != null)
        {
            _onCollisionStay2DRouted(collider);
        }
    }

    public void RegisterCollisionStay2D(System.Action<Collision2D> callback)
    {
        _onCollisionStay2DRouted += callback;
    }
}
