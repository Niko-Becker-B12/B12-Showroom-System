using UnityEditor;
using UnityEngine;

namespace Showroom
{
    public class ShowroomObjectMenuExtension : MonoBehaviour
    {

        [MenuItem("GameObject/Showroom/Showroom Manager", false, 0)]
        static void CreateShowroomManager(MenuCommand menuCommand)
        {

            Object showroomManagerPrefab = AssetDatabase.LoadAssetAtPath("Packages/B12-Showroom-System/Showroom_Manager/--- Showroom Manager ---.prefab", typeof(Object));

            if (showroomManagerPrefab != null)
            {

                Selection.activeGameObject = (GameObject)PrefabUtility.InstantiatePrefab(showroomManagerPrefab);

            }

        }

        [MenuItem("GameObject/Showroom/UI/Docking Elements", false, 5000)]
        static void CreateDockingElements(MenuCommand menuCommand)
        {

            Object dockingElementsPrefab = AssetDatabase.LoadAssetAtPath("Packages/B12-Showroom-System/CodenameDockingElements/PRF_CodeNameDockingElements.prefab", typeof(Object));

            GameObject parent = GameObject.Find("/--- User Interface ---");

            if (parent == null)
                parent = new GameObject("--- User Interface ---");

            if (dockingElementsPrefab != null)
            {

                Selection.activeGameObject = (GameObject)PrefabUtility.InstantiatePrefab(dockingElementsPrefab, parent.transform);

            }

        }

        [MenuItem("GameObject/Showroom/Navigation/Player", false, 5000)]
        static void CreatePlayer(MenuCommand menuCommand)
        {

            Object playerPrefab = AssetDatabase.LoadAssetAtPath("Packages/B12-Showroom-System/Showroom_Navigation/PRF_ShowroomNavigation.prefab", typeof(Object));

            if (playerPrefab != null)
            {

                Selection.activeGameObject = (GameObject)PrefabUtility.InstantiatePrefab(playerPrefab);

            }

        }

        [MenuItem("GameObject/Showroom/Enviroment/Base Enviroment", false, 5000)]
        static void CreateEnviroment(MenuCommand menuCommand)
        {

            Object enviromentPrefab = AssetDatabase.LoadAssetAtPath("Packages/B12-Showroom-System/Showroom_Enviroment/PRF_Background.prefab", typeof(Object));

            GameObject parent = GameObject.Find("/--- Enviroment ---");

            if (parent == null)
                parent = new GameObject("--- Enviroment ---");

            if (enviromentPrefab != null)
            {

                Selection.activeGameObject = (GameObject)PrefabUtility.InstantiatePrefab(enviromentPrefab, parent.transform);

            }

        }

        [MenuItem("GameObject/Showroom/World-Space-UI/Interact Button", false, 5000)]
        static void CreateInteractButton(MenuCommand menuCommand)
        {

            Object interactButtonPrefab = AssetDatabase.LoadAssetAtPath("Packages/B12-Showroom-System/Showroom_WorldSpaceUI/WUI_Showroom_InteractButton.prefab", typeof(Object));

            if (interactButtonPrefab != null)
            {

                if (Selection.activeGameObject != null)
                    PrefabUtility.InstantiatePrefab(interactButtonPrefab, Selection.activeGameObject.transform);
                else
                    Selection.activeGameObject = (GameObject)PrefabUtility.InstantiatePrefab(interactButtonPrefab);

            }

        }

        [MenuItem("GameObject/Showroom/World-Space-UI/Interact Button (Label)", false, 5000)]
        static void CreateInteractButtonLabel(MenuCommand menuCommand)
        {

            Object prefab = AssetDatabase.LoadAssetAtPath("Packages/B12-Showroom-System/Showroom_WorldSpaceUI/WUI_Showroom_InteractButton_Label Variant.prefab", typeof(Object));

            if (prefab != null)
            {

                if (Selection.activeGameObject != null)
                    PrefabUtility.InstantiatePrefab(prefab as GameObject, Selection.activeGameObject.transform);
                else
                    Selection.activeGameObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab as GameObject);

            }

        }

        [MenuItem("GameObject/Showroom/World-Space-UI/Label", false, 5000)]
        static void CreateLabel(MenuCommand menuCommand)
        {

            Object prefab = AssetDatabase.LoadAssetAtPath("Packages/B12-Showroom-System/Showroom_WorldSpaceUI/WUI_Showroom_Standard_Label.prefab", typeof(Object));

            if (prefab != null)
            {

                if (Selection.activeGameObject != null)
                    PrefabUtility.InstantiatePrefab(prefab, Selection.activeGameObject.transform);
                else
                    Selection.activeGameObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

            }

        }

        [MenuItem("GameObject/Showroom/World-Space-UI/Standard WorldSpaceUI Object", false, 5000)]
        static void CreateStandardObj(MenuCommand menuCommand)
        {

            Object prefab = AssetDatabase.LoadAssetAtPath("Packages/B12-Showroom-System/Showroom_WorldSpaceUI/WUI_Showroom_Standard_Object.prefab", typeof(Object));

            if (prefab != null)
            {

                if (Selection.activeGameObject != null)
                    PrefabUtility.InstantiatePrefab(prefab, Selection.activeGameObject.transform);
                else
                    Selection.activeGameObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

            }

        }

        [MenuItem("GameObject/Showroom/World-Space-UI/3D-Tooltip", false, 5000)]
        static void Create3DTooltip(MenuCommand menuCommand)
        {

            Object prefab = AssetDatabase.LoadAssetAtPath("Packages/B12-Showroom-System/Showroom_WorldSpaceUI/WUI_Showroom_Tooltip3D.prefab", typeof(Object));

            if (prefab != null)
            {

                if (Selection.activeGameObject != null)
                    PrefabUtility.InstantiatePrefab(prefab, Selection.activeGameObject.transform);
                else
                    Selection.activeGameObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

            }

        }

    }
}
