using UnityEngine;

namespace Assets.Scripts.Food.Interfaces
{
    public interface IFragments
    {
        GameObject Fragment { get; set; }
        GameObject FragmentToFollow { get; set; }
        int NbOfFragments { get; set; }
    }
}
