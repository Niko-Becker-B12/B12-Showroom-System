using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using DG.Tweening;
using ThisOtherThing.UI;

[System.Serializable]
[Sirenix.OdinInspector.InlineEditor]
[CreateAssetMenu(fileName = "New UI Skin", menuName = "Showroom/UI Skin")]
public class BaseUISkin : ScriptableObject
{

    [FoldoutGroup("General Settings")] public float animationSpeed = 1f;
    [FoldoutGroup("General Settings")] public Ease animationEasingType;

    [FoldoutGroup("Sidebar")] public bool useSidebarSettings = true;
    [FoldoutGroup("Sidebar")] public Vector2 sidebarClosedPos = Vector2.zero;
    [FoldoutGroup("Sidebar")] public Vector2 sidebarOpenedPos = Vector2.zero;
    [FoldoutGroup("Sidebar")] public Sprite sidebarBackground;
    [FoldoutGroup("Sidebar")] public Color sidebarBackgroundColor;
    [FoldoutGroup("Sidebar/Header")] public Vector2 sidebarHeaderSize = Vector2.zero;

    [FoldoutGroup("Sidebar/Sidebar Buttons")]
    [BoxGroup("Sidebar/Sidebar Buttons/Sidebar Button Colors")]
    public ColorBlock sidebarButtonColors = ColorBlock.defaultColorBlock;

    [FoldoutGroup("Sidebar/Sidebar Buttons")]
    [BoxGroup("Sidebar/Sidebar Buttons/Sidebar Text Colors")]
    public ColorBlock sidebarButtonTextColors = ColorBlock.defaultColorBlock;

    [FoldoutGroup("Sidebar/Sidebar Buttons")]
    [BoxGroup("Sidebar/Sidebar Buttons/Sidebar Additional Colors")]
    public ColorBlock sidebarButtonAdditionalColors = ColorBlock.defaultColorBlock;

    [FoldoutGroup("Sidebar/Sidebar Header Buttons")]
    [BoxGroup("Sidebar/Sidebar Header Buttons/Sidebar Button Colors")]
    public ColorBlock sidebarHeaderButtonColors = ColorBlock.defaultColorBlock;

    [FoldoutGroup("Sidebar/Sidebar Header Buttons")]
    [BoxGroup("Sidebar/Sidebar Header Buttons/Sidebar Text Colors")]
    public ColorBlock sidebarHeaderButtonTextColors = ColorBlock.defaultColorBlock;

    [FoldoutGroup("Sidebar/Sidebar Header Buttons")]
    [BoxGroup("Sidebar/Sidebar Header Buttons/Sidebar Additional Colors")]
    public ColorBlock sidebarHeaderButtonAdditionalColors = ColorBlock.defaultColorBlock;


    [FoldoutGroup("General Menu")] public bool useGeneralMenuSettings = true;
    [FoldoutGroup("General Menu")] public Vector2 generalMenuClosedPos = Vector2.zero;
    [FoldoutGroup("General Menu")] public Vector2 generalMenuOpenedPos = Vector2.zero;
    [FoldoutGroup("General Menu")] public Sprite generalMenuBackground;
    [FoldoutGroup("General Menu")] public Color generalMenuBackgroundColor;
    [FoldoutGroup("General Menu/Icons")] public Sprite generalMenuPlayButtonIcon;
    //[FoldoutGroup("General Menu/Icons")] public Sprite generalMenuPauseButtonIcon;
    [FoldoutGroup("General Menu/Icons")] public Sprite generalMenuReplayButtonIcon;
    [FoldoutGroup("General Menu/Icons")] public Sprite generalMenuCameraDropdownIcon;
    [FoldoutGroup("General Menu/Icons")] public Sprite generalMenuTransparencyToggleActiveIcon;
    [FoldoutGroup("General Menu/Icons")] public Sprite generalMenuTransparencyToggleDeactiveIcon;
    [FoldoutGroup("General Menu/Icons")] public Sprite generalMenuDragModeToggleActiveIcon;
    [FoldoutGroup("General Menu/Icons")] public Sprite generalMenuDragModeToggleDeactiveIcon;
    [FoldoutGroup("General Menu/Icons")] public Sprite generalMenuBackButtonIcon;
    [FoldoutGroup("General Menu/Icons")] public Sprite generalMenuHomeButtonIcon;
    [FoldoutGroup("General Menu")] public bool generalMenuHasDivider;


    [FoldoutGroup("General Menu/General Menu Buttons")]
    [BoxGroup("General Menu/General Menu Buttons/General Menu Button Colors")]
    public ColorBlock generalMenuButtonColors = ColorBlock.defaultColorBlock;


    [FoldoutGroup("Focus Menu")] public bool useFocusMenuSettings = true;
    [FoldoutGroup("Focus Menu")] public Vector2 focusMenuClosedPos = Vector2.zero;
    [FoldoutGroup("Focus Menu")] public Vector2 focusMenuOpenedPos = Vector2.zero;
    [FoldoutGroup("Focus Menu")] public Sprite focusMenuBackground;
    [FoldoutGroup("Focus Menu")] public Color focusMenuBackgroundColor;

    [FoldoutGroup("Focus Menu/Icons")] public Sprite focusMenuResetRotationButtonIcon;
    [FoldoutGroup("Focus Menu/Icons")] public Sprite focusMenuActualResetRotationButtonIcon;
    [FoldoutGroup("Focus Menu/Icons")] public Sprite focusMenuBackButtonIcon;

    [FoldoutGroup("Focus Menu/Focus Menu Buttons")]
    [BoxGroup("Focus Menu/Focus Menu Buttons/Focus Menu Button Colors")]
    public ColorBlock focusMenuButtonColors = ColorBlock.defaultColorBlock;


    [FoldoutGroup("Loading Screen")] public Sprite loadingScreenBackground;
    [FoldoutGroup("Loading Screen")] public Color loadingScreenBackgroundColor;
    [FoldoutGroup("Loading Screen")] public Sprite loadingScreenLogoSprite;
    [FoldoutGroup("Loading Screen")] public Color loadingScreenLoadingBarColor;

    [FoldoutGroup("Custom UI Containers")] public Sprite uiContainerBackground;
    [FoldoutGroup("Custom UI Containers")] public Color uiContainerBackgroundColor;


}