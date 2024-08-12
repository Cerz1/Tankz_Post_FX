using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankz_2020
{
    interface IDrawable
    {
        DrawLayer Layer { get; }
        void Draw();
    }
}
