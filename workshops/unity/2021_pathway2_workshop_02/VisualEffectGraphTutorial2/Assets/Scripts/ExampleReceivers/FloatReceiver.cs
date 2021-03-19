using UnityEngine;

public class FloatReceiver : MonoBehaviour, IValue<float>
{
    [SerializeField]
    [Range(1, 10)]
    private float value;

    public float Value
    {
        get => value;
        set => this.value = value;
    }
}
