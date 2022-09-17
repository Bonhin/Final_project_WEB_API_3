using Final_project_WEB_API_3.Dto;
using Final_project_WEB_API_3.Interface;
using Final_project_WEB_API_3.Models;
using System.Text.Json;

namespace Final_project_WEB_API_3.Repositories
{
    public class BoardGameRepository : IBoardGameRepository
    {
        private readonly string _boardGameDatabaseFile;
        public BoardGameRepository()
        {
            _boardGameDatabaseFile = $"{Environment.CurrentDirectory}\\bgg_dataset_js.json";
        }

        public List<BoardGames> ReadDatabase()
        {
            var deserilizedDb = JsonSerializer.Deserialize<List<BoardGames>>(File.ReadAllText(_boardGameDatabaseFile));
            return deserilizedDb;
        }

        public BoardGames Insert(BoardGameDto entity)
        {
            var file = ReadDatabase();

            var lastId = file.OrderBy(x => x.ID).Last().ID + 1;

            var boardGameToInsert = new BoardGames(iD: lastId,
                entity.Name, entity.YearPublished, entity.MinPlayers,
                entity.MaxPlayers, entity.PlayTime, entity.MinAge,
                entity.UsersRated, entity.RatingAverage, entity.BGGRank,
                entity.ComplexityAverage, entity.OwnedUsers,
                entity.Mechanics, entity.Domains);

            file.Add(boardGameToInsert);

            var serialized = JsonSerializer.Serialize(file);

            File.WriteAllText(_boardGameDatabaseFile, serialized);

            return (boardGameToInsert);
        }

        public BoardGames PutBoardGame(BoardGameDto entity, int id)
        {

            var file = ReadDatabase();

            var oldBoardGame = file.Where(x => x.ID == id).FirstOrDefault();

            file.Remove(oldBoardGame);

            var boardGameToInsert = new BoardGames(iD: id,
                entity.Name, entity.YearPublished, entity.MinPlayers,
                entity.MaxPlayers, entity.PlayTime, entity.MinAge,
                entity.UsersRated, entity.RatingAverage, entity.BGGRank,
                entity.ComplexityAverage, entity.OwnedUsers,
                entity.Mechanics, entity.Domains);

            file.Add(boardGameToInsert);

            var serialized = JsonSerializer.Serialize(file);

            File.WriteAllText(_boardGameDatabaseFile, serialized);

            return (boardGameToInsert);
        }

        public BoardGames PatchBoardGame(PatchBoardGameDto entity, int id)
        {
            var file = ReadDatabase();

            var oldBoardGame = file.Where(x => x.ID == id).FirstOrDefault();

            var newBoardGame = new BoardGames(oldBoardGame.ID,
                oldBoardGame.Name, oldBoardGame.YearPublished, oldBoardGame.MinPlayers,
                oldBoardGame.MaxPlayers, oldBoardGame.PlayTime, oldBoardGame.MinAge,
                entity.UsersRated, oldBoardGame.RatingAverage, oldBoardGame.BGGRank,
                oldBoardGame.ComplexityAverage, entity.OwnedUsers,
                entity.Mechanics, entity.Domains);

            file.Add(newBoardGame);

            file.Remove(oldBoardGame);

            var serialized = JsonSerializer.Serialize(file);
            File.WriteAllText(_boardGameDatabaseFile, serialized);


            return newBoardGame;
        }
        public void DeleteBoardGame(int key)
        {
            var file = ReadDatabase();

            var entity = file.Where(x => x.ID == key).FirstOrDefault();

            file.Remove(entity);

            var serialized = JsonSerializer.Serialize(file);
            File.WriteAllText(_boardGameDatabaseFile, serialized);
        }
    }
}
