using Nino.NewStateMatching.PlayerCharacter.Trigger.PlayerInput;

namespace Nino.NewStateMatching.PlayerCharacter.Trigger
{
    public class TriggerCategory : ExecuterCategory
    {
        protected override void AddExecuterGroupInitializers()
        {
            GeneralUtility.AddGroupInitializer(ref initializers, new ExecuterGroupInitializer(this, "PlayerInput",typeof(PlayerInputExecuterGroup)));
        }
        protected override string WriteAddress()
        {
            return "Trigger";
        }
    }
}
