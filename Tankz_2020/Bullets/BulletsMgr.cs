﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankz_2020
{
    static class BulletsMgr
    {
        private static Queue<Bullet>[] bullets;
        private static int queueSize = 1;

        public static void Init()
        {
            bullets = new Queue<Bullet>[(int)BulletType.LAST];

            Type[] bulletTypes = new Type[(int)BulletType.LAST];
            bulletTypes[0] = typeof(StdBullet);
            bulletTypes[1] = typeof(Rocket);

            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i] = new Queue<Bullet>(queueSize);

                for (int j = 0; j < queueSize; j++)
                {
                    Bullet b = (Bullet)Activator.CreateInstance(bulletTypes[i]);
                    bullets[i].Enqueue(b);
                }

            }
        }

        public static Bullet GetBullet(BulletType type)
        {
            Bullet b = null;
            int queueIndex = (int)type;

            if (bullets[queueIndex].Count > 0)
            {
                b = bullets[queueIndex].Dequeue();
                b.Reset();
            }

            return b;
        }

        public static void RestoreBullet(Bullet b)
        {
            b.IsActive = false;
            bullets[(int)b.Type].Enqueue(b);
        }

        public static void ClearAll()
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i].Clear();
            }
            bullets = null;
        }
    }
}

