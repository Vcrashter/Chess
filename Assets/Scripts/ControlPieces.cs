using UnityEngine;

public class ControlPieces : MonoBehaviour
{
    private Vector3 offset;
    [SerializeField] private Pieces _pieces;
    [SerializeField] private TeamPieces _teamPieces;
    [SerializeField] BoardControler boardControler;
    [SerializeField] private Vector2 myTileCoord;
    [SerializeField] private ControlPieces _whiteQueen;
    [SerializeField] private ControlPieces _darkQueen;
    private Vector3 intTile;
    private Vector2 _undoTilePos;
    private TileControler _myTile;
    private TileControler _undoTile;

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
        if (_teamPieces != TurnControler.Instance.TeamPieces)
        {
            return;
        }
        Movement();
        offset = transform.position - MouseWorldPosition();
        boardControler.DeactivateCollider();
        intTile = transform.position;
        _undoTilePos = _myTile.coordinates;
        _undoTile = _myTile;
    }

    private void OnMouseDrag()
    {
        if (_teamPieces != TurnControler.Instance.TeamPieces)
        {
            return;
        }
        Vector3 intPos = MouseWorldPosition() + offset;
        Vector3 newPos = new Vector3(intPos.x, 15f, intPos.z);
        transform.position = newPos;
    }

    private void OnMouseUp()
    {
        if (_teamPieces != TurnControler.Instance.TeamPieces)
        {
            return;
        }
        var selectedTile = boardControler.GetSelectedTile();
        boardControler.ActivateCollider();
        if (!selectedTile.GetComponent<TileControler>().isBlocked)
        {
            _myTile.DoUnBusy();
            _myTile = selectedTile.GetComponent<TileControler>();
            _myTile.DoBusy(this);
            gameObject.transform.position = selectedTile.position;
            myTileCoord = selectedTile.GetComponent<TileControler>().coordinates;
            boardControler.CheckChess();
            if (boardControler.GetTeamChess() == TurnControler.Instance.TeamPieces)
            {
                Undo();
            }
            else
            {
                boardControler.CheckChess();

                if (boardControler.GetTeamChess() == TurnControler.Instance.TeamPieces)
                {
                    boardControler.RemoveChess();
                    Undo();
                }
                else
                {
                    TurnControler.Instance.ChangeTeam();
                }
            }
        }
        else
        {
            gameObject.transform.position = intTile;
        }

        boardControler.UnHighlightMove();

        if (_pieces == Pieces.pawn)
        {
            if (_teamPieces == TeamPieces.white)
            {
                if (myTileCoord.y == 7)
                {
                    var queenIns = Instantiate(_whiteQueen, transform.position, Quaternion.identity);
                    queenIns.GetComponent<ControlPieces>().myTileCoord = myTileCoord;
                    boardControler.RemovePieces(this);
                    Destroy(gameObject);
                    boardControler.AddPieces(queenIns);
                    queenIns.gameObject.SetActive(true);
                }
            }
            else if (_teamPieces == TeamPieces.black)
            {
                if (myTileCoord.y == 0)
                {
                    var queenIns = Instantiate(_darkQueen, transform.position, Quaternion.identity);
                    queenIns.GetComponent<ControlPieces>().myTileCoord = myTileCoord;
                    boardControler.RemovePieces(this);
                    Destroy(gameObject);
                    boardControler.AddPieces(queenIns);
                    queenIns.gameObject.SetActive(true);
                }
            }
        }
    }
    private void Undo()
    {
        _myTile.Undo();
        gameObject.transform.position = intTile;
        myTileCoord = _undoTilePos;
        _myTile = _undoTile;
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    private void Movement()
    {
        boardControler.HighlightMoves(myTileCoord, _pieces, _teamPieces);
    }
    public TeamPieces GetTeam() => _teamPieces;

    public Vector2 GetTileCoord() => myTileCoord;

    public Pieces GetPieces() => _pieces;
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

public enum TeamPieces
{
    none,
    white,
    black
}