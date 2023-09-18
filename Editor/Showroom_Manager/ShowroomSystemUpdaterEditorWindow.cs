#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.Playables;
using System;
using static UnityEditor.UIElements.ToolbarMenu;

namespace Showroom.Editor
{
    public class ShowroomSystemUpdaterEditorWindow : OdinEditorWindow
    {

        //[ShowInInspector]
        public static ShowroomSystemPackageInfo packageInfo;

        [ShowInInspector]
        [HorizontalGroup("Split"), BoxGroup("Split/Left", false)]
        public static string version;

        [ShowInInspector]
        [HorizontalGroup("Split"), BoxGroup("Split/right", false), HideLabel]
        public static VersionType versionType;

        [HorizontalGroup("Split2"), BoxGroup("Split2/left", false)]
        [Button("+++")]
        void AddMajorVersion()
        {

            string[] subs = version.Split('.');

            int major = int.Parse(subs[0]);

            major++;

            version = string.Format("{0}.{1}.{2}", major, 0, 0);

        }

        [HorizontalGroup("Split2"), BoxGroup("Split2/Center", false)]
        [Button("++")]
        void AddMinorVersion()
        {

            string[] subs = version.Split('.');

            int minor = int.Parse(subs[1]);

            minor++;

            version = string.Format("{0}.{1}.{2}", subs[0], minor, 0);

        }

        [HorizontalGroup("Split2"), BoxGroup("Split2/Right", false)]
        [Button("+")]
        void AddVariantVersion()
        {

            string[] subs = version.Split('.');

            int variant = int.Parse(subs[2]);

            variant++;

            version = string.Format("{0}.{1}.{2}", subs[0], subs[1], variant);

        }

        [HorizontalGroup("Split3"), BoxGroup("Split3/left", false)]
        [Button("---")]
        void RemoveMajorVersion()
        {

            string[] subs = version.Split('.');

            int major = int.Parse(subs[0]);

            if (major > 0)
                major--;

            version = string.Format("{0}.{1}.{2}", major, 0, 0);

        }

        [HorizontalGroup("Split3"), BoxGroup("Split3/Center", false)]
        [Button("--")]
        void RemoveMinorVersion()
        {

            string[] subs = version.Split('.');

            int minor = int.Parse(subs[1]);

            if (minor > 0)
                minor--;

            version = string.Format("{0}.{1}.{2}", subs[0], minor, 0);

        }

        [HorizontalGroup("Split3"), BoxGroup("Split3/Right", false)]
        [Button("-")]
        void RemoveVariantVersion()
        {

            string[] subs = version.Split('.');

            int variant = int.Parse(subs[2]);

            if(variant > 0)
                variant--;

            version = string.Format("{0}.{1}.{2}", subs[0], subs[1], variant);

        }


        [MenuItem("Window/Showroom/Showroom System Updater")]
        private static void OpenWindow()
        {

            LoadCurrentVersionFile();

            GetWindow<ShowroomSystemUpdaterEditorWindow>().Show();

        }

        [Button]
        static void LoadCurrentVersionFile()
        {

            TextAsset t = (TextAsset)AssetDatabase.LoadAssetAtPath("Packages/com.b12.showroomsystem/package.json", typeof(TextAsset));

            packageInfo = JsonUtility.FromJson<ShowroomSystemPackageInfo>(t.text);

            string tempVersion = packageInfo.version;

            string[] s = tempVersion.Split('-');

            version = s[0];

            if(s.Length > 1)
            {

                switch (s[1])
                {

                    case "Alpha":
                        versionType = VersionType.alpha;
                        break;
                    case "Beta":
                        versionType = VersionType.beta;
                        break;
                    case "Pre":
                        versionType = VersionType.preRelease;
                        break;
                    default:
                        versionType = VersionType.release;
                        break;
                }

            }
            else
                versionType = VersionType.release;

        }

        [Button]
        static void WriteCurrentVersionFile()
        {

            if (packageInfo == null)
                return;

            TextAsset t = (TextAsset)AssetDatabase.LoadAssetAtPath("Packages/com.b12.showroomsystem/package.json", typeof(TextAsset));

            string path = AssetDatabase.GetAssetPath(t);

            //string jsonString = JsonUtility.ToJson(new ShowroomSystemPackageInfo
            //{
            //
            //    name = "com.b12.showroomsystem",
            //    version = "4.0.78-pre-release",
            //    displayName = "B12-Showroom-System",
            //    description = "Welcome to the B12 Showroom System Documentation!The currently online and useable Version is 4.0.0! Please report any upcoming issues, feature suggestions, or possible miss-information on the wiki at the Issues-Tab, thanks!",
            //    unity = "2020.3",
            //    documentationUrl = "https=//github.com/Niko-Becker-B12/B12-Showroom-System-Documentation/wiki/Documentation",
            //    changelogUrl = "",
            //    licensesUrl = ""
            //
            //}, true);


            string versionAdditionalInfo = "";

            switch(versionType)
            {

                case VersionType.alpha:
                    versionAdditionalInfo = "-Alpha";
                    break;
                case VersionType.beta:
                    versionAdditionalInfo = "-Beta";
                    break;
                case VersionType.preRelease:
                    versionAdditionalInfo = "-Pre-Release";
                    break;
                case VersionType.release:
                    versionAdditionalInfo = "";
                    break;

            }

            packageInfo.version = string.Format("{0}{1}", version, versionAdditionalInfo);


            string jsonString = JsonUtility.ToJson(packageInfo, true);

            // Write JSON to file.
            File.WriteAllText(path, jsonString);

            AssetDatabase.Refresh();

            LoadCurrentVersionFile();

        }



    }

    [Serializable]
    public class ShowroomSystemPackageInfo
    {

        public string name;
        public string version;
        public string displayName;
        public string description;
        public string type;
        public string unity;
        public bool hideInEditor = false;
        public string documentationUrl;
        public string changelogUrl;
        public string licensesUrl;
        Dictionary<string, string> dependencies = new Dictionary<string, string>();
        public string[] keywords = new string[]
        {

            "Showroom",
            "Studio-B12",
            "B12",
            "Sub-Level"

        };
        public ShowroomSystemPackageInfoAuthor author = new ShowroomSystemPackageInfoAuthor
        {

            name = "Studio B12 | Niko Becker",
            email = "n.becker@b12-gruppe.de",
            url = ""

        };

    }

    [Serializable]
    public class ShowroomSystemPackageInfoAuthor
    {

        public string name;
        public string email;
        public string url;

    }

    public enum VersionType
    {

        alpha,
        beta,
        preRelease,
        release

    }

}
#endif