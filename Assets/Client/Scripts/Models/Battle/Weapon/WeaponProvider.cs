using Scorewarrior.Test.Views;

namespace Scorewarrior.Test.Models
{
    public class WeaponProvider
    {
        public WeaponModel Model { get; }
        public WeaponView View { get; }

        public WeaponProvider(WeaponModel model, WeaponView view)
        {
            Model = model;
            View = view;
        }
    }
}