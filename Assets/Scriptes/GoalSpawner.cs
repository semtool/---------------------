using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

public class GoalSpawner : MonoBehaviour
{
    [SerializeField] private Goal _prefabGoal;
    [SerializeField] private Goal _prefabGoalOne;
    [SerializeField] private Goal _prefabGoalTwo;
    [SerializeField] private Plane _plane;
    [SerializeField] private float _goalSpeed;

    private List<Goal> _prefabs;
    private List<Goal> _goals;
    private float _minCoordinate = -20f;
    private float _maxCoordinate = 20f;
    private float _defaultCoordinateY = 1f;
    private GoalMover _goalMover;

    public List<Goal> Goals => _goals;

    private void Awake()
    {
        _prefabs = new List<Goal>()
        {
            _prefabGoal,
            _prefabGoalOne,
            _prefabGoalTwo
        };

        _goals = new List<Goal>();

        FillGoalList();
    }

    private void Start()
    {
        SpawnGoals();
    }

    private IEnumerator MoveToFinish(GoalMover goalMover)
    {
        while (true)
        {
            goalMover.MoveToFinishPosition(_plane.transform.position, _goalSpeed);

            var wait = new WaitForSeconds(0);

            yield return wait;
        }
    }

    public void SpawnGoals()
    {    
        foreach (var goal in _goals)
        {
            _goalMover = goal.GetComponent<GoalMover>();

            StartCoroutine(MoveToFinish(_goalMover));
        }
    }

    private void FillGoalList()
    {    
        foreach (var prefab in _prefabs)
        {
            Goal goal = GetGoal(prefab);
            _goals.Add(goal);
        }
    }

    private Goal GetGoal(Goal pefab)
    {
        return Instantiate(pefab, GetStartCoordinates() + _plane.transform.position, Quaternion.identity);
    }

    public Vector3 GetStartCoordinates()
    {
        return transform.position = new UnityEngine.Vector3(GetRandomCoordinate(), _defaultCoordinateY, GetRandomCoordinate());
    }

    private float GetRandomCoordinate()
    {
        return Random.Range(_minCoordinate, _maxCoordinate);
    }
}