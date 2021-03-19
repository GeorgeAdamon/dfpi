using UnityEngine;
using UnityEngine.VFX.Utility;

[AddComponentMenu("VFX/Property Binders/Simple Values/Float Binder")]
[VFXBinder("Simple Values/Float")]
public class FloatVFXBinder : ValueBinderBase<float>
{
    [VFXPropertyBinding(nameof(System.Single)),
     SerializeField,
     UnityEngine.Serialization.FormerlySerializedAs("m_Parameter")]
    protected ExposedProperty m_Property = "Some Float";

    protected override ExposedProperty Property => m_Property;
}
