using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
     
    public virtual void Attack(){}

    public virtual bool HandleMovement(Transform target, float maxDistance, float maxSpeed)
    {
        Vector2 distance = target.position - transform.position;
        Vector3 direction = distance.normalized;

        if (maxDistance * maxDistance <= distance.SqrMagnitude())
        {
            transform.position += direction *  maxSpeed * Time.deltaTime;
            if (direction.x < 0) transform.localScale = new Vector3(1, 1, 1);
            else if(direction.x > 0) transform.localScale = new Vector3(-1, 1, 1);
            
            return false;
        }
        else{return true;}
    }
}
