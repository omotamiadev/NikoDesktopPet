using UnityEngine;
using System.Collections;

public class CController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float animationFrameRate = 0.15f;
    public float randomMoveInterval = 2f;

    [Header("Sprite Renderer")]
    public SpriteRenderer spriteRenderer;

    [Header("Character Slots")]
    public CharacterData character0;
    public CharacterData character1;
    public CharacterData character2;
    public CharacterData character3;
    public CharacterData character4;
    public CharacterData character5;
    public CharacterData character6;
    public CharacterData character7;
    public CharacterData character8;
    public CharacterData character9;
    public CharacterData character10;
    public CharacterData character11;
    public CharacterData character12;

    private CharacterData currentCharacter;
    private Vector2 movement;
    private Vector2 lastNonZeroDirection = Vector2.down;

    private float animationTimer;
    private int animationFrame;
    private Rigidbody2D rb;

    private bool isManualControl = true;
    private Coroutine randomMovementCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (currentCharacter == null && character0 != null)
        {
            Swap(character0);
        }
        EnableManualControl();
    }

    void Update()
    {
        if (isManualControl)
        {
            HandleInput();
        }
        MoveCharacter();
        UpdateSpriteAnimation();
    }

    void HandleInput()
    {
        movement = Vector2.zero;
        if (Input.GetKey(KeyCode.UpArrow)) movement.y += 1;
        if (Input.GetKey(KeyCode.DownArrow)) movement.y -= 1;
        if (Input.GetKey(KeyCode.LeftArrow)) movement.x -= 1;
        if (Input.GetKey(KeyCode.RightArrow)) movement.x += 1;
        movement = movement.normalized;
    }

    void MoveCharacter()
    {
        if (rb == null) return;
        rb.velocity = movement * moveSpeed;
    }

    void UpdateSpriteAnimation()
    {
        if (currentCharacter == null || spriteRenderer == null) return;

        DirectionalAnimationSet animSet;
        if (movement == Vector2.zero)
        {
            animSet = GetDirectionSetFromVector(lastNonZeroDirection);
            spriteRenderer.sprite = animSet?.idle ?? null;
            animationTimer = 0f;
            animationFrame = 0;
        }
        else
        {
            lastNonZeroDirection = movement;
            animSet = GetDirectionSetFromVector(movement);
            if (animSet?.walkCycle != null && animSet.walkCycle.Length > 0)
            {
                animationTimer += Time.deltaTime;
                if (animationTimer >= animationFrameRate)
                {
                    animationTimer = 0f;
                    animationFrame = (animationFrame + 1) % animSet.walkCycle.Length;
                }
                spriteRenderer.sprite = animSet.walkCycle[animationFrame];
            }
            else
            {
                spriteRenderer.sprite = animSet?.idle ?? null;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isManualControl)
        {
            movement = Vector2.zero;
            if (randomMovementCoroutine != null)
                StopCoroutine(randomMovementCoroutine);
            randomMovementCoroutine = StartCoroutine(ResumeAfterCollision());
        }
    }
    IEnumerator ResumeAfterCollision()
    {
        float pauseDuration = Random.Range(0.5f, 1.5f);
        yield return new WaitForSeconds(pauseDuration);

        movement = GetRandomDirection();

        float walkDuration = Random.Range(0.5f, 1.5f);
        yield return new WaitForSeconds(walkDuration);

        movement = Vector2.zero;

        float nextPause = Random.Range(1f, 3f);
        yield return new WaitForSeconds(nextPause);

        if (!isManualControl)
            randomMovementCoroutine = StartCoroutine(RandomMovementRoutine());
    }

    DirectionalAnimationSet GetDirectionSetFromVector(Vector2 dir)
    {
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            return dir.x > 0 ? currentCharacter.right : currentCharacter.left;
        else
            return dir.y > 0 ? currentCharacter.up : currentCharacter.down;
    }

    private void Swap(CharacterData data)
    {
        if (data == null)
        {
            Debug.LogWarning("CharacterData is null.");
            return;
        }
        currentCharacter = data;
        animationTimer = 0f;
        animationFrame = 0;
        DirectionalAnimationSet animSet = GetDirectionSetFromVector(lastNonZeroDirection);
        spriteRenderer.sprite = animSet?.idle ?? null;
    }

    public void EnableManualControl()
    {
        isManualControl = true;
        movement = Vector2.zero;
        if (randomMovementCoroutine != null)
        {
            StopCoroutine(randomMovementCoroutine);
            randomMovementCoroutine = null;
        }
    }

    public void EnableRandomMovement()
    {
        isManualControl = false;
        movement = Vector2.zero;
        if (randomMovementCoroutine != null)
            StopCoroutine(randomMovementCoroutine);
        randomMovementCoroutine = StartCoroutine(RandomMovementRoutine());
    }

    IEnumerator RandomMovementRoutine()
    {
        while (!isManualControl)
        {
            movement = GetRandomDirection();

            float walkDuration = Random.Range(0.5f, 1.5f);
            yield return new WaitForSeconds(walkDuration);

            movement = Vector2.zero;

            float pauseDuration = Random.Range(1f, 3f);
            yield return new WaitForSeconds(pauseDuration);
        }
    }


    Vector2 GetRandomDirection()
    {
        int dir = Random.Range(0, 4);
        switch (dir)
        {
            case 0: return Vector2.up;
            case 1: return Vector2.down;
            case 2: return Vector2.left;
            case 3: return Vector2.right;
            default: return Vector2.zero;
        }
    }
    public void SwapToCharacter0() => Swap(character0);
    public void SwapToCharacter1() => Swap(character1);
    public void SwapToCharacter2() => Swap(character2);
    public void SwapToCharacter3() => Swap(character3);
    public void SwapToCharacter4() => Swap(character4);
    public void SwapToCharacter5() => Swap(character5);
    public void SwapToCharacter6() => Swap(character6);
    public void SwapToCharacter7() => Swap(character7);
    public void SwapToCharacter8() => Swap(character8);
    public void SwapToCharacter9() => Swap(character9);
    public void SwapToCharacter10() => Swap(character10);
    public void SwapToCharacter11() => Swap(character11);
    public void SwapToCharacter12() => Swap(character12);
}