#if UNITY_EDITOR
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;
using Sirenix.Utilities;
using System.Linq;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using Sirenix.Serialization;


namespace Showroom.Editor
{

    public class ShowroomManagerEditorWindow : OdinMenuEditorWindow
    {

        [MenuItem("Window/Showroom/Showroom Manager", priority = 0)]
        private static void OpenWindow()
        {

            GetWindow<ShowroomManagerEditorWindow>().Show();

        }

        private void OnDestroy()
        {
            
            base.OnDestroy();

        }

        protected override OdinMenuTree BuildMenuTree()
        {

            OdinMenuTree tree = new OdinMenuTree();

            tree.Add("Create New Use Case", new ShowroomManager.UseCase());

            tree.Config.DrawSearchToolbar = true;

            return tree;

        }

    }

}
#endif