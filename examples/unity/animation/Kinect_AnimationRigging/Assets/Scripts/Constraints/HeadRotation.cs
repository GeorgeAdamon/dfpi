using UnityEngine;

public class HeadRotation : RotationFixBase
{
    protected override float Z => kinectBinder.HeadTilt;
}
