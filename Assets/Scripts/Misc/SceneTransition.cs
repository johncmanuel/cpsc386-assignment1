using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance { get; private set; }
    [SerializeField] private Animator animator;
    private const string TriggerName = "Start";
    private const float TransitionTime = 1.5f;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Destroying duplicate SceneTransition instance...");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

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
