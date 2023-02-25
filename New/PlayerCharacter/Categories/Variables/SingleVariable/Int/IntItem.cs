using System.Collections;
using System.Collections.Generic;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class IntItem : VariableItem<int>
    {
        protected override void InitializeInstance()
        {
            value = 0;
        }
    }
}

