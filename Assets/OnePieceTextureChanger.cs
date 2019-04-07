using System.IO;
using SFB;
using UnityEngine;


public class OnePieceTextureChanger : MonoBehaviour
{

	[SerializeField] private Renderer modelRenderer;
	[SerializeField] private Animator animator;
	
	private static readonly ExtensionFilter[] ExtensionFilters = {
		new ExtensionFilter("Image Files", "png"),
	};

	public void ChangeOnePieceTexture()
	{
		var materials = modelRenderer.materials;
		var onePieceMaterial = materials[3];
		var onePieceShadeMaterial = materials[4];
		
		StandaloneFileBrowser.OpenFilePanelAsync("ワンピーステクスチャ", "", ExtensionFilters, false, (readPath) =>
		{
			if (readPath.Length > 0 && !string.IsNullOrEmpty(readPath[0]))
			{
				var path = readPath[0];
				var fileData = File.ReadAllBytes(path);
				var newTexture = new Texture2D(2, 2);
				newTexture.LoadImage(fileData);

				onePieceMaterial.mainTexture = newTexture;
				onePieceMaterial.SetTexture("_ShadeTexture", newTexture);
				onePieceShadeMaterial.mainTexture = newTexture;
				onePieceShadeMaterial.SetTexture("_ShadeTexture", newTexture);
				animator.SetInteger("mode", 1);
				Debug.Log(animator.GetInteger("mode"));
			}
		});
	}
}
