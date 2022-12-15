using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoordinateLabels : MonoBehaviour
{
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    private string _letter;

    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
        DisplayCoordinates();
    }

    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }
    }

    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z) + 1;

        XCoordinates();

        label.text = _letter + coordinates.y;

    }

    private void XCoordinates()
    {
        if (coordinates.x == 0f)
        {
            _letter = "A";
        }
        else if (coordinates.x == 10f)
        {
            _letter = "B";
        }
        else if (coordinates.x == 20f)
        {
            _letter = "C";
        }
        else if (coordinates.x == 30f)
        {
            _letter = "D";
        }
        else if (coordinates.x == 40f)
        {
            _letter = "E";
        }
        else if (coordinates.x == 50f)
        {
            _letter = "F";
        }
        else if (coordinates.x == 60f)
        {
            _letter = "G";
        }
        else if (coordinates.x == 70f)
        {
            _letter = "H";
        }
    }

    private void UpdateObjectName()
    {
        transform.parent.name = label.text;
    }
}
