using chessAPI.dataAccess.models;
using chessAPI.models.game;

namespace chessAPI.dataAccess.repositores;

public interface IGameRepository<TI, TC>
        where TI : struct, IEquatable<TI>
        where TC : struct
{
    Task<TI> addGame(clsNewGame Game);
    Task<IEnumerable<clsGameEntityModel<TI, TC>>> addGames(IEnumerable<clsNewGame> Games);
    Task<IEnumerable<clsGameEntityModel<TI, TC>>> getGames();
    Task<TI> updateGames(int id, DateTime started, int whites, int blacks, bool turn, int winner);
    Task<IEnumerable<clsGameEntityModel<TI, TC>>> getGamesByGame(TI gameId);
    Task updateGame(clsGame<TI> updatedGame);
    Task deleteGame(TI id);
}