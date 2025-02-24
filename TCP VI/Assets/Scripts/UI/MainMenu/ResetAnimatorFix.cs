using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetAnimatorFix : MonoBehaviour
{
    [SerializeField] Animator[] animators;

    void Start()
    {
        ResetAnimator();    
    }

    public void ReloadScene()
    {
        ResetAnimator();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ResetAnimator()
    {
        if(animators.Length > 0)
        {
            foreach(Animator anim in animators)
            {
                if(anim != null)
                {
                    anim.enabled = false;
                    anim.enabled = true;
                }
            }
        }
        else
        {
            Debug.LogWarning("Animator não atribuído");
        }
    }
}
