///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 09/05/2018
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: Manager that allows to the attatched gameobject sets the values of the shaders with the feature of Distance Fog.
///

using UnityEngine;

namespace CGF.Systems.Shaders.Managers
{

    /// \english
    /// <summary>
    /// Manager that allows to the attatched gameobject sets the values of the shaders with the feature of Distance Fog.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Gestor que permite al Gameobject asociado modificar los valores de los shaders que tengan la propiedad de Distance Fog.
    /// </summary>
    /// \endspanish
    public class CGFDistanceFogManager : CGFShaderManager
    {

        [SerializeField]
        private bool _distanceFog = false;

        [SerializeField]
        private Color _distanceFogColor = new Color(0.5019608f, 0.5019608f, 0.5019608f, 1);

        [SerializeField]
        private float _distanceFogStartPosition = 10;

        [SerializeField]
        private float _distanceFogLength = 0;

        [SerializeField]
        private float _distanceFogDensity = 1;

        [SerializeField]
        private bool _useAlpha = false;

        [SerializeField]
        private bool _worldDistanceFog = false;

        [SerializeField]
        private Vector3 _worldDistanceFogPosition = Vector3.zero;

        protected override void Awake()
        {

            _propertyReference = "_DistanceFog";

            base.Awake();

        }

        public override void SetParameters()
        {

            if (_materials != null)
            {

                for (int i = 0; i < _materials.Count; i++)
                {

                    SetFloat(_materials[i], "_DistanceFog", _distanceFog, true);

                    SetColor(_materials[i], "_DistanceFogColor", _distanceFogColor);

                    SetFloat(_materials[i], "_DistanceFogStartPosition", _distanceFogStartPosition);

                    SetFloat(_materials[i], "_DistanceFogLength", _distanceFogLength);

                    SetFloat(_materials[i], "_DistanceFogDensity", _distanceFogDensity);

                    SetFloat(_materials[i], "_UseAlpha", _useAlpha);

                    SetFloat(_materials[i], "_WorldDistanceFog", _worldDistanceFog);

                    SetVector(_materials[i], "_WorldDistanceFogPosition", _worldDistanceFogPosition);

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