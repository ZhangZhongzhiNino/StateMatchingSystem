using Nino.NewStateMatching.PlayerCharacter.Trigger.PlayerInput;

namespace Nino.NewStateMatching.PlayerCharacter.Trigger
{
    public class TriggerCategory : ExecuterCategory
    {
        protected override void AddExecuterGroupInitializers()
        {
            GeneralUtility.AddGroupInitializer(ref initializers, new PlayerInputExecuterGroupInitializer(this, "PlayerInput"));
        }

        protected override void RemoveExecuterGroups()
        {
        }

        protected override void RemoveExecuters()
        {
            
        }

        protected override string WriteAddress()
        {
            return "Trigger";
        }
    }
}
