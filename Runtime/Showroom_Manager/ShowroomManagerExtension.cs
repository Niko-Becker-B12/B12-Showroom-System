using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ThisOtherThing.UI.Shapes;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.Video;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Showroom.UI;
using Sirenix.Serialization;


namespace Showroom
{

    [System.Serializable]
    public class ShowroomManagerExtension
    {

        public List<GroupFunction> groupFunctions = new List<GroupFunction>();
        public List<GroupFunction> subGroupFunctions = new List<GroupFunction>();

        public List<string> GetAllTextData()
        {
            List<string> listRange = new List<string>();

            return listRange;
        }

        public List<string> GetGroupData()
        {
            List<string> listRange = new List<string>();

            return listRange;
        }

        public List<string> GetSubGroupData()
        {
            List<string> listRange = new List<string>();

            return listRange;
        }

    }

    public class GroupFunction
    {

        public List<Function> groupFunctions = new List<Function>();

    }

}