using Final_project_WEB_API_3.Interface;
using Final_project_WEB_API_3.Logs;
using Final_project_WEB_API_3.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Final_project_WEB_API_3.Filters
{
    public class LogsFilter : Attribute, IActionFilter
    {
        private readonly List<int> _statusCodeSucess;
        private readonly IBoardGameRepository _repository;
        private BoardGames beforeBoardGame;

        public LogsFilter(IBoardGameRepository repository)
        {
            _statusCodeSucess = new List<int>() { StatusCodes.Status200OK, StatusCodes.Status201Created };
            _repository = repository;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Method.Equals("put", StringComparison.InvariantCultureIgnoreCase)
                || context.HttpContext.Request.Method.Equals("patch", StringComparison.InvariantCultureIgnoreCase)
                || context.HttpContext.Request.Method.Equals("delete", StringComparison.InvariantCultureIgnoreCase))
            {
                var id = int.Parse(context.HttpContext.Request.Path.ToString().Split("/").Last());
                var database = _repository.ReadDatabase();
                beforeBoardGame = database.Where(x => x.ID == id).FirstOrDefault();
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Request.Method.Equals("put", StringComparison.InvariantCultureIgnoreCase)
                || context.HttpContext.Request.Method.Equals("patch", StringComparison.InvariantCultureIgnoreCase)
                || context.HttpContext.Request.Method.Equals("delete", StringComparison.InvariantCultureIgnoreCase))
            {
                if (_statusCodeSucess.Contains(context.HttpContext.Response.StatusCode))
                {
                    var id = int.Parse(context.HttpContext.Request.Path.ToString().Split("/").Last());

                    if (context.HttpContext.Request.Method.Equals("put", StringComparison.InvariantCultureIgnoreCase)
                       || context.HttpContext.Request.Method.Equals("patch", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var database = _repository.ReadDatabase();
                        BoardGames afterBoardGame = database.Where(x => x.ID == id).FirstOrDefault();

                        if (afterBoardGame != null)
                        {
                            CustomLogs.SaveLog(context.HttpContext.Request.Method, afterBoardGame.ID, afterBoardGame.Name, beforeBoardGame, afterBoardGame);
                        }
                    }
                    else if (context.HttpContext.Request.Method.Equals("delete", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (beforeBoardGame != null)
                        {
                            CustomLogs.SaveLog(context.HttpContext.Request.Method, beforeBoardGame.ID, beforeBoardGame.Name, beforeBoardGame);
                        }
                    }
                }
            }
        }
    }
}