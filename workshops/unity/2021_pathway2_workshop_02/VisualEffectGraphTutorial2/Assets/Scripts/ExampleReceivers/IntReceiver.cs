using UnityEngine;

public class IntReceiver : MonoBehaviour, IValue<int>
{
    [SerializeField]
    [Range(2,32)]
    private int value;

    public int Value
    {
        get => value;
        set => this.value = value;
    }
}
