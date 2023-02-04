using UnityEngine;

public class TileControler : MonoBehaviour
{
    public Vector2 coordinates;
    private Color _mouseColor = Color.grey;
    private Color _originalColor;
    private MeshRenderer _meshRenderer;
    private BoardControler _boardControler;
    private ControlPieces _myPiece;
    public bool isBlocked;
    public bool isBusy;
    private ControlPieces _undoPiece;

    void Start()
    {
        isBlocked = true;
        _meshRenderer = GetComponent<MeshRenderer>();
        _originalColor = _meshRenderer.material.color;
        FindParent(transform.parent.transform);
    }

    private void FindParent(Transform parent)
    {
        if (parent == null)
        {
            return; //daca nu mai exista parinte se opreste
        }
        _boardControler = parent.GetComponent<BoardControler>();
        if (_boardControler == null)
        {
            FindParent(parent.parent.transform); //se apeleaza la infinit
        }
    }

    private void OnMouseOver()
    {
        if (_boardControler != null)
        {
            _boardControler.AlocateColorTile(this);
        }
    }

    public void NoHighlightTiles()
    {
        _meshRenderer.material.color = _originalColor;
        isBlocked = true;
    }

    public void HighlightTiles()
    {
        _meshRenderer.material.color = _mouseColor;
        isBlocked = false;
    }

    public void DoBusy(ControlPieces pieces)
    {
        if (_myPiece != null)
        {
            _undoPiece = _myPiece;
            _undoPiece.gameObject.SetActive(false);
        }
        _myPiece = pieces;
        isBusy = true;
    }

    public void DoUnBusy()
    {
        _myPiece = null;
        isBusy = false;
    }

    public bool GetBusy() => isBusy;

    public TeamPieces GetTeam() => _myPiece.GetTeam();

    public void Undo()
    {
        if (_undoPiece != null)
        {
            _undoPiece.gameObject.SetActive(true);
            _myPiece = _undoPiece;
        }
        isBusy = true;
        if (_undoPiece == null)
        {
            _myPiece = null;
            isBusy = false;
        }
    }

    public ControlPieces GetMyPiece() => _myPiece;
}