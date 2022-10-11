using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public class GameParamProvider
    {
        Game game;
        public int GetNear()
        {
            return 2;
        }
        public Drawer GetDrawer()
        {
            return game.Drawer;
        }
        public GameParamProvider(Game g)
        {
            game = g;
        }
        public Board GetBoard()
        {
            return game.Board;
        }
        public Character GetMe()
        {
            return game.ActualCharacter;
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
        public void NoExecution(int commandCount)
        {
            game.NoExecution = commandCount;
        }
        public void Execute(int commandCount)
        {
            game.ImmediateExecution = commandCount;
        }
        public void Repeat(int commandCount)
        {
            game.Repeat = commandCount;
        }
        public bool IsFreePlace(Place p)
        {
            return game.IsOccupiedOrOutOfBounds(p);
        }
        public void PlayerCommand()
        {
            game.PlayerCommand();
        }
        public List<Character> GetCharacters()
        {
            return Game.Characters;
        }

        public void SpawnMonster(Monster monster)
        {
            game.SpawnMonster(monster);
        }
    }
}
