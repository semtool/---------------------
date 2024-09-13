using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private GoalSpawner _goalSpawner;

    private float _minCoordinateOfPosition = -10f;
    private float _maxCoordinateOfPosition = 10f;
    private float _maxTopCoordinateOfPosition = 1f;

    private void Awake()
    {
        _goalSpawner = GetComponent<GoalSpawner>();
    }

    public Vector3 GetPoinCoordinates()
    {
        return transform.position = new Vector3(GetRandomPosition(), _maxTopCoordinateOfPosition, GetRandomPosition());
    }

    private float GetRandomPosition()
    {
        return Random.Range(_minCoordinateOfPosition, _maxCoordinateOfPosition);
    }
}