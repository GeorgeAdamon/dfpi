
public class LeftShoulderRotation : RotationFixBase
{
    protected override float Z => kinectBinder.LeftShoulderY;
}
