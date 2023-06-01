Shader "Hidden/testVolume"
{
	Properties
	{
		_TintColor("TintColor",Color) = (1,1,1,1)
		_VolumeTex("Texture", 3D) = "white" {}
		_Intensity("Intensity", Range(0.0001, 4.0)) = 1
		_Density("Density", Range(0.01, 0.9)) = 0.5
		_Contrast("Contrast", Range(0.0, 2.0)) = 1
		_SliceMin("SliceMin",Vector) = (0,0,0,1)
		_SliceMax("SliceMax",Vector) = (1,1,1,1)
	}
		SubShader
		{
			Tags{"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
			Pass
			{
				Tags{"LightMode" = "ForwardBase"}
				ZWrite Off
				Cull back
				Blend SrcAlpha OneMinusSrcAlpha
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"
				sampler3D _VolumeTex;
				float4 _TintColor;
				float _Intensity;
				float _Density;
				float _Contrast;
				float3 _SliceMin;
				float3 _SliceMax;
				float4x4 _AxisRotationMatrix;
				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
					float3 vertexLocal:TEXCOORD1;
				};

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					o.vertexLocal = v.vertex.xyz;//object Space的点
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
				#define NUM_STEPS 128 //迭代次数越高，精度越高
					const float stepSize = 1.732f / NUM_STEPS;//1.732f
					float3 rayStartPos = i.vertexLocal + float3(0.5, 0.5, 0.5);//映射到0~1的box空间
					float3 rayDir = normalize(ObjSpaceViewDir(float4(i.vertexLocal, 0)));//归一化射线向量

					float4 col = float4(0, 0, 0, 0);
					float maxDensity = 0;
					float clipV = 0;
					[unroll]//循环展开，并行计算
					for (uint iStep = 0; iStep < NUM_STEPS; iStep++) {//使用无符号整型，因为负数会破坏循环展开
						 float t = iStep * stepSize;
						 float3 currPos = rayStartPos - rayDir * t;
						if (currPos.x < -0.001f || currPos.x>1.001f || currPos.y < -0.001f || currPos.y>1.001f || currPos.z < -0.001f || currPos.z>1.001f) {
							break;
						}
						if (currPos.x < _SliceMin.x || currPos.x>_SliceMax.x || currPos.y < _SliceMin.y || currPos.y>_SliceMax.y || currPos.z < _SliceMin.z || currPos.z>_SliceMax.z) {
							continue;
						}
						fixed c = tex3D(_VolumeTex, currPos).r * _Intensity;//取样3dtexture
						//自定义色彩混合区域 ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
						fixed4 src = float4(c, c, c, c);
						src.a *= (1 - _Density) * pow(src.a, _Contrast);//_Density值越大，颜色将越早地截止，因而也就不会采样到深层
						src.rgb *= src.a;
						col = (1 - col.a) * src + col;//柔性叠加
						//自定义色彩混合区域 ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑
						if (col.a > 1) break;
					}
					clip(col.a - 0.5);//裁切掉边缘不需要的区域
					return col * _TintColor;
				}
				ENDCG
			}
		}
}
