using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Showroom.UI
{

    [System.Serializable]
    public class UserInterfaceContainerModule
    {

        [Sirenix.OdinInspector.ReadOnly]
        public UserInterfaceContainerModuleType userInterfaceContainerModuleType = UserInterfaceContainerModuleType.custom;

        public virtual void Create(Transform moduleParent)
        {



        }

        public virtual void Update(Transform moduleParent)
        {



        }

    }

}