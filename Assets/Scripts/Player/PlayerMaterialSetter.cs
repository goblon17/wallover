using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaterialSetter : MonoBehaviour
{
	[SerializeField]
	protected Renderer renderer;

	public Material PlayerMaterial { set => renderer.material = value; }
}
