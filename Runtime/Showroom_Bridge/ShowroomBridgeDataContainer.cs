using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Showroom.UI;

namespace Showroom
{

    public class ShowroomBridgeDataContainer : ScriptableObject
    {

        public string subLevelName;
        public string subLevelSubTitle;
        public string subLevelID;

        public Camera subLevelMainCamera;

        public List<Camera> subLevelCameras = new List<Camera>();
        public List<Function> onLevelLoaded = new List<Function>();

        public bool isOnlyLevelInBuild = false;
        public bool showDebugMessages = false;
        public bool openSidebarMenu = true;

        public bool subLevelHasGeneralMenu;



    }

}