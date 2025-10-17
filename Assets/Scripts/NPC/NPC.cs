using UnityEngine;
using System.Collections.Generic;

public class NPC : MonoBehaviour
{
    public float speed = 5f;

    private int index = 0;

    public List<Transform> waypoints = new List<Transform>();

    void Start()
    {

    }

    void Update()
    {
        if (waypoints.Count > 0)
        {
            Transform target = waypoints[index];
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, target.position) < 0.1f)
            {
                index++;
                if (index >= waypoints.Count)
                {
                    index = 0;
                }
            }
            else
            {
                // transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
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
