using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Models.Assets
{
    public interface IAsset
    {
        string Key { get; }
        Currency Currency { get; }
        double GetValue();
        IAsset Consolidate(IAsset asset);
    }
}
