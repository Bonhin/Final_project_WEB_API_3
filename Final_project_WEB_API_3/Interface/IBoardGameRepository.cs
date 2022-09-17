using Final_project_WEB_API_3.Dto;
using Final_project_WEB_API_3.Models;

namespace Final_project_WEB_API_3.Interface
{
    public interface IBoardGameRepository
    {
        public List<BoardGames> ReadDatabase();

        public BoardGames Insert(BoardGameDto entity);

        public BoardGames PutBoardGame(BoardGameDto entity, int key);

        public BoardGames PatchBoardGame(PatchBoardGameDto entity, int key);

        public void DeleteBoardGame(int key);

    }
}
