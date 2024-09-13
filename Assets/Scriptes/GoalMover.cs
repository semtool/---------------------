using UnityEngine;

public class GoalMover : MonoBehaviour
{
    private Vector3 _targetDirection;
    private float _minCoordinate = -25f;
    private float _maxCoordinate = 25f;
    private float _defaultCoordinateY = 1f;

    private void Start()
    {
        _targetDirection = GetFinishMarkOfWay(_minCoordinate, _defaultCoordinateY, _maxCoordinate);
    }

    public void MoveToFinishPosition(Vector3 platformPosition,float speedOfMoving)
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetDirection + platformPosition, speedOfMoving);
    }

    private Vector3 GetFinishMarkOfWay(float xCoordinate, float yCoordinate, float zCoordinate)
    {
        return new Vector3(GetRandomCoordinate(xCoordinate, zCoordinate), yCoordinate, GetRandomCoordinate(xCoordinate, zCoordinate));
    }

    private float GetRandomCoordinate(float xCoordinate, float zCoordinate)
    {
        return Random.Range(xCoordinate, zCoordinate);
    }
}