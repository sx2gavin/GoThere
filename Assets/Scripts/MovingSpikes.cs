using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MovingSpikes : MonoBehaviour
{
    public float idleTime = 1.0f;
    public float outTime = 1.0f;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(MoveSpikes());
    }

    private IEnumerator MoveSpikes()
    {
        while (true)
        {
            yield return new WaitForSeconds(idleTime);
            animator.SetBool("IsOut", true);
            yield return new WaitForSeconds(outTime);
            animator.SetBool("IsOut", false);
        }
    }
}
