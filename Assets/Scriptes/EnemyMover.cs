using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public void Move(Transform goalTransform, float speedOfMoving)
    {
        transform.position = Vector3.MoveTowards(transform.position, goalTransform.position, speedOfMoving);
    }
}