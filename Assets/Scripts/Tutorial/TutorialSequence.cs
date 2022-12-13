using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSequence : MonoBehaviour
{
    public Animator animator;

    private void FixedUpdate()
    {
        if (Time.fixedDeltaTime % 2 == 0)
        {
            animator.SetFloat("Grip", 1.0f);
        }
        else
        {
            animator.SetFloat("Grip", 0.0f);
        }
    }
}
