///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 19/03/2018
/// Author: Chloroplast Games
/// Website: http://www.chloroplastgames.com
/// Programmers: Pau Elias Soriano, David Cuenca
/// Description: Class that extends the utility and functionality of CGFMaterialEditorUtilitiesClass.
///

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

    /// \english
    /// <summary>
    /// Class that extends the utility and functionality of CGFMaterialEditorUtilitiesClass.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Clase que extiende las utilidades y funcionalidades de CGFMaterialEditorUtilitiesClass.
    /// </summary>
    /// \endspanish
public class CGFMaterialEditorUtilitiesExtendedClass : CGFMaterialEditorUtilitiesClass
{

    #region Public Variables


    #endregion


    #region Utilities

    public static string CheckRenderMode(float renderMode = 0)
    {

        string colorString = "";

        if (renderMode == 0 || renderMode == 2 || renderMode == 3)
        {

            colorString = "(RGB)";

        }
        else if (renderMode == 1)
        {

            colorString = "(RGBA)";

        }

        return colorString;

    }

    public static void BuildMaterialComponentLocker(Type component)
    {

        List<Type> existingComponents = new List<Type>();

        bool componentAdded = false;

        if (Selection.activeGameObject != null)
        {

            foreach (Component components in Selection.activeGameObject.GetComponents<Component>())
            {

                existingComponents.Add(components.GetType());

            }

        }

        foreach (Type existingComponent in existingComponents)
        {

            if (existingComponents.Contains(component))
            {

                componentAdded = true;

            }

        }

        if (componentAdded == false)
        {

            GUI.enabled = false;

        }
        else
        {

            GUI.enabled = true;

        }

    }

    #endregion


    #region Utility Methods

    /// \english
    /// <summary>
    /// Color gradient and Color fill functions builder.
    /// </summary>
    /// <param name="gradient">Gradient property.</param>
    /// <param name="topColor">Top color property.</param>
    /// <param name="center">Center property.</param>
    /// <param name="width">Width property.</param>
    /// <param name="revert">Revert property.</param>
    /// <param name="changeDirection">Change direction property.</param>
    /// <param name="rotation">Rotation property.</param>
    /// <param name="colorFill">Color fill property.</param>
    /// <param name="colorFillLevel">Color fill level property.</param>
    /// <param name="topColorFillLevel">Top color fill level property.</param>
    /// <param name="renderMode">Render mode property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de las funciones de degradado de color y relleno de color.
    /// </summary>
    /// <param name="gradient">Propiedad degradado.</param>
    /// <param name="topColor">Propiedad color superior.</param>
    /// <param name="center">Propiedad centro.</param>
    /// <param name="width">Propiedad ancho.</param>
    /// <param name="revert">Propiedad inverso.</param>
    /// <param name="changeDirection">Propiedad cambiar direccion.</param>
    /// <param name="rotation">Propiedad rotación.</param>
    /// <param name="colorFill">Propiedad relleno de color</param>
    /// <param name="colorFillLevel">Propiedad de nivel del relleno de color.</param>
    /// <param name="topColorFillLevel">Propiedad del nivel del relleno del color superior</param>
    /// <param name="renderMode">Modo de renderizado.</param>
    /// \endspanish
    public static void BuildColorGradientAndColorFill(MaterialProperty gradient, MaterialProperty topColor, MaterialProperty center, MaterialProperty width, MaterialProperty revert, MaterialProperty changeDirection, MaterialProperty rotation, MaterialProperty colorFill, MaterialProperty colorFillLevel, MaterialProperty topColorFillLevel, float renderMode = 0)
    {

        BuildHeaderWithKeyword("Color Gradient", "Color Gradient.", gradient, true);
        BuildColor("Top Color " + CheckRenderMode(renderMode), "Color of the top part of the gradient.", topColor, gradient.floatValue);
        BuildSlider("Center", "Gradient center.", center, gradient.floatValue);
        BuildFloat("Width", "Gradient width.", width, gradient.floatValue);
        BuildToggleFloat("Revert", "Revert the ortientation of the gradient.", revert, toggleLock: gradient.floatValue);
        BuildToggleFloat("Change Direction", "Change direction of the gradient.", changeDirection, toggleLock: gradient.floatValue);
        BuildSlider("Rotation", "Gradient rotation.", rotation, gradient.floatValue);

        GUILayout.Space(25);

        BuildHeaderWithKeyword("Color Fill", "Color Fill.", colorFill, true);
        BuildSlider("Color Fill Level", "Level of color fill in relation the source color.", colorFillLevel, colorFill.floatValue);
        BuildSlider("Top Color Fill Level", "Level of top color fill in relation the source color.", topColorFillLevel, colorFill.floatValue * gradient.floatValue);

        GUILayout.Space(25);

    }

    /// \english
    /// <summary>
    /// Color mask function builder. 
    /// </summary>
    /// <param name="colorMask">Color mask property.</param>
    /// <param name="colorMaskMap">Color mask map property.</param>
    /// <param name="colorMaskColor">Color mask color property.</param>
    /// <param name="colorMaskLevel">Color mask level property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// <param name="renderMode">Render mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de máscara de color.
    /// </summary>
    /// <param name="colorMask">Propiedad de máscara de color.</param>
    /// <param name="colorMaskMap">Propiedad del mapa de la máscara de color.</param>
    /// <param name="colorMaskColor">Propiedad del color de la máscara.</param>
    /// <param name="colorMaskLevel">Propiedad nivel de la máscara</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// <param name="renderMode">Modo de renderizado.</param>
    /// \endspanish
    public static void BuildColorMask(MaterialProperty colorMask, MaterialProperty colorMaskMap, MaterialProperty colorMaskColor, MaterialProperty colorMaskLevel, MaterialEditor materialEditor, bool compactMode = false, float renderMode = 0)
    {

        BuildHeaderWithKeyword("Color Mask", "Color Fill.", colorMask, true);
        BuildTexture("Color Mask Map " + CheckRenderMode(renderMode), "Mask texture.", colorMaskMap, materialEditor, true, colorMask.floatValue, compactMode);
        BuildColor("Color Mask Color " + CheckRenderMode(renderMode), "Color of the mask.", colorMaskColor, colorMask.floatValue);
        BuildSlider("Color Mask Level", "Level of color mask in relation the source color.", colorMaskLevel, colorMask.floatValue);

        GUILayout.Space(25);

    }

    /// \english
    /// <summary>
    /// Height fog builder.
    /// </summary>
    /// <param name="heightFog">Height fog property.</param>
    /// <param name="heightFogColor">Height fog color property.</param>
    /// <param name="heightFogStartPosition">Height fog start position property.</param>
    /// <param name="fogHeight">Fog height property.</param>
    /// <param name="heightFogDensity">Height fog density property.</param>
    /// <param name="useAlphaValue">Use alpha value property.</param>
    /// <param name="localHeightFog">Local height property.</param>
    /// <param name="useAlphaAndAlphaClip">Use alpha and aplha clip property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la niebla por altura.
    /// </summary>
    /// <param name="heightFog">Propiedad niebla por altura.</param>
    /// <param name="heightFogColor">Propiedad color de la niebla por altura.</param>
    /// <param name="heightFogStartPosition">Propiedad posición de inicio de la niebla por altura.</param>
    /// <param name="fogHeight">Propiedad altura de la niebla por altura.</param>
    /// <param name="heightFogDensity">Propiedad densidad de la niebla por altura</param>
    /// <param name="useAlphaValue">Propiedad uso del valor de alpha.</param>
    /// <param name="localHeightFog">Propiedad niebla por altura local.</param>
    /// <param name="useAlphaAndAlphaClip">Propiedad uso del alpha y el alpha clip.</param>
    /// \endspanish
    public static void BuildHeightFog(MaterialProperty heightFog, MaterialProperty heightFogColor, MaterialProperty heightFogStartPosition, MaterialProperty fogHeight, MaterialProperty heightFogDensity, MaterialProperty useAlphaValue, MaterialProperty localHeightFog, float useAlphaAndAlphaClip = 1)
    {

        BuildHeaderWithKeyword("Height Fog", "Fog by vertex height.", heightFog, true);
        BuildColor("Height Fog Color (RGB)", "Color of the fog.", heightFogColor, heightFog.floatValue);
        BuildFloat("Height Fog Start Position", "Start point of the fog.", heightFogStartPosition, heightFog.floatValue);
        BuildFloat("Fog Height", "Height of the fog.", fogHeight, heightFog.floatValue);
        BuildSlider("Height Fog Density", "Level of fog in relation the source color.", heightFogDensity, heightFog.floatValue);
        BuildToggleFloat("Use Alpha", "If enabled fog doesn't affect the transparent parts of the source color.", useAlphaValue, toggleLock: heightFog.floatValue * useAlphaAndAlphaClip);
        BuildToggleFloat("Local Height Fog", "If enabled the fog is created based on the center of the mesh.", localHeightFog, toggleLock: heightFog.floatValue);

        //GUILayout.Space(25);

    }

    /// \english
    /// <summary>
    /// Distance fog builder.
    /// </summary>
    /// <param name="distanceFog">Distance fog property.</param>
    /// <param name="distanceFogColor">Distance fog color property.</param>
    /// <param name="distanceFogStartPosition">Distance fog start position property.</param>
    /// <param name="distanceFogLength">Distance fog length property.</param>
    /// <param name="distanceFogDensity">Distance fog density property.</param>
    /// <param name="useAlphaValue">Use alpha value property.</param>
    /// <param name="worldDistanceFog">World distance fog property.</param>
    /// <param name="worldDistanceFogPosition">World distance fog position property.</param>
    /// <param name="useAlphaAndAlphaClip">Use alpha and aplha clip property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la niebla por distancia.
    /// </summary>
    /// <param name="distanceFog">Propiedad niebla por distancia.</param>
    /// <param name="distanceFogColor">Propiedad color de la niebla por distancia.</param>
    /// <param name="distanceFogStartPosition">Propiedad posición de inicio de la niebla por distancia.</param>
    /// <param name="distanceFogLength">Propiedad longitud de la niebla por altura.</param>
    /// <param name="distanceFogDensity">Propiedad densidad de la niebla por distancia.</param>
    /// <param name="useAlphaValue">Propiedad uso del valor de alpha.</param>
    /// <param name="worldDistanceFog">Propiedad niebla por distancia en el mundo.</param>
    /// <param name="worldDistanceFogPosition">Propiedad posición de la niebla por distancia en el mundo.</param>
    /// <param name="useAlphaAndAlphaClip">Propiedad uso del alpha y el alpha clip.</param>
    /// \endspanish
    public static void BuildDistanceFog(MaterialProperty distanceFog, MaterialProperty distanceFogColor, MaterialProperty distanceFogStartPosition, MaterialProperty distanceFogLength, MaterialProperty distanceFogDensity, MaterialProperty useAlpha, MaterialProperty worldDistanceFog, MaterialProperty worldDistanceFogPosition, float useAlphaAndAlphaClip = 1)
    {

        BuildHeaderWithKeyword("Distance Fog", "Fog by camera distance.", distanceFog, true);
        BuildColor("Distance Fog Color (RGB)", "Color of the fog.", distanceFogColor, distanceFog.floatValue);
        BuildFloat("Distance Fog Start Position", "Start point of the fog.", distanceFogStartPosition, distanceFog.floatValue);
        BuildFloat("Distance Fog Length", "Length of the fog.", distanceFogLength, distanceFog.floatValue);
        BuildSlider("Distance Fog Density", "Level of fog in relation the source color.", distanceFogDensity, distanceFog.floatValue);
        BuildToggleFloat("Use Alpha", "If enabled fog doesn't affect the transparent parts of the source color.", useAlpha, toggleLock: distanceFog.floatValue * useAlphaAndAlphaClip);
        BuildToggleFloat("World Distance Fog", "If enabled the fog is created based on a position of the world.", worldDistanceFog, toggleLock: distanceFog.floatValue);
        BuildVector3("World Distance Fog Position", "World position of the distance fog.", worldDistanceFogPosition, distanceFog.floatValue);

        //GUILayout.Space(25);

    }

    /// \english
    /// <summary>
    /// Mesh intersection function builder.
    /// </summary>
    /// <param name="meshIntersection">Mesh intersection property.</param>
    /// <param name="intersectionColor">Intersection color property.</param>
    /// <param name="intersectionTexture">Intersection texture property.</param>
    /// <param name="intersectionFalloff">Intersection falloff property.</param>
    /// <param name="intersectionDistance">Intersection distance property.</param>
    /// <param name="intersectionFill">Intersection fill property.</param>
    /// <param name="intersectionHardEdge">Intersection hard edge property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// <param name="renderMode">Render mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de la intersección de la mesh.
    /// </summary>
    /// <param name="meshIntersection">Propiedad intersección de la mesh.</param>
    /// <param name="intersectionColor">Propiedad color de la intersección.</param>
    /// <param name="intersectionTexture">Propiedad textura de la intersección.</param>
    /// <param name="intersectionFalloff">Propiedad declive de la intersección.</param>
    /// <param name="intersectionDistance">Propiedad distnacia de la intersección.</param>
    /// <param name="intersectionFill">Propiedad relleno de la intersección.</param>
    /// <param name="intersectionHardEdge">Propiedad borde afilado.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// <param name="renderMode">Modo de renderizado.</param>
    /// \endspanish
    public static void BuildMeshIntersection(MaterialProperty meshIntersection, MaterialProperty intersectionColor, MaterialProperty intersectionTexture, MaterialProperty intersectionFalloff, MaterialProperty intersectionDistance, MaterialProperty intersectionFill, MaterialProperty intersectionHardEdge, MaterialEditor materialEditor, bool compactMode = false, float renderMode = 0)
    {

        BuildHeaderWithKeyword("Mesh Intersection", "Mesh intersection detection.", meshIntersection, true);
        BuildColor("Intersection Color " + CheckRenderMode(renderMode), "Intersection color.", intersectionColor, meshIntersection.floatValue);
        BuildTexture("Intersection Texture " + CheckRenderMode(renderMode), "Texture applied on the intersection area.", intersectionTexture, materialEditor, true, meshIntersection.floatValue, compactMode);
        BuildFloat("Intersection Fallof", "Fallof value.", intersectionFalloff, meshIntersection.floatValue);
        BuildFloat("Intersection Distance", "Intersection lenght.", intersectionDistance, meshIntersection.floatValue);
        BuildToggleFloat("Intersection Fill", "If enable the color fill is applied.", intersectionFill, toggleLock: meshIntersection.floatValue);
        BuildToggleFloat("Intersection Hard Edge", "If enable creates a hard edge between the intersection area colr and the source color.", intersectionHardEdge, toggleLock: meshIntersection.floatValue);

        GUILayout.Space(25);

    }

    /// \english
    /// <summary>
    /// Light function builder.
    /// </summary>
    /// <param name="light">Light property.</param>
    /// <param name="directionalLight">Directional light property.</param>
    /// <param name="ambient">Ambient property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de ls función de luz.
    /// </summary>
    /// <param name="light">Propiedad luz.</param>
    /// <param name="directionalLight">Propiedad luz direccional.</param>
    /// <param name="ambient">Propiedad ambiente.</param>
    /// \endspanish
    public static void BuildLight(MaterialProperty light, MaterialProperty directionalLight, MaterialProperty ambient)
    {

        BuildHeaderWithKeyword("Light", "Light and Ambient light.", light, true);
        BuildToggleFloat("Directional Light", "If enabled main directional light affect to the source mesh.", directionalLight, toggleLock: light.floatValue);
        BuildToggleFloat("Ambient", "If enabled ambient light affect to the source mesh.", ambient, toggleLock: light.floatValue);

        GUILayout.Space(25);

    }

    /// \english
    /// <summary>
    /// Simulated light function builder.
    /// </summary>
    /// <param name="simulatedLight">Simulated light property.</param>
    /// <param name="simulatedLightRampTexture">Simulated light ramp texture property.</param>
    /// <param name="simulatedLightLevel">Simulated light level property.</param>
    /// <param name="simulatedLightPosition">Simulated light position property.</param>
    /// <param name="simulatedLightDistance">Simulated light distance property.</param>
    /// <param name="gradientRamp">Gradient ramp property.</param>
    /// <param name="centerColor">Center color property.</param>
    /// <param name="useExternalColor">Use external color property.</param>
    /// <param name="externalColor">External color property.</param>
    /// <param name="additiveSimulatedLight">Additive simulated light property.</param>
    /// <param name="additiveSimulatedLightLevel">Additive simulated light level property.</param>
    /// <param name="posterize">Posterize property.</param>
    /// <param name="steps">Steps property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de luz simulada.
    /// </summary>
    /// <param name="simulatedLight">Propiedad luz simulada.</param>
    /// <param name="simulatedLightRampTexture">Propiedad textura de la rampa de la luz simulada.</param>
    /// <param name="simulatedLightLevel">Propiedad nivel de la luz simulada.</param>
    /// <param name="simulatedLightPosition">Propiedad posición de la luz simulada.</param>
    /// <param name="simulatedLightDistance">Propiedad distancia de la luz simulada.</param>
    /// <param name="gradientRamp">Propiedad rampa de degradado.</param>
    /// <param name="centerColor">Propiedad color central.</param>
    /// <param name="useExternalColor">Propiedad usar color externo.</param>
    /// <param name="externalColor">Propiedad color externo.</param>
    /// <param name="additiveSimulatedLight">Propiedad luz simulada aditiva.</param>
    /// <param name="additiveSimulatedLightLevel">Propiedad nivel de la luz simulada aditiva.</param>
    /// <param name="posterize">Propiedad posterizado.</param>
    /// <param name="steps">Propiedad pasos.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildSimulatedLight(MaterialProperty simulatedLight, MaterialProperty simulatedLightRampTexture, MaterialProperty simulatedLightLevel, MaterialProperty simulatedLightPosition, MaterialProperty simulatedLightDistance, MaterialProperty gradientRamp, MaterialProperty centerColor, MaterialProperty useExternalColor, MaterialProperty externalColor, MaterialProperty additiveSimulatedLight, MaterialProperty additiveSimulatedLightLevel, MaterialProperty posterize, MaterialProperty steps, MaterialEditor materialEditor, bool compactMode = false)
    {

        BuildHeaderWithKeyword("Simulated Light", "Simulated Light.", simulatedLight, true);
        BuildTexture("Simulated Light Ramp Texture (RGB)", "Color ramp of the simulated light based on a texture. The top part of the texture is the center of the simulated light and the bottom part is the external part of the simulated light.", simulatedLightRampTexture, materialEditor, true, simulatedLight.floatValue - gradientRamp.floatValue, compactMode);
        BuildSlider("Simulated Light Level", "Level of simulated light in relation the source color.", simulatedLightLevel, simulatedLight.floatValue);
        BuildVector3("Simulated Light Position", "World position of the simulated light.", simulatedLightPosition, simulatedLight.floatValue);
        BuildFloatPositive("Simulated Light Distance", "Simulated light circunference diameter.", simulatedLightDistance, simulatedLight.floatValue);
        BuildToggleFloat("Gradient Ramp", "If enabled uses a gradient ramp between two colors instead a ramp texture.", gradientRamp, toggleLock: simulatedLight.floatValue);
        BuildColor("Center Color (RGB)", "Color of the center of the simulated light if gradient ramp is enabled.", centerColor, simulatedLight.floatValue * gradientRamp.floatValue);
        BuildToggleFloat("Use External Color", "If enabled uses a color for the external part of the light instead de source color.", useExternalColor, toggleLock: simulatedLight.floatValue * gradientRamp.floatValue);
        BuildColor("External Color (RGB)", "Color of the expernal part of the simulated light if gradient ramp is enabled.", externalColor, simulatedLight.floatValue * gradientRamp.floatValue * useExternalColor.floatValue);
        BuildToggleFloat("Additive Simulated Light", "If enabled adds the simulated light color to the source color.", additiveSimulatedLight, toggleLock: simulatedLight.floatValue);
        BuildSlider("Additive Simulated Light Level", "Level of simulated light addition in relation the source color.", additiveSimulatedLightLevel, additiveSimulatedLight.floatValue * simulatedLight.floatValue);
        BuildToggleFloat("Posterize", "If enabled converts the ramp texture or the gradient ramp to multiple regions of fewer tones.", posterize, toggleLock: simulatedLight.floatValue * gradientRamp.floatValue);
        BuildFloatPositive("Steps", "Color steps of the posterization.", steps, simulatedLight.floatValue * posterize.floatValue * gradientRamp.floatValue);

        //GUILayout.Space(25);

    }

    /// \english
    /// <summary>
    /// Lightmap function builder.
    /// </summary>
    /// <param name="lightmap">Lightmap property.</param>
    /// <param name="lightmapColor">Lightmap color property.</param>
    /// <param name="lightmapLevel">Lightmap level property.</param>
    /// <param name="shadowLevel">Shadow level property.</param>
    /// <param name="multiplyLightmap">Multiply lightmap property.</param>
    /// <param name="desaturateLightColor">Desaturate light color property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lightmap">Propiedad lightmap.</param>
    /// <param name="lightmapColor">Propiedad color del lightmap.</param>
    /// <param name="lightmapLevel">Propiedad nivel del lightmap.</param>
    /// <param name="shadowLevel">Propiedad nivel de la sombra.</param>
    /// <param name="multiplyLightmap">Propiedad multiplicar lightmap.</param>
    /// <param name="desaturateLightColor">Propiedad desaturar color de la luz.</param>
    /// \endspanish
    public static void BuildLightmap(MaterialProperty lightmap, MaterialProperty lightmapColor, MaterialProperty lightmapLevel, MaterialProperty shadowLevel, MaterialProperty multiplyLightmap, MaterialProperty desaturateLightColor)
    {

        BuildHeaderWithKeyword("Lightmap", "Lightmap.", lightmap, true);
        BuildColor("Lightmap Color (RGB)", "Color of the lightmap.", lightmapColor, lightmap.floatValue);
        BuildSlider("Lightmap Level", "Level of light of the lightmap in relation the source color.", lightmapLevel, lightmap.floatValue);
        BuildSlider("Shadow level", "Level of shadow of the lightmap in relation the source color.", shadowLevel, lightmap.floatValue);
        BuildToggleFloat("Multiyply Lightmap", "If enabled the lightmap color is multiplied by the source color.", multiplyLightmap, toggleLock: lightmap.floatValue);
        BuildToggleFloat("Desaturate Light Color", "If enabled color of the light of the lightmap is desaturated to grey scale.", desaturateLightColor, toggleLock: lightmap.floatValue);

        GUILayout.Space(25);

    }

    /// \english
    /// <summary>
    /// Sprite outline function builder.
    /// </summary>
    /// <param name="spriteOutline">Sprite outline property.</param>
    /// <param name="outlineColor">Outline color property.</param>
    /// <param name="outlineWidth">Outline width property.</param>
    /// <param name="outlineSharp">Outline sharp property.</param>
    /// <param name="innerSharp">Inner sharp property.</param>
    /// <param name="disableTopLeftOutline">Disable top left outline property.</param>
    /// <param name="disableTopOutline">Disable top outline property.</param>
    /// <param name="disableTopRightOutline">Disable top right outline property.</param>
    /// <param name="disableRightOutline">Disable right outline property.</param>
    /// <param name="disableBottomRightOutline">Disable bottom right outline property.</param>
    /// <param name="disableBottomOutline">Disable bottom outline property.</param>
    /// <param name="disableBottomLeftOutline">Disable bottom left outline property.</param>
    /// <param name="disableLeftOutline">Disable left outline property.</param>
    /// <param name="topLeftDistance">Top left distance property.</param>
    /// <param name="topDistance">Top distance property.</param>
    /// <param name="topRightDistance">Top right distance property.</param>
    /// <param name="rightDistance">Right distance property.</param>
    /// <param name="bottomRightDistance">Bottom right distance property.</param>
    /// <param name="bottomDistance">Bottom distance property.</param>
    /// <param name="bottomLeftDistance">Bottom left distance property.</param>
    /// <param name="leftDistance">Left ditance property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de borde de sprite.
    /// </summary>
    /// <param name="spriteOutline">Propiedad borde de sprite.</param>
    /// <param name="outlineColor">Propiedad color dle borde.</param>
    /// <param name="outlineWidth">Propiedad ancho del borde.</param>
    /// <param name="outlineSharp">Propiedad borde afilado.</param>
    /// <param name="innerSharp">Propiedad interior afilado.</param>
    /// <param name="disableTopLeftOutline">Propiedad desactivar borde superior izquierdo.</param>
    /// <param name="disableTopOutline">Propiedad desactivar borde superior.</param>
    /// <param name="disableTopRightOutline">Propiedad desactivar borde superior derecho.</param>
    /// <param name="disableRightOutline">Propiedad desactivar borde derecho.</param>
    /// <param name="disableBottomRightOutline">Propiedad desactivar borde inferior derecho.</param>
    /// <param name="disableBottomOutline">Propiedad desactivar borde inferior.</param>
    /// <param name="disableBottomLeftOutline">Propiedad desactivar borde inferior izquierdo.</param>
    /// <param name="disableLeftOutline">Propiedad desactivar borde izquierdo.</param>
    /// <param name="topLeftDistance">Propiedad distancia superior izquierda.</param>
    /// <param name="topDistance">Propiedad distancia superior.</param>
    /// <param name="topRightDistance">Propiedad distancia superior derecha.</param>
    /// <param name="rightDistance">Propiedad distancia derecha.</param>
    /// <param name="bottomRightDistance">Propiedad distancia inferior derecha.</param>
    /// <param name="bottomDistance">Propiedad distancia inferior.</param>
    /// <param name="bottomLeftDistance">Propiedad distancia inferior izquierda.</param>
    /// <param name="leftDistance">Propiedad distancia izquierda.</param>
    /// \endspanish
    public static void BuildSpriteOutline(MaterialProperty spriteOutline, MaterialProperty outlineColor, MaterialProperty outlineWidth, MaterialProperty outlineSharp, MaterialProperty innerSharp, MaterialProperty disableTopLeftOutline, MaterialProperty disableTopOutline, MaterialProperty disableTopRightOutline, MaterialProperty disableRightOutline, MaterialProperty disableBottomRightOutline, MaterialProperty disableBottomOutline, MaterialProperty disableBottomLeftOutline, MaterialProperty disableLeftOutline, MaterialProperty topLeftDistance, MaterialProperty topDistance, MaterialProperty topRightDistance, MaterialProperty rightDistance, MaterialProperty bottomRightDistance, MaterialProperty bottomDistance, MaterialProperty bottomLeftDistance, MaterialProperty leftDistance)
    {

        BuildHeaderWithKeyword("Sprite Outline", "Outline for sprites.", spriteOutline, true);
        BuildColor("Outline Color (RGBA)", "Color of the outline.", outlineColor, spriteOutline.floatValue);
        BuildFloatPositive("Outline Width", "Width of the outline.", outlineWidth, spriteOutline.floatValue);
        BuildToggleFloat("Outline Sharp", "Hard edge for the external part of the outline.", outlineSharp, toggleLock: spriteOutline.floatValue);
        BuildToggleFloat("Inner Sharp", "Hard edge for the internal part of the outline.", innerSharp, toggleLock: spriteOutline.floatValue);
        BuildToggleFloat("Disable Top Left Outline", "If enabled disables the top left outline.", disableTopLeftOutline, toggleLock: spriteOutline.floatValue);
        BuildToggleFloat("Disable Top Outline", "If enabled disables the top outline.", disableTopOutline, toggleLock: spriteOutline.floatValue);
        BuildToggleFloat("Disable Right Outline", "If enabled disables the right outline.", disableTopRightOutline, toggleLock: spriteOutline.floatValue);
        BuildToggleFloat("Disable Bottom Right Outline", "If enabled disables the bottom right outline.", disableRightOutline, toggleLock: spriteOutline.floatValue);
        BuildToggleFloat("Disable Right Outline", "If enabled disables the right outline.", disableBottomRightOutline, toggleLock: spriteOutline.floatValue);
        BuildToggleFloat("Disable Bottom Outline", "If enabled disables the bottom outline.", disableBottomOutline, toggleLock: spriteOutline.floatValue);
        BuildToggleFloat("Disable Bottom Left Outline", "If enabled disables the bottom left outline.", disableBottomLeftOutline, toggleLock: spriteOutline.floatValue);
        BuildToggleFloat("Disable Left Outline", "If enabled disables the left outline.", disableLeftOutline, toggleLock: spriteOutline.floatValue);
        BuildVector3("Top Left Distance", "Distance of top left outline.", topLeftDistance, spriteOutline.floatValue);
        BuildVector3("Top Distance", "Distance of top outline.", topDistance, spriteOutline.floatValue);
        BuildVector3("Top Right Distance", "Distance of top right outline.", topRightDistance, spriteOutline.floatValue);
        BuildVector3("Right Distance", "Distance of right outline.", rightDistance, spriteOutline.floatValue);
        BuildVector3("Bottom Right Distance", "Distance of bottom right outline.", bottomRightDistance, spriteOutline.floatValue);
        BuildVector3("Bottom Distance", "Distance of bottom outline.", bottomDistance, spriteOutline.floatValue);
        BuildVector3("Bottom Left Distance", "Distance of bottom left outline.", bottomLeftDistance, spriteOutline.floatValue);
        BuildVector3("Left Distance", "Distance of left outline.", leftDistance, spriteOutline.floatValue);

        GUILayout.Space(25);

    }

    /// \english
    /// <summary>
    /// Sprite pixel outline function builder.
    /// </summary>
    /// <param name="spritePixelOutline">Sprite pixel outline property.</param>
    /// <param name="pixelOutlineColor">Pixel outline color property.</param>
    /// <param name="pixelOutlineWidth">Pixel outline width property.</param>
    /// <param name="pixelOutlineReverse">Pixel outline reverse property.</param>
    /// <param name="outerPixelOutline">Outer pixel outline property.</param>
    /// <param name="discardTransparentPixels">Discard transparent pixels property.</param>
    /// <param name="disableTopLeftPixelOutline">Disable top left pixel outline property.</param>
    /// <param name="disableTopPixelOutline">Disable top pixel outline property.</param>
    /// <param name="disableTopRightPixelOutline">Disable top right pixel outline property.</param>
    /// <param name="disableRightPixelOutline">Disable right pixel outline property.</param>
    /// <param name="disableBottomRightPixelOutline">Disable bottom right pixel outline property.</param>
    /// <param name="disableBottomPixelOutline">Disable bottom pixel outline property.</param>
    /// <param name="disableBottomLeftPixelOutline">Disable bottom left pixel outline property.</param>
    /// <param name="disableLeftPixelOutline">Disable left pixel outline property.</param>
    /// <param name="disablePixelTopLeftDistance">Disable top left pixel outline property.</param>
    /// <param name="pixelOutlineTopDistance">Pixel outline top distance property.</param>
    /// <param name="pixelOutlineTopRightDistance">Pixel outline top right distance property.</param>
    /// <param name="pixelOutlineRightDistance">Pixel outline right distance property.</param>
    /// <param name="pixelOutlineBottomRightDistance">Pixel outline bottom right distance property.</param>
    /// <param name="pixelOutlineBottomDistance">Pixel outline bottom distance property.</param>
    /// <param name="pixelOutlineBottomLeftDistance">Pixel outline bottom left distance property.</param>
    /// <param name="pixelOutlineLeftDistance">Pixel outline left distance property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de borde pixel de sprite.
    /// </summary>
    /// <param name="spritePixelOutline">Propiedad borde pixel de sprite.</param>
    /// <param name="pixelOutlineColor">Propiedad color del borde pixel.</param>
    /// <param name="pixelOutlineWidth">Propiedad ancho del borde pixel.</param>
    /// <param name="pixelOutlineReverse">Propiedad borde invertido pixel.</param>
    /// <param name="outerPixelOutline">Propiedad borde externo pixel.</param>
    /// <param name="discardTransparentPixels">Propiedad descartar píxeles transparentes</param>
    /// <param name="disableTopLeftPixelOutline">Propiedad desactivar borde pixel superior izquierdo.</param>
    /// <param name="disableTopPixelOutline">Propiedad desactivar borde pixel superior.</param>
    /// <param name="disableTopRightPixelOutline">Propiedad desactivar borde pixel superior derecho.</param>
    /// <param name="disableRightPixelOutline">Propiedad desactivar borde pixel derecho.</param>
    /// <param name="disableBottomRightPixelOutline">Propiedad desactivar borde pixel inferior derecho.</param>
    /// <param name="disableBottomPixelOutline">Propiedad desactivar borde pixel inferior.</param>
    /// <param name="disableBottomLeftPixelOutline">Propiedad desactivar borde pixel inferior izquierdo.</param>
    /// <param name="disableLeftPixelOutline">Propiedad desactivar borde pixel superior.</param>
    /// <param name="disablePixelTopLeftDistance">Propiedad desactivar borde pixel superior izquierdo.</param>
    /// 
    /// <param name="pixelOutlineTopDistance">Propiedad distancia superior.</param>
    /// <param name="pixelOutlineTopRightDistance">Propiedad distancia superior derecha.</param>
    /// <param name="pixelOutlineRightDistance">Propiedad distancia derecha.</param>
    /// <param name="pixelOutlineBottomRightDistance">Propiedad distancia inferior derecha.</param>
    /// <param name="pixelOutlineBottomDistance">Propiedad distancia inferior.</param>
    /// <param name="pixelOutlineBottomLeftDistance">Propiedad distancia inferior izquierda.</param>
    /// <param name="pixelOutlineLeftDistance">Propiedad distancia izquierda.</param>
    /// \endspanish
    public static void BuildSpritePixelOutline(MaterialProperty spritePixelOutline, MaterialProperty pixelOutlineColor, MaterialProperty pixelOutlineWidth, MaterialProperty pixelOutlineReverse, MaterialProperty outerPixelOutline, MaterialProperty discardTransparentPixels, MaterialProperty disableTopLeftPixelOutline, MaterialProperty disableTopPixelOutline, MaterialProperty disableTopRightPixelOutline, MaterialProperty disableRightPixelOutline, MaterialProperty disableBottomRightPixelOutline, MaterialProperty disableBottomPixelOutline, MaterialProperty disableBottomLeftPixelOutline, MaterialProperty disableLeftPixelOutline, MaterialProperty disablePixelTopLeftDistance, MaterialProperty pixelOutlineTopDistance, MaterialProperty pixelOutlineTopRightDistance, MaterialProperty pixelOutlineRightDistance, MaterialProperty pixelOutlineBottomRightDistance, MaterialProperty pixelOutlineBottomDistance, MaterialProperty pixelOutlineBottomLeftDistance, MaterialProperty pixelOutlineLeftDistance)
    {

        BuildHeaderWithKeyword("Sprite Pixel Outline", "Pixel art outline for sprites.", spritePixelOutline, true);
        BuildColor("Outline Color (RGBA)", "Color of the outline.", pixelOutlineColor, spritePixelOutline.floatValue);
        BuildFloatPositive("Outline Width", "Width of the outline.", pixelOutlineWidth, spritePixelOutline.floatValue);
        BuildToggleFloat("Outline Reverse", "If enabled outlines is created from center of the sprite", pixelOutlineReverse, toggleLock: spritePixelOutline.floatValue);
        BuildToggleFloat("Outer Outline", "If enabled outlines is created from external part of the sprite.", outerPixelOutline, toggleLock: spritePixelOutline.floatValue);
        BuildToggleFloat("Discard Transparent Pixels", "If enabled outline doesn't affect to non opaque pixels.", discardTransparentPixels, toggleLock: spritePixelOutline.floatValue);
        BuildToggleFloat("Disable Top Left Outline", "If enabled disables the top left outline.", disableTopLeftPixelOutline, toggleLock: spritePixelOutline.floatValue);
        BuildToggleFloat("Disable Top Outline", "If enabled disables the top outline.", disableTopPixelOutline, toggleLock: spritePixelOutline.floatValue);
        BuildToggleFloat("Disable Right Outline", "If enabled disables the right outline.", disableTopRightPixelOutline, toggleLock: spritePixelOutline.floatValue);
        BuildToggleFloat("Disable Bottom Right Outline", "If enabled disables the bottom right outline.", disableRightPixelOutline, toggleLock: spritePixelOutline.floatValue);
        BuildToggleFloat("Disable Right Outline", "If enabled disables the right outline.", disableBottomRightPixelOutline, toggleLock: spritePixelOutline.floatValue);
        BuildToggleFloat("Disable Bottom Outline", "If enabled disables the bottom outline.", disableBottomPixelOutline, toggleLock: spritePixelOutline.floatValue);
        BuildToggleFloat("Disable Bottom Left Outline", "If enabled disables the bottom left outline.", disableBottomLeftPixelOutline, toggleLock: spritePixelOutline.floatValue);
        BuildToggleFloat("Disable Left Outline", "If enabled disables the left outline.", disableLeftPixelOutline, toggleLock: spritePixelOutline.floatValue);
        BuildVector3("Top Left Distance", "Distance of top left outline.", disablePixelTopLeftDistance, spritePixelOutline.floatValue);
        BuildVector3("Top Distance", "Distance of top outline.", pixelOutlineTopDistance, spritePixelOutline.floatValue);
        BuildVector3("Top Right Distance", "Distance of top right outline.", pixelOutlineTopRightDistance, spritePixelOutline.floatValue);
        BuildVector3("Right Distance", "Distance of right outline.", pixelOutlineRightDistance, spritePixelOutline.floatValue);
        BuildVector3("Bottom Right Distance", "Distance of bottom right outline.", pixelOutlineBottomRightDistance, spritePixelOutline.floatValue);
        BuildVector3("Bottom Distance", "Distance of bottom outline.", pixelOutlineBottomDistance, spritePixelOutline.floatValue);
        BuildVector3("Bottom Left Distance", "Distance of bottom left outline.", pixelOutlineBottomLeftDistance, spritePixelOutline.floatValue);
        BuildVector3("Left Distance", "Distance of left outline.", pixelOutlineLeftDistance, spritePixelOutline.floatValue);

        GUILayout.Space(25);

    }

    /// \english
    /// <summary>
    /// Color adjustment fucntion builder.
    /// </summary>
    /// <param name="colorAdjustment">Color adjustment property.</param>
    /// <param name="hue">Hue property.</param>
    /// <param name="saturation">Saturation property.</param>
    /// <param name="value">Value property.</param>
    /// <param name="colorAdjustmentMaskMap">Color adjustment mask map property.</param>
    /// <param name="colorAdjustmentMaskLevel">Color adjustment mask level property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de ajuste de color.
    /// </summary>
    /// <param name="colorAdjustment">Propiedad ajuste de color.</param>
    /// <param name="hue">Propiedad matiz.</param>
    /// <param name="saturation">Propiedad saturación.</param>
    /// <param name="value">Propiedad brillo.</param>
    /// <param name="colorAdjustmentMaskMap">Propiedad mapa de la máscara de ajuste de color.</param>
    /// <param name="colorAdjustmentMaskLevel">Propiedad nivel de la máscara de ajuste de color.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildSpriteColorAdjustment(MaterialProperty colorAdjustment, MaterialProperty hue, MaterialProperty saturation, MaterialProperty value, MaterialProperty colorAdjustmentMaskMap, MaterialProperty colorAdjustmentMaskLevel, MaterialEditor materialEditor, bool compactMode = false)
    {

        BuildHeaderWithKeyword("Sprite Color Adjustment", "Color adjustment for sprite shaders.", colorAdjustment, true);
        BuildSlider("Hue", "Color.", hue, colorAdjustment.floatValue);
        BuildSlider("Saturation", "Color quantity.", saturation, colorAdjustment.floatValue);
        BuildSlider("Value", "Brightness of the color.", value, colorAdjustment.floatValue);
        BuildTexture("Color Adjustment Mask Map (RGBA)", "Mask texture.", colorAdjustmentMaskMap, materialEditor, true, colorAdjustment.floatValue, compactMode);
        BuildSlider("Color Adjustment Mask Level", "Level of mask effect in relation the source color.", colorAdjustmentMaskLevel, colorAdjustment.floatValue);

        GUILayout.Space(25);

    }

    /// \english
    /// <summary>
    /// UV scroll function builder.
    /// </summary>
    /// <param name="uvScroll">UV scroll property.</param>
    /// <param name="flipUVHorizontal">Flip UV horizontal property.</param>
    /// <param name="flipUVVertical">Flip UV vertical property.</param>
    /// <param name="uvScrollAnimation">UV scroll animation property.</param>
    /// <param name="uvScrollSpeed">UV scroll speed property.</param>
    /// <param name="scrollByTexel">Scroll by texel property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// UV scroll constructor.
    /// </summary>
    /// <param name="uvScroll">Propiedad UV scroll.</param>
    /// <param name="flipUVHorizontal">Propiedad voltear UV horizontalmente.</param>
    /// <param name="flipUVVertical">Propiedad voltear UV verticalmente.</param>
    /// <param name="uvScrollAnimation">Propiedad animación de desplazamiento de las UV.</param>
    /// <param name="uvScrollSpeed">Propiedad velocidad de desplazamiento de las UV.</param>
    /// <param name="scrollByTexel">Propiedad desplazamiento por texel.</param>
    /// \endspanish
    public static void BuildUVScroll(MaterialProperty uvScroll, MaterialProperty flipUVHorizontal, MaterialProperty flipUVVertical, MaterialProperty uvScrollAnimation, MaterialProperty uvScrollSpeed, MaterialProperty scrollByTexel)
    {

        BuildHeaderWithKeyword("UV Scroll", "Scroll and Flip the UVs from a texture.", uvScroll, true);
        BuildToggleFloat("Flip UV Horizontal", "Flip UV Horizontal.", flipUVHorizontal, toggleLock: uvScroll.floatValue);
        BuildToggleFloat("Flip UV Vertical", "Flip UV Vertical.", flipUVVertical, toggleLock: uvScroll.floatValue);
        BuildToggleFloat("UV Scroll Animation", "If enabled the UV they animated.", uvScrollAnimation, toggleLock: uvScroll.floatValue);
        BuildVector2("UV Scroll Speed", "UV Scroll Speed.", uvScrollSpeed, uvScroll.floatValue);
        BuildToggleFloat("Scroll By Texel", "Scroll animation texel by texel.", scrollByTexel, toggleLock: uvScroll.floatValue);

        GUILayout.Space(25);

    }

    /// \english
    /// <summary>
    /// Distortion function builder.
    /// </summary>
    /// <param name="distortionMap">Distortion map property.</param>
    /// <param name="distortionLevel">distortion level property.</param>
    /// <param name="distortionScale">Distortion scale property.</param>
    /// <param name="distortionMask">Distortion mask property.</param>
    /// <param name="useUVScroll">Use UV scroll property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de distorsión.
    /// </summary>
    /// <param name="distortionMap">Propiedad mapa de distorsión.</param>
    /// <param name="distortionLevel">Propiedad nivel de distorsión.</param>
    /// <param name="distortionScale">Propiedad escala de la distorsión.</param>
    /// <param name="distortionMask">Propiedad máscara de la distorsión.</param>
    /// <param name="useUVScroll">Propiedad uso del desplazamiento de UV.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildDistortion(MaterialProperty distortionMap, MaterialProperty distortionLevel, MaterialProperty distortionScale, MaterialProperty distortionMask, MaterialProperty useUVScroll, MaterialEditor materialEditor, bool compactMode = false)
    {

        BuildHeader("Distortion", "Distortion effect.");
        BuildTexture("Distortion Map (RG)", "Distortion Map (RG).", distortionMap, materialEditor, true, compactMode);
        BuildSlider("Distortion Level", "Distortion Level.", distortionLevel);
        BuildSlider("Distortion Scale", "Distortion Scale.", distortionScale);
        BuildTexture("Distortion Mask (RGB)", "Distortion Mask (RGB).", distortionMask, materialEditor, true, compactMode);
        BuildToggleFloat("Use UV Scroll", "Use UV Scroll.", useUVScroll);

        GUILayout.Space(25);

    }

    /// \english
    /// <summary>
    /// Projector function builder.
    /// </summary>
    /// <param name="shadowMap">Shadow map property.</param>
    /// <param name="shadowMapTilling">Shadow map tilling property.</param>
    /// <param name="shadowMapOffset">Shadow map offset property.</param>
    /// <param name="shadowColor">Sadow color property.</param>
    /// <param name="shadowLevel">Shadow level property.</param>
    /// <param name="falloffMap">Falloff map property.</param>
    /// <param name="backfaceCulling">Backface culling property.</param>
    /// <param name="useVertexPosition">property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Compact mode.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de proyector.
    /// </summary>
    /// <param name="shadowMap">Propiedad mapa de sombra.</param>
    /// <param name="shadowMapTilling">Propiedad repetición del mapa de sombra.</param>
    /// <param name="shadowMapOffset">Propiedad desplazamiento del mapa de sombra.</param>
    /// <param name="shadowColor">Propiedad color de la sombra.</param>
    /// <param name="shadowLevel">Propiedad nivel de la sombra.</param>
    /// <param name="falloffMap">Propiedad mapa de declive.</param>
    /// <param name="backfaceCulling">Propiedad oclisión de la cara trasera.</param>
    /// <param name="useVertexPosition">Propiedad uso de la posición de los vértices.</param>
    /// <param name="materialEditor">Material editor</param>
    /// <param name="compactMode">Modo compacto.</param>
    /// \endspanish
    public static void BuildProjector(MaterialProperty shadowMap, MaterialProperty shadowMapTilling, MaterialProperty shadowMapOffset, MaterialProperty shadowColor, MaterialProperty shadowLevel, MaterialProperty falloffMap, MaterialProperty backfaceCulling, MaterialProperty useVertexPosition, MaterialEditor materialEditor, bool compactMode = false)
    {

        BuildHeader("Projector", "Projector functionalities.");
        BuildTexture("Cookie (RGB)", "Projection texture. Only uses the RGB channels.", shadowMap, materialEditor, true, compactMode);
        BuildVector2("Cookie Tilling", "Scale of the UV of the texture of the projection.", shadowMapTilling);
        BuildVector2("Cookie Offset", "Offset of the UV of the texture of the projection.", shadowMapOffset);
        BuildColor("Cookie Color (RGB)", "Color of the projection. Only uses the RGB channels.", shadowColor);
        BuildSlider("Cookie Level", "Projection level intensity.", shadowLevel);
        BuildTexture("Falloff Map (RGB)", "Texture that determines the fading of the projection along its trajectory, it is a linear gradient texture. Only uses the RGB channels.", falloffMap, materialEditor, false, compactMode);
        BuildToggleFloat("Backface Culling", "If enabled cull the projection on the backfaces of the mesh.", backfaceCulling);
        BuildToggleFloat("Use Vertex Position", "If enabled use the vertex position instead the vertex normal to cull the backfaces of the mesh.", useVertexPosition, toggleLock: backfaceCulling.floatValue);

        GUILayout.Space(25);

    }

    /// \english
    /// <summary>
    /// Particle Unlit properties builder.
    /// </summary>
    /// <param name="texture">Texture property.</param>
    /// <param name="useAlphaClip">Use alpha clip property.</param>
    /// <param name="cutoff">Cutoff property.</param>
    /// <param name="color">Color property.</param>
    /// <param name="materialEditor">Material editor</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de las propiedades de partícula unlit.
    /// </summary>
    /// <param name="texture">Propiedad textura.</param>
    /// <param name="useAlphaClip">Propiedad utilizar descarte por canal alpha.</param>
    /// <param name="cutoff">Propiedad descarte.</param>
    /// <param name="color">Propiedad color.</param>
    /// <param name="materialEditor">Material editor</param>
    /// \endspanish
    public static void BuildParticleUnlit(MaterialProperty texture, MaterialProperty useAlphaClip, MaterialProperty cutoff, MaterialProperty color, MaterialEditor materialEditor)
    {

        BuildHeader("Particle Unlit", "Particle Unlit functionalities.");
        CGFMaterialEditorUtilitiesClass.BuildTexture("Particle Texture (RGBA)", "Texture of the particle.", texture, materialEditor, false);
        CGFMaterialEditorUtilitiesClass.BuildKeyword("Use Alpha Clip", "Enables Alpha Clip.", useAlphaClip, true);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Alpha cutoff", "Alpha Cutoff value.", cutoff, useAlphaClip.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildColor("Color (RGBA)", "Main color.", color);

        GUILayout.Space(25);

    }

    /// \english
    /// <summary>
    /// Camera fading function builder.
    /// </summary>
    /// <param name="cameraFading">Camera fading property.</param>
    /// <param name="cameraFadingNearPoint">Camera fading near point property.</param>
    /// <param name="cameraFadingFarPoint">Camera fading far point property.</param>
    /// <param name="opacityClip">Opaciry clip property.</param>
    /// <param name="opacityClipThreshold">Opaciry clip threshold property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de desvanecimiento por la cámara.
    /// </summary>
    /// <param name="cameraFading">Propiedad desvanecimiento por la cámara.</param>
    /// <param name="cameraFadingNearPoint">Propiedad punto inicial del desvanecimiento.</param>
    /// <param name="cameraFadingFarPoint">Propiedad punto final del desvanecimiento.</param>
    /// <param name="opacityClip">Propiedad descarte por opacidad.</param>
    /// <param name="opacityClipThreshold">Propiedad umbral de descarte por opacidad.</param>
    /// \endspanish
    public static void BuildCameraFading(MaterialProperty cameraFading, MaterialProperty cameraFadingNearPoint, MaterialProperty cameraFadingFarPoint, MaterialProperty opacityClip, MaterialProperty opacityClipThreshold)
    {

        BuildHeaderWithKeyword("Camera Fading", "Opacity by camera distance.", cameraFading, true);
        BuildFloat("Camera Fading Near Point", "Start point of the fading. Offset to position camera.", cameraFadingNearPoint, toggleLock: cameraFading.floatValue);
        BuildFloat("Camera Fading Far Point", "End point of the fading.", cameraFadingFarPoint, toggleLock: cameraFading.floatValue);
        BuildToggleFloat("Opacity Clip", "If enabled clips the mesh with less opacity than the opacity clip threshold.", opacityClip, toggleLock: cameraFading.floatValue);
        BuildSlider("Opacity Clip Threshold", "Opacity clip threshold.", opacityClipThreshold, cameraFading.floatValue * opacityClip.floatValue);

        //GUILayout.Space(25);

    }

    /// \english
    /// <summary>
    /// Soft particles function builder.
    /// </summary>
    /// <param name="softParticles">Soft particles property.</param>
    /// <param name="fadeDistance">Fade distance property.</param>
    /// <param name="fadeFalloff">Fade falloff property.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor de la función de partículas suaves.
    /// </summary>
    /// <param name="softParticles">Propiedad Soft particles.</param>
    /// <param name="fadeDistance">Propiedad distancia del desvanecimiento.</param>
    /// <param name="fadeFalloff">Propiedad declive del desvanecimiento.</param>
    /// \endspanish
    public static void BuildSoftParticles(MaterialProperty softParticles, MaterialProperty fadeDistance, MaterialProperty fadeFalloff)
    {

        BuildHeaderWithKeyword("Soft Particles", "Fade out particles when they get close to the surface of objects.", softParticles, true);
        BuildFloat("Fade Distance", "Fade lenght.", fadeDistance, toggleLock: softParticles.floatValue);
        BuildFloat("Fade Falloff", "Fallof value.", fadeFalloff, toggleLock: softParticles.floatValue);

        GUILayout.Space(25);

    }

    /// \english
    /// <summary>
    /// Build of the toggle to manage visibility of a gizmo.
    /// </summary>
    /// <param name="enable">Initial status.</param>
    /// <param name="text">Property text.</param>
    /// <param name="description">Property description.</param>
    /// <param name="propertyGizmo">Property that locks the property.</param>
    /// <returns>Compact mode status.</returns>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor del toggle que gestiona la visibilidad de un gizmo.
    /// </summary>
    /// <param name="enable">Estado inicial.</param>
    /// <param name="text">Texto de la propiedad.</param>
    /// <param name="description">Descripción de la propiedad.</param>
    /// <param name="propertyGizmo">Propiedad que bloquea la propiedad.</param>
    /// <returns>Estado del modo compacto.</returns>
    /// \endspanish
    public static bool BuildShowGizmo(bool enable, string text, string description, MaterialProperty propertyGizmo)
    {
        bool showGizmoTemp = enable;

        bool showGizmo = EditorGUILayout.Toggle(new GUIContent(text, description), enable);

        if (showGizmoTemp != showGizmo)
        {

            if (propertyGizmo.floatValue == 0)
            {

                propertyGizmo.floatValue = 1;

                propertyGizmo.floatValue = 0;

            }
            else
            {

                propertyGizmo.floatValue = 0;

                propertyGizmo.floatValue = 1;

            }

        }

        GUILayout.Space(25);

        return showGizmo;

    }

    /// \english
    /// <summary>
    /// Build of the toggle to manage visibility of a gizmo.
    /// </summary>
    /// <param name="enable">Initial status.</param>
    /// <param name="text">Property text.</param>
    /// <param name="description">Property description.</param>
    /// <param name="toggleLock">Boolean that locks the property.</param>
    /// <param name="propertyGizmo">Property that locks the property.</param>
    /// <returns>Compact mode status.</returns>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Constructor del toggle que gestiona la visibilidad de un gizmo.
    /// </summary>
    /// <param name="enable">Estado inicial.</param>
    /// <param name="text">Texto de la propiedad.</param>
    /// <param name="description">Descripción de la propiedad.</param>
    /// <param name="toggleLock">Float que bloquea la propiedad.</param>
    /// <param name="propertyGizmo">Propiedad que bloquea la propiedad.</param>
    /// <returns>Estado del modo compacto.</returns>
    /// \endspanish
    public static bool BuildShowGizmo(bool enable, string text, string description, float toggleLock, MaterialProperty propertyGizmo)
    {
        bool showGizmoTemp = enable;

        if (toggleLock == 1)
        {

            GUI.enabled = true;

        }
        else
        {

            GUI.enabled = false;

        }

        bool showGizmo = EditorGUILayout.Toggle(new GUIContent(text, description), enable);


        if (showGizmoTemp != showGizmo)
        {

            if (propertyGizmo.floatValue == 0)
            {

                propertyGizmo.floatValue = 1;

                propertyGizmo.floatValue = 0;

            }
            else
            {

                propertyGizmo.floatValue = 0;

                propertyGizmo.floatValue = 1;

            }

        }

        GUILayout.Space(25);

        GUI.enabled = true;

        return showGizmo;

    }

    /// \english
    /// <summary>
    /// Draw a height fog position handle.
    /// </summary>
    /// <param name="enableProperty">Property that enables the handle.</param>
    /// <param name="startPosition">Handle position.</param>
    /// <param name="height">Handle position of height.</param>
    /// <param name="localHeightFog">Show the handles of local height fog.</param>
    /// <param name="showHandle">Show the handles.</param>
    /// <param name="editor">Editor of the selected gameobject.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Dibuja un controlador de posición de la niebla por altura.
    /// </summary>
    /// <param name="enableProperty">Propiedad que activa el controlador.</param>
    /// <param name="startPosition">Posición del controlador.</param>
    /// <param name="height">Posición del controlador de la altura.</param>
    /// <param name="localHeightFog">Muestra los controladores de la niebla local por altura.</param>
    /// <param name="showHandle">Muestra los controladores.</param>
    /// <param name="editor">Editor del gameobject seleccionado.</param>
    /// \endspanish
    public static void DrawHeightFogArrowHandle(MaterialProperty enableProperty, MaterialProperty startPosition, MaterialProperty height, MaterialProperty localHeightFog, bool showHandle, Editor editor)
    {

        EditorGUI.BeginChangeCheck();
        {

            Vector3 startPositionHandlePosition = Vector3.zero;

            Vector3 heightHandlePosition;

            if (Selection.activeTransform != null)
            {

                Vector3 activeTransformPosition = Selection.activeTransform.position;

                Vector3 activeTransformLocalScale = Selection.activeTransform.localScale;

                if (showHandle & enableProperty.floatValue == 1)
                {

                    if (localHeightFog.floatValue == 1)
                    {

                        float localStartPosition = activeTransformPosition.y + startPosition.floatValue * activeTransformLocalScale.y;

                        startPositionHandlePosition = Handles.PositionHandle(new Vector3(activeTransformPosition.x, localStartPosition, activeTransformPosition.z), Quaternion.identity);

                        float localHeightPosition = localStartPosition + height.floatValue * activeTransformLocalScale.y;

                        heightHandlePosition = Handles.PositionHandle(new Vector3(activeTransformPosition.x, localHeightPosition, activeTransformPosition.z), Quaternion.identity);

                        Handles.DrawDottedLine(startPositionHandlePosition, heightHandlePosition, 3);

                        if (EditorGUI.EndChangeCheck())
                        {

                            startPosition.floatValue = (startPositionHandlePosition.y - activeTransformPosition.y) / activeTransformLocalScale.y;

                            height.floatValue = (heightHandlePosition.y - localStartPosition) / activeTransformLocalScale.y;

                            editor.Repaint();

                            editor.serializedObject.ApplyModifiedProperties();

                        }

                    }
                    else
                    {

                        startPositionHandlePosition = Handles.PositionHandle(new Vector3(activeTransformPosition.x, startPosition.floatValue, activeTransformPosition.z), Quaternion.identity);

                        heightHandlePosition = Handles.PositionHandle(new Vector3(activeTransformPosition.x, startPosition.floatValue + height.floatValue, activeTransformPosition.z), Quaternion.identity);


                        Handles.DrawDottedLine(startPositionHandlePosition, heightHandlePosition, 3);

                        if (EditorGUI.EndChangeCheck())
                        {

                            startPosition.floatValue = startPositionHandlePosition.y;

                            height.floatValue = heightHandlePosition.y - startPosition.floatValue;

                            editor.Repaint();

                            editor.serializedObject.ApplyModifiedProperties();

                        }

                    }
                }
            }
        }

    }

    /// \english
    /// <summary>
    /// Draw distance fog sphere handle.
    /// </summary>
    /// <param name="enableProperty">Property that enables the handle.</param>
    /// <param name="startPosition">Handle position.</param>
    /// <param name="length">Handle radius.</param>
    /// <param name="worldDistanceFog">Show the handles of world distance fog.</param>
    /// <param name="worldDistanceFogPosition">Position of handle position of world distance fog.</param>
    /// <param name="showHandle">Show the handles.</param>
    /// <param name="editor">Editor of the selected gameobject.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Dibuja un controlador esférico de la niebla por distancia.
    /// </summary>
    /// <param name="enableProperty">Propiedad que activa el controlador.</param>
    /// <param name="startPosition">Posición del controlador.</param>
    /// <param name="length">Radio del controlador.</param>
    /// <param name="worldDistanceFog">Muestra los contorladores de la niebla de distancia del mundo.</param>
    /// <param name="worldDistanceFogPosition">Posición del controlador de posicion de la niebla de distancia del mundo.</param>
    /// <param name="showHandle">Muestra los controladores.</param>
    /// <param name="editor">Editor del gameobject seleccionado.</param>
    /// \endspanish
    public static void DrawDistanceFogSphereHandle(MaterialProperty enableProperty, MaterialProperty startPosition, MaterialProperty length, MaterialProperty worldDistanceFog, MaterialProperty worldDistanceFogPosition, bool showHandle, Editor editor)
    {

        EditorGUI.BeginChangeCheck();

        Vector3 handleWorldPosition = Vector3.zero;

        float startPositionHandleRadius;

        float lengthHandleRadius;

        if (showHandle & enableProperty.floatValue == 1)
        {

            if (worldDistanceFog.floatValue == 1)
            {

                handleWorldPosition = Handles.PositionHandle(worldDistanceFogPosition.vectorValue, Quaternion.identity);

                Handles.color = Color.blue;

                startPositionHandleRadius = Handles.RadiusHandle(Quaternion.identity, worldDistanceFogPosition.vectorValue, startPosition.floatValue);

                Handles.color = Color.red;

                lengthHandleRadius = Handles.RadiusHandle(Quaternion.identity, worldDistanceFogPosition.vectorValue, length.floatValue);

                if (EditorGUI.EndChangeCheck())
                {

                    worldDistanceFogPosition.vectorValue = handleWorldPosition;

                    startPosition.floatValue = startPositionHandleRadius;

                    length.floatValue = lengthHandleRadius;

                    editor.Repaint();

                    editor.serializedObject.ApplyModifiedProperties();

                }

            }
            else
            {

                Handles.color = Color.blue;

                startPositionHandleRadius = Handles.RadiusHandle(Quaternion.identity, Camera.main.transform.position, startPosition.floatValue);

                Handles.color = Color.red;

                lengthHandleRadius = Handles.RadiusHandle(Quaternion.identity, Camera.main.transform.position, length.floatValue);

                if (EditorGUI.EndChangeCheck())
                {

                    startPosition.floatValue = startPositionHandleRadius;

                    length.floatValue = lengthHandleRadius;

                    editor.Repaint();

                    editor.serializedObject.ApplyModifiedProperties();

                }

            }

        }

    }

    /// \english
    /// <summary>
    /// Draw camera fading sphere handle.
    /// </summary>
    /// <param name="enableProperty">Property that enables the handle.</param>
    /// <param name="nearPoint">Handle position.</param>
    /// <param name="farPoint">Handle radius.</param>
    /// <param name="showHandle">Show the handles.</param>
    /// <param name="editor">Editor of the selected gameobject.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Dibuja un controlador esférico del desvanecimiento por la cámara.
    /// </summary>
    /// <param name="enableProperty">Propiedad que activa el controlador.</param>
    /// <param name="nearPoint">Posición del controlador.</param>
    /// <param name="farPoint">Radio del controlador.</param>
    /// <param name="showHandle">Muestra los controladores.</param>
    /// <param name="editor">Editor del gameobject seleccionado.</param>
    /// \endspanish
    public static void DrawCameraFadingSphereHandle(MaterialProperty enableProperty, MaterialProperty nearPoint, MaterialProperty farPoint, bool showHandle, Editor editor)
    {

        EditorGUI.BeginChangeCheck();

        float nearPointHandleRadius;

        float farPointHandleRadius;

        if (showHandle & enableProperty.floatValue == 1)
        {

            Handles.color = Color.blue;

            nearPointHandleRadius = Handles.RadiusHandle(Quaternion.identity, Camera.main.transform.position, nearPoint.floatValue);

            Handles.color = Color.red;

            farPointHandleRadius = Handles.RadiusHandle(Quaternion.identity, Camera.main.transform.position, farPoint.floatValue);

            if (EditorGUI.EndChangeCheck())
            {

                nearPoint.floatValue = nearPointHandleRadius;

                farPoint.floatValue = farPointHandleRadius;

                editor.Repaint();

                editor.serializedObject.ApplyModifiedProperties();

            }

        }

    }

    /// \english
    /// <summary>
    /// Draw a sphere handle.
    /// </summary>
    /// <param name="enableProperty">Property that enables the handle.</param>
    /// <param name="position">Handle position.</param>
    /// <param name="radius">Handle radius.</param>
    /// <param name="color">Handle color.</param>
    /// <param name="showRadiusHandle">Show the radius handle.</param>
    /// <param name="showPositionHandle">Show the position handle.</param>
    /// <param name="editor">Editor of the selected gameobject.</param>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Dibuja un controlador esférico.
    /// </summary>
    /// <param name="enableProperty">Propiedad que activa el controlador.</param>
    /// <param name="position">Posición del controlador.</param>
    /// <param name="radius">Radio del controlador</param>
    /// <param name="color">Color del controlador.</param>
    /// <param name="showRadiusHandle">Muestra el controlador de radio.</param>
    /// <param name="showPositionHandle">Muestra el controlador de posición.</param>
    /// <param name="editor">Editor del gameobject seleccionado.</param>
    /// \endspanish
    public static void DrawSphereHandle(MaterialProperty enableProperty, MaterialProperty position, MaterialProperty radius, MaterialProperty color, bool showRadiusHandle, bool showPositionHandle, Editor editor)
    {

        EditorGUI.BeginChangeCheck();

        Vector3 handlePosition = Vector3.zero;

        float handleRadius;

        if (showRadiusHandle & enableProperty.floatValue == 1)
        {

            if (showPositionHandle)
            {

                handlePosition = Handles.PositionHandle(position.vectorValue, Quaternion.identity);

            }

            Handles.color = color.colorValue;

            handleRadius = Handles.RadiusHandle(Quaternion.identity, position.vectorValue, radius.floatValue / 2);

            if (EditorGUI.EndChangeCheck())
            {

                if (showPositionHandle)
                {

                    position.vectorValue = handlePosition;

                }

                if (showRadiusHandle)
                {

                    radius.floatValue = handleRadius * 2;

                }

                editor.Repaint();

            }

        }

    }

    #endregion

}
