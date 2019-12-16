///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 09/05/2018
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: Gestor que permite al Gameobject asociado modificar los valores de los shaders. Padre de los gestores de materiales. 
///

using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.CGF.Systems.Shaders;

namespace CGF.Systems.Shaders.Managers
{

    /// \english
    /// <summary>
    /// Class that allows store the components that contains shaders.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Clase que permite almacenar los componentes que contienen shaders.
    /// </summary>
    /// \endspanish
    [Serializable]
    public class ShaderMaterial
    {

        public MeshRenderer renderer;

        public CGFShaderBehavior shaderBehavior;

        public Material material;

    }

    /// \english
    /// <summary>
    /// Manager that allows to the attatched gameobject set the material of multiple gameobjects. Parent of the material managers.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Gestor que permite al Gameobject asociado configurar los materiales de distintos gameobjects. Padre de los gestores de materiales. 
    /// </summary>
    /// \endspanish
    [ExecuteInEditMode]
    public abstract class CGFShaderManager : MonoBehaviour
    {

        #region Public Variables

        #endregion


        #region Private Variables

        /// \english
        /// <summary>
        /// Allows animate the manager values.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Permite animar los valores del gestor.
        /// </summary>
        /// \endspanish
        [SerializeField]
        private bool _useAnimator;

        /// \english
        /// <summary>
        /// Reference property.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Propiedad de referencia.
        /// </summary>
        /// \endspanish
        protected string _propertyReference;

        /// \english
        /// <summary>
        /// Enables the automatic mode.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Activa el modo automático.
        /// </summary>
        /// \endspanish
        [SerializeField]
        private bool _automatic = true;

        /// \english
        /// <summary>
        /// Allows to get all the supported materials in runtime.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Permite obtener todos los materiales compatibles en timepo de ejecución.
        /// </summary>
        /// \endspanish
        [SerializeField]
        private bool _getAllMaterials = true;

        /// \english
        /// <summary>
        /// List of components that stores the iformation of the shader.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Lista de componentes que almacenan la información del shader.
        /// </summary>
        /// \endspanish
        [SerializeField]
        protected List<ShaderMaterial> _materials;

        #endregion


        #region Main Methods

        protected virtual void Awake()
        {

            if (_automatic)
            {

                if (_getAllMaterials)
                {

                    _materials = FindAllMaterials();

                }

                SetParameters();
            }

        }

        protected virtual void Update()
        {

            if (_useAnimator || Application.isEditor)
            {

                SetParameters();

            }

        }

        #endregion


        #region Utility Methods

        /// \english
        /// <summary>
        /// Get all the supported materials from the scene.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Obtiene todo los materiales compatibles de la escena.
        /// </summary>
        /// \endspanish
        public virtual void GetAllMaterials()
        {

            _materials = FindAllMaterials();

        }

        /// \english
        /// <summary>
        /// Remove all materials from the supported materials list.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Elimina todos los materiales de la lista de materiales compatibles.
        /// </summary>
        /// \endspanish
        public virtual void ClearMaterials()
        {

            _materials.Clear();

        }

        /// \english
        /// <summary>
        /// Search materials in the scene.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Busca todo los materiales de la escena.
        /// </summary>
        /// \endspanish
        protected virtual List<ShaderMaterial> FindAllMaterials()
        {

            List<ShaderMaterial> materials = new List<ShaderMaterial>();

            MeshRenderer[] renderers = FindObjectsOfType<MeshRenderer>();

            for (int i = 0; i < renderers.Length; i++)
            {

                Material material = renderers[i].sharedMaterial;

                if (material != null)
                {

                    if (material.shader != null)
                    {

                        if (material.shader.name.Contains("CG Framework/"))
                        {

                            if (material.HasProperty(_propertyReference))
                            {

                                CGFShaderBehavior shaderBehavior = renderers[i].gameObject.GetComponent<CGFShaderBehavior>();

                                ShaderMaterial shaderMaterial = new ShaderMaterial
                                {
                                    renderer = renderers[i],
                                    material = material,
                                    shaderBehavior = shaderBehavior
                                };

                                materials.Add(shaderMaterial);

                            }

                        }

                    }

                }

            }

            return materials;

        }

        /// \english
        /// <summary>
        /// Get all the material properties.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Obtiene todas las propiedades del material.
        /// </summary>
        /// \endspanish
        public virtual void GetAllProperties()
        {

            for (int i = 0; i < _materials.Count; i++)
            {

                if (_materials[i].renderer != null)
                {

                    Material material = _materials[i].renderer.sharedMaterial;

                    if (material != null)
                    {

                        if (material.shader != null)
                        {

                            if (material.shader.name.Contains("CG Framework/"))
                            {

                                if (material.HasProperty(_propertyReference))
                                {

                                    _materials[i].shaderBehavior = _materials[i].renderer.gameObject.GetComponent<CGFShaderBehavior>();

                                    _materials[i].material = material;

                                }

                            }

                        }

                    }

                }
                else
                {

                    _materials[i].material = null;

                    _materials[i].shaderBehavior = null;

                }

            }

        }

        /// \english
        /// <summary>
        /// Set the material properties.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece las propiedades del material.
        /// </summary>
        /// \endspanish
        public virtual void SetParameters()
        {

        }

        /// \english
        /// <summary>
        /// Set the value of a Vector.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de un Vector.
        /// </summary>
        /// \endspanish
        public virtual void SetVector(ShaderMaterial material, string propertyName, Vector4 propertyValue)
        {
            if (material.shaderBehavior != null)
            {

                propertyName = propertyName.Replace("_", "");

                material.shaderBehavior.GetType().GetProperty(propertyName).SetValue(material.shaderBehavior, (Vector3)propertyValue, null);

            }
            else if (material.material != null)
            {

                material.material.SetVector(propertyName, propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// Set the value of a VectorArray. 
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de un VectorArray.
        /// </summary>
        /// \endspanish
        public virtual void SetVectorArray(ShaderMaterial material, string propertyName, Vector4[] propertyValue)
        {

            if (material.shaderBehavior != null)
            {

                propertyName = propertyName.Replace("_", "");

                material.shaderBehavior.GetType().GetProperty(propertyName).SetValue(material.shaderBehavior, propertyValue, null);

            }
            else if (material.material != null)
            {

                material.material.SetVectorArray(propertyName, propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// Set the value of a Texture.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de un Texture.
        /// </summary>
        /// \endspanish
        public virtual void SetTexture(ShaderMaterial material, string propertyName, Texture propertyValue)
        {

            if (material.shaderBehavior != null)
            {

                propertyName = propertyName.Replace("_", "");

                material.shaderBehavior.GetType().GetProperty(propertyName).SetValue(material.shaderBehavior, propertyValue, null);

            }
            else if (material.material != null)
            {

                material.material.SetTexture(propertyName, propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// Set the value of a Matrix.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de un Matrix.
        /// </summary>
        /// \endspanish
        public virtual void SetMatrix(ShaderMaterial material, string propertyName, Matrix4x4 propertyValue)
        {

            if (material.shaderBehavior != null)
            {

                propertyName = propertyName.Replace("_", "");

                material.shaderBehavior.GetType().GetProperty(propertyName).SetValue(material.shaderBehavior, propertyValue, null);

            }
            else if (material.material != null)
            {

                material.material.SetMatrix(propertyName, propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// Set the value of a MatrixArray.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de un MatrixArray.
        /// </summary>
        /// \endspanish
        public virtual void SetMatrixArray(ShaderMaterial material, string propertyName, Matrix4x4[] propertyValue)
        {

            if (material.shaderBehavior != null)
            {

                propertyName = propertyName.Replace("_", "");

                material.shaderBehavior.GetType().GetProperty(propertyName).SetValue(material.shaderBehavior, propertyValue, null);

            }
            else if (material.material != null)
            {

                material.material.SetMatrixArray(propertyName, propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// Set the value of a Color.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de un Color.
        /// </summary>
        /// \endspanish
        public virtual void SetColor(ShaderMaterial material, string propertyName, Color propertyValue)
        {

            if (material.shaderBehavior != null)
            {

                propertyName = propertyName.Replace("_", "");

                material.shaderBehavior.GetType().GetProperty(propertyName).SetValue(material.shaderBehavior, propertyValue, null);

            }
            else if (material.material != null)
            {

                material.material.SetColor(propertyName, propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// Set the value of a Float.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de un Float.
        /// </summary>
        /// \endspanish
        public virtual void SetFloat(ShaderMaterial material, string propertyName, float propertyValue)
        {

            if (material.shaderBehavior != null)
            {

                propertyName = propertyName.Replace("_", "");

                material.shaderBehavior.GetType().GetProperty(propertyName).SetValue(material.shaderBehavior, propertyValue, null);

            }
            else if (material.material != null)
            {

                material.material.SetFloat(propertyName, propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// Set the value of a FloatArray.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de un FloatArray.
        /// </summary>
        /// \endspanish
        public virtual void SetFloatArray(ShaderMaterial material, string propertyName, float[] propertyValue)
        {

            if (material.shaderBehavior != null)
            {

                propertyName = propertyName.Replace("_", "");

                material.shaderBehavior.GetType().GetProperty(propertyName).SetValue(material.shaderBehavior, propertyValue, null);

            }
            else if (material.material != null)
            {

                material.material.SetFloatArray(propertyName, propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// Set the value of a Float using a bool.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de un Float a través de una booleana.
        /// </summary>
        /// \endspanish
        public virtual void SetFloat(ShaderMaterial material, string propertyName, bool floatValue, bool keywordValue)
        {

            if (material.shaderBehavior != null)
            {

                propertyName = propertyName.Replace("_", "");

                material.shaderBehavior.GetType().GetProperty(propertyName).SetValue(material.shaderBehavior, floatValue, null);

            }
            else if (material.material != null)
            {

                SetFloat(material, propertyName, floatValue ? 1 : 0);

                SetKeyword(material.material, propertyName, floatValue, keywordValue);

            }

        }


        /// \english
        /// <summary>
        /// Enable or disable a keyword according to its parameters.
        /// </summary>
        /// <param name="property">Property that manage the keyword status.</param>
        /// <param name="enable">Property float value used to enable o disable the keyword.</param>
        /// <param name="mode">Defines the default behavior of the keyword. If true, the keyword is enabled by default. If false, the keyword is disabled by default.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Activa o desactiva una keyword de acuerdo con sus parámetros.
        /// </summary>
        /// <param name="property">Propiedad que gestiona el estado de la keyword.</param>
        /// <param name="enable">El valor de la propiedad usado para activar o desactivar la keyword.</param>
        /// <param name="mode">Define el comportamiento por defecto de la keyword. Si es true, la keyword esá activada por defecto. Si es false, la keyword esá desactivada por defecto.</param>
        /// \endspanish 
        public static void SetKeyword(Material material, string propertyName, bool property, bool mode)
        {

            if (mode)
            {

                SetKeywordInternal(material, propertyName, property, property, "_ON");

            }
            else
            {

                SetKeywordInternal(material, propertyName, property, !property, "_OFF");

            }

        }

        /// \english
        /// <summary>
        /// Enable or disable a keyword.
        /// </summary>
        /// <param name="property">Property that manage the keyword status.</param>
        /// <param name="on">Property float value used to enable o disable the keyword.</param>
        /// <param name="defaultKeywordSuffix">Keyword suffix.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Activa o desactiva una keyword de acuerdo con sus parámetros.
        /// </summary>
        /// <param name="property">Propiedad que gestiona el estado de la keyword.</param>
        /// <param name="on">El valor de la propiedad usado para activar o desactivar la keyword.</param>
        /// <param name="defaultKeywordSuffix">Sufijo dela keyword.</param>
        /// \endspanish 
        private static void SetKeywordInternal(Material material, string propertyName, bool property, bool on, string defaultKeywordSuffix)
        {

            string keyword = propertyName.ToUpperInvariant() + defaultKeywordSuffix;

            if (on)
            {

                material.EnableKeyword(keyword);

            }
            else
            {

                material.DisableKeyword(keyword);

            }

        }

        /// \english
        /// <summary>
        /// 
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de un Float a traves de una booleana.
        /// </summary>
        /// \endspanish
        public virtual void SetFloat(ShaderMaterial material, string propertyName, bool floatValue)
        {

            if (material.shaderBehavior != null)
            {

                propertyName = propertyName.Replace("_", "");

                material.shaderBehavior.GetType().GetProperty(propertyName).SetValue(material.shaderBehavior, floatValue, null);

            }
            else if (material.material != null)
            {

                SetFloat(material, propertyName, floatValue ? 1 : 0);

            }

        }

        /// \english
        /// <summary>
        /// 
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de un ComputeBuffer.
        /// </summary>
        /// \endspanish
        public virtual void SetBuffer(ShaderMaterial material, string propertyName, ComputeBuffer propertyValue)
        {

            if (material.shaderBehavior != null)
            {

                propertyName = propertyName.Replace("_", "");

                material.shaderBehavior.GetType().GetProperty(propertyName).SetValue(material.shaderBehavior, propertyValue, null);

            }
            else if (material.material != null)
            {

                material.material.SetBuffer(propertyName, propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// 
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de un Color[].
        /// </summary>
        /// \endspanish
        public virtual void SetColorArray(ShaderMaterial material, string propertyName, Color[] propertyValue)
        {

            if (material.shaderBehavior != null)
            {

                propertyName = propertyName.Replace("_", "");

                material.shaderBehavior.GetType().GetProperty(propertyName).SetValue(material.shaderBehavior, propertyValue, null);

            }
            else if (material != null)
            {

                material.material.SetColorArray(propertyName, propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// 
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de un tag.
        /// </summary>
        /// \endspanish
        public virtual void SetOverrideTag(ShaderMaterial material, string propertyName, string propertyValue)
        {

            if (material.shaderBehavior != null)
            {

                propertyName = propertyName.Replace("_", "");

                material.shaderBehavior.GetType().GetProperty(propertyName).SetValue(material.shaderBehavior, propertyValue, null);

            }
            else if (material != null)
            {

                material.material.SetOverrideTag(propertyName, propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// 
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de un int.
        /// </summary>
        /// \endspanish
        public virtual void SetInt(ShaderMaterial material, string propertyName, int propertyValue)
        {

            if (material.shaderBehavior != null)
            {

                propertyName = propertyName.Replace("_", "");

                material.shaderBehavior.GetType().GetProperty(propertyName).SetValue(material.shaderBehavior, propertyValue, null);

            }
            else if (material != null)
            {

                material.material.SetInt(propertyName, propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// 
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de un pass.
        /// </summary>
        /// \endspanish
        public virtual void SetPass(ShaderMaterial material, int propertyValue)
        {

            if (material != null)
            {

                material.material.SetPass(propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// 
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de un Offset.
        /// </summary>
        /// \endspanish
        public virtual void SetTextureOffset(ShaderMaterial material, string propertyName, Vector2 propertyValue)
        {

            if (material.shaderBehavior != null)
            {

                propertyName = propertyName.Replace("_", "");

                propertyName = propertyName + "Offset";

                material.shaderBehavior.GetType().GetProperty(propertyName).SetValue(material.shaderBehavior, propertyValue, null);

            }
            else if (material != null)
            {

                material.material.SetTextureOffset(propertyName, propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// 
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de un Scale.
        /// </summary>
        /// \endspanish
        public virtual void SetTextureTiling(ShaderMaterial material, string propertyName, Vector2 propertyValue)
        {

            if (material.shaderBehavior != null)
            {

                propertyName = propertyName.Replace("_", "");

                propertyName = propertyName + "Tiling";

                material.shaderBehavior.GetType().GetProperty(propertyName).SetValue(material.shaderBehavior, propertyValue, null);

            }
            else if (material.material != null)
            {

                material.material.SetTextureScale(propertyName, propertyValue);

            }

        }

        #endregion


        #region Utility Events

        #endregion

    }

}