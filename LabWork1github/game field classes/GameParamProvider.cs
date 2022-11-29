using LabWork1github.static_constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    public class GameParamProvider
    {
        readonly Game game;
        public int GetNear()
        {
            return StaticStartValues.STARTER_NEAR;
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
            return game.GetClosestMonster();
        }
        public Character GetPartner()
        {
            return GetMe().Partner;
        }
        public Trap GetTrap()
        {
            return game.GetClosestTrap();
        }
        public bool IsFreePlace(Place p)
        {
            return !game.IsOccupiedOrOutOfBounds(p);
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
        public void SetActualCharacter(Character character)
        {
            game.ActualCharacter = character;
        }
    }
}
