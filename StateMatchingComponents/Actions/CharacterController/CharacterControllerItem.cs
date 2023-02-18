using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMatching.Helper;

namespace StateMatching.Action
{
    public class CharacterControllerItem : Item<CharacterController>
    {

        CharacterController controller;

        public override CharacterController value 
        {
            get { return controller; }
            set { controller = value; }
        }

        public CharacterControllerItem(string controllerName, CharacterController controller)
        {
            itemName = controllerName;
            value = controller;

        }
    }

}
