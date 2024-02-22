
public class PlayerGold : CommaText
{
    private PlayerSO _playerData;

    private GameStateManager _gameStateManager;
    private void Awake()
    {
        _playerData = GameManager.Instance.Player.GetComponent<Player>().Data;
        _playerData.PlayerData.OnGoldUI += base.ChangeGold;
    }
    private void Start()
    {
        _gameStateManager = GameManager.Instance.GameStateManager;

        if (_gameStateManager.CurrentGameState == GameState.LOADGAME)
        {
            int loadPlayerGold = GameManager.Instance.JsonReaderManager.LoadedPlayerData.SavePlayerGold;
            base.ChangeGold(loadPlayerGold);

            return;
        }

        base.ChangeGold(_playerData.PlayerData.PlayerGold);
    }
}
