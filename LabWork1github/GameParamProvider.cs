using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    class GameParamProvider
    {
        Game game;
        public GameParamProvider(Game g)
        {
            game = g;
        }
        public Board GetBoard()
        {
            return game.Board;
        }
        public Player GetPlayer()
        {
            return game.Player;
        }
        public List<Monster> GetMonsters()
        {
            return game.Monsters;
        }
        public List<Trap> GetTraps()
        {
            return game.Traps;
        }
        public int GetRound()
        {
            return game.Round;
        }
        public Monster GetMonster()
        {
            return game.ActualMonster;
        }
        public Trap GetTrap()
        {
            return game.ActualTrap;
        }
    }
}
