using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_O.XOBLL.Enums;

namespace X_O.XOBLL.Interfaces
{
    public interface ISaveAndLoadFile
    {
        void Save(GameResult result, GameMode mode);
        int[] Load(GameMode mode);
    }
}
