﻿using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Audio;

namespace Tankz_2020
{
    enum SpecialFX { Explosion_1, LAST}

    static class GfxMgr
    {
        //Dictionary string,Texture
        private static Dictionary<string, Texture> textures;
        private static Dictionary<string, AudioClip> clips;

        private static List<GameObject>[] specialFx;

        static GfxMgr()
        {
            //init dictionary
            textures = new Dictionary<string, Texture>();
            clips = new Dictionary<string, AudioClip>();
        }

        public static void InitFX()
        {
            specialFx = new List<GameObject>[(int)SpecialFX.LAST];

            specialFx[(int)SpecialFX.Explosion_1] = new List<GameObject>();
            specialFx[(int)SpecialFX.Explosion_1].Add(new Explosion1());
        }

        public static AudioClip AddClip(string name, string path)
        {
            AudioClip c = new AudioClip(path);
            if (c != null)
            {
                clips[name] = c;
            }
            return c;
        }

        public static AudioClip GetClip(string name)
        {
            AudioClip c = null;
            if (clips.ContainsKey(name))
            {
                c = clips[name];
            }
            return c;
        }

        public static Texture AddTexture(string name, string path)
        {
            Texture t = new Texture(path);
            if (t != null)
            {
                textures[name] = t;
            }
            return t;
        }

        public static Texture GetTexture(string name)
        {
            Texture t = null;
            if (textures.ContainsKey(name))
            {
                t = textures[name];
            }
            return t;
        }

        public static GameObject GetSpecialFX(SpecialFX type)
        {
            GameObject fx = null;
            int listIndex = (int)type;

            for (int i = 0; i < specialFx[listIndex].Count; i++)
            {
                if (!specialFx[listIndex][i].IsActive)
                {
                    return specialFx[listIndex][i];
                }
            }

            return fx;
        }


        public static void ClearAll()
        {
            textures.Clear();
            clips.Clear();
        }
    }
}
