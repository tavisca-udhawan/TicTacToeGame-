using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class UserAuthorizeAttribute : ResultFilterAttribute, IActionFilter
    {
        static int players = 0;
        string player1Tokken = "";
        string player2Tokken = "";
        public void OnActionExecuted(ActionExecutedContext context)
        {
           // throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string apiKey = context.HttpContext.Request.Headers["apikey"].ToString();
            Repository key = new Repository();
            int IsKeyExists=key.KeyCheck(apiKey);
            if (IsKeyExists == 1)
            {
                if (players < 2)
                {
                    players++;
                }
                else if(!apiKey.Equals(player1Tokken) && !apiKey.Equals(player2Tokken))
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
            }
            else if (IsKeyExists==2)
            {
                throw new UnauthorizedAccessException("Api Key not passed");
            }
            else
            {
                if (IsKeyExists==0)
                    throw new UnauthorizedAccessException("Invalid Api Key passed");
            }
        }
    }
}
