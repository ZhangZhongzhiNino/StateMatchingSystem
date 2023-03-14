using System.Collections;
using System.Collections.Generic;
namespace Nino.NewStateMatching.PlayerCharacter.Variable
{
    public class SingleVariableExecuterGroup : ExecuterGroup
    {
        public ExecuterInitializerCreater executerCreater;
        protected override void AddExecuterInitializers()
        {
            executerCreater = new ExecuterInitializerCreater(this, typeof(SingleVariableExecuter));
        }

        protected override string WriteLocalAddress()
        {
            return "Single Variable";
        }
        
    }
}
