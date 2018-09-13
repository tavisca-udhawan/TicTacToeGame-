using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicTacToe.Controllers
{
    [Route("api/game")]
    [Logging]
    [ExceptionHandler]
    public class GamesController : Controller
    {
        static int[] board = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        static int count = 0;
        static bool player1Status = true;
        static bool player2Status = true;
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Welcome to Tic Tac Toe Game" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "Invalid Request";
        }

        // POST api/values
        [HttpPost]

        [UserAuthorize]
        public ActionResult Post([FromBody]int position)
        {
            return makeMove(position);
        }
        public ActionResult makeMove(int position)
        {
            if (position >= 1 && position <= 9)
            {
                if (UserAuthorizeAttribute.player1 && count < 9)
                {
                    if (player1Status)
                    {
                        if (board[position - 1] == position)
                        {
                            player1Status = false;
                            player2Status = true;
                            board[position - 1] = 'O';
                            count++;
                            if (GetStatus() == 1)
                            {
                                UserAuthorizeAttribute.player1 = false;
                                UserAuthorizeAttribute.player2 = false;
                                UserAuthorizeAttribute.player1Tokken = "";
                                UserAuthorizeAttribute.player2Tokken = "";

                                return Ok("Player1 wins the game");
                            }

                            return Ok("Added to board");

                        }
                        else
                            return BadRequest("Position is not empty");
                    }
                    else
                    {
                        return BadRequest("Player2 Turn");
                    }
                }
                else if (UserAuthorizeAttribute.player2 && count < 9)
                {
                    if (player2Status)
                    {
                        if (board[position - 1] == position)
                        {
                            player1Status = true;
                            player2Status = false;
                            board[position - 1] = 'X';
                            count++;
                            if (GetStatus() == 1)
                            {
                                UserAuthorizeAttribute.player1 = false;
                                UserAuthorizeAttribute.player2 = false;
                                UserAuthorizeAttribute.player1Tokken = "";
                                UserAuthorizeAttribute.player2Tokken = "";

                                return Ok("Player2 wins the game");
                            }

                            return Ok("Added to board");

                        }
                        else
                            return BadRequest("Position is not empty");
                    }
                    else
                    {
                        return BadRequest("Player1 Turn");
                    }
                }
                else if (GetStatus() == 0 && count == 9)
                {
                    UserAuthorizeAttribute.player1 = false;
                    UserAuthorizeAttribute.player2 = false;
                    UserAuthorizeAttribute.player1Tokken = "";
                    UserAuthorizeAttribute.player2Tokken = "";
                    return Ok("Draw");
                }
                else if (GetStatus() == 0 && count < 9)
                {
                    UserAuthorizeAttribute.player1 = false;
                    UserAuthorizeAttribute.player2 = false;
                    UserAuthorizeAttribute.player1Tokken = "";
                    UserAuthorizeAttribute.player2Tokken = "";
                    return Ok("In Progress");
                }
                return Ok("Next Turn");
            }
            else
            {
                return BadRequest("You entered invalid position");
            }


        }

        public int GetStatus()
        {
            if (board[0] == board[1] && board[0] == board[2])
                return 1;
            else if (board[3] == board[4] && board[3] == board[5])
                return 1;
            else if (board[6] == board[7] && board[6] == board[8])
                return 1;
            else if (board[0] == board[3] && board[0] == board[6])
                return 1;
            else if (board[1] == board[4] && board[1] == board[7])
                return 1;
            else if (board[2] == board[5] && board[2] == board[8])
                return 1;
            else if (board[0] == board[4] && board[0] == board[8])
                return 1;
            else if (board[2] == board[4] && board[2] == board[6])
                return 1;
            else
                return 0;
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
