using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    class TrapAI
    {
        public TrapAI(List<Trap> trapList)
        {
            traps = trapList;
        }

        private List<Trap> traps = new List<Trap>();
        public bool Spawning = false;
        public Place spawnPoint;
        private Random random = new Random();

        public void Step(int roundNum, Player player)
        {
            foreach(Trap trap in traps)
            {
                AffectPlayer(player, trap);
                if(trap.Type.MoveRound != null && trap.Type.Range != null && roundNum % trap.Type.MoveRound == 0)
                {
                    moveTrap(trap, player, random.NextDouble());
                }
            }
        }

        private void moveTrap(Trap trap, Player player, double rand)
        {
            uint XDistance = player.Place.X - trap.Place.X;
            uint YDistance = player.Place.Y - trap.Place.Y;
            if (XDistance == 0 && YDistance == 0)
                return;
            if(rand >= 0.5 && Math.Abs(XDistance) <= Math.Abs(trap.Type.Range))
            {
                trap.Place.X = (uint)((int)trap.Place.X + (int)(XDistance * 0.5));
                return;
            }
            if (rand < 0.5 && Math.Abs(YDistance) <= Math.Abs(trap.Type.Range))
            {
                trap.Place.Y = (uint)((int)trap.Place.Y + (int)(YDistance * 0.5));
                return;
            }

        }

        private void AffectPlayer(Player player, Trap trap)
        {
            if (player.Place.X == trap.Place.X && player.Place.Y == trap.Place.Y)
            {
                switch (trap.Type.EffectType)
                {
                    case TrapEffect.Damage:
                        player.Damage(trap.Type.EffectNumber);
                        break;
                    case TrapEffect.Heal:
                        player.Heal(trap.Type.EffectNumber);
                        break;
                    case TrapEffect.Teleport:
                        player.Teleport(trap.Type.EffectPlace);
                        break;
                    case TrapEffect.Spawner:
                        Spawning = true;
                        spawnPoint = trap.Type.EffectPlace;
                        break;
                }
            }
        }
    }
}
