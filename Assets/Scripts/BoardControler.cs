using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BoardControler : MonoBehaviour
{
    [SerializeField] private Image image;

    private Dictionary<Vector2, TileControler> allTiles = new Dictionary<Vector2, TileControler>();
    private TileControler selectedTiles;
    private List<Vector2> moves = new List<Vector2>();
    [SerializeField] private List<ControlPieces> allPieces = new List<ControlPieces>();
    private TeamPieces _teamChess = TeamPieces.none;

    private void Awake()
    {
        var tiles = gameObject.GetComponentsInChildren<TileControler>();

        for (int i = 0; i < tiles.Length; i++)
        {
            allTiles.Add(tiles[i].coordinates, tiles[i]);
        }
    }

    public bool HighlightMoves(Vector2 piecePos, Pieces pieces, TeamPieces teamPieces, bool checkForChess = false)
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

        if (checkForChess)
        {
            for (int i = 0; i < moves.Count; i++)
            {
                allTiles.TryGetValue(moves[i], out tile);
                if (tile.GetBusy())
                {
                    if (tile.GetMyPiece().GetPieces() == Pieces.king)
                    {
                        if (teamPieces == TeamPieces.white)
                        {
                            _teamChess = TeamPieces.black;
                            ChangeColor(Color.black);
                            return true;
                        }
                        else if (teamPieces == TeamPieces.black)
                        {
                            _teamChess = TeamPieces.white;
                            ChangeColor(Color.white);
                            return true;
                        }
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < moves.Count; i++)
            {
                allTiles.TryGetValue(moves[i], out tile);
                if (tile != null)
                {
                    tile.HighlightTiles();
                }
            }
        }
        return false;
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

    public void CheckChess()
    {
        foreach (var item in allPieces)
        {
            if (item.GetTeam() != TeamPieces.none)
            {
                if (HighlightMoves(item.GetTileCoord(), item.GetPieces(), item.GetTeam(), true))
                {
                    return;
                }
            }
        }
        _teamChess = TeamPieces.none;
        ChangeColor(Color.red);
    }

    public TeamPieces GetTeamChess() => _teamChess;

    public void AddPieces(ControlPieces pieces)
    {
        allPieces.Add(pieces);
    }

    public void RemovePieces(ControlPieces pieces)
    {
        if (allPieces.Contains(pieces))
        {
            allPieces.Remove(pieces);
        }
    }

    public void RemoveChess()
    {
        _teamChess = TeamPieces.none;
        ChangeColor(Color.red);
    }

    private void ChangeColor(Color color)
    {
        image.color = color;
    }
}