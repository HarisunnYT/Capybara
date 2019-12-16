///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 19/03/2018
/// Author: Chloroplast Games
/// Website: http://www.chloroplastgames.com
/// Programmers: Pau Elias Soriano
/// Description: Blend mode helper functions.
///

#ifndef BLEND_MODES
#define BLEND_MODES

	/// \english
    /// <summary>
    /// Convert Hue values to RGB values.
    /// </summary>
    /// <param name="pqt">Hue values.</param>
	/// <returns>RGB values.</returns>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Convierte los valores en Hue a valores en RGB.
    /// </summary>
    /// <param name="pqt">Valores en Hue.</param>
	/// <returns>Valores en RGB.</returns>
    /// \endspanish
	fixed HueToRgb(fixed3 pqt)
	{

		if (pqt.z < .0) pqt.z += 1.0;

		if (pqt.z > 1.0) pqt.z -= 1.0;

		if (pqt.z < 1.0 / 6.0) return pqt.x + (pqt.y - pqt.x) * 6.0 * pqt.z;

		if (pqt.z < 1.0 / 2.0) return pqt.y;

		if (pqt.z < 2.0 / 3.0) return pqt.x + (pqt.y - pqt.x) * (2.0 / 3.0 - pqt.z) * 6.0;

		return pqt.x;

	}

	/// \english
    /// <summary>
    /// Convert HSL values to RGB values.
    /// </summary>
    /// <param name="hsl">HSL values.</param>
	/// <returns>RGB values.</returns>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Convierte los valores en HSL a valores en RGB.
    /// </summary>
    /// <param name="hsl">Valores en HSL.</param>
	/// <returns>Valores en RGB.</returns>
    /// \endspanish
	fixed3 HslToRgb (fixed3 hsl)
	{ 

		fixed3 rgb;

		fixed3 pqt;

		if (hsl.y == 0)
		{

			rgb = hsl.z;

		}
		else
		{

			pqt.y = hsl.z < .5 ? hsl.z * (1.0 + hsl.y) : hsl.z + hsl.y - hsl.z * hsl.y;

			pqt.x = 2.0 * hsl.z - pqt.y;

			rgb.r = HueToRgb(fixed3(pqt.x, pqt.y, hsl.x + 1.0 / 3.0));

			rgb.g = HueToRgb(fixed3(pqt.x, pqt.y, hsl.x));

			rgb.b = HueToRgb(fixed3(pqt.x, pqt.y, hsl.x - 1.0 / 3.0));

		}

		return rgb;

	}

	/// \english
    /// <summary>
    /// Convert RGB values to HSL values.
    /// </summary>
    /// <param name="rgb">RGB values.</param>
	/// <returns>HSL values.</returns>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Convierte los valores en RGB a valores en HSL.
    /// </summary>
    /// <param name="rgb">Valores en RGB.</param>
	/// <returns>Valores en HSL.</returns>
    /// \endspanish
	fixed3 RgbToHsl(fixed3 rgb)
	{

		fixed maxC = max(rgb.r, max(rgb.g, rgb.b));

		fixed minC = min(rgb.r, min(rgb.g, rgb.b));

		fixed3 hsl;

		hsl = (maxC + minC) / 2.0;

		if (maxC == minC)
		{

			hsl.x = hsl.y = .0;

		}
		else
		{

			fixed d = maxC - minC;

			hsl.y = (hsl.z > .5) ? d / (2.0 - maxC - minC) : d / (maxC + minC);

			if (rgb.r > rgb.g && rgb.r > rgb.b)
			{

        		hsl.x = (rgb.g - rgb.b) / d + (rgb.g < rgb.b ? 6.0 : .0);

			}
			else if (rgb.g > rgb.b) 
			{

        		hsl.x = (rgb.b - rgb.r) / d + 2.0;

			}
			else
			{

        		hsl.x = (rgb.r - rgb.g) / d + 4.0;

			}

			hsl.x /= 6.0f;

		}

		return hsl;

	}

	/// \english
    /// <summary>
    /// Desaturate a color.
    /// </summary>
    /// <param name="color">Color to desaturate.</param>
	/// <returns>Desaturated color.</returns>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Desatura un color.
    /// </summary>
    /// <param name="color">Color a desaturar.</param>
	/// <returns>Color desaturado.</returns>
    /// \endspanish
	fixed Desaturate (fixed4 color)
	{
	
		return 0.299 * color.r + 0.587 * color.g + 0.114 * color.b;

	}

	/// \english
    /// <summary>
    /// Calcule the color of blend modes.
    /// </summary>
    /// <param name="src">Source color.</param>
	/// <param name="dst">Destine color.</param>
	/// <returns>Color of blend mode.</returns>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Calcula el color de los modos de fusión.
    /// </summary>
    /// <param name="src">Color fuente.</param>
	/// <param name="dst">Color de destino.</param>
	/// <returns>Color del modo de fucsión.</returns>
    /// \endspanish
	fixed4 CalculateBlendMode (fixed4 src, fixed4 dst)
	{ 

		#ifdef _BLENDMODE_DARKEN

			fixed4 result = min(src, dst);

			result.a = dst.a;

			return result;

		#elif _BLENDMODE_MULTIPLY

			fixed4 result = src * dst;

			result.a = dst.a;

			return result;

		#elif _BLENDMODE_COLORBURN

			fixed4 result = 1.0 - (1.0 - src) / dst;

			result.a = dst.a;

			return saturate(result);

		#elif _BLENDMODE_LINEARBURN

			fixed4 result = src + dst - 1.0;

			result.a = dst.a;

			return result;

		#elif _BLENDMODE_DARKERCOLOR

			fixed4 result = Desaturate(src) < Desaturate(dst) ? src : dst;

			result.a = dst.a;

			return result; 

		#elif _BLENDMODE_LIGHTEN

			fixed4 result = max(src, dst);

			result.a = dst.a;

			return result;

		#elif _BLENDMODE_SCREEN

			fixed4 result = 1.0 - (1.0 - src) * (1.0 - dst);

			result.a = dst.a;

			return result;

		#elif _BLENDMODE_COLORDODGE

			fixed4 result = src / (1.0 - dst);

			result.a = dst.a;

			return saturate(result);

		#elif _BLENDMODE_LINEARDODGE

			fixed4 result = src + dst;

			result.a = dst.a;

			return result;

		#elif _BLENDMODE_LIGHTERCOLOR

			fixed4 result = Desaturate(src) > Desaturate(dst) ? src : dst;

			result.a = dst.a;

			return result; 

		#elif _BLENDMODE_OVERLAY

			fixed4 result = src > 0.5 ? 1.0 - 2.0 * (1.0 - src) * (1.0 - dst) : 2.0 * src * dst;
			result.a = dst.a;
			return result;

		#elif _BLENDMODE_SOFTLIGHT

			fixed4 result = (1.0 - src) * src * dst + src * (1.0 - (1.0 - src) * (1.0 - dst));

			result.a = dst.a;

			return result;

		#elif _BLENDMODE_HARDLIGHT

			fixed4 result = dst > 0.5 ? 1.0 - (1.0 - src) * (1.0 - 2.0 * (dst - 0.5)) : src * (2.0 * dst);

			result.a = dst.a;

			return result;

		#elif _BLENDMODE_VIVIDLIGHT

			fixed4 result = dst > 0.5 ? src / (1.0 - (dst - 0.5) * 2.0) : 1.0 - (1.0 - src) / (dst * 2.0);

			result.a = dst.a;

			return saturate(result);

		#elif _BLENDMODE_LINEARLIGHT

			fixed4 result = dst > 0.5 ? src + 2.0 * (dst - 0.5) : src + 2.0 * dst - 1.0;

			result.a = dst.a;

			return result;

		#elif _BLENDMODE_PINLIGHT

			fixed4 result = dst > 0.5 ? max(src, 2.0 * (dst - 0.5)) : min(src, 2.0 * dst);

			result.a = dst.a;

			return result;

		#elif _BLENDMODE_HARDMIX

			fixed4 result = (dst > 1.0 - src) ? 1.0 : 0.0;

			result.a = dst.a;

			return result;

		#elif _BLENDMODE_DIFFERENCE

			fixed4 result = abs(src - dst);

			result.a = dst.a;

			return result;

		#elif _BLENDMODE_EXCLUSION

			fixed4 result = src + dst - 2.0 * src * dst;

			result.a = dst.a;

			return result; 

		#elif _BLENDMODE_SUBTRACT

			fixed4 result = src - dst;

			result.a = dst.a;

			return result;

		#elif _BLENDMODE_DIVIDE

			fixed4 result = src / dst;

			result.a = dst.a;

			return result;

		#elif _BLENDMODE_HUE

			fixed3 aHsl = RgbToHsl(src.rgb);

			fixed3 bHsl = RgbToHsl(dst.rgb);

			fixed3 rHsl = fixed3(bHsl.x, aHsl.y, aHsl.z);

			return fixed4(HslToRgb(rHsl), dst.a);

		#elif _BLENDMODE_SATURATION

			fixed3 aHsl = RgbToHsl(src.rgb);

			fixed3 bHsl = RgbToHsl(dst.rgb);

			fixed3 rHsl = fixed3(aHsl.x, bHsl.y, aHsl.z);

			return fixed4(HslToRgb(rHsl), dst.a);

		#elif _BLENDMODE_COLOR

			fixed3 aHsl = RgbToHsl(src.rgb);

			fixed3 bHsl = RgbToHsl(dst.rgb);

			fixed3 rHsl = fixed3(bHsl.x, bHsl.y, aHsl.z);

			return fixed4(HslToRgb(rHsl), dst.a);

		#elif _BLENDMODE_LUMINOSITY

			fixed3 aHsl = RgbToHsl(src.rgb);

			fixed3 bHsl = RgbToHsl(dst.rgb);

			fixed3 rHsl = fixed3(aHsl.x, aHsl.y, bHsl.z);

			return fixed4(HslToRgb(rHsl), dst.a);

		#else

			return dst;

		#endif

	}

#endif