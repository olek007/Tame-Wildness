using UnityEngine;
using System.Collections;

public class PatrolingController : MonoBehaviour 
{
    public VectorPath2D PatrolingPath;
    public bool IsLooping;
    public bool MoveOnStart;

    private IMotorInterface _motor;
    private Vector2[] _waypoints;
    private int _curIndex;
    private int _incrementor = 1;

	// Use this for initialization
	void Start () 
    {
        _motor = this.GetInterface<IMotorInterface>();
        if (_motor == null)
            throw new MissingComponentException("A patroling controller needs some sort of Motor component (found in the Motor subfolder within the Scriptfolder) attached to it");
        if (PatrolingPath == null)
            throw new MissingReferenceException("A patroling path needs to reference a VectorPath2D object to determine the patroling path");

        _waypoints = PatrolingPath.GetWorldPoints();

        if (MoveOnStart == true)
        {
            StartPatroling();
        }
	}

    public void OnRoutedTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject);
        StartPatroling();
    }

    public void StartPatroling()
    {
        _motor.MoveTo(_waypoints[_curIndex], () =>
        {
            _curIndex += _incrementor;
            if (_curIndex >= _waypoints.Length || _curIndex < 0)
            {
                if (IsLooping == true)
                {
                    _incrementor *= -1;
                    _curIndex += _incrementor;
                    StartPatroling();
                }
                else
                {
                    _motor.Stop();
                }
            }
            else
            {
                StartPatroling();
            }
        });
    }
}
