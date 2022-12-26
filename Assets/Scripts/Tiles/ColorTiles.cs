using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTiles : MonoBehaviour
{
    public Vector2 coordinates;
    private Color _mouseColor = Color.red;
    private Color _originalColor;
    private MeshRenderer meshRenderer;
    private BoardControler boardControler;
    public bool isBlocked;

    void Start()
    {
        isBlocked = true;
        meshRenderer = GetComponent<MeshRenderer>();
        _originalColor = meshRenderer.material.color;
        FindParent(transform.parent.transform);
    }

    private void FindParent(Transform parent)
    {
        if (parent == null)
        {
            return; //daca nu mai exista parinte se opreste
        }
        boardControler = parent.GetComponent<BoardControler>();
        if (boardControler == null)
        {
            FindParent(parent.parent.transform); //se apeleaza la infinit
        }
    }

    private void OnMouseOver()
    {
        if (boardControler != null)
        {
            boardControler.AlocateColorTile(this);
        }
    }

    public void NoHighlightTiles()
    {
        meshRenderer.material.color = _originalColor;
        isBlocked = true;
    }

    public void HighlightTiles()
    {
        meshRenderer.material.color = _mouseColor;
        isBlocked = false;
    }
}