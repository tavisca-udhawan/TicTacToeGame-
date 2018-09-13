using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe
{
    [ExceptionHandler]
    public class UserAuthorizeAttribute : ResultFilterAttribute, IActionFilter
    {
        static int players = 0;
        public static bool player1 = false;
        public static bool player2 = false;
        public static string player1Tokken = "";
        public static string player2Tokken = "";
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string apiKey = context.HttpContext.Request.Headers["apikey"].ToString();
            Repository key = new Repository();
            int IsKeyExists = key.KeyCheck(apiKey);
            if (IsKeyExists == 1)
            {
                if (players < 2 && !apiKey.Equals(player1Tokken))
                {
                    players++;
                }
                else if (!apiKey.Equals(player1Tokken) && !apiKey.Equals(player2Tokken) && player1 == false && player2 == false)
                {
                    throw new Exception("Game Over");
                }
                else if (!apiKey.Equals(player1Tokken) && !apiKey.Equals(player2Tokken))
                {
                    throw new Exception("No more than 2 players can play game");
                }
                if (players == 1)
                {
                    player1Tokken = apiKey;
                }
                else if (!apiKey.Equals(player1Tokken))
                {
                    player2Tokken = apiKey;
                }
                if (apiKey == player1Tokken)
                {
                    player1 = true;
                    player2 = false;
                }
                else if (apiKey == player2Tokken)
                {
                    player2 = true;
                    player1 = false;
                }
            }
            else if (IsKeyExists == 2)
            {
                throw new UnauthorizedAccessException("Api Key not passed");
            }
            else
            {
                if (IsKeyExists == 0)
                    throw new UnauthorizedAccessException("Invalid Api Key passed");
            }
        }
    }
}
