using UnityEngine;
using System;
using System.Collections.Generic;

public interface IMotorInterface
{
    /// <summary>
    /// Move the object to the specified position.
    /// </summary>
    /// <param name="position">Positon to move the object to</param>
    /// <param name="onTargetReached">If passed the callback is invoked once the object reaches the position</param>
    void MoveTo(Vector2 position, Action onTargetReached = null);
    
    /// <summary>
    /// Move the object in the specified direction.
    /// </summary>
    /// <param name="direction">Direction vector specifing movement</param>
    void Move(Vector2 direction);

    /// <summary>
    /// Stops any movement.
    /// </summary>
    void Stop();

    /// <summary>
    /// Let's the object jump if it is capeable of jumping.
    /// </summary>
    void Jump();
}
