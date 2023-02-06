using chessAPI.dataAccess.common;
using chessAPI.dataAccess.interfaces;
using chessAPI.dataAccess.models;
using chessAPI.models.game;
using Dapper;

namespace chessAPI.dataAccess.repositores;

public sealed class clsGameRepository<TI, TC> : clsDataAccess<clsGameEntityModel<TI, TC>, TI, TC>, IGameRepository<TI, TC>
        where TI : struct, IEquatable<TI>
        where TC : struct
{
    public clsGameRepository(IRelationalContext<TC> rkm,
                               ISQLData queries,
                               ILogger<clsGameRepository<TI, TC>> logger) : base(rkm, queries, logger)
    {
    }

    // public async Task<IEnumerable<clsNewGame>> getGames()
    // {
    //     List<clsNewGame> Games = new List<clsNewGame>();
    //     var p = new DynamicParameters();
    //     return Games;
    // }

    public async Task<TI> addGame(clsNewGame Game)
    {
        var p = new DynamicParameters();
        p.Add("@STARTED", Game.started);
        p.Add("@WHITES", Game.whites);
        p.Add("@BLACKS", Game.blacks);
        p.Add("@TURN", Game.turn);
        p.Add("@WINNER", Game.winner);
        return await add<TI>(p).ConfigureAwait(false);
    }

    public async Task<IEnumerable<clsGameEntityModel<TI, TC>>> addGames(IEnumerable<clsNewGame> Games)
    {
        var r = new List<clsGameEntityModel<TI, TC>>(Games.Count());
        foreach (var Game in Games)
        {
            TI GameId = await addGame(Game).ConfigureAwait(false);
            r.Add(new clsGameEntityModel<TI, TC>() { id = GameId, started = Game.started, whites = Game.whites, blacks = Game.blacks, turn = Game.turn, winner = Game.winner });
        }
        return r;
    }

    public Task deleteGame(TI id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<clsGameEntityModel<TI, TC>>> getGamesByGame(TI gameId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<clsGameEntityModel<TI, TC>>> getGames()
    {
        var p = new DynamicParameters();
        return await getALL(p).ConfigureAwait(false);
    }

    public Task updateGame(clsGame<TI> updatedGame)
    {
        throw new NotImplementedException();
    }

    protected override DynamicParameters fieldsAsParams(clsGameEntityModel<TI, TC> entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        var p = new DynamicParameters();
        p.Add("ID", entity.id);
        p.Add("@STARTED", entity.started);
        p.Add("@WHITES", entity.whites);
        p.Add("@BLACKS", entity.blacks);
        p.Add("@TURN", entity.turn);
        p.Add("@WINNER", entity.winner);
        return p;
    }

    protected override DynamicParameters keyAsParams(TI key)
    {
        var p = new DynamicParameters();
        p.Add("ID", key);
        return p;
    }

    public async Task<TI> updateGames(int id, DateTime started, int whites, int blacks, bool turn, int winner)
    {
        var p = new DynamicParameters();
        p.Add("ID", id);
        p.Add("STARTED", started);
        p.Add("WHITES", whites);
        p.Add("BLACKS", blacks);
        p.Add("TURN", turn);
        p.Add("WINNER", winner);
        return await setRow<TI>(p).ConfigureAwait(false); 
    }
}