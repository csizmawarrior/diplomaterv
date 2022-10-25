using LabWork1github.static_constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork1github.EventHandling
{
    public class TriggerEventHandler
    {
        public TriggerEvent TriggeringEvent { get; set; }
        public List<Command> Commands { get; set; } = new List<Command>();
        public GameParamProvider GameParamProvider { get; set; }
        public CharacterType Owner { get; set; }

        //TODO: The if might needs some fine tuning
        public virtual void OnEvent(object sender, TriggerEvent args)
        {
            if (args.EventType.Equals(TriggeringEvent.EventType) && (TriggeringEvent.TargetCharacter == null || (args.TargetCharacter.GetType().Equals(TriggeringEvent.TargetCharacter.GetType())))) {
                if ((TriggeringEvent.Amount == StaticStartValues.PLACEHOLDER_AMOUNT || TriggeringEvent.Amount == args.Amount)
                    && (TriggeringEvent.SourcePlace == null || (TriggeringEvent.SourcePlace.DirectionTo(args.SourcePlace) == Directions.COLLISION))
                    && (TriggeringEvent.TargetPlace == null || (TriggeringEvent.TargetPlace.DirectionTo(args.TargetPlace) == Directions.COLLISION))
                    && (TriggeringEvent.SourceCharacter == null || TriggeringEvent.SourceCharacter.GetType().Equals(args.SourceCharacter.GetType())) )
                {
                    Character backupActualCharacter = GameParamProvider.GetMe();
                    foreach (Character c in GameParamProvider.GetCharacters())
                    {
                        if (c.GetCharacterType().Equals(Owner))
                        {
                            GameParamProvider.SetActualCharacter(c);
                            foreach (Command command in Commands)
                            {
                                command.Execute(GameParamProvider);
                            }
                        }
                    }
                    GameParamProvider.SetActualCharacter(backupActualCharacter);
                }
            }
        }
    }
}
