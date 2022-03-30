using System.Collections;
using UnityEngine;

public class Duck : MonoBehaviour
{

    [SerializeField] private int points;

    [SerializeField,Min(0)] private float _travelTime = 5f;

    public void OnClick() 
    {
        Game_Manager.Singleton.points += points;
        StopAllCoroutines();
        gameObject.SetActive(false);
    }


    public void GoTo(Vector3 destination) 
    {
        StartCoroutine( GoToRoutine(destination) );
    }

    private IEnumerator GoToRoutine(Vector3 destination) 
    {
        Vector3 _startValue = transform.position;
        float t = 0;
        while (t < _travelTime) 
        {
            transform.position = Vector3.Lerp(_startValue, destination, t/_travelTime);
            t += Time.deltaTime;
            yield return null; ;
        }
        StopAllCoroutines();
        gameObject.SetActive(false);
    }
}