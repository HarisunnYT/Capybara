///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 16/07/2016
/// Author: Chloroplast Games
/// Website: http://www.chloroplastgames.com
/// Programmers: Pau Elias
/// Description: Comportamiento que permite al gameobject asociado mover sinusoidalmente sus vértices.
///

using UnityEngine;

// Local Namespace
namespace Assets.CGF.Systems.ObjectMesh
{

    [RequireComponent(typeof(MeshFilter))]
    /// <summary>    
    /// Comportamiento que permite al gameobject asociado mover sinusoidalmente sus vértices.
    /// </summary>
    public class CGFVertexSinMovementBehavior : MonoBehaviour
    {

        #region Public Variables

        /// <summary>    
        /// Altitud del movimiento de la onda.
        /// </summary>
        public float waveScale = 0.5f;

        /// <summary>    
        /// Velocidad del movimiento de la onda.
        /// </summary>
        public float waveSpeed = 1.0f;

        /// <summary>    
        /// Eje en el que se produce el movimiento de la onda.
        /// </summary>
        public enum Axes
        {
            /// <summary>    
            /// 
            /// </summary>
            X,

            /// <summary>    
            /// 
            /// </summary>
            Y,

            /// <summary>    
            /// 
            /// </summary>
            Z

        }

        /// <summary>    
        /// Eje seleccionado en el que se produce el movimiento de la onda.
        /// </summary>
        public Axes axis;

        /// <summary>    
        /// Magnitud de la onda en cada uno de los ejes.
        /// </summary>
        public Vector3 magnitude = new Vector3(0.1f, 0.1f, 0.1f);

        /// <summary>    
        /// Fuerza del ruido que se aplica al movimiento de las ondas.
        /// </summary>
        public float noiseStrength;

        /// <summary>    
        /// Multiplicador del ruido que se aplica al movimiento de las ondas.
        /// </summary>
        public float noiseMultiplier;

        /// <summary>    
        /// Se utiliza el modelo 3D como "collider".
        /// </summary>
        public bool meshAsCollider;

        #endregion


        #region Private Variables

        /// <summary>    
        /// "Mesh" del gameobject asociado.
        /// </summary>
        private Mesh myMesh;

        /// <summary>    
        /// Vertices del modelo.
        /// </summary>
        private Vector3[] vertices;

        /// <summary>    
        /// Vertices modificados del modelo.
        /// </summary>
        private Vector3[] newVertices;

        /// <summary>    
        /// Factor de aleatoriedad para el movimiento de la onda.
        /// </summary>
        private float[] randomFactor;

        /// <summary>    
        /// "MeshCollider" del gameobject asociado.
        /// </summary>
        private MeshCollider myCollider;

        #endregion


        #region Main Methods

        void Awake()
        {
            myMesh = GetComponent<MeshFilter>().mesh;

            if (meshAsCollider & GetComponent<MeshCollider>() != null)
            {

                myCollider = gameObject.GetComponent<MeshCollider>();

            }

        }

        void Start()
        {

            if (vertices == null)
            {

                vertices = myMesh.vertices;

            }

            newVertices = new Vector3[vertices.Length];

            randomFactor = new float[vertices.Length];

            for (int i = 0; i < newVertices.Length; i++)
            {

                randomFactor[i] = Random.Range(0.0f, 1.0f);

                i++;

            }

            if (meshAsCollider & myCollider != null)
            {

                myCollider.sharedMesh = myMesh;

            }

        }

        void Update()
        {

            VertexSinMovement();

            myMesh.vertices = newVertices;

            myMesh.RecalculateNormals();

        }

        #endregion


        #region Utility Methods

        /// <summary>    
        /// Realiza el movimiento sinusoidal a los vertices de la "mesh".
        /// </summary>
        public void VertexSinMovement()
        {

            for (int i = 0; i < newVertices.Length; i++)
            {

                Vector3 vertex = vertices[i];

                float x;

                float y;

                float z;

                switch (axis)
                {

                    case (Axes.X):

                        x = randomFactor[i] * magnitude.x;

                        y = vertex.y * magnitude.y;

                        z = vertex.z * magnitude.z;

                        vertex.x += Mathf.Sin((Time.time * waveSpeed) + y + x + z) * waveScale;

                        if (noiseStrength != 0)
                        {
                            vertex.x += Mathf.PerlinNoise(x + noiseMultiplier, y + Mathf.Sin(Time.time * 0.1f)) * noiseStrength;
                        }

                        break;

                    case (Axes.Y):

                        x = vertex.x * magnitude.x;

                        y = randomFactor[i] * magnitude.y;

                        z = vertex.z * magnitude.z;

                        vertex.y += Mathf.Sin((Time.time * waveSpeed) + y + x + z) * waveScale;

                        if (noiseStrength != 0)
                        {
                            vertex.y += Mathf.PerlinNoise(x + noiseMultiplier, y + Mathf.Sin(Time.time * 0.1f)) * noiseStrength;
                        }

                        break;

                    case (Axes.Z):

                        x = vertex.x * magnitude.x;

                        y = vertex.y * magnitude.y;

                        z = randomFactor[i] * magnitude.z;

                        vertex.z += Mathf.Sin((Time.time * waveSpeed) + y + x + z) * waveScale;

                        if (noiseStrength != 0)
                        {
                            vertex.z += Mathf.PerlinNoise(x + noiseMultiplier, y + Mathf.Sin(Time.time * 0.1f)) * noiseStrength;
                        }

                        break;

                }

                newVertices[i] = vertex;

            }

        }

        #endregion

    }

}