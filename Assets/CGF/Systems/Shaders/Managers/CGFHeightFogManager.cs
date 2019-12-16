///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 09/05/2018
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: Gestor que permite al Gameobject asociado modificar los valores de los shaders que tengan la propiedad de Height Fog.
///

using UnityEngine;

namespace CGF.Systems.Shaders.Managers
{

    /// \english
    /// <summary>
    /// Manager that allows to the attatched gameobject sets the values of the shaders with the feature of Height Fog.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Gestor que permite al Gameobject asociado modificar los valores de los shaders que tengan la propiedad de Height Fog.
    /// </summary>
    /// \endspanish
    public class CGFHeightFogManager : CGFShaderManager
    {

        [SerializeField]
        private bool _heightFog = false;

        [SerializeField]
        private Color _heightFogColor = new Color(0.5f, 0.5f, 0.5f, 1);

        [SerializeField]
        private float _heightFogDensity = 1;

        [SerializeField]
        private float _heightFogStartPosition = 0;

        [SerializeField]
        private float _fogHeight = 0.5f;

        [SerializeField]
        private bool _useAlphaValue = false;

        [SerializeField]
        private bool _localHeightFog = false;

        protected override void Awake()
        {

            _propertyReference = "_HeightFog";

            base.Awake();

        }

        public override void SetParameters()
        {

            if (_materials != null)
            {

                for (int i = 0; i < _materials.Count; i++)
                {

                    SetFloat(_materials[i], "_HeightFog", _heightFog, true);

                    SetColor(_materials[i], "_HeightFogColor", _heightFogColor);

                    SetFloat(_materials[i], "_HeightFogDensity", _heightFogDensity);

                    SetFloat(_materials[i], "_HeightFogStartPosition", _heightFogStartPosition);

                    SetFloat(_materials[i], "_FogHeight", _fogHeight);

                    SetFloat(_materials[i], "_UseAlphaValue", _useAlphaValue);

                    SetFloat(_materials[i], "_LocalHeightFog", _localHeightFog);

                }

            }

        }

        #region Public Variables

        #endregion


        #region Private Variables

        #endregion


        #region Main Methods

        #endregion


        #region Utility Methods

        #endregion


        #region Utility Events

        #endregion

    }

}