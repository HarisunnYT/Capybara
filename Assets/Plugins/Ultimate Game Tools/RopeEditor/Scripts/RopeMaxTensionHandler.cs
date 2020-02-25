using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeMaxTensionHandler : MonoBehaviour
{
	private void Start ()
  {
    _rope = GetComponent<UltimateRope>();
    _started = false;
    _dynamicEnd = false;
  }
	
	private void LateUpdate ()
  {
    if(_rope != null && _rope.RopeStart != null && _rope.RopeNodes != null && _rope.RopeNodes.Count > 0 && _rope.RopeNodes[0].goNode != null)
    {
      // Max tension reached?

      if(Vector3.Distance(_rope.RopeStart.transform.position, _rope.RopeNodes[0].goNode.transform.position) > (_rope.RopeNodes[0].fLength * _lengthThreshold))
      {
        // Send notifications?

        if (_started == false)
        {
          _started = true;

          if(_eventCallGameObject != null && !string.IsNullOrEmpty(_maxTensionStartCallMethod))
          {
            _eventCallGameObject.SendMessage(_maxTensionStartCallMethod);
          }
        }
        else
        {
          if (_eventCallGameObject != null && !string.IsNullOrEmpty(_maxTensionContinueCallMethod))
          {
            _eventCallGameObject.SendMessage(_maxTensionContinueCallMethod);
          }
        }

        // Make end dynamic? (unplug something)

        if(_dynamicEnd == false && _makeEndDynamicOnMaxTension)
        {
          Rigidbody rigidBodyEnd = _rope.RopeNodes[0].goNode.GetComponent<Rigidbody>();

          if(rigidBodyEnd.isKinematic)
          {
            rigidBodyEnd.isKinematic = false;
            _dynamicEnd = true;
          }

          Vector3 dir = (_rope.RopeNodes[0].goNode.transform.position - _rope.RopeStart.transform.position).normalized;
          rigidBodyEnd.transform.position = _rope.RopeStart.transform.position + (dir * _rope.RopeNodes[0].fLength);
        }

        // Fix haywire? (Visual fix)

        if (_fixHaywire)
        {
          FixHaywire();
        }
      }
    }

    // Send end max tension reached notification?

    if(_started)
    {
      _started = false;

      if (_eventCallGameObject != null && !string.IsNullOrEmpty(_maxTensionEndCallMethod))
      {
        _eventCallGameObject.SendMessage(_maxTensionEndCallMethod);
      }
    }
	}

  private void FixedUpdate()
  {
    // Fix haywire? (physics step)

    if (_fixHaywire && _started)
    {
      FixHaywire();
    }
  }

  private void FixHaywire()
  {
    Vector3 startPos = _rope.RopeStart.transform.position;
    Vector3 endPos   = _rope.RopeNodes[0].goNode.transform.position;

    Vector3 dir = (_rope.RopeNodes[0].goNode.transform.position - _rope.RopeStart.transform.position);
    float length = dir.magnitude;
    dir = dir.normalized;

    int linkCount = _rope.RopeNodes[0].segmentLinks.Length;    

    for (int i = 0; i < linkCount; ++i)
    {
      float t = i / (float)linkCount;

      if(i == linkCount - 1)
      {
        float normalizedLinkLength = ((1.0f / linkCount) * _rope.RopeNodes[0].fLength) / length;
        t = 1.0f - normalizedLinkLength;
      }

      _rope.RopeNodes[0].segmentLinks[i].transform.position = startPos + (dir * t * length);

      Quaternion rotation = Quaternion.LookRotation(endPos - startPos);
      _rope.RopeNodes[0].segmentLinks[i].transform.rotation = rotation;
    }
  }

  [SerializeField]
  private float _lengthThreshold = 1.01f;
  [SerializeField]
  public string _maxTensionStartCallMethod = "";
  [SerializeField]
  public string _maxTensionEndCallMethod = "";
  [SerializeField]
  public string _maxTensionContinueCallMethod = "";
  [SerializeField]
  public GameObject _eventCallGameObject = null;
  [SerializeField]
  public bool _fixHaywire = false;
  [SerializeField]
  public bool _makeEndDynamicOnMaxTension = false;

  private UltimateRope _rope;
  private bool _started;
  private bool _dynamicEnd;
}
