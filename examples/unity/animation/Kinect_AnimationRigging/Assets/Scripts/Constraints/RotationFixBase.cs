using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RotationFixBase : MonoBehaviour
{
    [Header("References")]
    public Transform    target;
    public KinectBinder kinectBinder;

    [Space]
    
    [Range(0.01f,10f)]
    public float smoothing = 5f;
    
    [SerializeField]
    private float _x;
    
    [SerializeField]
    private float _y;
    
    [SerializeField]
    private float _z;

    protected virtual float X => _x;

    protected virtual float Y => _y;

    protected virtual float Z => _z;


    private void OnEnable()
    {
        target = transform;
    }

    protected virtual void Update()
    {
        if (kinectBinder == null || target == null)
            return;
        
        target.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(X, Y, Z), Time.deltaTime * smoothing);
    }
}
