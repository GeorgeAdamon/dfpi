using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Serialization;
using Kinect = Windows.Kinect;

public class KinectBinder : MonoBehaviour
{
    
    private BodySourceManager BodyManager;
    private RigBinder         RigBinder;
    
    private Dictionary<ulong, GameObject> _bodies = new Dictionary<ulong, GameObject>();

    private Vector3 leftHandPosition;
    private Vector3 rightHandPosition;

    private Vector3 leftAnklePosition;
    private Vector3 rightAnklePosition;

    private Vector3 leftKneePosition;
    private Vector3 rightKneePosition;

    private Vector3 leftElbowPosition;
    private Vector3 rightElbowPosition;

    private Vector3 pelvisSpineBase;
    

    private Quaternion headRotation;
    private Quaternion leftShoulderRotation;
    private Quaternion rightShoulderRotation;
    private Quaternion leftAnkleRotation;
    private Quaternion rightAnkleRotation;
    private Quaternion torsoRotation;
    private Quaternion DerechaMano;
    private Quaternion IzquierdaMano;

    public Transform LeftHandTarget => RigBinder.LeftArmConstraint.data.target;
    public Transform RightHandTarget => RigBinder.RightArmConstraint.data.target;
    
    public Transform RightFootTarget => RigBinder.RightLegConstraint.data.target;
    public Transform LeftFootTarget => RigBinder.LeftLegConstraint.data.target;
    
    public Transform RightKneeTarget => RigBinder.RightLegConstraint.data.hint;
    public Transform LeftKneeTarget => RigBinder.LeftArmConstraint.data.hint;

    public Transform RightElbowTarget => RigBinder.RightArmConstraint.data.hint;
    public Transform LeftElbowTarget  => RigBinder.LeftArmConstraint.data.hint;
   
    [FormerlySerializedAs("PelvisObject")]
    [Space]

    public Transform PelvisTarget;
   

    [Header("Rotations")]
    public float LeftShoulderY = 0;
    public float RightShoulderY = 0;
    public float LeftAnkleY = -90;
    public float RightAnkleY = 90;

    [FormerlySerializedAs("HeadZ")]
    public float HeadTilt = 0;
    
    public float torsoRotationY = 180;
    
    public bool user = false;


    //Kinect Joint Dictionary
    private Dictionary<Kinect.JointType, Kinect.JointType> _BoneMap = new Dictionary<Kinect.JointType, Kinect.JointType>()
    {
            { Kinect.JointType.FootLeft, Kinect.JointType.AnkleLeft },
            { Kinect.JointType.AnkleLeft, Kinect.JointType.KneeLeft },
            { Kinect.JointType.KneeLeft, Kinect.JointType.HipLeft },
            { Kinect.JointType.HipLeft, Kinect.JointType.SpineBase },
        
            { Kinect.JointType.FootRight, Kinect.JointType.AnkleRight },
            { Kinect.JointType.AnkleRight, Kinect.JointType.KneeRight },
            { Kinect.JointType.KneeRight, Kinect.JointType.HipRight },
            { Kinect.JointType.HipRight, Kinect.JointType.SpineBase },
        
            { Kinect.JointType.HandTipLeft, Kinect.JointType.HandLeft },
            { Kinect.JointType.ThumbLeft, Kinect.JointType.HandLeft },
            { Kinect.JointType.HandLeft, Kinect.JointType.WristLeft },
            { Kinect.JointType.WristLeft, Kinect.JointType.ElbowLeft },
            { Kinect.JointType.ElbowLeft, Kinect.JointType.ShoulderLeft },
            { Kinect.JointType.ShoulderLeft, Kinect.JointType.SpineShoulder },
        
            { Kinect.JointType.HandTipRight, Kinect.JointType.HandRight },
            { Kinect.JointType.ThumbRight, Kinect.JointType.HandRight },
            { Kinect.JointType.HandRight, Kinect.JointType.WristRight },
            { Kinect.JointType.WristRight, Kinect.JointType.ElbowRight },
            { Kinect.JointType.ElbowRight, Kinect.JointType.ShoulderRight },
            { Kinect.JointType.ShoulderRight, Kinect.JointType.SpineShoulder },
        
            { Kinect.JointType.SpineBase, Kinect.JointType.SpineMid },
            { Kinect.JointType.SpineMid, Kinect.JointType.SpineShoulder },
            { Kinect.JointType.SpineShoulder, Kinect.JointType.Neck },
            { Kinect.JointType.Neck, Kinect.JointType.Head },
    };

    void Start()
    {
        BodyManager = GetComponent<BodySourceManager>();
        RigBinder   = GetComponent<RigBinder>();
    }

    private void Update()
    {
        // No Body Manager
        if (BodyManager == null) 
            return;
        
        var data = BodyManager.GetData();
      
        // No Body Data
        if (data == null) 
            return;
        
        var trackedIds = new List<ulong>();

        // Get Body IDs
        for (var i = 0; i<data.Length; i++)
        {
            var body = data[i];
          
            if (body == null)
                continue;

            if (body.IsTracked)
                trackedIds.Add(body.TrackingId);
        }
        
        // Current IDs
        var knownIds = new List<ulong>(_bodies.Keys);

        // If an ID is no longer present, destroy the body
        for (var i = 0; i<knownIds.Count; i++)
        {
            var trackingId = knownIds[i];
            
            if (trackedIds.Contains(trackingId)) 
                continue;
         
            Destroy(_bodies[trackingId]);
            _bodies.Remove(trackingId);
            user = false;
        }

        
        for (var i = 0; i <data.Length; i++)
        {
            var body = data[i];
            
            if (body == null || !body.IsTracked)
                continue;
            
            if(!_bodies.ContainsKey(body.TrackingId))
                _bodies[body.TrackingId] = CreateBody(body.TrackingId);                    
            
            user = true;

            UpdateBody(body, _bodies[body.TrackingId]);
                
            //Hands
            RightHandTarget.transform.position = rightHandPosition;
            LeftHandTarget.transform.position  = leftHandPosition;

            //Feet
            RightFootTarget.transform.position = rightAnklePosition;
            LeftFootTarget.transform.position  = leftAnklePosition;
                                
            //Knees
            RightKneeTarget.transform.position = rightKneePosition;
            LeftKneeTarget.transform.position  = leftKneePosition;

            //pelvis
            PelvisTarget.transform.position = new Vector3(pelvisSpineBase.x, pelvisSpineBase.y +0.1f, pelvisSpineBase.z);
               
            //Elbows                
            RightElbowTarget.transform.position = rightElbowPosition;
            LeftElbowTarget.transform.position  = leftElbowPosition;
        }
    }
    
    
    private static Color GetColorForState(Kinect.TrackingState state)
    {
        switch (state)
        {
            case Kinect.TrackingState.Tracked:
                return Color.green;

            case Kinect.TrackingState.Inferred:
                return Color.red;

            default:
                return Color.black;
        }
    }
    
   
    private static float Round(float articulation)
    {
        return Mathf.Round(articulation * Mathf.Pow(10, 2)) / 100;
    }
    
    private static GameObject CreateBody(ulong id)
    {
        var body = new GameObject("Body:" + id);

        for (var jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            //por cada joint crea un cubo
            var jointObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            
            jointObj.transform.localScale = new Vector3(0.000005f, 0.000005f, 0.000005f);
            jointObj.name                 = jt.ToString();
            jointObj.transform.parent     = body.transform;
        }
        
        return body;
    }
    
    private void UpdateBody(Kinect.Body body, GameObject bodyObject)
    {
        for (var jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            var           sourceJoint = body.Joints[jt];
            
            switch (jt)
            {
                case Kinect.JointType.SpineBase:
                    pelvisSpineBase = GetJointPosition(sourceJoint);
                    break;
                case Kinect.JointType.SpineMid:
                    torsoRotation      = getJointRotation(body, jt);
                    torsoRotationY = map(torsoRotation.w, 0.4f, -0.3f, 110, 240);
                    break;
                case Kinect.JointType.Neck:
                    headRotation = getJointRotation(body, jt);
                    float rotZ = headRotation.x;
                    if (rotZ > 0.05f)
                    {
                        rotZ = 0.05f;
                    }
                    else if (rotZ < -0.05f)
                    {
                        rotZ = -0.05f;
                    }
                    else
                    {
                        rotZ = headRotation.x;
                    }
                    HeadTilt = map(rotZ, -0.05f, 0.05f, -40, 40);
                    break;
                case Kinect.JointType.ShoulderLeft:
                    leftShoulderRotation = getJointRotation(body, jt);
                    RightShoulderY   = map(leftShoulderRotation.x, 0.70f, 0.80f, 0, -30);
                    break;
                case Kinect.JointType.ElbowLeft:
                    leftElbowPosition = GetJointPosition(sourceJoint);
                    break;
                case Kinect.JointType.HandLeft:
                    leftHandPosition = GetJointPosition(sourceJoint);
                    break;
                case Kinect.JointType.ShoulderRight:
                    rightShoulderRotation = getJointRotation(body, jt);
                    LeftShoulderY    = map(rightShoulderRotation.x, 0.80f, 0.70f, 0, -30);
                    break;
                case Kinect.JointType.ElbowRight:
                    rightElbowPosition = GetJointPosition(sourceJoint);
                    break;
                case Kinect.JointType.HandRight:
                    rightHandPosition = GetJointPosition(sourceJoint);
                    break;
                case Kinect.JointType.KneeLeft:
                    leftKneePosition = GetJointPosition(sourceJoint);
                    break;
                case Kinect.JointType.AnkleLeft:
                    leftAnklePosition  = GetJointPosition(sourceJoint);
                    leftAnkleRotation = getJointRotation(body, jt);
                    if(leftAnkleRotation.x < -0.6)
                        LeftAnkleY = 90;
                    else
                        LeftAnkleY = map(leftAnkleRotation.x, 0.9f, -0.5f, 50, 170);
                    break;
                case Kinect.JointType.KneeRight:
                    rightKneePosition = GetJointPosition(sourceJoint);
                    break;
                case Kinect.JointType.AnkleRight:
                    rightAnklePosition  = GetJointPosition(sourceJoint);
                    rightAnkleRotation  = getJointRotation(body, jt);
                    RightAnkleY = map(rightAnkleRotation.x, 0.9f, 0.3f, -30, -160);
                    break;
                case Kinect.JointType.Head:
                case Kinect.JointType.WristLeft:
                case Kinect.JointType.WristRight:
                case Kinect.JointType.HipLeft:
                case Kinect.JointType.FootLeft:
                case Kinect.JointType.HipRight:
                case Kinect.JointType.FootRight:
                case Kinect.JointType.SpineShoulder:
                case Kinect.JointType.HandTipLeft:
                case Kinect.JointType.ThumbLeft:
                case Kinect.JointType.HandTipRight:
                case Kinect.JointType.ThumbRight:
                    break;
            }
        }
    }

    private static Vector3 GetJointPosition(Kinect.Joint joint)
    {
        return new Vector3(joint.Position.X *2.8f, joint.Position.Y * 2.8f, joint.Position.Z * 2.8f);
    }

    private static Quaternion getJointRotation(Kinect.Body body, Kinect.JointType jointTd)
    {
        var orientation = body.JointOrientations[jointTd].Orientation;
                
        return new Quaternion(orientation.X, orientation.Y, orientation.Z, orientation.W);
    }

    private static float map(float x, float x1, float x2, float y1, float y2)
    {
        var m = (y2 - y1) / (x2 - x1);
        var c = y1 - m * x1; // point of interest: c is also equal to y2 - m * x2, though float math might lead to slightly different results.

        return m * x + c;
    }

}
