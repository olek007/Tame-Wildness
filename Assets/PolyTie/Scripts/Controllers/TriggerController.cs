using UnityEngine;
using System.Collections;

public class TriggerController : MonoBehaviour 
{
    public enum MessageRouting
    {
        Both,
        OnlyTriggerObject,
        OnlyTarget,
    }
    /// <summary>
    /// Weather the message should be sent to the current game object the trigger resides or to the
    /// specified target object or to both.
    /// </summary>
    public MessageRouting Routing = MessageRouting.OnlyTriggerObject;

    /// <summary>
    /// Target object the trigger message should be routed to.
    /// </summary>
    public GameObject Target;

    /// <summary>
    /// True if message should be sent once an object enters the trigger area.
    /// </summary>
    public bool ListenToOnEnter = true;        
    /// <summary>
    /// True if message should be while an object stays within the trigger area.
    /// </summary>
    public bool ListenToOnStay = true;    
    /// <summary>
    /// True if message should be sent once an object leaves the trigger area.
    /// </summary>
    public bool ListenToOnExits = true;
    /// <summary>
    /// True if the trigger object should be destroyed once it's triggered.
    /// </summary>
    public bool DestroyOnTrigger;

	// Use this for initialization
	void Start () 
    {
        if ((Routing == MessageRouting.Both || Routing == MessageRouting.OnlyTarget) && Target == null)
        {
            Debug.LogError("No target specified to route the message to. Trigger routing is set to OnlyTriggerObject");
            Routing = MessageRouting.OnlyTriggerObject;
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((Routing == MessageRouting.Both || Routing == MessageRouting.OnlyTarget) && ListenToOnEnter == true)
        {
            Target.SendMessage("OnRoutedTriggerEnter2D", other, SendMessageOptions.DontRequireReceiver);
        }
        if (ListenToOnEnter == true && DestroyOnTrigger == true)
            Destroy(gameObject);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if ((Routing == MessageRouting.Both || Routing == MessageRouting.OnlyTarget) && ListenToOnStay == true)
        {
            Target.SendMessage("OnRoutedTriggerStay2D", other, SendMessageOptions.DontRequireReceiver);
        }
        if (ListenToOnStay == true && DestroyOnTrigger == true)
            Destroy(gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if ((Routing == MessageRouting.Both || Routing == MessageRouting.OnlyTarget) && ListenToOnExits == true)
        {
            Target.SendMessage("OnRoutedTriggerExit2D", other, SendMessageOptions.DontRequireReceiver);
        }
        if (ListenToOnExits == true && DestroyOnTrigger == true)
            Destroy(gameObject);
    }
}
