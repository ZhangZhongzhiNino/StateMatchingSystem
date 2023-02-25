using UnityEngine;
namespace Nino.NewStateMatching.PlayerCharacter
{
    public class ActionUtility
    {
        public static PlayerCharacterInternalActionSelecter CreateInternalActionSelector(SMSPlayerCharacterRoot root)
        {
            PlayerCharacterInternalActionSelecter newSelecter = ScriptableObject.CreateInstance<PlayerCharacterInternalActionSelecter>();
            newSelecter.root = root;
            return newSelecter;
        }
    }
}

