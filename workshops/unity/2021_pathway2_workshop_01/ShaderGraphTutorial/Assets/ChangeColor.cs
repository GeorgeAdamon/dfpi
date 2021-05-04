using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Color waterColor;
    public Color metalColor;

    [Range(0,1)]
    public float transition;


    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Mathf.Lerp(4, 5, transition);
        Color interpolatedColor = Color.Lerp(waterColor, metalColor, transition);

        material.SetColor("_Color", interpolatedColor);
    }
}
