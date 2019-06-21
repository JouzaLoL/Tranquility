using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquility.Objects;

namespace Tranquility.Modules
{
    public interface IModule
    {
        void HandleCommand(IEnumerable<string> args);
        Fov GetFov();
    }
}
