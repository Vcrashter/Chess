using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTiles : MonoBehaviour
{
    Color _mouseColor = Color.red;
    Color _originalColor;
    MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        _originalColor = meshRenderer.material.color;
    }

    private void OnMouseOver()
    {
        meshRenderer.material.color = _mouseColor;
    }

    private void OnMouseExit()
    {
        meshRenderer.material.color = _originalColor;
    }
}
