using System.Collections.Generic;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class IntData : BaseVariableData
    {
        public List<int> items;
        protected override void InitializeInstance()
        {
            items = new List<int>();
        }
    }
}

