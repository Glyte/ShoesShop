namespace ShoesShop
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            FormLogin formLogin = new FormLogin();

            if (formLogin.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new FormProducts(formLogin.CurrentUser, formLogin.IsGuest));
            }

        }
    }
}
