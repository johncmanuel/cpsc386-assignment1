using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private const string TriggerName = "Start";
    private const float TransitionTime = 1.5f;

    private void Start()
    {
        if (animator == null)
            Debug.Log("Animator is null;");
    }

    public IEnumerator TriggerTransition()
    {
        animator.SetTrigger(TriggerName);
        yield return new WaitForSeconds(TransitionTime);
    }
}
