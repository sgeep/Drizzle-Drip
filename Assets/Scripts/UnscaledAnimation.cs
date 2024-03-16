using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnscaledAnimation : MonoBehaviour
{
    public Animator animator;
    private float lastRealTime;

    void Start()
    {
        lastRealTime = Time.realtimeSinceStartup;
    }

    void Update()
    {
        float deltaTime = Time.realtimeSinceStartup - lastRealTime;
        lastRealTime = Time.realtimeSinceStartup;

        animator.Update(deltaTime);
    }
}
