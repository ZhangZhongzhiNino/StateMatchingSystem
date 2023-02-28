using System.Collections;
using System.Collections.Generic;

namespace Nino.NewStateMatching.PlayerCharacter.Variable
{

    public class IntItem : VariableItem<int, IntValue>
    {
        protected override ItemValue CreateNewValue()
        {
            return new IntValue();
        }
    }
}

