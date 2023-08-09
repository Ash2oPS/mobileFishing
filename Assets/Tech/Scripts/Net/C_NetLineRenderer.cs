using EtienneSibeaux.Debugger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EtienneSibeaux.Net
{
    [RequireComponent(typeof(LineRenderer))]
    public class C_NetLineRenderer : MonoBehaviour
    {
        [Header("---References---")]
        [SerializeField] private C_Net _net;

        [SerializeField] private LineRenderer _lineRenderer;

        private Vector3[] _positions;

        private void Awake()
        {
            _lineRenderer.positionCount = 2;
            _positions = new Vector3[2];
            UpdateLineRendererPositions();
        }

        public void UpdateLineRendererPositions()
        {
            _positions[0] = transform.position;
            _positions[1] = _net.Player.NetParent.transform.position;
            _lineRenderer.SetPositions(_positions);
        }
    }
}