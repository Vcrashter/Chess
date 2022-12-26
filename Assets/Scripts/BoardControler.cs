using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardControler : MonoBehaviour
{
    private Dictionary<Vector2, ColorTiles> allTiles = new Dictionary<Vector2, ColorTiles>();
    private ColorTiles selectedTiles;

    private void Start()
    {
        var tiles = gameObject.GetComponentsInChildren<ColorTiles>();

        for (int i = 0; i < tiles.Length; i++)
        {
            allTiles.Add(tiles[i].coordinates, tiles[i]);
        }
    }

    public void HighlightMoves(Vector2 piecePos, List<(int, int)> freePos)
    {
        for (int i = 0; i < freePos.Count; i++)
        {
            ColorTiles tile;
            allTiles.TryGetValue(new Vector2(piecePos.x + freePos[i].Item1, piecePos.y + freePos[i].Item2), out tile);
            if (tile != null)
            {
                tile.HighlightTiles();
            }
        }
    }

    public void UnHighlightMove()
    {
        var tiles = allTiles.Values.ToList();
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].NoHighlightTiles();
        }
    }

    public void AlocateColorTile(ColorTiles selectedTile)
    {
        selectedTiles = selectedTile;
    }

    public Transform GetSelectedTile()
    {
        return selectedTiles.transform;
    }
}