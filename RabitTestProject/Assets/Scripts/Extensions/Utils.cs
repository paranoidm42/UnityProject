using UnityEngine;

namespace rabbit_game
{
    public  class Utils : MonoBehaviour
    {
         public static float EaseInOut(float time, float duration) {
            return -0.5f * (Mathf.Cos(Mathf.PI * time / duration) - 1.0f);
        }
        
        public  static float EaseOutSine(float time, float duration) {
            return Mathf.Sin(time/duration * Mathf.PI * 0.5f);
        }

    }
}
