using UnityEngine;

namespace MagestyUtilities.AudioSystem
{
    public static class AudioSourceExtension 
    {
        /// <summary>
        /// Is the AudioSource pitch less than zero
        /// </summary>
        /// <param name="source">AudioSource to check</param>
        /// <returns></returns>
        public static bool IsReversePitch(this AudioSource source) 
        {
            return source.pitch < 0f;
        }

        /// <summary>
        /// Calculate the remainingTime of the given AudioSource, if we keep playing with the same pitch
        /// </summary>
        /// <param name="source">AuidoSource to calculate remaining time for</param>
        /// <returns></returns>
        public static float GetClipRemainingTime(this AudioSource source) {
            float remainingTime = (source.clip.length - source.time) / source.pitch;
            return source.IsReversePitch() ?
                (source.clip.length + remainingTime) :
                remainingTime;
        }
    }
}