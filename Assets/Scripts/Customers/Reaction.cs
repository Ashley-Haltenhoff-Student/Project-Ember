using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Reaction : MonoBehaviour
{
    [SerializeField] private Sprite[] reactions;

    private float reactionDuration = 2f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void React(string reaction)
    {
        foreach (Sprite s in reactions)
        {
            if (s.name == reaction)
            {
                spriteRenderer.sprite = s;
                StartCoroutine(AnimateReaction());
                return;
            }
        }

        Debug.Log("There is no emoji named " +  reaction);
        spriteRenderer.sprite = null;
    }

    private IEnumerator AnimateReaction()
    {
        animator.SetTrigger("summonEmoji");

        yield return new WaitForSeconds(reactionDuration);

        spriteRenderer.sprite = null;
    }
}
