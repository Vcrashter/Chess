using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ControlPieces : MonoBehaviour
{
    private Vector3 offset;
    [SerializeField] private Pieces _pieces;
    [SerializeField] BoardControler boardControler;
    [SerializeField] private Vector2 myTileCoord;
    private Vector3 intTile;
    private TileControler _myTile;

    private void Start()
    {
        _myTile = boardControler.GetTile(myTileCoord);
        if (_myTile != null)
        {
            _myTile.DoBusy(this);
        }
    }

    private void OnMouseDown()
    {
        Movement();
        offset = transform.position - MouseWorldPosition();
        transform.GetComponent<Collider>().enabled = false;
        intTile = transform.position;
    }

    private void OnMouseDrag()
    {
        Vector3 intPos = MouseWorldPosition() + offset;
        Vector3 newPos = new Vector3(intPos.x, 15f, intPos.z);
        transform.position = newPos;
    }

    private void OnMouseUp()
    {
        var selectedTile = boardControler.GetSelectedTile();
        transform.GetComponent<Collider>().enabled = true;
        if (!selectedTile.GetComponent<TileControler>().isBlocked)
        {
            _myTile.DoUnBusy();
            _myTile = selectedTile.GetComponent<TileControler>();
            _myTile.DoBusy(this);
            gameObject.transform.position = selectedTile.position;
            myTileCoord = selectedTile.GetComponent<TileControler>().coordinates;
        }
        else
        {
            gameObject.transform.position = intTile;
        }
        boardControler.UnHighlightMove();
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    private void Movement()
    {
        boardControler.HighlightMoves(myTileCoord, _pieces);
    }
}

public enum Pieces
{
    none,
    pawn,
    bishop,
    knight,
    rook,
    queen,
    king
}