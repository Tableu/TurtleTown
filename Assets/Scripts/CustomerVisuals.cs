using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerVisuals : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public void FlipVisuals()
    {
        if (transform.rotation.y > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void FlipVisuals(int direction)
    {
        if (direction > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void Walk()
    {
        animator.SetBool("Walk", true);
        animator.SetBool("Idle", false);
    }

    public void Idle()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Idle", true);
    }
}
