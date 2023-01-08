using UnityEditor.Search;
using UnityEngine;
using UnityEngine.VFX;

public class ControlPieces : MonoBehaviour
{
    private Vector3 offset;
    [SerializeField] private Pieces _pieces;
    [SerializeField] private TeamPieces _teamPieces;
    [SerializeField] BoardControler boardControler;
    [SerializeField] private Vector2 myTileCoord;
    [SerializeField] private GameObject _whiteQueen;
    [SerializeField] private GameObject _darkQueen;
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
        boardControler.DeactivateCollider();
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
        boardControler.ActivateCollider();
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

        if (_pieces == Pieces.pawn)
        {
            if (_teamPieces == TeamPieces.white)
            {
                if (myTileCoord.y == 7)
                {
                    var queenIns = Instantiate(_whiteQueen, transform.position, Quaternion.identity);
                    queenIns.GetComponent<ControlPieces>().myTileCoord = myTileCoord;
                    Destroy(gameObject);
                }
            }
            else if (_teamPieces == TeamPieces.black)
            {
                if (myTileCoord.y == 0)
                {
                    var queenIns = Instantiate(_darkQueen, transform.position, Quaternion.identity);
                    queenIns.GetComponent<ControlPieces>().myTileCoord = myTileCoord;
                    Destroy(gameObject);
                }
            }
        }
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

    public TeamPieces GetTeam()
    {
        return _teamPieces;
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

public enum TeamPieces
{
    none,
    white,
    black
}