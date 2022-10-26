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

            if (TriggeringEvent.SourceCharacter != CharacterOptions.NULL && TriggeringEvent.SourceCharacter != args.SourceCharacter) 
            {
                if ( ! (TriggeringEvent.SourceCharacter == CharacterOptions.Partner || TriggeringEvent.SourceCharacter == CharacterOptions.Me))
                {
                    return;
                }
            }
            if ( TriggeringEvent.TargetCharacterOption != CharacterOptions.NULL && (TriggeringEvent.TargetCharacterOption != args.TargetCharacterOption || 
                    args.TargetCharacter == null))
            {
                if ( ! (TriggeringEvent.TargetCharacterOption == CharacterOptions.Partner || TriggeringEvent.TargetCharacterOption == CharacterOptions.Me))
                    return;
            }

            if (TriggeringEvent.TargetCharacterOption == CharacterOptions.Player && ! GameParamProvider.GetPlayer().Equals(args.TargetCharacter))
                return;

            if (args.EventType.Equals(TriggeringEvent.EventType)) {
                if ((TriggeringEvent.Amount == StaticStartValues.PLACEHOLDER_AMOUNT || TriggeringEvent.Amount == args.Amount)
                    && (TriggeringEvent.SourcePlace == null || (TriggeringEvent.SourcePlace.DirectionTo(args.SourcePlace) == Directions.COLLISION))
                    && (TriggeringEvent.TargetPlace == null || (TriggeringEvent.TargetPlace.DirectionTo(args.TargetPlace) == Directions.COLLISION)))
                {
                    Character backupActualCharacter = GameParamProvider.GetMe();
                    foreach (Character c in GameParamProvider.GetCharacters())
                    {
                        if (c.GetCharacterType().Equals(Owner))
                        {
                            if ((TriggeringEvent.SourceCharacter == CharacterOptions.Me && !c.Equals(sender)) ||
                                (TriggeringEvent.SourceCharacter == CharacterOptions.Partner && !sender.Equals(c.GetPartner())) ||
                                (TriggeringEvent.TargetCharacterOption == CharacterOptions.Me && !c.Equals(args.TargetCharacter)) ||
                                (TriggeringEvent.TargetCharacterOption == CharacterOptions.Partner && args.TargetCharacter.Equals(c.GetPartner()))
                                )
                                return;

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
