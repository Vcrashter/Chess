using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BoardControler : MonoBehaviour
{
    private Dictionary<Vector2, TileControler> allTiles = new Dictionary<Vector2, TileControler>();
    private TileControler selectedTiles;

    private void Awake()
    {
        var tiles = gameObject.GetComponentsInChildren<TileControler>();

        for (int i = 0; i < tiles.Length; i++)
        {
            allTiles.Add(tiles[i].coordinates, tiles[i]);
        }
    }

    public void HighlightMoves(Vector2 piecePos, Pieces pieces)
    {
        List<Vector2> moves = new List<Vector2>();
        TileControler tile;
        switch (pieces)
        {
            case Pieces.pawn:
                moves.Add(new Vector2(0, 1) + piecePos);
                if (piecePos.y == 1)
                {
                    for (int i = 1; i < 3; i++)
                    {
                        allTiles.TryGetValue(new Vector2(0, i) + piecePos, out tile);
                        if (tile == null) { break; }
                        if (tile.GetBusy()) { break; }
                        moves.Add(tile.coordinates);
                    }
                }
                if (piecePos.y == 7)
                {
                    Destroy(gameObject);
                   // Instantiate<>(, transform.position, Quaternion.identity);
                }
                //moves.Add(new Vector2(1, 1) + piecePos);
                //moves.Add(new Vector2(-1, 1) + piecePos);
                break;
            case Pieces.bishop:
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(i, i) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy()) { break; }
                    moves.Add(tile.coordinates);
                }
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(-i, i) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy()) { break; }
                    moves.Add(tile.coordinates);
                }
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(i, -i) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy()) { break; }
                    moves.Add(tile.coordinates);
                }
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(-i, -i) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy()) { break; }
                    moves.Add(tile.coordinates);
                }
                break;
            case Pieces.knight:
                moves.Add(new Vector2(2, 1) + piecePos);
                moves.Add(new Vector2(2, -1) + piecePos);
                moves.Add(new Vector2(1, 2) + piecePos);
                moves.Add(new Vector2(-1, 2) + piecePos);
                moves.Add(new Vector2(-2, 1) + piecePos);
                moves.Add(new Vector2(-2, -1) + piecePos);
                moves.Add(new Vector2(1, -2) + piecePos);
                moves.Add(new Vector2(-1, -2) + piecePos);
                break;
            case Pieces.rook:
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(i, 0) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy()) { break; }
                    moves.Add(tile.coordinates);
                }
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(-i, 0) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy()) { break; }
                    moves.Add(tile.coordinates);
                }
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(0, i) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy()) { break; }
                    moves.Add(tile.coordinates);
                }
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(0, -i) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy()) { break; }
                    moves.Add(tile.coordinates);
                }
                break;
            case Pieces.queen:
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(i, i) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy()) { break; }
                    moves.Add(tile.coordinates);
                }
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(-i, i) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy()) { break; }
                    moves.Add(tile.coordinates);
                }
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(i, -i) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy()) { break; }
                    moves.Add(tile.coordinates);
                }
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(-i, -i) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy()) { break; }
                    moves.Add(tile.coordinates);
                }
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(i, 0) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy()) { break; }
                    moves.Add(tile.coordinates);
                }
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(-i, 0) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy()) { break; }
                    moves.Add(tile.coordinates);
                }
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(0, i) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy()) { break; }
                    moves.Add(tile.coordinates);
                }
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(0, -i) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy()) { break; }
                    moves.Add(tile.coordinates);
                }
                break;
            case Pieces.king:
                moves.Add(new Vector2(0, 1) + piecePos);
                moves.Add(new Vector2(1, 1) + piecePos);
                moves.Add(new Vector2(1, 0) + piecePos);
                moves.Add(new Vector2(-1, -1) + piecePos);
                moves.Add(new Vector2(0, -1) + piecePos);
                moves.Add(new Vector2(-1, 0) + piecePos);
                moves.Add(new Vector2(1, -1) + piecePos);
                moves.Add(new Vector2(-1, 1) + piecePos);
                break;
            default:
                break;
        }

        for (int i = 0; i < moves.Count; i++)
        {
            allTiles.TryGetValue(moves[i], out tile);
            if (tile != null && !tile.GetBusy())
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

    public void AlocateColorTile(TileControler selectedTile)
    {
        this.selectedTiles = selectedTile;
    }

    public Transform GetSelectedTile()
    {
        return selectedTiles.transform;
    }

    public TileControler GetTile(Vector2 tileCoord)
    {
        TileControler tile;
        allTiles.TryGetValue(tileCoord, out tile);
        if (tile != null)
        {
            return tile;
        }
        Debug.LogError("Didnt find tile " + tileCoord);
        return null;
    }
}