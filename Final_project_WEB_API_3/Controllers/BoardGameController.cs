using Final_project_WEB_API_3.Dto;
using Final_project_WEB_API_3.Filters;
using Final_project_WEB_API_3.Interface;
using Final_project_WEB_API_3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final_project_WEB_API_3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BoardGameController : ControllerBase
    {
        private readonly ILogger<BoardGameController> _logger;
        // private readonly List<BoardGames> _database;
        private readonly IBoardGameRepository _boardGameRepository;

        public BoardGameController(ILogger<BoardGameController> logger, IBoardGameRepository repository)
        {
            _logger = logger;
            // _database = new List<BoardGames>();
            _boardGameRepository = repository;
        }

        [HttpPost("Filter")]
        [ProducesResponseType(typeof(List<BoardGames>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult FilterByBody([FromHeader] int page, [FromHeader] int pageSize, [FromBody] BoardGameFilterDto entity)
        {
            var boardGameDatabase = _boardGameRepository.ReadDatabase();
            var searchedBoardGames = boardGameDatabase
                .Where(x => x.MinPlayers >= entity.MinPlayers
                && x.MinAge >= entity.MinAge
                && x.BGGRank <= entity.BGGRank);

            if (!searchedBoardGames.Any())
            {
                return NotFound("Nenhum Board Game encontrado");
            }

            var boardGameList = searchedBoardGames
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(x => x.BGGRank)
                .ToList();

            return Ok(boardGameList);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<BoardGames>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Get([FromHeader] int page, [FromHeader] int pageSize)
        {
            var boardGameDatabase = _boardGameRepository.ReadDatabase();
            var boardGameList = boardGameDatabase
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(x => x.BGGRank)
                .ToList();

            if (!boardGameList.Any())
            {
                return BadRequest("Número de página solicitada maior que o existente");
            }
            return Ok(boardGameList);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BoardGames), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var boardGameDatabase = _boardGameRepository.ReadDatabase();

            var searchedBoardGames = boardGameDatabase.Where(x => x.ID == id);

            if (!searchedBoardGames.Any())
            {
                return NotFound("ID inexistente");
            }
            return Ok(searchedBoardGames);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BoardGames), StatusCodes.Status201Created)]
        public IActionResult CreateBoardGame([FromBody] BoardGameDto entity)
        {
            var boardGameCreated = _boardGameRepository.Insert(entity);
            return Created(string.Empty, boardGameCreated);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BoardGames), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult Update([FromBody] BoardGameDto entity, int id)
        {
            var boardGameDatabase = _boardGameRepository.ReadDatabase();

            var searchedBoardGames = boardGameDatabase.Where(x => x.ID == id);

            if (!searchedBoardGames.Any())
            {
                return NotFound("Id inexistente");
            }
            var boardGameUpdated = _boardGameRepository.PutBoardGame(entity, id);
            return Ok(boardGameUpdated);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(BoardGames), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult ParcialUpdate([FromBody] PatchBoardGameDto entity, int id)
        {
            var boardGameDatabase = _boardGameRepository.ReadDatabase();

            var searchedBoardGames = boardGameDatabase.Where(x => x.ID == id);

            if (!searchedBoardGames.Any())
            {
                return NotFound("Id inexistente");
            }
            return Ok(_boardGameRepository.PatchBoardGame(entity, id));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]

        public IActionResult Delete(int id)
        {
            var boardGameDatabase = _boardGameRepository.ReadDatabase();

            var searchedBoardGames = boardGameDatabase.Where(x => x.ID == id);

            if (!searchedBoardGames.Any())
            {
                return NotFound("Id inexistente");
            }
            _boardGameRepository.DeleteBoardGame(id);
            return Ok("Board Game deletado");
        }
    }
}