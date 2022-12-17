using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPieces : MonoBehaviour
{
    private Vector3 _intPos;
    private Vector3 _airPos;

    protected virtual void Start()
    {
        _intPos = gameObject.transform.position;
    }

    protected virtual void Update()
    {
        Move();
    }

    public virtual void Move() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            _airPos = _intPos - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            transform.position = Camera.main.WorldToScreenPoint(Input.mousePosition) + _airPos;
        }
    }
}
