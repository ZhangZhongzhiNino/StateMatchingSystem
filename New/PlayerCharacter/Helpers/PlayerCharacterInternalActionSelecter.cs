namespace Nino.NewStateMatching.PlayerCharacter
{
    public class PlayerCharacterInternalActionSelecter : InternalActionSelecter
    {
        public SMSPlayerCharacterRoot root;

        public override void Update()
        {
            if (root.inputCategory != null && !categories.Contains("Input")) categories.Add("Input");
            else if (root.inputCategory == null && categories.Contains("Input")) categories.Remove("Input");

            if (root.internalEventCategory != null && !categories.Contains("Internal Event")) categories.Add("Internal Event");
            else if (root.internalEventCategory == null && categories.Contains("Internal Event")) categories.Remove("Internal Event");

            if (root.internalEventCategory != null && !categories.Contains("Internal Event")) categories.Add("Internal Event");
            else if (root.internalEventCategory == null && categories.Contains("Internal Event")) categories.Remove("Internal Event");
        }

        protected override void OnSelectCategoryChanged()
        {
            
        }

        protected override void OnSelectExecuterChanged()
        {
            
        }

        protected override void OnSelectExecuterGroupChanged()
        {
            
        }
    }
}

