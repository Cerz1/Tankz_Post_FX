using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankz_2020
{
    class ShootState : State
    {
        public override void Update()
        {
            if (stateMachine.Owner.LastShotBullet == null || !stateMachine.Owner.LastShotBullet.IsActive)
            {
                stateMachine.GoTo(StateEnum.WAIT);
            }
        }
    }
}
