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
        transform.GetComponent<Collider>().enabled = true;
        if (!boardControler.GetSelectedTile().GetComponent<ColorTiles>().isBlocked)
        {
            gameObject.transform.position = boardControler.GetSelectedTile().position;
            myTileCoord = boardControler.GetSelectedTile().GetComponent<ColorTiles>().coordinates;
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
        Dictionary<int, (int, int)> moves = new Dictionary<int, (int, int)>();
        switch (_pieces)
        {
            case Pieces.pawn:
                moves.Add(0, (0, 1));
                moves.Add(1, (0, 2));
                break;
            case Pieces.bishop:
                moves.Add(0, (1, 1));
                moves.Add(1, (2, 2));
                moves.Add(2, (3, 3));
                moves.Add(3, (4, 4));
                moves.Add(4, (5, 5));
                moves.Add(5, (6, 6));
                moves.Add(6, (7, 7));
                moves.Add(7, (-1, -1));
                moves.Add(8, (-2, -2));
                moves.Add(9, (-3, -3));
                moves.Add(10, (-4, -4));
                moves.Add(11, (-5, -5));
                moves.Add(12, (-6, -6));
                moves.Add(13, (-7, -7));
                moves.Add(14, (1, -1));
                moves.Add(15, (2, -2));
                moves.Add(16, (3, -3));
                moves.Add(17, (4, -4));
                moves.Add(18, (5, -5));
                moves.Add(19, (6, -6));
                moves.Add(20, (7, -7));
                moves.Add(21, (-1, 1));
                moves.Add(22, (-2, 2));
                moves.Add(23, (-3, 3));
                moves.Add(24, (-4, 4));
                moves.Add(25, (-5, 5));
                moves.Add(26, (-6, 6));
                moves.Add(27, (-7, 7));
                break;
            case Pieces.knight:
                moves.Add(0, (2, 1));
                moves.Add(1, (2, -1));
                moves.Add(2, (1, 2));
                moves.Add(3, (-1, 2));
                moves.Add(4, (-2, 1));
                moves.Add(5, (-2, -1));
                moves.Add(6, (1, -2));
                moves.Add(7, (-1, -2));
                break;
            case Pieces.rook:
                moves.Add(0, (0, 1));
                moves.Add(1, (0, 2));
                moves.Add(2, (0, 3));
                moves.Add(3, (0, 4));
                moves.Add(4, (0, 5));
                moves.Add(5, (0, 6));
                moves.Add(6, (0, 7));
                moves.Add(7, (0, -1));
                moves.Add(8, (0, -2));
                moves.Add(9, (0, -3));
                moves.Add(10, (0, -4));
                moves.Add(11, (0, -5));
                moves.Add(12, (0, -6));
                moves.Add(13, (0, -7));
                moves.Add(14, (1, 0));
                moves.Add(15, (2, 0));
                moves.Add(16, (3, 0));
                moves.Add(17, (4, 0));
                moves.Add(18, (5, 0));
                moves.Add(19, (6, 0));
                moves.Add(20, (7, 0));
                moves.Add(21, (-1, 0));
                moves.Add(22, (-2, 0));
                moves.Add(23, (-3, 0));
                moves.Add(24, (-4, 0));
                moves.Add(25, (-5, 0));
                moves.Add(26, (-6, 0));
                moves.Add(27, (-7, 0));
                break;
            case Pieces.queen:
                moves.Add(0, (1, 1));
                moves.Add(1, (2, 2));
                moves.Add(2, (3, 3));
                moves.Add(3, (4, 4));
                moves.Add(4, (5, 5));
                moves.Add(5, (6, 6));
                moves.Add(6, (7, 7));
                moves.Add(7, (-1, -1));
                moves.Add(8, (-2, -2));
                moves.Add(9, (-3, -3));
                moves.Add(10, (-4, -4));
                moves.Add(11, (-5, -5));
                moves.Add(12, (-6, -6));
                moves.Add(13, (-7, -7));
                moves.Add(14, (1, -1));
                moves.Add(15, (2, -2));
                moves.Add(16, (3, -3));
                moves.Add(17, (4, -4));
                moves.Add(18, (5, -5));
                moves.Add(19, (6, -6));
                moves.Add(20, (7, -7));
                moves.Add(21, (-1, 1));
                moves.Add(22, (-2, 2));
                moves.Add(23, (-3, 3));
                moves.Add(24, (-4, 4));
                moves.Add(25, (-5, 5));
                moves.Add(26, (-6, 6));
                moves.Add(27, (-7, 7));
                moves.Add(28, (0, 1));
                moves.Add(29, (0, 2));
                moves.Add(30, (0, 3));
                moves.Add(31, (0, 4));
                moves.Add(32, (0, 5));
                moves.Add(33, (0, 6));
                moves.Add(34, (0, 7));
                moves.Add(35, (0, -1));
                moves.Add(36, (0, -2));
                moves.Add(37, (0, -3));
                moves.Add(38, (0, -4));
                moves.Add(39, (0, -5));
                moves.Add(40, (0, -6));
                moves.Add(41, (0, -7));
                moves.Add(42, (1, 0));
                moves.Add(43, (2, 0));
                moves.Add(44, (3, 0));
                moves.Add(45, (4, 0));
                moves.Add(46, (5, 0));
                moves.Add(47, (6, 0));
                moves.Add(48, (7, 0));
                moves.Add(49, (-1, 0));
                moves.Add(50, (-2, 0));
                moves.Add(51, (-3, 0));
                moves.Add(52, (-4, 0));
                moves.Add(53, (-5, 0));
                moves.Add(54, (-6, 0));
                moves.Add(55, (-7, 0));
                break;
            case Pieces.king:
                moves.Add(0, (0, 1));
                moves.Add(1, (1, 1));
                moves.Add(2, (1, 0));
                moves.Add(3, (-1, -1));
                moves.Add(4, (0, -1));
                moves.Add(5, (-1, 0));
                moves.Add(6, (1, -1));
                moves.Add(7, (-1, 1));
                break;
            default:
                break;
        }
        boardControler.HighlightMoves(myTileCoord, moves.Values.ToList());
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