/*
	Для работы с новым плагином видео.
	Обеспечивает настройку MeshRenderer по информации VideoDecoder, управленим видимостью в зависимости
	от состояния и прочее.
	Автор: Кущ А.Е.
*/


using UnityEngine;

[RequireComponent(typeof(UnityToolbag.SortingLayerExposed))]
[RequireComponent(typeof(VideoDecoder))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class VideoScreen : MonoBehaviour
{
	[SerializeField]
	bool setDefaultsToRenderer = true;

	[SerializeField]
	bool hideUntilVideoLoaded = true;

	[SerializeField]
	bool showOnlyWhilePlaying = false;

	[SerializeField]
	bool hideOnVideoStop = false;

	[SerializeField]
	GameObject destroyOnVideoStop;

//	[SerializeField]
//	GameObject enableOnVideoStop;

	[SerializeField]
	bool resizeToVideo = true;

	[SerializeField]
	float pixelsPerUnit = 100f;

	[SerializeField]
	Vector2 scale = new Vector2(1f, 1f);

	VideoDecoder videoDecoder;
	MeshRenderer meshRenderer;

	void Awake()
	{
		meshRenderer = GetComponent<MeshRenderer>();
		videoDecoder = GetComponent<VideoDecoder>();

		if (videoDecoder.Material == null)
			SetDefaultShader();

		videoDecoder.PlayingCompleted += OnPlayingCompleted;
		videoDecoder.PlayingStarted += OnPlayingStarted;

		SetupMeshFilter();
		SetupRenderer();

		if (hideUntilVideoLoaded)
			meshRenderer.enabled = false;
	}
		
	private void SetDefaultShader()
	{
		string shaderName = videoDecoder.HasAlphaChannel ? "Color Space/YUVtoRGBA" : "Color Space/YUVtoRGB";
		videoDecoder.Material = new Material(Shader.Find(shaderName));
	}

	void OnPlayingStarted(VideoDecoder sender)
	{
		OnChangePlayingState();
	}

	void OnPlayingCompleted(VideoDecoder sender)
	{
		OnChangePlayingState();

		if (hideOnVideoStop)
			meshRenderer.enabled = false;

//		if (enableOnVideoStop != null)
//			enableOnVideoStop.SetActive(true);

		if (destroyOnVideoStop != null)
			Destroy(destroyOnVideoStop);
	}

	void OnChangePlayingState()
	{
		UpdateState();
	}

	void OnDestroy()
	{
		videoDecoder.PlayingCompleted -= OnPlayingCompleted;
		videoDecoder.PlayingStarted -= OnPlayingStarted;
	}

	private void SetupMeshFilter()
	{
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
		meshFilter.sharedMesh = quad.GetComponent<MeshFilter>().sharedMesh;
		DestroyImmediate(quad);
	}

	void SetupRenderer()
	{
		meshRenderer.material = videoDecoder.WorkingMaterial;

		if (setDefaultsToRenderer)
		{
			meshRenderer.receiveShadows = false;
			meshRenderer.useLightProbes = false;
			meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
			meshRenderer.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
		}

		if (resizeToVideo)
		{
			float scaleX = scale.x * videoDecoder.Width / pixelsPerUnit;
			float scaleY = scale.y * videoDecoder.Height / pixelsPerUnit;
			transform.localScale = new Vector2(scaleX, scaleY);
		}
	}

	bool meshRendererInitialized = false;
	private void UpdateState()
	{
		if (!meshRendererInitialized && videoDecoder.IsVideoLoaded)
		{
			SetupRenderer();
			meshRenderer.enabled = true;
			meshRendererInitialized = true;
		}

		if (showOnlyWhilePlaying)
			meshRenderer.enabled = videoDecoder.IsPlaying;
	}

	void Update()
	{
		UpdateState();
	}
}
