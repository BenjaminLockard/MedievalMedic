using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWalk : MonoBehaviour
{
    public DialogueManager dialogueManager;
    
    [Header("Target X positions")]
    public float approachX = 2f; // X position when UI opens
    public float leaveX = 10f;   // X position when NPC walks away

    [Header("Movement Settings")]
    public float speed = 2f;

    private bool isMoving = false;
    private Animator animator;
    private bool hasIsWalkingParam = false;
    private const string isWalkingParamName = "isWalking";

    public AudioSource npcAudio;
    public AudioClip npcSound;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            // Cache whether the animator has the parameter to avoid errors
            foreach (var p in animator.parameters)
            {
                if (p.name == isWalkingParamName && p.type == AnimatorControllerParameterType.Bool)
                {
                    hasIsWalkingParam = true;
                    break;
                }
            }
        }
    }

    public void ResetPosition()
    {
        StopAllCoroutines();
        isMoving = false;
        animator.SetBool("isWalking", false);
        transform.position = new Vector3(leaveX, transform.position.y, transform.position.z);
    }

    // Called when player opens UI or interaction starts
    public void Approach()
    {
        if (!isMoving)
            StartCoroutine(MoveToX(approachX));
    }

    // Called when the day ends (NPC walks off screen and stays gone)
    public void Leave()
    {
        if (!isMoving)
            StartCoroutine(MoveToX(leaveX));
    }

    // Called after player confirms / ends interaction
    public void LeaveAndReturn(float delayBeforeReturn = 1f)
    {
        if (!isMoving)
            StartCoroutine(LeaveAndComeBack(delayBeforeReturn));
    }

    private IEnumerator LeaveAndComeBack(float delay)
    {
        yield return MoveToX(leaveX); // Walk away first
        yield return MoveToX(approachX); // Come back automatically
    }

    private IEnumerator MoveToX(float targetX)
    {
        isMoving = true;

        // Start walking animation only if param exists
        if (animator != null && hasIsWalkingParam)
            animator.SetBool(isWalkingParamName, true);

        // Move until near target
        while (Mathf.Abs(transform.position.x - targetX) > 0.05f)
        {
            float direction = Mathf.Sign(targetX - transform.position.x);

            // Face movement direction properly (faces right by default)
            Vector3 scale = transform.localScale;
            if (direction < 0)
                scale.x = Mathf.Abs(scale.x); // Face right
            else
                scale.x = -Mathf.Abs(scale.x); // Face left
            transform.localScale = scale;

            // Move toward target
            Vector2 newPos = transform.position;
            newPos.x = Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime);
            transform.position = newPos;

            yield return null;
        }

        // Snap to exact target position
        transform.position = new Vector2(targetX, transform.position.y);

        // Stop walking animation only if param exists
        if (animator != null && hasIsWalkingParam)
            animator.SetBool(isWalkingParamName, false);

        isMoving = false;

        if (targetX == approachX)
        {
            dialogueManager.promptUser();
            npcAudio.PlayOneShot(npcSound, 1.0f);
        }
    }
}