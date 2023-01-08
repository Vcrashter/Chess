using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BoardControler : MonoBehaviour
{
    private Dictionary<Vector2, TileControler> allTiles = new Dictionary<Vector2, TileControler>();
    private TileControler selectedTiles;
    private List<Vector2> moves = new List<Vector2>();
    [SerializeField] private List<ControlPieces> allPieces = new List<ControlPieces>();

    private void Awake()
    {
        var tiles = gameObject.GetComponentsInChildren<TileControler>();

        for (int i = 0; i < tiles.Length; i++)
        {
            allTiles.Add(tiles[i].coordinates, tiles[i]);
        }
    }

    public void HighlightMoves(Vector2 piecePos, Pieces pieces, TeamPieces teamPieces)
    {
        moves.Clear();
        TileControler tile;
        switch (pieces)
        {
            case Pieces.pawn:
                if (teamPieces == TeamPieces.white)
                {
                    allTiles.TryGetValue(new Vector2(0, 1) + piecePos, out tile);
                    if (tile != null)
                    {
                        if (!tile.GetBusy())
                        {
                            moves.Add(tile.coordinates);
                            if (piecePos.y == 1)
                            {
                                for (int i = 1; i < 3; i++)
                                {
                                    allTiles.TryGetValue(new Vector2(0, i) + piecePos, out tile);
                                    if (tile == null) { break; }
                                    if (!tile.GetBusy()) { moves.Add(tile.coordinates); }
                                }
                            }
                        }
                    }

                    allTiles.TryGetValue(new Vector2(1, 1) + piecePos, out tile);
                    if (tile != null)
                    {
                        if (tile.GetBusy())
                        {
                            if (tile.GetTeam() != teamPieces)
                            {
                                moves.Add(tile.coordinates);
                            }
                        }
                    }

                    allTiles.TryGetValue(new Vector2(-1, 1) + piecePos, out tile);
                    if (tile != null)
                    {
                        if (tile.GetBusy())
                        {
                            if (tile.GetTeam() != teamPieces)
                            {
                                moves.Add(tile.coordinates);
                            }
                        }
                    }
                }
                else if (teamPieces == TeamPieces.black)
                {
                    allTiles.TryGetValue(new Vector2(0, -1) + piecePos, out tile);
                    if (tile != null)
                    {
                        if (!tile.GetBusy())
                        {
                            moves.Add(tile.coordinates);
                            if (piecePos.y == 6)
                            {
                                for (int i = 1; i < 3; i++)
                                {
                                    allTiles.TryGetValue(new Vector2(0, -i) + piecePos, out tile);
                                    if (tile == null) { break; }
                                    if (!tile.GetBusy()) { moves.Add(tile.coordinates); }
                                }
                            }
                        }
                    }

                    allTiles.TryGetValue(new Vector2(1, -1) + piecePos, out tile);
                    if (tile != null)
                    {
                        if (tile.GetBusy())
                        {
                            if (tile.GetTeam() != teamPieces)
                            {
                                moves.Add(tile.coordinates);
                            }
                        }
                    }

                    allTiles.TryGetValue(new Vector2(-1, -1) + piecePos, out tile);
                    if (tile != null)
                    {
                        if (tile.GetBusy())
                        {
                            if (tile.GetTeam() != teamPieces)
                            {
                                moves.Add(tile.coordinates);
                            }
                        }
                    }
                }

                break;
            case Pieces.bishop:
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(i, i) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy())
                    {
                        if (tile.GetTeam() != teamPieces)
                        {
                            moves.Add(tile.coordinates);
                        }
                        break;
                    }
                    moves.Add(tile.coordinates);
                }
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(-i, i) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy())
                    {
                        if (tile.GetTeam() != teamPieces)
                        {
                            moves.Add(tile.coordinates);
                        }
                        break;
                    }
                    moves.Add(tile.coordinates);
                }
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(i, -i) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy())
                    {
                        if (tile.GetTeam() != teamPieces)
                        {
                            moves.Add(tile.coordinates);
                        }
                        break;
                    }
                    moves.Add(tile.coordinates);
                }
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(-i, -i) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy())
                    {
                        if (tile.GetTeam() != teamPieces)
                        {
                            moves.Add(tile.coordinates);
                        }
                        break;
                    }
                    moves.Add(tile.coordinates);
                }
                break;
            case Pieces.knight:
                TryGetTile(2, 1, piecePos, teamPieces);
                TryGetTile(2, -1, piecePos, teamPieces);
                TryGetTile(1, 2, piecePos, teamPieces);
                TryGetTile(-1, 2, piecePos, teamPieces);
                TryGetTile(-2, 1, piecePos, teamPieces);
                TryGetTile(-2, -1, piecePos, teamPieces);
                TryGetTile(1, -2, piecePos, teamPieces);
                TryGetTile(-1, -2, piecePos, teamPieces);
                break;
            case Pieces.rook:
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(i, 0) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy())
                    {
                        if (tile.GetTeam() != teamPieces)
                        {
                            moves.Add(tile.coordinates);
                        }
                        break;
                    }
                    moves.Add(tile.coordinates);
                }
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(-i, 0) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy())
                    {
                        if (tile.GetTeam() != teamPieces)
                        {
                            moves.Add(tile.coordinates);
                        }
                        break;
                    }
                    moves.Add(tile.coordinates);
                }
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(0, i) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy())
                    {
                        if (tile.GetTeam() != teamPieces)
                        {
                            moves.Add(tile.coordinates);
                        }
                        break;
                    }
                    moves.Add(tile.coordinates);
                }
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(0, -i) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy())
                    {
                        if (tile.GetTeam() != teamPieces)
                        {
                            moves.Add(tile.coordinates);
                        }
                        break;
                    }
                    moves.Add(tile.coordinates);
                }
                break;
            case Pieces.queen:
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(i, i) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy())
                    {
                        if (tile.GetTeam() != teamPieces)
                        {
                            moves.Add(tile.coordinates);
                        }
                        break;
                    }
                    moves.Add(tile.coordinates);
                }
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(-i, i) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy())
                    {
                        if (tile.GetTeam() != teamPieces)
                        {
                            moves.Add(tile.coordinates);
                        }
                        break;
                    }
                    moves.Add(tile.coordinates);
                }
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(i, -i) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy())
                    {
                        if (tile.GetTeam() != teamPieces)
                        {
                            moves.Add(tile.coordinates);
                        }
                        break;
                    }
                    moves.Add(tile.coordinates);
                }
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(-i, -i) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy())
                    {
                        if (tile.GetTeam() != teamPieces)
                        {
                            moves.Add(tile.coordinates);
                        }
                        break;
                    }
                    moves.Add(tile.coordinates);
                }
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(i, 0) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy())
                    {
                        if (tile.GetTeam() != teamPieces)
                        {
                            moves.Add(tile.coordinates);
                        }
                        break;
                    }
                    moves.Add(tile.coordinates);
                }
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(-i, 0) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy())
                    {
                        if (tile.GetTeam() != teamPieces)
                        {
                            moves.Add(tile.coordinates);
                        }
                        break;
                    }
                    moves.Add(tile.coordinates);
                }
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(0, i) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy())
                    {
                        if (tile.GetTeam() != teamPieces)
                        {
                            moves.Add(tile.coordinates);
                        }
                        break;
                    }
                    moves.Add(tile.coordinates);
                }
                for (int i = 1; i < 8; i++)
                {
                    allTiles.TryGetValue(new Vector2(0, -i) + piecePos, out tile);
                    if (tile == null) { break; }
                    if (tile.GetBusy())
                    {
                        if (tile.GetTeam() != teamPieces)
                        {
                            moves.Add(tile.coordinates);
                        }
                        break;
                    }
                    moves.Add(tile.coordinates);
                }
                break;
            case Pieces.king:
                TryGetTile(0, 1, piecePos, teamPieces);
                TryGetTile(1, 1, piecePos, teamPieces);
                TryGetTile(1, 0, piecePos, teamPieces);
                TryGetTile(0, -1, piecePos, teamPieces);
                TryGetTile(-1, 0, piecePos, teamPieces);
                TryGetTile(-1, 1, piecePos, teamPieces);
                TryGetTile(1, -1, piecePos, teamPieces);
                TryGetTile(-1, -1, piecePos, teamPieces);
                break;
            default:
                break;
        }

        for (int i = 0; i < moves.Count; i++)
        {
            allTiles.TryGetValue(moves[i], out tile);
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

    private void TryGetTile(int x, int y, Vector2 piecePos, TeamPieces teamPieces)
    {
        TileControler tile;
        allTiles.TryGetValue(new Vector2(x, y) + piecePos, out tile);
        if (tile != null)
        {
            if (tile.GetBusy())
            {
                if (tile.GetTeam() != teamPieces)
                {
                    moves.Add(tile.coordinates);
                }
            }
            else
            {
                moves.Add(tile.coordinates);
            }
        }
    }

    public void ActivateCollider()
    {
        foreach (var item in allPieces)
        {
            if (item != null)
            {
                item.GetComponent<Collider>().enabled = true;
            }
        }
    }

    public void DeactivateCollider()
    {
        foreach (var item in allPieces)
        {
            if (item != null)
            {
                item.GetComponent<Collider>().enabled = false;
            }
        }
    }
}