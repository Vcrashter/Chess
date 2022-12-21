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
    [SerializeField] private Vector2 myTile;

    private void OnMouseDown()
    {
        Movement();
        offset = transform.position - MouseWorldPosition();
        transform.GetComponent<Collider>().enabled = false;
    }

    private void OnMouseDrag()
    {
        Vector3 intPos = MouseWorldPosition() + offset;
        Vector3 newPos = new Vector3(intPos.x, 15f, intPos.z);
        transform.position = newPos;
    }

    private void OnMouseUp()
    {
        transform.GetComponent<Collider>().enabled = true;
        gameObject.transform.position = boardControler.GetSelectedTile().position;
        myTile = boardControler.GetSelectedTile().GetComponent<ColorTiles>().coordinates;
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
        Dictionary<int, (int, int)> moves = new Dictionary<int, (int, int)>();
        switch (_pieces)
        {
            case Pieces.pawn:
                moves.Add(0, (0, 1));
                moves.Add(1, (0, 2));
                break;
            case Pieces.bishop:
                break;
            case Pieces.knight:
                break;
            case Pieces.rook:
                break;
            case Pieces.queen:
                break;
            case Pieces.king:
                break;
            default:
                break;
        }
        boardControler.HighlightMoves(myTile, moves.Values.ToList());
    }
}

enum Pieces
{
    none,
    pawn,
    bishop,
    knight,
    rook,
    queen,
    king
}