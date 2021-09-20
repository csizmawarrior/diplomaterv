﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github
{
    class MonsterAI
    {

        private Random random = new Random();

        public void Step(int roundNum, Player player, List<Monster> monsters)
        {
           foreach(Monster monster in monsters)
            {
                uint XDistance = player.Place.X - monster.Place.X;
                uint YDistance = player.Place.Y - monster.Place.Y;
                if (player.Place.Y < monster.Place.Y)
                    YDistance = monster.Place.Y - player.Place.Y;
                if (player.Place.X < monster.Place.X)
                    XDistance = monster.Place.X - player.Place.X;

                if (monster.Type.ShootRound != null && monster.Type.ShootRound != 0)
                    shootAttempt(XDistance, YDistance, player, monster, roundNum);
                if (monster.Type.MoveRound != null && monster.Type.MoveRound != 0)
                    moveMonster(XDistance, YDistance, player, monster, roundNum);
            }
        }

        private void shootAttempt(uint xDistance, uint yDistance, Player player, Monster monster, int roundNum)
        {
            if(roundNum % monster.Type.ShootRound == 0)
            {
                if (Math.Abs(xDistance) <= Math.Abs(monster.Type.Range) && Math.Abs(yDistance) <= Math.Abs(monster.Type.Range))
                    player.Damage(50);
            }
        }

        private void moveMonster(uint XDistance, uint YDistance, Player player, Monster monster, int roundNum)
        {
            double rand = random.NextDouble();
            if(roundNum % monster.Type.MoveRound == 0)
            {
                if (rand >= 0.5 && Math.Abs(XDistance) <= Math.Abs(monster.Type.Range))
                {
                    if(monster.Place.X < player.Place.X)
                        monster.Place.X = (uint)((int)monster.Place.X + (int)(XDistance * 0.5));
                    else
                        monster.Place.X = (uint)((int)monster.Place.X - (int)(XDistance * 0.5));
                    return;
                }
                if (rand < 0.5 && Math.Abs(YDistance) <= Math.Abs(monster.Type.Range))
                {
                    if(monster.Place.Y < player.Place.Y)
                        monster.Place.Y = (uint)((int)monster.Place.Y + (int)(YDistance * 0.5));
                    else
                        monster.Place.Y = (uint)((int)monster.Place.Y - (int)(YDistance * 0.5));
                    return;
                }
            }
        }
    }
}