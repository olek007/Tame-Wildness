using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformMotor : MonoBehaviour, IMotorInterface 
{
    public float Speed = 5.0f;
    public float MovementSnappyness = 0.1f;
    public float TargetReachedThreshold = 0.2f;

    public Vector2 movementTarget { get; private set; }
    public Vector3 movementDirection { get; private set; }
    public float speedModulation { get; set; }

    private Action _targetReached;
    private bool _isCallbackCleanLocked;
    private bool _isUsingMovementTarget;
    private bool _isStopped;

    void Awake()
    {
        speedModulation = 1.0f;
    }

	// Use this for initialization
	void Start () 
    {
        if (rigidbody2D.isKinematic == false)
            rigidbody2D.isKinematic = true;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (_isStopped == true)
        {
            return;
        }

        if (_isUsingMovementTarget == true)
        {
            Vector2 diff = (movementTarget - (Vector2)transform.position);
            float dist = diff.magnitude;
            movementDirection = diff.normalized;

            if (dist > TargetReachedThreshold)
            {
                Vector2 targetVelocity = movementDirection * Speed * speedModulation;
                
                float softRadius = TargetReachedThreshold * 2.0f;
                if (dist <= softRadius)
                    targetVelocity = targetVelocity * Mathf.Max(0.1f, (dist / softRadius));

                rigidbody2D.velocity = targetVelocity;
            }
            else
            {
                Stop();
                if (_targetReached != null)
                {
                    _targetReached();

                    if (_isCallbackCleanLocked == false)
                    {
                        _targetReached = null;
                    }
                    _isCallbackCleanLocked = false;
                }
            }
        }
        else
        {
            Vector2 targetVelocity = movementDirection * Speed * speedModulation;

            rigidbody2D.velocity = targetVelocity;
        }
	}

    public void MoveTo(Vector2 position, Action onTargetReached = null)
    {
        movementTarget = position;
        _isUsingMovementTarget = true;
        _isStopped = false;

        if (onTargetReached != null)
        {
            _targetReached = onTargetReached;
            _isCallbackCleanLocked = true;
        }

        StartCoroutine(modulateSpeed(MovementSnappyness));
    }

    private IEnumerator modulateSpeed(float time)
    {
        speedModulation = 0;
        while (speedModulation < 1.0f)
        {
            speedModulation += (1 / time) * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    public void Move(Vector2 direction)
    {
        movementDirection = direction;
        _isUsingMovementTarget = false;
        _isStopped = false;
    }

    public void Stop()
    {
        rigidbody2D.AddForce(Vector2.zero);
        rigidbody2D.velocity = Vector2.zero;
        _isStopped = true;
    }

    public void Jump()
    {
    }
}
