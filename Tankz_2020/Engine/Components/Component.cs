using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankz_2020
{
    enum ComponentType { SoundEmitter, RandomizeSoundEmitter, Animation }
    abstract class Component
    {
        protected bool isEnabled;

        public bool IsEnabled
        {
            get { return isEnabled && GameObject.IsActive; }
            set { isEnabled = value; }
        }

        public GameObject GameObject { get; protected set; }
        public Component(GameObject owner)
        {
            GameObject = owner;
        }
    }
}
