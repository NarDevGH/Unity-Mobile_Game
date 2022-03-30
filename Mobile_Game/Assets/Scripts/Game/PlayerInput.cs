using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _duckLayer;
    void Update()
    {
        if (Input.touchCount > 0) 
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = _camera.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began) 
            {
                RaycastHit hit;
                if (Physics.Raycast(touchPosition, Vector3.forward,out hit, Mathf.Infinity, _duckLayer))
                {
                    hit.transform.GetComponent<Duck>().OnClick();
                }
            }
        }
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 touchPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(touchPosition, Vector3.forward, out hit, Mathf.Infinity, _duckLayer))
            {
                hit.transform.GetComponent<Duck>().OnClick();
            }
        }
    }
}
