using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckSpawn_Manager : MonoBehaviour
{
    private int MAX_SPAWN_ON_SAME_ROW = 4;
    private int DUCKS_POOL_SIZE = 10;

    [SerializeField] private DuckRow_Str[] _duckRows;
    [SerializeField] private GameObject _duckPrefab;

    public static DuckSpawn_Manager Instance;

    private int _lastRowIndex;
    private int _consecutiveSameRowSpawns;

    private List<GameObject> _ducksPool = new List<GameObject>();

    private void Awake()
    {
        HandleInstances();
        GenerateDucksPool();
    }

    private void GenerateDucksPool()
    {
        for (int i = 0; i < DUCKS_POOL_SIZE; i++)
        {
            GameObject duckClone = Instantiate(_duckPrefab);
            duckClone.SetActive(false);
            _ducksPool.Add(duckClone);
        }
    }

    private void HandleInstances()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(this);
        }
    }


    public void CloneDuckAtRandomRow(TravelDirection travelDirection = TravelDirection.LeftToRight) 
    {
        int rowIndex = UnityEngine.Random.Range(0, _duckRows.Length);

        #region IfSameRowLogic
        if (rowIndex == _lastRowIndex)
        {
            if (_consecutiveSameRowSpawns == MAX_SPAWN_ON_SAME_ROW) 
            {
                while (rowIndex == _lastRowIndex) 
                {
                    rowIndex = UnityEngine.Random.Range(0, _duckRows.Length);
                }
            }
            _consecutiveSameRowSpawns++;
        }
        else 
        {
            _consecutiveSameRowSpawns = 0;
        }
        #endregion

        if (travelDirection == TravelDirection.LeftToRight)
        {
            CloneDuck(_duckRows[rowIndex].start, _duckRows[rowIndex].end, _duckRows[rowIndex].layerOrder);
            
        }
        else 
        {
            CloneDuck(_duckRows[rowIndex].end, _duckRows[rowIndex].start, _duckRows[rowIndex].layerOrder);
        }
    }

    public void CloneDuck(Transform start, Transform end, int orderInLayer)
    {
        GameObject duck = GetDisabledDuck();

        if (duck == null) return;

        duck.SetActive(true);
        duck.transform.position = start.position;
        duck.transform.rotation = start.rotation;
        duck.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
        duck.GetComponent<Duck>().GoTo(end.position);
    }

    private GameObject GetDisabledDuck() 
    {
        foreach (GameObject duck in _ducksPool) 
        {
            if (!duck.activeSelf) return duck;
        }
        return null;
    }

}

[Serializable]
public struct DuckRow_Str 
{
    public Transform start;
    public Transform end;
    public int layerOrder;
    [HideInInspector] public float lastSpawnTime;
}

public enum TravelDirection {LeftToRight,RightToTleft}
