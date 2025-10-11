using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Player player;
    private Animator animator;

    void Start()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player.moveDirection.sqrMagnitude > 0)
        {
            animator.SetInteger("transition", 1);
        }
        else
        {
            animator.SetInteger("transition", 0);
        }

        if( player.moveDirection.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        else if( player.moveDirection.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }
}
