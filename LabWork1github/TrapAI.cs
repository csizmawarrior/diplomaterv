using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    class TrapAI
    {
        public TrapAI()
        {
        }

        public bool Spawning = false;
        public Place spawnPoint;
        private Random random = new Random();

        public void Step(int roundNum, Player player, List<Trap> traps)
        {
            Spawning = false;
            foreach(Trap trap in traps)
            {
                AffectPlayer(player, trap);
                if(trap.Type.MoveRound != null && trap.Type.MoveRound != 0)
                {
                    
                }
            }
        }

        private void moveTrap(Trap trap, Player player, double rand)
        {
            int XDistance = player.Place.X - trap.Place.X;
            int YDistance = player.Place.Y - trap.Place.Y;
            if (player.Place.Y < trap.Place.Y)
                YDistance = trap.Place.Y - player.Place.Y;
            if (player.Place.X < trap.Place.X)
                XDistance = trap.Place.X - player.Place.X;
            if (XDistance == 0 && YDistance == 0)
                return;
           

        }

        private void AffectPlayer(Player player, Trap trap)
        {
            if (trap.Place.DirectionTo(player.Place) == "collision")
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
                       // player.Teleport(trap.Type.EffectPlace);
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
