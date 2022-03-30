using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class first_Stage : MonoBehaviour
{
    [SerializeField,Min(0)] private float _stageDuration;
    [SerializeField,Min(0)] private float _timeBetweenSpawn;
    [Space]
    [SerializeField] private GameObject _duck;


    public IEnumerator StageRoutine() 
    {
        Game_Manager.Singleton.points = 0;
        Coroutine spawnRoutine = StartCoroutine(SpawnDucksRoutine());
        yield return new WaitForSeconds(_stageDuration);
        StopCoroutine(spawnRoutine);
        yield return new WaitForSeconds(3);
    }

    private IEnumerator SpawnDucksRoutine() 
    {
        while (true) 
        {
            DuckSpawn_Manager.Instance.CloneDuckAtRandomRow();
            yield return new WaitForSeconds(_timeBetweenSpawn);
        }
    }
}
