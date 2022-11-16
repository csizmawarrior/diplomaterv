using LabWork1github.Commands;
using LabWork1github.EventHandling;
using LabWork1github.static_constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.Visitors
{
    public static class DynamicEnemyGrammarVisitorDelegates
    {
        public static void Spawn(GameParamProvider provider, SpawnCommand command)
        {
            if (!(provider.GetMe() is Trap))
                return;
            TriggerEvent spawnEvent = new TriggerEvent
            {
                SourceCharacter = CharacterOptions.Trap,
                TargetCharacterOption = CharacterOptions.Monster
            };

            if (!provider.IsFreePlace(command.TargetPlace))
                return;
            if ((command.TargetCharacterType == null || Program.GetCharacterType(command.TargetCharacterType.Name) == null)
                && provider.GetMe().GetCharacterType().SpawnType == null)
            {
                return;
            }
            if ((command.TargetCharacterType == null || (!(Program.GetCharacterType(command.TargetCharacterType.Name) is MonsterType)))
                && provider.GetMe().GetCharacterType().SpawnType == null)
                return;
            if (provider.GetMe().GetCharacterType().SpawnType == null)
                command.TargetCharacterType = Program.GetCharacterType(command.TargetCharacterType.Name);
            else
            {
                if (!(Program.GetCharacterType(provider.GetMe().GetCharacterType().SpawnType.Name) is MonsterType))
                    return;
                else
                    command.TargetCharacterType = provider.GetMe().GetCharacterType().SpawnType;
            }

            Monster newMonster = new Monster(command.TargetCharacterType.Health, (MonsterType)command.TargetCharacterType, command.TargetPlace);
            spawnEvent.TargetPlace = command.TargetPlace;

            provider.SpawnMonster(newMonster);
            spawnEvent.TargetCharacter = newMonster;
            EventCollection.InvokeTrapSpawned(provider.GetMe(), spawnEvent);
        }

        public static void SpawnRandom(GameParamProvider provider, SpawnCommand command)
        {
            Random rand = new Random();
            int XPos = (int)(rand.Next() % provider.GetBoard().Height);
            int YPos = (int)(rand.Next() % provider.GetBoard().Width);
            command.TargetPlace = new Place(XPos, YPos);

            bool found = false;
            while (!found)
            {
                int index = (int)(rand.Next() % Program.CharacterTypes.Count);
                if (Program.CharacterTypes.ElementAt(index) is MonsterType)
                {
                    command.TargetCharacterType = Program.CharacterTypes.ElementAt(index);
                    found = true;
                }
            }
            Spawn(provider, command);
        }

        public static void TeleportTrap(GameParamProvider provider, TeleportCommand command)
        {
            if (!(provider.GetMe() is Trap))
                return;
            TriggerEvent teleportEvent = new TriggerEvent
            {
                EventType = EventType.Teleport,
                SourceCharacter = CharacterOptions.Trap,
                TargetCharacterOption = CharacterOptions.Trap,
                SourcePlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y)
            };
            foreach (Trap trap in provider.GetTraps())
            {
                if (trap.Place.DirectionTo(provider.GetMe().Place).Equals(Directions.COLLISION) && !trap.Equals(provider.GetMe()))
                {
                    if (command.Random)
                    {
                        Random rand = new Random();
                        int XPos = (int)(rand.Next() % provider.GetBoard().Height);
                        int YPos = (int)(rand.Next() % provider.GetBoard().Width);
                        command.TargetPlace = new Place(XPos, YPos);
                    }
                    if (provider.IsFreePlace(command.TargetPlace))
                    {
                        teleportEvent.TargetPlace = command.TargetPlace;
                        trap.Place = command.TargetPlace;
                        teleportEvent.TargetCharacter = trap;
                        EventCollection.InvokeTrapTeleported(provider.GetMe(), teleportEvent);
                    }
                }
            }
        }
        public static void TeleportMonster(GameParamProvider provider, TeleportCommand command)
        {
            if (!(provider.GetMe() is Trap))
                return;
            TriggerEvent teleportEvent = new TriggerEvent
            {
                EventType = EventType.Teleport,
                SourceCharacter = CharacterOptions.Trap,
                TargetCharacterOption = CharacterOptions.Monster,
                SourcePlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y)
            };
            foreach (Monster monster in provider.GetMonsters())
            {
                if (monster.Place.DirectionTo(provider.GetMe().Place).Equals(Directions.COLLISION))
                {
                    if (command.Random)
                    {
                        Random rand = new Random();
                        int XPos = (int)(rand.Next() % provider.GetBoard().Height);
                        int YPos = (int)(rand.Next() % provider.GetBoard().Width);
                        command.TargetPlace = new Place(XPos, YPos);
                    }
                    if (provider.IsFreePlace(command.TargetPlace))
                    {
                        teleportEvent.TargetPlace = command.TargetPlace;
                        monster.Place = command.TargetPlace;
                        teleportEvent.TargetCharacter = monster;
                        EventCollection.InvokeTrapTeleported(provider.GetMe(), teleportEvent);
                    }
                }
            }
        }
        public static void TeleportPartner(GameParamProvider provider, TeleportCommand command)
        {
            if (! (provider.GetMe() is Trap))
                return;
            TriggerEvent teleportEvent = new TriggerEvent
            {
                EventType = EventType.Teleport,
                SourceCharacter = CharacterOptions.Trap,
                SourcePlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y)
            };
            if (provider.GetPartner() == null)
                return;
            if (provider.GetPartner() is Monster)
                teleportEvent.TargetCharacterOption = CharacterOptions.Monster;
            if (provider.GetPartner() is Trap)
                teleportEvent.TargetCharacterOption = CharacterOptions.Trap;
            if (provider.GetPartner().Place.DirectionTo(provider.GetMe().Place).Equals(Directions.COLLISION))
            {
                if (command.Random)
                {
                    Random rand = new Random();
                    int XPos = (int)(rand.Next() % provider.GetBoard().Height);
                    int YPos = (int)(rand.Next() % provider.GetBoard().Width);
                    command.TargetPlace = new Place(XPos, YPos);
                }
                if (provider.IsFreePlace(command.TargetPlace))
                {
                    teleportEvent.TargetPlace = command.TargetPlace;
                    provider.GetPartner().Place = command.TargetPlace;
                    EventCollection.InvokeTrapTeleported(provider.GetMe(), teleportEvent);
                }
            }
        }
        public static void TeleportPlayer(GameParamProvider provider, TeleportCommand command)
        {
            if (!(provider.GetMe() is Trap))
                return;
            TriggerEvent teleportEvent = new TriggerEvent
            {
                EventType = EventType.Teleport,
                SourceCharacter = CharacterOptions.Trap,
                TargetCharacterOption = CharacterOptions.Player,
                SourcePlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y)
            };
            if (provider.GetPlayer().Place.DirectionTo(provider.GetMe().Place).Equals(Directions.COLLISION))
            {
                if (command.Random)
                {
                    Random rand = new Random();
                    int XPos = (int)(rand.Next() % provider.GetBoard().Height);
                    int YPos = (int)(rand.Next() % provider.GetBoard().Width);
                    command.TargetPlace = new Place(XPos, YPos);
                }
                if (provider.IsFreePlace(command.TargetPlace))
                {
                    teleportEvent.TargetPlace = command.TargetPlace;
                    provider.GetPlayer().Place = command.TargetPlace;
                    teleportEvent.TargetCharacter = provider.GetPlayer();
                    EventCollection.InvokeTrapTeleported(provider.GetMe(), teleportEvent);
                }
            }

        }

        //TODO: check with test if fall check this way okay or not
        public static void MoveDirection(GameParamProvider provider, MoveCommand command)
        {
            TriggerEvent moveEvent = new TriggerEvent
            {
                EventType = EventType.Move,
                SourcePlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y),
            };
            if (provider.GetMe() is Monster)
                moveEvent.SourceCharacter = CharacterOptions.Monster;
            if (provider.GetMe() is Trap)
                moveEvent.SourceCharacter = CharacterOptions.Trap;
            switch (command.Direction)
            {
                case Directions.FORWARD:
                    if (provider.IsFreePlace(new Place((int)provider.GetMe().Place.X - command.Distance, (int)provider.GetMe().Place.Y)))
                        provider.GetMe().Place.X -= (int)command.Distance;
                    break;
                case Directions.BACKWARDS:
                    if (provider.IsFreePlace(new Place((int)provider.GetMe().Place.X + command.Distance, (int)provider.GetMe().Place.Y)))
                        provider.GetMe().Place.X += (int)command.Distance;
                    break;
                case Directions.LEFT:
                    if (provider.IsFreePlace(new Place((int)provider.GetMe().Place.X, (int)provider.GetMe().Place.Y - command.Distance)))
                        provider.GetMe().Place.Y -= (int)command.Distance;
                    break;
                case Directions.RIGHT:
                    if (provider.IsFreePlace(new Place((int)provider.GetMe().Place.X, (int)provider.GetMe().Place.Y + command.Distance)))
                        provider.GetMe().Place.Y += (int)command.Distance;
                    break;
            }
            moveEvent.TargetPlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y);
            EventCollection.InvokeSomeoneMoved(provider.GetMe(), moveEvent);
        }
        public static void MoveToPlace(GameParamProvider provider, MoveCommand command)
        {
            if (command.TargetPlace.Equals(StaticStartValues.PLACEHOLDER_PLACE))
                return;
            TriggerEvent moveEvent = new TriggerEvent
            {
                EventType = EventType.Move,
                SourcePlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y),
            };
            if (provider.GetMe() is Monster)
                moveEvent.SourceCharacter = CharacterOptions.Monster;
            if (provider.GetMe() is Trap)
                moveEvent.SourceCharacter = CharacterOptions.Trap;
            if (provider.IsFreePlace(command.TargetPlace))
                provider.GetMe().Place = command.TargetPlace;

            moveEvent.TargetPlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y);
            EventCollection.InvokeSomeoneMoved(provider.GetMe(), moveEvent);

        }

        public static void MoveToPlayer(GameParamProvider provider, MoveCommand command)
        {
            TriggerEvent moveEvent = new TriggerEvent
            {
                EventType = EventType.Move,
                SourcePlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y),
            };
            if (provider.GetMe() is Monster)
                moveEvent.SourceCharacter = CharacterOptions.Monster;
            if (provider.GetMe() is Trap)
                moveEvent.SourceCharacter = CharacterOptions.Trap;
            Random rand = new Random();
            if (provider.GetMe().Place.X < provider.GetPlayer().Place.X)
            {
                int xChange = rand.Next() % (provider.GetPlayer().Place.X - provider.GetMe().Place.X);
                if (provider.IsFreePlace(new Place(provider.GetMe().Place.X + xChange, provider.GetMe().Place.Y)))
                    provider.GetMe().Place.X += xChange;
            }
            if (provider.GetMe().Place.X > provider.GetPlayer().Place.X)
            {
                int xChange = rand.Next() % (provider.GetMe().Place.X - provider.GetPlayer().Place.X);
                if (provider.IsFreePlace(new Place(provider.GetMe().Place.X - xChange, provider.GetMe().Place.Y)))
                    provider.GetMe().Place.X -= xChange;
            }
            if (provider.GetMe().Place.Y < provider.GetPlayer().Place.Y)
            {
                int yChange = rand.Next() % (provider.GetPlayer().Place.Y - provider.GetMe().Place.Y);
                if (provider.IsFreePlace(new Place(provider.GetMe().Place.Y + yChange, provider.GetMe().Place.Y)))
                    provider.GetMe().Place.Y += yChange;
            }
            if (provider.GetMe().Place.Y > provider.GetPlayer().Place.Y)
            {
                int yChange = rand.Next() % (provider.GetMe().Place.Y - provider.GetPlayer().Place.Y);
                if (provider.IsFreePlace(new Place(provider.GetMe().Place.Y - yChange, provider.GetMe().Place.Y)))
                    provider.GetMe().Place.Y -= yChange;
            }

            moveEvent.TargetPlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y);
            EventCollection.InvokeSomeoneMoved(provider.GetMe(), moveEvent);
        }
        public static void MoveRandom(GameParamProvider provider, MoveCommand command)
        {
            int counter = 0;
            Random rand = new Random();
            int XPos = -1;
            int YPos = -1;
            while (XPos == -1 && YPos == -1 && counter < 10)
            {
                XPos = (int)(rand.Next() % provider.GetBoard().Height);
                YPos = (int)(rand.Next() % provider.GetBoard().Width);
                if (!provider.IsFreePlace(new Place(XPos, YPos)))
                {
                    XPos = -1;
                    YPos = -1;
                    counter++;
                }
            }
            if (counter >= 10)
                return;
            command.TargetPlace = new Place(XPos, YPos);
            MoveToPlace(provider, command);
        }

        public static void ShootDirection(GameParamProvider provider, ShootCommand command)
        {
            TriggerEvent shootEvent = new TriggerEvent
            {
                EventType = EventType.Shoot,
                SourceCharacter = CharacterOptions.Monster,
                SourcePlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y),
                Amount = command.HealthChangeAmount
            };
            switch (command.Direction)
            {
                case Directions.FORWARD:
                    for (int i = 0; i <= command.Distance; i++)
                    {
                        if (provider.GetMe().Place.X - i >= 0)
                        {
                            if (provider.GetPlayer().Place.X == provider.GetMe().Place.X - i
                                && provider.GetPlayer().Place.Y == provider.GetMe().Place.Y)
                            {
                                provider.GetPlayer().Damage(command.HealthChangeAmount);
                                shootEvent.TargetCharacterOption = CharacterOptions.Player;
                                shootEvent.TargetCharacter = provider.GetPlayer();
                            }
                            shootEvent.TargetPlace = new Place(provider.GetMe().Place.X - i, provider.GetMe().Place.Y);
                            EventCollection.InvokeSomeoneShot(provider.GetMe(), shootEvent);
                        }
                    }
                    break;
                case Directions.BACKWARDS:
                    for (int i = 0; i <= command.Distance; i++)
                    {
                        if (provider.GetMe().Place.X + i < provider.GetBoard().Height)
                        {
                            if (provider.GetPlayer().Place.X == provider.GetMe().Place.X + i
                                && provider.GetPlayer().Place.Y == provider.GetMe().Place.Y)
                            {
                                provider.GetPlayer().Damage(command.HealthChangeAmount);
                                shootEvent.TargetCharacterOption = CharacterOptions.Player;
                                shootEvent.TargetCharacter = provider.GetPlayer();
                            }
                            shootEvent.TargetPlace = new Place(provider.GetMe().Place.X + i, provider.GetMe().Place.Y);
                            EventCollection.InvokeSomeoneShot(provider.GetMe(), shootEvent);
                        }
                    }
                    break;
                case Directions.LEFT:
                    for (int i = 0; i <= command.Distance; i++)
                    {
                        if (provider.GetMe().Place.Y - i >= 0)
                        {
                            if (provider.GetPlayer().Place.Y == provider.GetMe().Place.Y - i
                                && provider.GetPlayer().Place.X == provider.GetMe().Place.X)
                            {
                                provider.GetPlayer().Damage(command.HealthChangeAmount);
                                shootEvent.TargetCharacterOption = CharacterOptions.Player;
                                shootEvent.TargetCharacter = provider.GetPlayer();
                            }
                            shootEvent.TargetPlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y - i);
                            EventCollection.InvokeSomeoneShot(provider.GetMe(), shootEvent);
                        }
                    }
                    break;
                case Directions.RIGHT:
                    for (int i = 0; i <= command.Distance; i++)
                    {
                        if (provider.GetMe().Place.Y + i < provider.GetBoard().Width)
                        {
                            if (provider.GetPlayer().Place.Y == provider.GetMe().Place.Y + i
                                && provider.GetPlayer().Place.X != provider.GetMe().Place.X)
                            {
                                provider.GetPlayer().Damage(command.HealthChangeAmount);
                                shootEvent.TargetCharacterOption = CharacterOptions.Player;
                                shootEvent.TargetCharacter = provider.GetPlayer();
                            }
                            shootEvent.TargetPlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y + i);
                            EventCollection.InvokeSomeoneShot(provider.GetMe(), shootEvent);
                        }
                    }
                    break;
            }
        }

        public static void ShootToPlace(GameParamProvider provider, ShootCommand command)
        {
            if (command.TargetPlace.X < 0 || command.TargetPlace.X >= provider.GetBoard().Height ||
                    command.TargetPlace.Y < 0 || command.TargetPlace.Y >= provider.GetBoard().Width)
            {
                return;
            }
            TriggerEvent shootEvent = new TriggerEvent
            {
                EventType = EventType.Shoot,
                SourceCharacter = CharacterOptions.Monster,
                SourcePlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y),
                TargetPlace = command.TargetPlace,
                Amount = command.HealthChangeAmount
            };
            if (provider.GetPlayer().Place.X == (command.TargetPlace.X) && provider.GetPlayer().Place.Y == (command.TargetPlace.Y))
            {
                provider.GetPlayer().Damage(command.HealthChangeAmount);
                shootEvent.TargetCharacterOption = CharacterOptions.Player;
                shootEvent.TargetCharacter = provider.GetPlayer();
            }
            EventCollection.InvokeSomeoneShot(provider.GetMe(), shootEvent);
        }

        public static void ShootToPlayer(GameParamProvider provider, ShootCommand command)
        {
            TriggerEvent shootEvent = new TriggerEvent
            {
                EventType = EventType.Shoot,
                SourceCharacter = CharacterOptions.Monster,
                SourcePlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y),
                TargetCharacterOption = CharacterOptions.Player,
                TargetPlace = new Place(provider.GetPlayer().Place.X, provider.GetPlayer().Place.Y),
                Amount = command.HealthChangeAmount
            };
            provider.GetPlayer().Damage(command.HealthChangeAmount);
            shootEvent.TargetCharacter = provider.GetPlayer();
            EventCollection.InvokeSomeoneShot(provider.GetMe(), shootEvent);
        }

        public static void ShootRandom(GameParamProvider provider, ShootCommand command)
        {
            Random rand = new Random();
            int damage = (int)((rand.Next() % provider.GetPlayer().GetHealth()) / 3);
            int XPos = (int)(rand.Next() % provider.GetBoard().Height);
            int YPos = (int)(rand.Next() % provider.GetBoard().Width);
            command.HealthChangeAmount = damage;
            command.TargetPlace = new Place(XPos, YPos);
            ShootToPlace(provider, command);
        }

        public static void DamageDirection(GameParamProvider provider, DamageCommand command)
        {
            TriggerEvent damageEvent = new TriggerEvent
            {
                EventType = EventType.Damage,
                SourceCharacter = CharacterOptions.Trap,
                SourcePlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y),
                Amount = command.HealthChangeAmount
            };
            switch (command.Direction)
            {
                case Directions.FORWARD:
                    //distance is 1 as default value
                    for (int i = 0; i <= command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.X - i >= 0)
                        {
                            if (provider.GetPlayer().Place.X == provider.GetMe().Place.X - i
                                && provider.GetPlayer().Place.Y == provider.GetMe().Place.Y)
                            {
                                provider.GetPlayer().Damage(command.HealthChangeAmount);
                                damageEvent.TargetCharacterOption = CharacterOptions.Player;
                                damageEvent.TargetCharacter = provider.GetPlayer();
                            }
                            foreach (Monster monster in provider.GetMonsters())
                            {
                                if (monster.Place.X == provider.GetMe().Place.X - i
                                    && monster.Place.Y == provider.GetMe().Place.Y)
                                {
                                    monster.Damage(command.HealthChangeAmount);
                                    damageEvent.TargetCharacterOption = CharacterOptions.Monster;
                                    damageEvent.TargetCharacter = monster;
                                }
                            }
                            damageEvent.TargetPlace = new Place(provider.GetMe().Place.X - i, provider.GetMe().Place.Y);
                            EventCollection.InvokeTrapDamaged(provider.GetMe(), damageEvent);
                        }
                    }
                    break;
                case Directions.BACKWARDS:
                    for (int i = 0; i <= command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.X + i < provider.GetBoard().Height)
                        {
                            if (provider.GetPlayer().Place.X == provider.GetMe().Place.X + i
                                && provider.GetPlayer().Place.Y == provider.GetMe().Place.Y)
                            {
                                provider.GetPlayer().Damage(command.HealthChangeAmount);
                                damageEvent.TargetCharacterOption = CharacterOptions.Player;
                                damageEvent.TargetCharacter = provider.GetPlayer();
                            }
                            foreach (Monster monster in provider.GetMonsters())
                            {
                                if (monster.Place.X == provider.GetMe().Place.X + i
                                    && monster.Place.Y == provider.GetMe().Place.Y)
                                {
                                    monster.Damage(command.HealthChangeAmount);
                                    damageEvent.TargetCharacterOption = CharacterOptions.Monster;
                                    damageEvent.TargetCharacter = monster;
                                }
                            }
                            damageEvent.TargetPlace = new Place(provider.GetMe().Place.X + i, provider.GetMe().Place.Y);
                            EventCollection.InvokeTrapDamaged(provider.GetMe(), damageEvent);
                        }
                    }
                    break;
                case Directions.LEFT:
                    for (int i = 0; i <= command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.Y - i >= 0)
                        {
                            if (provider.GetPlayer().Place.Y == provider.GetMe().Place.Y - i
                                && provider.GetPlayer().Place.X == provider.GetMe().Place.X)
                            {
                                provider.GetPlayer().Damage(command.HealthChangeAmount);
                                damageEvent.TargetCharacterOption = CharacterOptions.Player;
                                damageEvent.TargetCharacter = provider.GetPlayer();
                            }
                            foreach (Monster monster in provider.GetMonsters())
                            {
                                if (monster.Place.Y == provider.GetMe().Place.Y - i
                                    && monster.Place.X == provider.GetMe().Place.X)
                                {
                                    monster.Damage(command.HealthChangeAmount);
                                    damageEvent.TargetCharacterOption = CharacterOptions.Monster;
                                    damageEvent.TargetCharacter = monster;
                                }
                            }
                            damageEvent.TargetPlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y - i);
                            EventCollection.InvokeTrapDamaged(provider.GetMe(), damageEvent);
                        }
                    }
                    break;
                case Directions.RIGHT:
                    for (int i = 0; i <= command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.Y + i < provider.GetBoard().Width)
                        {

                            if (provider.GetPlayer().Place.Y == provider.GetMe().Place.Y + i
                                && provider.GetPlayer().Place.X == provider.GetMe().Place.X)
                            {
                                provider.GetPlayer().Damage(command.HealthChangeAmount);
                                damageEvent.TargetCharacterOption = CharacterOptions.Player;
                                damageEvent.TargetCharacter = provider.GetPlayer();
                            }
                            foreach (Monster monster in provider.GetMonsters())
                            {
                                if (monster.Place.Y == provider.GetMe().Place.Y + i
                                    && monster.Place.X == provider.GetMe().Place.X)
                                {
                                    monster.Damage(command.HealthChangeAmount);
                                    damageEvent.TargetCharacterOption = CharacterOptions.Monster;
                                    damageEvent.TargetCharacter = monster;
                                }
                            }
                            damageEvent.TargetPlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y + i);
                            EventCollection.InvokeTrapDamaged(provider.GetMe(), damageEvent);
                        }
                    }
                    break;
            }
        }

        public static void DamageToPlace(GameParamProvider provider, DamageCommand command)
        {
            if (command.TargetPlace.X < 0 || command.TargetPlace.X >= provider.GetBoard().Height ||
                    command.TargetPlace.Y < 0 || command.TargetPlace.Y >= provider.GetBoard().Width)
            {
                return;
            }
            TriggerEvent damageEvent = new TriggerEvent
            {
                EventType = EventType.Damage,
                SourceCharacter = CharacterOptions.Trap,
                SourcePlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y),
                Amount = command.HealthChangeAmount,
                TargetPlace = command.TargetPlace
            };
            if (provider.GetPlayer().Place.X == (command.TargetPlace.X) && provider.GetPlayer().Place.Y == (command.TargetPlace.Y))
            {
                provider.GetPlayer().Damage(command.HealthChangeAmount);
                damageEvent.TargetCharacterOption = CharacterOptions.Player;
                damageEvent.TargetCharacter = provider.GetPlayer();
            }
            foreach (Monster monster in provider.GetMonsters())
            {
                if (monster.Place.X == (command.TargetPlace.X) && monster.Place.Y == (command.TargetPlace.Y))
                {
                    monster.Damage(command.HealthChangeAmount);
                    damageEvent.TargetCharacterOption = CharacterOptions.Monster;
                    damageEvent.TargetCharacter = monster;
                }
            }
            EventCollection.InvokeTrapDamaged(provider.GetMe(), damageEvent);
        }

        public static void DamageToPlayer(GameParamProvider provider, DamageCommand command)
        {
            TriggerEvent damageEvent = new TriggerEvent
            {
                EventType = EventType.Damage,
                SourceCharacter = CharacterOptions.Trap,
                SourcePlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y),
                Amount = command.HealthChangeAmount,
                TargetPlace = new Place(provider.GetPlayer().Place.X, provider.GetPlayer().Place.Y),
                TargetCharacterOption = CharacterOptions.Player
            };
            provider.GetPlayer().Damage(command.HealthChangeAmount);
            damageEvent.TargetCharacter = provider.GetPlayer();
            EventCollection.InvokeTrapDamaged(provider.GetMe(), damageEvent);
        }

        public static void DamageToPartner(GameParamProvider provider, DamageCommand command)
        {
            if (provider.GetPartner() == null)
            {
                provider.GetDrawer().WriteCommand(ErrorMessages.PartnerError.NON_EXISTANT_PARTNER + provider.GetMe().Name);
                return;
            }
            if (provider.GetPartner() is Trap)
            {
                provider.GetDrawer().WriteCommand(ErrorMessages.HealthChangeError.CHARACTER_HAS_NO_HEALTH);
                return;
            }
            TriggerEvent damageEvent = new TriggerEvent
            {
                EventType = EventType.Damage,
                SourceCharacter = CharacterOptions.Trap,
                SourcePlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y),
                Amount = command.HealthChangeAmount,
                TargetPlace = new Place(provider.GetPartner().Place.X, provider.GetPartner().Place.Y),
                TargetCharacterOption = CharacterOptions.Monster
            };
            provider.GetPartner().Damage(command.HealthChangeAmount);
            EventCollection.InvokeTrapDamaged(provider.GetMe(), damageEvent);
        }

        public static void DamageToMonster(GameParamProvider provider, DamageCommand command)
        {
            TriggerEvent damageEvent = new TriggerEvent
            {
                EventType = EventType.Damage,
                SourceCharacter = CharacterOptions.Trap,
                SourcePlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y),
                Amount = command.HealthChangeAmount,
                TargetPlace = new Place(provider.GetMonster().Place.X, provider.GetMonster().Place.Y),
                TargetCharacterOption = CharacterOptions.Monster
            };
            provider.GetMonster().Damage(command.HealthChangeAmount);
            damageEvent.TargetCharacter = provider.GetMonster();
            EventCollection.InvokeTrapDamaged(provider.GetMe(), damageEvent);
        }
        public static void DamageRandom(GameParamProvider provider, DamageCommand command)
        {
            Random rand = new Random();
            int damage = (int)((rand.Next() % provider.GetPlayer().GetHealth()) / 3);
            int XPos = (int)(rand.Next() % provider.GetBoard().Height);
            int YPos = (int)(rand.Next() % provider.GetBoard().Width);
            command.HealthChangeAmount = damage;
            command.TargetPlace = new Place(XPos, YPos);
            DamageToPlace(provider, command);
        }

        public static void HealDirection(GameParamProvider provider, HealCommand command)
        {
            TriggerEvent healEvent = new TriggerEvent
            {
                EventType = EventType.Heal,
                SourceCharacter = CharacterOptions.Trap,
                SourcePlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y),
                Amount = command.HealthChangeAmount
            };
            switch (command.Direction)
            {
                case Directions.FORWARD:
                    //distance is 1 as default value
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.X - i >= 0)
                        {
                            if (provider.GetPlayer().Place.X == provider.GetMe().Place.X - i
                                && provider.GetPlayer().Place.Y == provider.GetMe().Place.Y)
                            {
                                provider.GetPlayer().Heal(command.HealthChangeAmount);
                                healEvent.TargetCharacterOption = CharacterOptions.Player;
                                healEvent.TargetCharacter = provider.GetPlayer();
                            }
                            foreach (Monster monster in provider.GetMonsters())
                            {
                                if (monster.Place.X == provider.GetMe().Place.X - i
                                && monster.Place.Y == provider.GetMe().Place.Y)
                                {
                                    monster.Heal(command.HealthChangeAmount);
                                    healEvent.TargetCharacterOption = CharacterOptions.Monster;
                                    healEvent.TargetCharacter = monster;
                                }
                            }
                            healEvent.TargetPlace = new Place(provider.GetMe().Place.X - i, provider.GetMe().Place.Y);
                            EventCollection.InvokeTrapHealed(provider.GetMe(), healEvent);
                        }
                    }
                    break;
                case Directions.BACKWARDS:
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.X + i < provider.GetBoard().Height)
                        {
                            if (provider.GetPlayer().Place.X == provider.GetMe().Place.X + i
                                && provider.GetPlayer().Place.Y == provider.GetMe().Place.Y)
                            {
                                provider.GetPlayer().Heal(command.HealthChangeAmount);
                                healEvent.TargetCharacterOption = CharacterOptions.Player;
                                healEvent.TargetCharacter = provider.GetPlayer();
                            }
                            foreach (Monster monster in provider.GetMonsters())
                            {
                                if (monster.Place.X == provider.GetMe().Place.X + i
                                    && monster.Place.Y == provider.GetMe().Place.Y)
                                {
                                    monster.Heal(command.HealthChangeAmount);
                                    healEvent.TargetCharacterOption = CharacterOptions.Monster;
                                    healEvent.TargetCharacter = monster;
                                }
                            }
                            healEvent.TargetPlace = new Place(provider.GetMe().Place.X + i, provider.GetMe().Place.Y);
                            EventCollection.InvokeTrapHealed(provider.GetMe(), healEvent);
                        }
                    }
                    break;
                case Directions.LEFT:
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.Y - i >= 0)
                        {
                            if (provider.GetPlayer().Place.Y == provider.GetMe().Place.Y - i
                                && provider.GetPlayer().Place.X == provider.GetMe().Place.X)
                            {
                                provider.GetPlayer().Heal(command.HealthChangeAmount);
                                healEvent.TargetCharacterOption = CharacterOptions.Player;
                                healEvent.TargetCharacter = provider.GetPlayer();
                            }
                            foreach (Monster monster in provider.GetMonsters())
                            {
                                if (monster.Place.Y == provider.GetMe().Place.Y - i
                                    && monster.Place.X == provider.GetMe().Place.X)
                                {
                                    monster.Heal(command.HealthChangeAmount);
                                    healEvent.TargetCharacterOption = CharacterOptions.Monster;
                                    healEvent.TargetCharacter = monster;
                                }
                            }
                            healEvent.TargetPlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y - i);
                            EventCollection.InvokeTrapHealed(provider.GetMe(), healEvent);
                        }
                    }
                    break;
                case Directions.RIGHT:
                    for (int i = 0; i < command.Distance; i++)
                    {
                        if ((int)provider.GetMe().Place.Y + i < provider.GetBoard().Width)
                        {
                            if (provider.GetPlayer().Place.Y == provider.GetMe().Place.Y + i
                                && provider.GetPlayer().Place.X == provider.GetMe().Place.X)
                            {
                                provider.GetPlayer().Heal(command.HealthChangeAmount);
                                healEvent.TargetCharacterOption = CharacterOptions.Player;
                                healEvent.TargetCharacter = provider.GetPlayer();
                            }
                            foreach (Monster monster in provider.GetMonsters())
                            {
                                if (monster.Place.Y == provider.GetMe().Place.Y + i
                                    && monster.Place.X == provider.GetMe().Place.X)
                                {
                                    monster.Heal(command.HealthChangeAmount);
                                    healEvent.TargetCharacterOption = CharacterOptions.Monster;
                                    healEvent.TargetCharacter = monster;
                                }
                            }
                            healEvent.TargetPlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y + i);
                            EventCollection.InvokeTrapHealed(provider.GetMe(), healEvent);
                        }
                    }
                    break;
            }
        }

        public static void HealToPlace(GameParamProvider provider, HealCommand command)
        {
            if (command.TargetPlace.X < 0 || command.TargetPlace.X >= provider.GetBoard().Height ||
                command.TargetPlace.Y < 0 || command.TargetPlace.Y >= provider.GetBoard().Width)
            {
                return;
            }
            TriggerEvent healEvent = new TriggerEvent
            {
                EventType = EventType.Heal,
                SourceCharacter = CharacterOptions.Trap,
                SourcePlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y),
                Amount = command.HealthChangeAmount,
                TargetPlace = command.TargetPlace
            };
            if (provider.GetPlayer().Place.X == (command.TargetPlace.X) && provider.GetPlayer().Place.Y == (command.TargetPlace.Y))
            {
                provider.GetPlayer().Heal(command.HealthChangeAmount);
                healEvent.TargetCharacterOption = CharacterOptions.Player;
                healEvent.TargetCharacter = provider.GetPlayer();
            }
            foreach (Monster monster in provider.GetMonsters())
            {
                if (monster.Place.X == (command.TargetPlace.X) && monster.Place.Y == (command.TargetPlace.Y))
                {
                    monster.Heal(command.HealthChangeAmount);
                    healEvent.TargetCharacterOption = CharacterOptions.Monster;
                    healEvent.TargetCharacter = monster;
                }
            }
            EventCollection.InvokeTrapHealed(provider.GetMe(), healEvent);
        }

        public static void HealToPlayer(GameParamProvider provider, HealCommand command)
        {
            TriggerEvent healEvent = new TriggerEvent
            {
                EventType = EventType.Heal,
                SourceCharacter = CharacterOptions.Trap,
                SourcePlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y),
                Amount = command.HealthChangeAmount,
                TargetPlace = new Place(provider.GetPlayer().Place.X, provider.GetPlayer().Place.Y),
                TargetCharacterOption = CharacterOptions.Player
            };
            provider.GetPlayer().Heal(command.HealthChangeAmount);
            healEvent.TargetCharacter = provider.GetPlayer();
            EventCollection.InvokeTrapHealed(provider.GetMe(), healEvent);
        }

        public static void HealToPartner(GameParamProvider provider, HealCommand command)
        {
            if (provider.GetPartner() == null)
            {
                provider.GetDrawer().WriteCommand(ErrorMessages.PartnerError.NON_EXISTANT_PARTNER + provider.GetMe().Name);
                return;
            }
            if (provider.GetPartner() is Trap)
            {
                provider.GetDrawer().WriteCommand(ErrorMessages.HealthChangeError.CHARACTER_HAS_NO_HEALTH);
                return;
            }
            TriggerEvent healEvent = new TriggerEvent
            {
                EventType = EventType.Heal,
                SourceCharacter = CharacterOptions.Trap,
                SourcePlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y),
                Amount = command.HealthChangeAmount,
                TargetPlace = new Place(provider.GetPartner().Place.X, provider.GetPartner().Place.Y),
                TargetCharacterOption = CharacterOptions.Monster
            };
            provider.GetPartner().Heal(command.HealthChangeAmount);
            EventCollection.InvokeTrapHealed(provider.GetMe(), healEvent);
        }

        public static void HealToMonster(GameParamProvider provider, HealCommand command)
        {
            TriggerEvent healEvent = new TriggerEvent
            {
                EventType = EventType.Heal,
                SourceCharacter = CharacterOptions.Trap,
                SourcePlace = new Place(provider.GetMe().Place.X, provider.GetMe().Place.Y),
                Amount = command.HealthChangeAmount,
                TargetPlace = new Place(provider.GetMonster().Place.X, provider.GetMonster().Place.Y),
                TargetCharacterOption = CharacterOptions.Monster
            };
            provider.GetMonster().Heal(command.HealthChangeAmount);
            healEvent.TargetCharacter = provider.GetMonster();
            EventCollection.InvokeTrapHealed(provider.GetMe(), healEvent);
        }
        public static void HealRandom(GameParamProvider provider, HealCommand command)
        {
            Random rand = new Random();
            int Heal = (int)((rand.Next() % provider.GetPlayer().GetHealth()) / 5);
            int XPos = (int)(rand.Next() % provider.GetBoard().Height);
            int YPos = (int)(rand.Next() % provider.GetBoard().Width);
            command.HealthChangeAmount = Heal;
            command.TargetPlace = new Place(XPos, YPos);
            HealToPlace(provider, command);
        }

        public static void HealthChange(GameParamProvider provider, NumberParameterDeclareCommand command)
        {
                if (provider.GetMe().GetCharacterType() is MonsterType )
                        provider.GetMe().GetCharacterType().Health = command.Number;
        }
        public static void HealChange(GameParamProvider provider, NumberParameterDeclareCommand command)
        {
            if (provider.GetMe().GetCharacterType() is TrapType)
                provider.GetMe().GetCharacterType().Heal = command.Number;
        }
        public static void DamageChange(GameParamProvider provider, NumberParameterDeclareCommand command)
        {
            provider.GetMe().GetCharacterType().Damage = command.Number;
        }
        public static void TeleportPlaceChange(GameParamProvider provider, PlaceParameterDeclareCommand command)
        {
            if(provider.GetBoard().Height <= command.Place.X || provider.GetBoard().Width <= command.Place.Y)
            {
                provider.GetDrawer().WriteCommand(ErrorMessages.GameError.PLACE_OUT_OF_BOUNDS);
                return;
            }
            if (provider.GetMe().GetCharacterType() is TrapType)
                provider.GetMe().GetCharacterType().TeleportPlace = command.Place;
        }
        public static void SpawnPlaceChange(GameParamProvider provider, PlaceParameterDeclareCommand command)
        {
            if (provider.GetBoard().Height <= command.Place.X || provider.GetBoard().Width <= command.Place.Y)
            {
                provider.GetDrawer().WriteCommand(ErrorMessages.GameError.PLACE_OUT_OF_BOUNDS);
                return;
            }
            if (provider.GetMe().GetCharacterType() is TrapType)
                provider.GetMe().GetCharacterType().SpawnPlace = command.Place;
        }
        public static void SpawnTypeChange(GameParamProvider provider, TypeParameterDeclareCommand command)
        {
            if(Program.GetCharacterType(command.CharacterType.Name) == null)
            {
                provider.GetDrawer().WriteCommand(ErrorMessages.TypeCreationError.TYPE_DOES_NOT_EXIST);
                return;
            }
            provider.GetMe().GetCharacterType().SpawnType = command.CharacterType;
            
        }
    }
}
