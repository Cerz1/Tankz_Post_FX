using Aiv.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankz_2020
{
    class RandomizeSoundEmitter : Component
    {
        AudioSource source;
        List<AudioClip> clips;

        public float Volume { get { return source.Volume; } set { source.Volume = value; } }

        public RandomizeSoundEmitter(GameObject owner) : base(owner)
        {
            source = new AudioSource();
            clips = new List<AudioClip>();
        }

        public void AddClip(string clipName)
        {
            clips.Add(GfxMgr.GetClip(clipName));
        }

        public void Play(float volume)
        {
            source.Volume = volume;
            RandomizePitch();
            source.Play(GetRandomClip());
        }

        public void Play()
        {
            RandomizePitch();
            source.Play(GetRandomClip());
        }

        protected void RandomizePitch()
        {
            source.Pitch = RandomGenerator.GetRandomFloat() * 0.4f + 0.8f;//0.8 => 1.2
        }

        protected AudioClip GetRandomClip()
        {
            return clips[RandomGenerator.GetRandomInt(0, clips.Count)];
        }
    }
}
