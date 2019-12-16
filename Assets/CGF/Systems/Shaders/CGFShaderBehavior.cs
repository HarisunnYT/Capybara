/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 03/09/2016
/// Author: Chloroplast Games
/// Website: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: Behavior that allows to the attatched gameobject set the material.
///

using UnityEngine;
using UnityEngine.UI;

// Local Namespace
namespace Assets.CGF.Systems.Shaders
{


    /// \english
    /// <summary>
    /// Behavior that allows to the attatched gameobject set the material.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Comportamiento que permite al gameobject asociado configurar el material.
    /// </summary>
    /// \endspanish
    [ExecuteInEditMode]
    public class CGFShaderBehavior : MonoBehaviour
    {

        #region Public Variables

        #endregion


        #region Private Variables

        /// \english
        /// <summary>
        /// Shader path.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Ruta del Shader.
        /// </summary>
        /// \endspanish
        [SerializeField]
        protected Shader _shader;

        /// \english
        /// <summary>
        /// Stores de previous material.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Guarda el primer material.
        /// </summary>
        /// \endspanish
        [SerializeField]
        protected bool _savePreviousMaterial = true;

        /// \english
        /// <summary>
        /// Multiple materials with the same shader.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Multiple materiales con el mismo shader.
        /// </summary>
        /// \endspanish
        [SerializeField]
        protected bool _multipleShader = false;

        /// \english
        /// <summary>
        /// Material index.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Índice del material.
        /// </summary>
        /// \endspanish
        [SerializeField]
        protected int _materialIndex;

        /// \english
        /// <summary>
        /// Material to store the new shader.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Material para almacenar el nuevo Shader.
        /// </summary>
        /// \endspanish
        protected Material _tempMaterial;

        /// \english
        /// <summary>
        /// Default material.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Material predeterminado.
        /// </summary>
        /// \endspanish
        protected Material _defaultMaterial;

        /// \english
        /// <summary>
        /// Material to modify.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Material a modificar.
        /// </summary>
        /// \endspanish
        [HideInInspector]
        public Material _myMaterial;

        /// \english
        /// <summary>
        /// The Projector component to the attatched gameogject.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// El componente Projector del gameobject asociado.
        /// </summary>
        /// \endspanish
        [SerializeField]
        protected Projector _projector;

        /// \english
        /// <summary>
        /// The Renderer component to the attatched gameogject.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// El componente Renderer del gameobject asociado.
        /// </summary>
        /// \endspanish
        protected Renderer _myRenderer;

        /// \english
        /// <summary>
        /// The SpriteRenderer component to the attatched gameogject.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// El componente SpriteRenderer del gameobject asociado.
        /// </summary>
        /// \endspanish
        protected SpriteRenderer _mySpriteRenderer;

        /// \english
        /// <summary>
        /// The MaskableGraphic component to the attatched gameogject.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// El componente MaskableGraphic del gameobject asociado.
        /// </summary>
        /// \endspanish
        protected MaskableGraphic _myMaskableGraphic;

        /// \english
        /// <summary>
        /// MaterialPropertyBlock object.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Objeto tipo MaterialPropertyBlock.
        /// </summary>
        /// \endspanish
        protected MaterialPropertyBlock _materialPropertyBlock;

        /// \english
        /// <summary>
        /// Shader name
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Nombre del shader.
        /// </summary>
        /// \endspanish
        protected string _shaderName = "";

        #endregion


        #region Main Methods

        protected virtual void Awake()
        {

            _projector = gameObject.GetComponent<Projector>();

            _myRenderer = gameObject.GetComponent<Renderer>();

            _mySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

            _myMaskableGraphic = gameObject.GetComponent<MaskableGraphic>();

            if (_savePreviousMaterial)
            {

                if (_myRenderer != null)
                {

                    if (_myRenderer.sharedMaterial != null)
                    {

                        _defaultMaterial = new Material(_myRenderer.sharedMaterial.shader);

                    }

                }

                if (_myMaskableGraphic != null)
                {

                    _defaultMaterial = new Material(_myMaskableGraphic.material.shader);

                }

                if (_projector != null)
                {
                    _defaultMaterial = new Material(_projector.material.shader);
                }

            }

        }

        protected virtual void OnEnable()
        {

            SetShaderEditor();

        }

        protected virtual void Start()
        {

            SetShaderEditor();

        }

        protected virtual void OnDisable()
        {

            if (transform.gameObject != null)
            {

                if (gameObject.activeInHierarchy || gameObject.activeSelf)
                {

                    if ((!Application.isPlaying) && (Application.isEditor))
                    {

                        if (_defaultMaterial == null || !_savePreviousMaterial)
                        {

                            _defaultMaterial = new Material(Shader.Find("Standard"));

                        }

                        if (_myRenderer != null)
                        {

                            _myRenderer.sharedMaterial = _defaultMaterial;

                            _myRenderer.sharedMaterial.hideFlags = HideFlags.None;

                        }

                        if (_myMaskableGraphic != null)
                        {

                            _myMaskableGraphic.material = _defaultMaterial;

                            _myMaskableGraphic.material.hideFlags = HideFlags.None;

                        }

                        if (_projector != null)
                        {

                            _projector.material = _defaultMaterial;

                            _projector.material.hideFlags = HideFlags.None;

                        }

                    }

                }

            }

        }

        protected virtual void OnDestroy()
        {

            if ((!Application.isPlaying) && (Application.isEditor))
            {

                if (_tempMaterial != null) DestroyImmediate(_tempMaterial);


                if ((gameObject.activeInHierarchy || gameObject.activeSelf) && _defaultMaterial != null)
                {

                    if (_myRenderer != null)
                    {

                        _myRenderer.sharedMaterial = _defaultMaterial;

                        _myRenderer.sharedMaterial.hideFlags = HideFlags.None;

                    }

                    if (_myMaskableGraphic != null)
                    {

                        _myMaskableGraphic.material = _defaultMaterial;

                        _myMaskableGraphic.material.hideFlags = HideFlags.None;

                    }

                    if (_projector != null)
                    {

                        _projector.material = _defaultMaterial;

                        _projector.material.hideFlags = HideFlags.None;

                    }

                }

            }

        }

        protected virtual void Update()
        {

            if (Application.isEditor && !Application.isPlaying)
            {

                SetShaderParameters();

            }

        }

        #endregion


        #region Utility Methods

        /// \english
        /// <summary>
        /// Add the properties to the shader.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Añade las propiedades al shader.
        /// </summary>
        /// \endspanish
        protected virtual void SetShaderParameters()
        {

        }

        /// \english
        /// <summary>
        /// Add the shader to the material.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Añade el shader al material.
        /// </summary>
        /// \endspanish
        public virtual void SetShaderEditor()
        {

            if (_shader != null)
            {

                SetMaterial(_shader);

            }
            else if (!_shaderName.Equals(""))
            {

                Shader shader = Shader.Find(_shaderName);

                if (shader != null)
                {

                    _shader = shader;

                    SetMaterial(shader);

                }
                else
                {

                    Debug.LogError("Shader -- " + _shaderName + " -- not exist");

                }                

            }

        }

        /// \english
        /// <summary>
        /// Sets the materials.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Configura el material.
        /// </summary>
        /// <param name="shader">Shader a modifical del materal.</param>
        /// \endspanish
        private void SetMaterial(Shader shader)
        {

            if (_myRenderer != null)
            {

                if (_multipleShader)
                {

                    if (_myRenderer.sharedMaterials[_materialIndex] == null || _myRenderer.sharedMaterials[_materialIndex].shader != shader)
                    {

                        _tempMaterial = new Material(shader);

                        hideFlags = HideFlags.None;

                        _myRenderer.sharedMaterials[_materialIndex] = _tempMaterial;

                        _myRenderer.sharedMaterials[_materialIndex].shader = shader;

                        _myRenderer.sharedMaterials[_materialIndex].name = shader.name;

                        CopyShaderParameter();

                    }

                    _myMaterial = _myRenderer.sharedMaterials[_materialIndex];

                }
                else
                {

                    if (_myRenderer.sharedMaterial == null || _myRenderer.sharedMaterial.shader != shader)
                    {

                        _tempMaterial = new Material(shader);

                        hideFlags = HideFlags.None;

                        _myRenderer.sharedMaterial = _tempMaterial;

                        CopyShaderParameter();

                    }

                    _myMaterial = _myRenderer.sharedMaterial;

                }

            }

            if (_myMaskableGraphic != null)
            {

                if (_myMaskableGraphic.material == null || _myMaskableGraphic.material.shader != _shader)
                {

                    _tempMaterial = new Material(_shader);

                    _myMaskableGraphic.material = _tempMaterial;

                    _myMaterial = _myMaskableGraphic.material;

                    CopyShaderParameter();

                }

            }


            if (_projector != null)
            {

                if (_projector.material == null || _projector.material.shader != _shader)
                {

                    _tempMaterial = new Material(shader);

                    _projector.material = _tempMaterial;

                    _myMaterial = _projector.material;

                    CopyShaderParameter();

                }

            }

        }

        /// \english
        /// <summary>
        /// Copy the shader parameters to the material.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Copia los parámetros del shader al material.
        /// </summary>
        /// \endspanish
        protected virtual void CopyShaderParameter()
        {

        }

        /// \english
        /// <summary>
        /// Gets the value of a vector.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Obtiene el valor de un vector.
        /// </summary>
        /// \endspanish
        public virtual Vector3 GetVector(string propertyName)
        {
            Vector3 vector = new Vector3();

            if (_myRenderer)
            {

                if (_multipleShader)
                {

                    vector = _myRenderer.sharedMaterials[_materialIndex].GetVector(propertyName);

                }
                else
                {

                    vector = _myRenderer.sharedMaterial.GetVector(propertyName);

                }

            }

            if (_myMaskableGraphic)
            {

                vector = _myMaskableGraphic.material.GetVector(propertyName);

            }

            if (_projector)
            {

                vector = _projector.material.GetVector(propertyName);

            }

            return vector;

        }

        /// \english
        /// <summary>
        /// Sets the value of a vector.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de un vector.
        /// </summary>
        /// \endspanish
        public virtual void SetVector(string propertyName, Vector4 propertyValue)
        {

            if (_myRenderer)
            {

                if (_multipleShader)
                {

                    _myRenderer.sharedMaterials[_materialIndex].SetVector(propertyName, propertyValue);

                }
                else
                {

                    _myRenderer.sharedMaterial.SetVector(propertyName, propertyValue);

                }

            }


            if (_myMaskableGraphic)
            {

                _myMaskableGraphic.material.SetVector(propertyName, propertyValue);

            }

            if (_projector)
            {

                _projector.material.SetVector(propertyName, propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// Sets the value of a vector array.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de una array de vectores.
        /// </summary>
        /// \endspanish
        public virtual void SetVectorArray(string propertyName, Vector4[] propertyValue)
        {

            if (_myRenderer)
            {

                if (_multipleShader)
                {

                    _myRenderer.sharedMaterials[_materialIndex].SetVectorArray(propertyName, propertyValue);

                }
                else
                {

                    _myRenderer.sharedMaterial.SetVectorArray(propertyName, propertyValue);

                }

            }

            if (_myMaskableGraphic)
            {

                _myMaskableGraphic.material.SetVectorArray(propertyName, propertyValue);

            }

            if (_projector)
            {

                _projector.material.SetVectorArray(propertyName, propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// Sets the value of a texture.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de una textura.
        /// </summary>
        /// \endspanish
        public virtual void SetTexture(string propertyName, Texture propertyValue)
        {

            if (_myRenderer)
            {

                if (_multipleShader)
                {

                    _myRenderer.sharedMaterials[_materialIndex].SetTexture(propertyName, propertyValue);

                }
                else
                {

                    _myRenderer.sharedMaterial.SetTexture(propertyName, propertyValue);

                }

            }

            if (_myMaskableGraphic)
            {

                _myMaskableGraphic.material.SetTexture(propertyName, propertyValue);

            }

            if (_projector)
            {

                _projector.material.SetTexture(propertyName, propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// Gets the value of a texture.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Obtiene el valor de una textura.
        /// </summary>
        /// \endspanish
        public virtual Texture GetTexture(string propertyName)
        {

            Texture texture = null;

            if (_myRenderer)
            {

                if (_multipleShader)
                {

                    texture = _myRenderer.sharedMaterials[_materialIndex].GetTexture(propertyName);

                }
                else
                {

                    texture = _myRenderer.sharedMaterial.GetTexture(propertyName);

                }

            }

            if (_myMaskableGraphic)
            {

                texture = _myMaskableGraphic.material.GetTexture(propertyName);

            }

            if (_projector)
            {

                texture = _projector.material.GetTexture(propertyName);

            }

            return texture;

        }

        /// \english
        /// <summary>
        /// Sets the value of a matrix.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de una matriz.
        /// </summary>
        /// \endspanish
        public virtual void SetMatrix(string propertyName, Matrix4x4 propertyValue)
        {

            if (_myRenderer)
            {

                if (_multipleShader)
                {

                    _myRenderer.sharedMaterials[_materialIndex].SetMatrix(propertyName, propertyValue);

                }
                else
                {

                    _myRenderer.sharedMaterial.SetMatrix(propertyName, propertyValue);

                }

            }

            if (_myMaskableGraphic)
            {

                _myMaskableGraphic.material.SetMatrix(propertyName, propertyValue);

            }

            if (_projector)
            {

                _projector.material.SetMatrix(propertyName, propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// Sets the value of a matrix array.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de una array de matrices.
        /// </summary>
        /// \endspanish
        public virtual void SetMatrixArray(string propertyName, Matrix4x4[] propertyValue)
        {

            if (_myRenderer)
            {

                if (_multipleShader)
                {

                    _myRenderer.sharedMaterials[_materialIndex].SetMatrixArray(propertyName, propertyValue);

                }
                else
                {

                    _myRenderer.sharedMaterial.SetMatrixArray(propertyName, propertyValue);

                }

            }

            if (_myMaskableGraphic)
            {

                _myMaskableGraphic.material.SetMatrixArray(propertyName, propertyValue);

            }

            if (_projector)
            {

                _projector.material.SetMatrixArray(propertyName, propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// Sets the value of a color
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de un color.
        /// </summary>
        /// \endspanish
        public virtual void SetColor(string propertyName, Color propertyValue)
        {

            if (_myRenderer)
            {

                if (_multipleShader)
                {

                    _myRenderer.sharedMaterials[_materialIndex].SetColor(propertyName, propertyValue);

                }
                else
                {

                    _myRenderer.sharedMaterial.SetColor(propertyName, propertyValue);

                }

            }

            if (_myMaskableGraphic)
            {

                _myMaskableGraphic.material.SetColor(propertyName, propertyValue);

            }

            if (_projector)
            {

                _projector.material.SetColor(propertyName, propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// Gets the value of a color.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Obtiene el valor de un color.
        /// </summary>
        /// \endspanish
        public virtual Color GetColor(string propertyName)
        {
            Color color = Color.white;

            if (_myRenderer)
            {

                if (_multipleShader)
                {

                    color = _myRenderer.sharedMaterials[_materialIndex].GetColor(propertyName);

                }
                else
                {

                    color = _myRenderer.sharedMaterial.GetColor(propertyName);

                }

            }

            if (_myMaskableGraphic)
            {

                color = _myMaskableGraphic.material.GetColor(propertyName);

            }

            if (_projector)
            {

                color = _projector.material.GetColor(propertyName);

            }

            return color;

        }

        /// \english
        /// <summary>
        /// Sets the value of a float.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de un float.
        /// </summary>
        /// \endspanish
        public virtual void SetFloat(string propertyName, float propertyValue)
        {

            if (_myRenderer)
            {

                if (_multipleShader)
                {

                    _myRenderer.sharedMaterials[_materialIndex].SetFloat(propertyName, propertyValue);

                }
                else
                {

                    _myRenderer.sharedMaterial.SetFloat(propertyName, propertyValue);

                }

            }

            if (_myMaskableGraphic)
            {

                _myMaskableGraphic.material.SetFloat(propertyName, propertyValue);

            }

            if (_projector)
            {

                _projector.material.SetFloat(propertyName, propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// Gets the value of a float
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Obtiene el valor de un float.
        /// </summary>
        /// \endspanish
        public virtual float GetFloat(string propertyName)
        {

            float value = 0;

            if (_myRenderer)
            {

                if (_multipleShader)
                {

                    value = _myRenderer.sharedMaterials[_materialIndex].GetFloat(propertyName);

                }
                else
                {

                    value = _myRenderer.sharedMaterial.GetFloat(propertyName);

                }

            }

            if (_myMaskableGraphic)
            {

                value = _myMaskableGraphic.material.GetFloat(propertyName);

            }

            if (_projector)
            {

                value = _projector.material.GetFloat(propertyName);

            }

            return value;

        }

        /// \english
        /// <summary>
        /// Sets the value of a float array.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de una array de floats.
        /// </summary>
        /// \endspanish
        public virtual void SetFloatArray(string propertyName, float[] propertyValue)
        {

            if (_myRenderer)
            {

                if (_multipleShader)
                {

                    _myRenderer.sharedMaterials[_materialIndex].SetFloatArray(propertyName, propertyValue);

                }
                else
                {

                    _myRenderer.sharedMaterial.SetFloatArray(propertyName, propertyValue);

                }

            }

            if (_myMaskableGraphic)
            {

                _myMaskableGraphic.material.SetFloatArray(propertyName, propertyValue);

            }

            if (_projector)
            {

                _projector.material.SetFloatArray(propertyName, propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// Sets the value of a float using a boolean.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de un float a través de una booleana.
        /// </summary>
        /// \endspanish
        public virtual void SetFloat(string propertyName, bool floatValue)
        {

            if (_myRenderer)
            {

                SetFloat(propertyName, floatValue ? 1 : 0);

            }

            if (_myMaskableGraphic)
            {

                SetFloat(propertyName, floatValue ? 1 : 0);

            }

            if (_projector)
            {

                SetFloat(propertyName, floatValue ? 1 : 0);

            }

        }

        /// \english
        /// <summary>
        /// Obtiene the value of a float using a boolean.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Obtiene el valor de un Float a través de una booleana.
        /// </summary>
        /// \endspanish
        public virtual bool GetBoolean(string propertyName)
        {

            float fValue = GetFloat(propertyName);

            return fValue == 1;

        }

        /// \english
        /// <summary>
        /// Sets the value of a compute buffer.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de un compute buffer.
        /// </summary>
        /// \endspanish
        public virtual void SetBuffer(string propertyName, ComputeBuffer propertyValue)
        {

            if (_myRenderer)
            {

                if (_multipleShader)
                {

                    _myRenderer.sharedMaterials[_materialIndex].SetBuffer(propertyName, propertyValue);

                }
                else
                {

                    _myRenderer.sharedMaterial.SetBuffer(propertyName, propertyValue);

                }

            }

            if (_myMaskableGraphic)
            {

                _myMaskableGraphic.material.SetBuffer(propertyName, propertyValue);

            }

            if (_projector)
            {

                _projector.material.SetBuffer(propertyName, propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// Sets the value of a color array.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de una array de colores.
        /// </summary>
        /// \endspanish
        public virtual void SetColorArray(string propertyName, Color[] propertyValue)
        {

            if (_myRenderer)
            {

                if (_multipleShader)
                {

                    _myRenderer.sharedMaterials[_materialIndex].SetColorArray(propertyName, propertyValue);

                }
                else
                {

                    _myRenderer.sharedMaterial.SetColorArray(propertyName, propertyValue);

                }

            }

            if (_myMaskableGraphic)
            {

                _myMaskableGraphic.material.SetColorArray(propertyName, propertyValue);

            }

            if (_projector)
            {

                _projector.material.SetColorArray(propertyName, propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// Sets the value of a tag.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de un tag.
        /// </summary>
        /// \endspanish
        public virtual void SetOverrideTag(string propertyName, string propertyValue)
        {

            if (_myRenderer)
            {

                if (_multipleShader)
                {

                    _myRenderer.sharedMaterials[_materialIndex].SetOverrideTag(propertyName, propertyValue);

                }
                else
                {

                    _myRenderer.sharedMaterial.SetOverrideTag(propertyName, propertyValue);

                }

            }

            if (_myMaskableGraphic)
            {

                _myMaskableGraphic.material.SetOverrideTag(propertyName, propertyValue);

            }

            if (_projector)
            {

                _projector.material.SetOverrideTag(propertyName, propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// Sets the value of a int.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de un int.
        /// </summary>
        /// \endspanish
        public virtual void SetInt(string propertyName, int propertyValue)
        {

            if (_myRenderer)
            {

                if (_multipleShader)
                {
                    _myRenderer.sharedMaterials[_materialIndex].SetInt(propertyName, propertyValue);
                }
                else
                {
                    _myRenderer.sharedMaterial.SetInt(propertyName, propertyValue);
                }

            }

            if (_myMaskableGraphic)
            {

                _myMaskableGraphic.material.SetInt(propertyName, propertyValue);

            }

            if (_projector)
            {

                _projector.material.SetInt(propertyName, propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// Sets the value of a pass.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de un pass.
        /// </summary>
        /// \endspanish
        public virtual void SetPass(int propertyValue)
        {

            if (_myRenderer)
            {

                if (_multipleShader)
                {

                    _myRenderer.sharedMaterials[_materialIndex].SetPass(propertyValue);

                }
                else
                {

                    _myRenderer.sharedMaterial.SetPass(propertyValue);

                }

            }

            if (_myMaskableGraphic)
            {

                _myMaskableGraphic.material.SetPass(propertyValue);

            }

            if (_projector)
            {

                _projector.material.SetPass(propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// Sets the value of a texture offset.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor del offset de una textura
        /// </summary>
        /// \endspanish
        public virtual void SetTextureOffset(string propertyName, Vector2 propertyValue)
        {

            if (_myRenderer)
            {

                if (_multipleShader)
                {

                    _myRenderer.sharedMaterials[_materialIndex].SetTextureOffset(propertyName, propertyValue);

                }
                else
                {

                    _myRenderer.sharedMaterial.SetTextureOffset(propertyName, propertyValue);

                }

            }

            if (_myMaskableGraphic)
            {

                _myMaskableGraphic.material.SetTextureOffset(propertyName, propertyValue);

            }

            if (_projector)
            {

                _projector.material.SetTextureOffset(propertyName, propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// Gets the value of a texture offset.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Obtiene el valor de un offset de una textura.
        /// </summary>
        /// \endspanish
        public virtual Vector2 GetTextureOffset(string propertyName)
        {

            Vector2 propertyValue = Vector2.zero;

            if (_myRenderer)
            {

                if (_multipleShader)
                {

                    propertyValue = _myRenderer.sharedMaterials[_materialIndex].GetTextureOffset(propertyName);

                }
                else
                {

                    propertyValue = _myRenderer.sharedMaterial.GetTextureOffset(propertyName);

                }

            }

            if (_myMaskableGraphic)
            {

                propertyValue = _myMaskableGraphic.material.GetTextureOffset(propertyName);

            }

            if (_projector)
            {

                propertyValue = _projector.material.GetTextureOffset(propertyName);

            }

            return propertyValue;

        }

        /// \english
        /// <summary>
        /// Sets the value of a texture scale.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Establece el valor de la escala de una textura.
        /// </summary>
        /// \endspanish
        public virtual void SetTextureTiling(string propertyName, Vector2 propertyValue)
        {

            if (_myRenderer)
            {

                if (_multipleShader)
                {

                    _myRenderer.sharedMaterials[_materialIndex].SetTextureScale(propertyName, propertyValue);

                }
                else
                {

                    _myRenderer.sharedMaterial.SetTextureScale(propertyName, propertyValue);

                }

            }

            if (_myMaskableGraphic)
            {

                _myMaskableGraphic.material.SetTextureScale(propertyName, propertyValue);

            }

            if (_projector)
            {

                _projector.material.SetTextureScale(propertyName, propertyValue);

            }

        }

        /// \english
        /// <summary>
        /// gets the value of a texture scale.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Obtiene el valor de la escala de una textura.
        /// </summary>
        /// \endspanish
        public virtual Vector2 GetTextureTiling(string propertyName)
        {

            Vector2 value = Vector2.zero;

            if (_myRenderer)
            {

                if (_multipleShader)
                {
                    value = _myRenderer.sharedMaterials[_materialIndex].GetTextureScale(propertyName);
                }
                else
                {
                    value = _myRenderer.sharedMaterial.GetTextureScale(propertyName);
                }

            }

            if (_myMaskableGraphic)
            {

                value = _myMaskableGraphic.material.GetTextureScale(propertyName);

            }

            if (_projector)
            {

                value = _projector.material.GetTextureScale(propertyName);

            }

            return value;

        }

        #endregion


        #region Utility Events

        #endregion

    }

}
