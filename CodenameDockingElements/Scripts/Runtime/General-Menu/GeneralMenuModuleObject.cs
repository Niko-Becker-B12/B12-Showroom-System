using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Sirenix.Serialization;

namespace Showroom.UI
{

    public class GeneralMenuModuleObject : MonoBehaviour
    {

        [OdinSerialize]
        public CustomGeneralMenuModule data;


        public virtual void SetUp()
        {

            Debug.Log("Setting up General Menu Generic Module");

            data.generalMenuModuleObject = this.gameObject.GetComponent<GeneralMenuModuleObject>();

        }

    }

}