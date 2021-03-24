using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Serialization;

public class RigBinder : MonoBehaviour
{
    [Header("Original Joints")]
    public Transform Root;
    
    private Transform LeftUpperLeg;
    private Transform RightUpperLeg;

    [Space]
    
    public Transform LeftShoulder;
    public Transform RightShoulder;
   
    [Space]
    private Transform Spine1;
    private Transform Spine2;
    private Transform Spine3;
    
    [Space]
    
    public Transform Neck;
    
    [Header("Animation Rigging References")]
    public TwoBoneIKConstraint LeftLegConstraint;

    public TwoBoneIKConstraint RightLegConstraint;
    
    [Space]
    public TwoBoneIKConstraint LeftArmConstraint;

    public TwoBoneIKConstraint RightArmConstraint;

    [Space]
    
    public OverrideTransform LeftShoulderConstraint;

    public OverrideTransform RightShoulderConstraint;

    [Space]

    public OverrideTransform Hips;

    public OverrideTransform SpineConstraint;

    public OverrideTransform ChestConstraint;

    public OverrideTransform UpperChestConstraint;

    public OverrideTransform NeckConstraint;

    public OverrideTransform HeadConstraint;




    void Awake()
    {

        for (int i = 0; i < Root.childCount; i++)
        {
            var child = Root.GetChild(i);

            if (child.name.Contains("Leg"))
            {
                if (child.name.Contains("Left"))
                {
                    LeftUpperLeg = child;
                }                      
                else if (child.name.Contains("Right"))
                {
                    RightUpperLeg = child;
                }
            }
            else if (child.name.Contains("Spine"))
            {
                Spine1 = child;
            }
        }

        Spine2 = Spine1.GetChild(0);
        Spine3 = Neck.parent;
        
        Bind();
    }



    public void Bind()
    {
        NeckConstraint.data.constrainedObject = Neck;
        HeadConstraint.data.constrainedObject = Neck.GetChild(0);
        
        LeftShoulderConstraint.data.constrainedObject  = LeftShoulder;
        RightShoulderConstraint.data.constrainedObject = RightShoulder;

        Hips.data.constrainedObject = Root;
        
        SpineConstraint.data.constrainedObject      = Spine1;
        ChestConstraint.data.constrainedObject      = Spine1.GetChild(0);
        
        UpperChestConstraint.data.constrainedObject = Neck.parent;
        
        Bind(LeftLegConstraint, LeftUpperLeg);
        Bind(RightLegConstraint, RightUpperLeg);
        
        Bind(LeftArmConstraint, LeftShoulder.GetChild(0));
        Bind(RightArmConstraint,  RightShoulder.GetChild(0));
    }

    private static void Bind(TwoBoneIKConstraint ik, Transform root)
    {
        Bind(ik,root, root.GetChild(0), root.GetChild(0).GetChild(0));
    }
    
    private static void Bind(TwoBoneIKConstraint ik, Transform root, Transform mid, Transform tip)
    {
        ik.data.tip  = tip;
        ik.data.mid  = mid;
        ik.data.root = root;
    }
}
