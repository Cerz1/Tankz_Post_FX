﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankz_2020
{
    class JoypadController : Controller
    {
        public JoypadController(int controllerIndex) : base(controllerIndex)
        {
            
        }

        public override float GetHorizontal()
        {
            float direction;

            if (Game.Window.JoystickRight(index))
            {
                direction = 1;
            }
            else if (Game.Window.JoystickLeft(index))
            {
                direction = -1;
            }
            else
            {
                direction = Game.Window.JoystickAxisLeft(index).X;
            }

            return direction;
        }

        public override float GetVertical()
        {
            float direction;

            if (Game.Window.JoystickUp(index))
            {
                direction = -1;
            }
            else if (Game.Window.JoystickDown(index))
            {
                direction = 1;
            }
            else
            {
                direction = Game.Window.JoystickAxisLeft(index).Y;
            }

            return direction;
        }

        public override bool IsFirePressed()
        {
            return Game.Window.JoystickX(index);
        }

        public override bool IsJumpPressed()
        {
            return Game.Window.JoystickA(index);
        }

        public override bool NextWeapon()
        {
            return Game.Window.JoystickShoulderRight(index);
        }

        public override bool PrevWeapon()
        {
            return Game.Window.JoystickShoulderLeft(index);
        }
    }
}
