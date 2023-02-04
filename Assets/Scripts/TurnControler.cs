using UnityEngine;

public class TurnControler : MonoBehaviour
{
    private TeamPieces _teamPieces;
    public TeamPieces TeamPieces { get { return _teamPieces; } private set { } }

    public static TurnControler Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _teamPieces = TeamPieces.white;
    }

    public void ChangeTeam()
    {
        if (_teamPieces == TeamPieces.white)
        {
            _teamPieces = TeamPieces.black;
        }
        else
        {
            _teamPieces = TeamPieces.white;
        }
    }
}
