using UnityEngine;
using UnityEngine.VFX.Utility;

[AddComponentMenu("VFX/Property Binders/Simple Values/Integer Binder")]
[VFXBinder("Simple Values/Integer")]
public class IntVFXBinder : ValueBinderBase<int>
{
    [VFXPropertyBinding(nameof(System.Int32)),
     SerializeField,
     UnityEngine.Serialization.FormerlySerializedAs("m_Parameter")]
    protected ExposedProperty m_Property = "Some Int";

    protected override ExposedProperty Property => m_Property;
}
