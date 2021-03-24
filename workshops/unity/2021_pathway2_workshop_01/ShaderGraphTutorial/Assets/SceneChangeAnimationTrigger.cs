using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeAnimationTrigger : MonoBehaviour
{
    public Animator animator;

    public string triggerName;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
       Transform
        if (Input.GetKey(KeyCode.Space)) animator.SetTrigger(triggerName);
    }
}
