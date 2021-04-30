using UnityEngine;

public static class SamplingUtility
{
  
  public float GetNormalizedTime(float timeIntervalDuration = 5.0f)
  {
    return (Time.time % timeIntervalDuration) / timeIntervalDuration;
  }
  
  public float SampleTexture_Grayscale(Texture2D tex, float x, float y)
  {
    return tex.GetPixelBilinear(x,y).grayscale;
  }

  public float SampleCurve(AnimationCurve crv, float x)
  {
    return crv.Evaluate(x);
  }

}
