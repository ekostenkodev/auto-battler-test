namespace Scorewarrior.Test.Views
{
    public class BulletProvider
    {
        public BulletModel Model { get; }
        public BulletView View { get; }

        public BulletProvider(BulletModel model, BulletView view)
        {
            Model = model;
            View = view;
        }
    }
}