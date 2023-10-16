using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIScript : MonoBehaviour
{
    public Animator animator;
    public GameObject panel;

    public void In()
    {
       if(panel != null)
        {
            if(animator != null)
            {
                bool IsOpen = animator.GetBool("OutAndIn");

                animator.SetBool("OutAndIn", !IsOpen);
            }
        }
    }
    


}
