namespace chessAPI.models.player;

public sealed class clsUpdatePlayer
{
    public clsUpdatePlayer()
    {
        id = 0;
        email = "";
    }

    public int id { get; set; }
    public string email { get; set; }
}