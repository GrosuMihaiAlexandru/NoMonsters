using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public Sprite penguin;
    public Sprite seal;
    public Sprite vampirePenguin;

    public RuntimeAnimatorController penguinAnimator;
    public RuntimeAnimatorController sealAnimator;
    public RuntimeAnimatorController vampirePenguinAnimator;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        //spriteRenderer.sprite = penguin;
        SetCharacter();
    }


    public void SetCharacter()
    {
        Debug.Log(GameManager.instance.selectedCharacter);
        switch (GameManager.instance.selectedCharacter)
        {
            case 0:
                spriteRenderer.sprite = penguin;
                animator.runtimeAnimatorController = penguinAnimator;
                break;
            case 1:
                spriteRenderer.sprite = seal;
                animator.runtimeAnimatorController = sealAnimator;
                break;
            case 2:
                spriteRenderer.sprite = vampirePenguin; 
                animator.runtimeAnimatorController = vampirePenguinAnimator;
                break;
            default:
                spriteRenderer.sprite = penguin;
                animator.runtimeAnimatorController = penguinAnimator;
                break;
        }
    }

}
