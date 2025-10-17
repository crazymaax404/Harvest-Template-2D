using UnityEngine;
using System.Collections.Generic;

public class NPC : MonoBehaviour
{
    public float speed = 5f;

    private int index = 0;
    private float initialSpeed;
    private Animator animator;

    public List<Transform> waypoints = new List<Transform>();

    void Start()
    {
        initialSpeed = speed;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (DialogControl.instance.isShowing)
        {
            speed = 0f;
            animator.SetBool("isWalking", false);
        }
        else
        {
            speed = initialSpeed;
            animator.SetBool("isWalking", true);
        }

        if (waypoints.Count > 0)
        {
            Transform target = waypoints[index];
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, target.position) < 0.1f)
            {
                index = Random.Range(0, waypoints.Count);
                if (index >= waypoints.Count)
                {
                    index = 0;
                }
            }
        }

        Vector2 direction = waypoints[index].position - transform.position;
        if (direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 180f);
        }
    }
}
