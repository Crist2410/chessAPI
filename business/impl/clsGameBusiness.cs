using chessAPI.business.interfaces;
using chessAPI.dataAccess.repositores;
using chessAPI.models.game;

namespace chessAPI.business.impl;

public sealed class clsGameBusiness<TI, TC> : IGameBusiness<TI> 
    where TI : struct, IEquatable<TI>
    where TC : struct
{
    internal readonly IGameRepository<TI, TC> GameRepository;

    public clsGameBusiness(IGameRepository<TI, TC> GameRepository)
    {
        this.GameRepository = GameRepository;
    }

    public async Task<clsGame<TI>> addGame(clsNewGame newGame)
    {
        var x = await GameRepository.addGame(newGame).ConfigureAwait(false);
        return new clsGame<TI>(x, newGame.started, newGame.whites, newGame.blacks, newGame.turn, newGame.winner);
    }

    public async Task<List<clsGame<TI>>> getGames()
    {
        List<clsGame<TI>> games = new List<clsGame<TI>>();
        var x = await GameRepository.getGames().ConfigureAwait(false);
        foreach(var value in x){
            clsGame<TI> player = new clsGame<TI>(value.id,value.started,value.whites,value.blacks,value.turn,value.winner);
            games.Add(player);
        }
        return games;
    }

    public async Task<clsGame<TI>> updateGame(clsUpdateGame updateGame)
    {
        var x = await GameRepository.updateGames(updateGame.id, updateGame.started, updateGame.whites, updateGame.blacks, updateGame.turn, updateGame.winner).ConfigureAwait(false);
        return new clsGame<TI>(x, updateGame.started, updateGame.whites, updateGame.blacks, updateGame.turn, updateGame.winner);
    }
}