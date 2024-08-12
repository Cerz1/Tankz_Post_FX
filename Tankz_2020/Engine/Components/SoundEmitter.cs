using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Audio;

namespace Tankz_2020
{
    class SoundEmitter : Component
    {
        AudioSource source;
        AudioClip clip;

        public float Volume { get { return source.Volume; } set { source.Volume = value; } }
        public float Pitch { get { return source.Pitch; } set { source.Pitch = value; } }

        public SoundEmitter(GameObject owner, string clipName) : base(owner)
        {
            source = new AudioSource();
            clip = GfxMgr.GetClip(clipName);
        }

        public void Play(float volume, float pitch = 1f)
        {
            source.Volume = volume;
            source.Pitch = pitch;
            source.Play(clip);
        }

        public void Play()
        {
            source.Play(clip);
        }


    }
}
