#region Assembly UIEffect, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
#endregion

using Coffee.UIEffects;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[AddComponentMenu("UI/UIEffects/UIDissolve", 3)]
public class CustomUIDissolve : BaseMaterialEffect, IMaterialModifier
{
	private const uint k_ShaderId = 0u;

	private static readonly ParameterTexture s_ParamTex = new ParameterTexture(8, 128, "_ParamTex");

	private static readonly int k_TransitionTexId = Shader.PropertyToID("_TransitionTex");

	private bool _lastKeepAspectRatio;

	private EffectArea _lastEffectArea;

	private static Texture _defaultTransitionTexture;

	[Tooltip("Current location[0-1] for dissolve effect. 0 is not dissolved, 1 is completely dissolved.")]
	[FormerlySerializedAs("m_Location")]
	[SerializeField]
	[Range(0f, 1f)]
	private float m_EffectFactor = 0.5f;

	[Tooltip("Edge width.")]
	[SerializeField]
	[Range(0f, 1f)]
	private float m_Width = 0.5f;

	[Tooltip("Edge softness.")]
	[SerializeField]
	[Range(0f, 1f)]
	private float m_Softness = 0.5f;

	[Tooltip("Edge color.")]
	[SerializeField]
	[ColorUsage(false)]
	private Color m_Color = new Color(0f, 0.25f, 1f);

	[Tooltip("Edge color effect mode.")]
	[SerializeField]
	private ColorMode m_ColorMode = ColorMode.Add;

	[Tooltip("Noise texture for dissolving (single channel texture).")]
	[SerializeField]
	[FormerlySerializedAs("m_NoiseTexture")]
	private Texture m_TransitionTexture;

	[Header("Advanced Option")]
	[Tooltip("The area for effect.")]
	[SerializeField]
	protected EffectArea m_EffectArea;

	[Tooltip("Keep effect aspect ratio.")]
	[SerializeField]
	private bool m_KeepAspectRatio;

	[Header("Effect Player")]
	[SerializeField]
	private EffectPlayer m_Player;

	[Tooltip("Reverse the dissolve effect.")]
	[FormerlySerializedAs("m_ReverseAnimation")]
	[SerializeField]
	private bool m_Reverse = false;
	public bool Reverse 
	{
		get => m_Reverse;
		set => m_Reverse = value;
	}

	public float effectFactor
	{
		get
		{
			return m_EffectFactor;
		}
		set
		{
			value = Mathf.Clamp(value, 0f, 1f);
			if (!Mathf.Approximately(m_EffectFactor, value))
			{
				m_EffectFactor = value;
				SetEffectParamsDirty();
			}
		}
	}

	public float width
	{
		get
		{
			return m_Width;
		}
		set
		{
			value = Mathf.Clamp(value, 0f, 1f);
			if (!Mathf.Approximately(m_Width, value))
			{
				m_Width = value;
				SetEffectParamsDirty();
			}
		}
	}

	public float softness
	{
		get
		{
			return m_Softness;
		}
		set
		{
			value = Mathf.Clamp(value, 0f, 1f);
			if (!Mathf.Approximately(m_Softness, value))
			{
				m_Softness = value;
				SetEffectParamsDirty();
			}
		}
	}

	public Color color
	{
		get
		{
			return m_Color;
		}
		set
		{
			if (!(m_Color == value))
			{
				m_Color = value;
				SetEffectParamsDirty();
			}
		}
	}

	public Texture transitionTexture
	{
		get
		{
			return m_TransitionTexture ? m_TransitionTexture : defaultTransitionTexture;
		}
		set
		{
			if (!(m_TransitionTexture == value))
			{
				m_TransitionTexture = value;
				SetMaterialDirty();
			}
		}
	}

	private static Texture defaultTransitionTexture => _defaultTransitionTexture ? _defaultTransitionTexture : (_defaultTransitionTexture = Resources.Load<Texture>("Default-Transition"));

	public EffectArea effectArea
	{
		get
		{
			return m_EffectArea;
		}
		set
		{
			if (m_EffectArea != value)
			{
				m_EffectArea = value;
				SetVerticesDirty();
			}
		}
	}

	public bool keepAspectRatio
	{
		get
		{
			return m_KeepAspectRatio;
		}
		set
		{
			if (m_KeepAspectRatio != value)
			{
				m_KeepAspectRatio = value;
				SetVerticesDirty();
			}
		}
	}

	public ColorMode colorMode
	{
		get
		{
			return m_ColorMode;
		}
		set
		{
			if (m_ColorMode != value)
			{
				m_ColorMode = value;
				SetMaterialDirty();
			}
		}
	}

	public override ParameterTexture paramTex => s_ParamTex;

	public EffectPlayer effectPlayer => m_Player ?? (m_Player = new EffectPlayer());

	public override Hash128 GetMaterialHash(Material material)
	{
		if (!base.isActiveAndEnabled || !material || !material.shader)
		{
			return BaseMaterialEffect.k_InvalidHash;
		}

		uint u32_ = (uint)((int)m_ColorMode << 6);
		uint instanceID = (uint)transitionTexture.GetInstanceID();
		return new Hash128((uint)material.GetInstanceID(), u32_, instanceID, 0u);
	}

	public override void ModifyMaterial(Material newMaterial, Graphic graphic)
	{
		GraphicConnector graphicConnector = GraphicConnector.FindConnector(graphic);
		newMaterial.shader = Shader.Find($"Hidden/{newMaterial.shader.name} (UIDissolve)");
		SetShaderVariants(newMaterial, m_ColorMode);
		newMaterial.SetTexture(k_TransitionTexId, transitionTexture);
		paramTex.RegisterMaterial(newMaterial);
	}

	public override void ModifyMesh(VertexHelper vh, Graphic graphic)
	{
		if (base.isActiveAndEnabled)
		{
			float normalizedIndex = paramTex.GetNormalizedIndex(this);
			Texture texture = transitionTexture;
			float aspectRatio = ((m_KeepAspectRatio && (bool)texture) ? ((float)texture.width / (float)texture.height) : (-1f));
			Rect rect = m_EffectArea.GetEffectArea(vh, base.rectTransform.rect, aspectRatio);
			UIVertex vertex = default(UIVertex);
			int currentVertCount = vh.currentVertCount;
			for (int i = 0; i < currentVertCount; i++)
			{
				vh.PopulateUIVertex(ref vertex, i);
				base.connector.GetPositionFactor(m_EffectArea, i, rect, vertex.position, out var x, out var y);
				vertex.uv0 = new Vector2(Packer.ToFloat(vertex.uv0.x, vertex.uv0.y), Packer.ToFloat(x, y, normalizedIndex));
				vh.SetUIVertex(vertex, i);
			}
		}
	}

	protected override void SetEffectParamsDirty()
	{
		paramTex.SetData(this, 0, m_EffectFactor);
		paramTex.SetData(this, 1, m_Width);
		paramTex.SetData(this, 2, m_Softness);
		paramTex.SetData(this, 4, m_Color.r);
		paramTex.SetData(this, 5, m_Color.g);
		paramTex.SetData(this, 6, m_Color.b);
	}

	protected override void SetVerticesDirty()
	{
		base.SetVerticesDirty();
		_lastKeepAspectRatio = m_KeepAspectRatio;
		_lastEffectArea = m_EffectArea;
	}

	protected override void OnDidApplyAnimationProperties()
	{
		base.OnDidApplyAnimationProperties();
		if (_lastKeepAspectRatio != m_KeepAspectRatio || _lastEffectArea != m_EffectArea)
		{
			SetVerticesDirty();
		}
	}

	public void Play(bool reset = true)
	{
		effectPlayer.Play(reset);
	}

	public void Stop(bool reset = true)
	{
		effectPlayer.Stop(reset);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		effectPlayer.OnEnable(delegate (float f)
		{
			effectFactor = (m_Reverse ? (1f - f) : f);
		});
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		effectPlayer.OnDisable();
	}
}
#if false // Decompilation log
'257' items in cache
------------------
Resolve: 'netstandard, Version=2.1.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Found single assembly: 'netstandard, Version=2.1.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Load from: 'E:\Frameworks\Unity\versions\2022.3.10f1\Editor\Data\NetStandard\ref\2.1.0\netstandard.dll'
------------------
Resolve: 'UnityEngine.CoreModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'UnityEngine.CoreModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'E:\Frameworks\Unity\versions\2022.3.10f1\Editor\Data\Managed\UnityEngine\UnityEngine.CoreModule.dll'
------------------
Resolve: 'UnityEngine.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'UnityEngine.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'E:\Repositories\Unity\PyramidCollection\Library\ScriptAssemblies\UnityEngine.UI.dll'
------------------
Resolve: 'UnityEngine.TextRenderingModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'UnityEngine.TextRenderingModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'E:\Frameworks\Unity\versions\2022.3.10f1\Editor\Data\Managed\UnityEngine\UnityEngine.TextRenderingModule.dll'
------------------
Resolve: 'UnityEngine.AnimationModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'UnityEngine.AnimationModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'E:\Frameworks\Unity\versions\2022.3.10f1\Editor\Data\Managed\UnityEngine\UnityEngine.AnimationModule.dll'
------------------
Resolve: 'UnityEngine.UIModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'UnityEngine.UIModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'E:\Frameworks\Unity\versions\2022.3.10f1\Editor\Data\Managed\UnityEngine\UnityEngine.UIModule.dll'
------------------
Resolve: 'UnityEditor.CoreModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'UnityEditor.CoreModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Load from: 'E:\Frameworks\Unity\versions\2022.3.10f1\Editor\Data\Managed\UnityEngine\UnityEditor.CoreModule.dll'
------------------
Resolve: 'System.Runtime.InteropServices, Version=2.1.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'System.Runtime.InteropServices, Version=4.1.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
WARN: Version mismatch. Expected: '2.1.0.0', Got: '4.1.2.0'
Load from: 'E:\Frameworks\Unity\versions\2022.3.10f1\Editor\Data\NetStandard\compat\2.1.0\shims\netstandard\System.Runtime.InteropServices.dll'
------------------
Resolve: 'System.Runtime.CompilerServices.Unsafe, Version=2.1.0.0, Culture=neutral, PublicKeyToken=null'
Could not find by name: 'System.Runtime.CompilerServices.Unsafe, Version=2.1.0.0, Culture=neutral, PublicKeyToken=null'
#endif
