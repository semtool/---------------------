using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiSpawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefabEnemyFirst;
    [SerializeField] private Enemy _prefabEnemySecond;
    [SerializeField] private Enemy _prefabEnemyThird;
    [SerializeField] private Plane _plane;
    [SerializeField] private float _speedOfMoving;
  
    private List<Vector3> _enemyCoorditates;
    private List<Enemy> _prefabs;
    private List<Enemy> _enemies;
    private GoalSpawner _goalSpawner;
    private SpawnPoint _point;
    private EnemyMover _enemyMover;

    private int _counterOfSpawnPoints = 0;
    private int _intervalOfInstantiation = 2;
    private float _valueOfUpdate = 0;


    public void Awake()
    {
        _prefabs = new List<Enemy>()
        {
            _prefabEnemyFirst,
            _prefabEnemySecond,
            _prefabEnemyThird
        };

        _enemies = new List<Enemy>();
        _point = GetComponent<SpawnPoint>();
        _goalSpawner = GetComponent<GoalSpawner>();
        _enemyCoorditates = new List<Vector3>();

        CreatePointsOfSpawn();
    }

    private void Start()
    {     
        StartCoroutine(SpawnEnemyInRandomPoint());
    }

    private IEnumerator SpawnEnemyInRandomPoint()
    {
        while (true)
        {
            SpawnEnemies();

            var wait = new WaitForSeconds(_intervalOfInstantiation);

            yield return wait;
        }
    }

    private IEnumerator MoveInLine(EnemyMover moover, Goal goal)
    {
        while (true)
        {
            moover.Move(goal.transform, _speedOfMoving);

            var wait = new WaitForSeconds(_valueOfUpdate);

            yield return wait;
        }
    }

    private void SpawnEnemies()
    {
        _enemies.Clear();

        SetStartCoordinatesToEnemy();
        
        for ( int i = 0; i< _enemies.Count; i++)
        {
            for (int j = 0; j < _goalSpawner.Goals.Count; j++)
            {
                if ( i == j)
                {
                    _enemyMover = _enemies[i].GetComponent<EnemyMover>();
                    StartCoroutine(MoveInLine(_enemyMover, _goalSpawner.Goals[i]));
                }
            }
        }
    }

    private void SetStartCoordinatesToEnemy()
    {
        for (int i = 0; i < _prefabs.Count; i++)
        {
            for (int k = 0; k < _enemyCoorditates.Count; k++)
            {
                if (i == k)
                {
                    _enemies.Add(GetEnemy(_prefabs[i], _plane.transform.position +_enemyCoorditates[k]));
                }
            }
        }
    }

    private void CreatePointsOfSpawn()
    {
        while (_counterOfSpawnPoints < _prefabs.Count)
        {
            _enemyCoorditates.Add(_point.GetPoinCoordinates());
            _counterOfSpawnPoints++;
        }
    }

    private Enemy GetEnemy(Enemy prefab, Vector3 pointCoordinates)
    {
        return Instantiate(prefab, pointCoordinates, Quaternion.identity);
    }
}