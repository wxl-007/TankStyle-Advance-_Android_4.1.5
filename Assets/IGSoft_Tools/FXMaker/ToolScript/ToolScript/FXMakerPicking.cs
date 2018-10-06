// ----------------------------------------------------------------------------------
//
// FXMaker
// Created by ismoon - 2012 - ismoonto@gmail.com
//
// ----------------------------------------------------------------------------------

// Attribute ------------------------------------------------------------------------
// Property -------------------------------------------------------------------------
// Loop Function --------------------------------------------------------------------
// Control Function -----------------------------------------------------------------
// Event Function -------------------------------------------------------------------
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System.Collections;
using System.IO;
using System;

public class FXMakerPicking : MonoBehaviour
{
	// Attribute ------------------------------------------------------------------------
	// const

	// Loop Function --------------------------------------------------------------------
	void Awake()
	{
	}

	void OnEnable()
	{
	}
	
	void OnDisable()
	{
	}

	void Start()
	{
	}

	// -----------------------------------------------------------------------------------
	void LateUpdate()
	{
		if (Camera.mainCamera == null)
			return;

		if (FXMakerMain.inst.IsGUIMousePosition())
			return;

		if (FXMakerGizmo.inst.IsActiveAxis())
			return;

  		if (Input.GetMouseButtonDown(0))
		{
			GameObject	pickObj		= null;
			Vector3		clickPos	= Input.mousePosition;
			Ray			ray			= Camera.mainCamera.ScreenPointToRay(clickPos);
// 			RaycastHit	pickedObject;

			// particle
			{
				GameObject			instRoot = FXMakerMain.inst.GetInstanceRoot();
				FxmInfoIndexing[]	coms = instRoot.GetComponentsInChildren<FxmInfoIndexing>(true);

				foreach (FxmInfoIndexing fxmInfoIndexing in coms)
				{
					if (fxmInfoIndexing.IsPickingParticle(ray))
					{
						pickObj = fxmInfoIndexing.gameObject;
						break;
					}
				}
			}

			// mesh
//			if (pickObj == null && Physics.Raycast(ray, out pickedObject))
			if (pickObj == null)
			{
				RaycastHit[]	hits = Physics.RaycastAll(ray);
				if (hits != null && 0 < hits.Length)
				{
					float fMinDist = -1;
					foreach (RaycastHit hit in hits)
					{
						float dist = Vector3.Distance(hit.point, hit.collider.bounds.center);
						if (fMinDist < 0 || dist < fMinDist)
						{
							if (hit.transform.gameObject.GetComponent<FxmInfoIndexing>() != null)
							{
								fMinDist = dist;
								pickObj	= hit.transform.gameObject;
							}
						}
					}
				}
			}

			// selected
			if (pickObj != null)
			{
				FxmInfoIndexing indexCom = pickObj.GetComponent<FxmInfoIndexing>();
				if (indexCom != null && indexCom.m_OriginalTrans.gameObject != FXMakerMain.inst.GetFXMakerHierarchy().GetSelectedGameObject())
				{
					FXMakerMain.inst.GetFXMakerHierarchy().SetActiveGameObject(indexCom.m_OriginalTrans.gameObject);
				}
			}
		}
	}

	// Property -------------------------------------------------------------------------
	// Control Function -----------------------------------------------------------------
	// Event Function -------------------------------------------------------------------
}
#endif
