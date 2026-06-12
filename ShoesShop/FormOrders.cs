using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ShoesShop
{
    public partial class FormOrders : Form
    {
        private FormProducts formProducts;

        private bool isBackButtonClicked = false;

        public FormOrders()
        {
            InitializeComponent();
        }

        public FormOrders(FormProducts formProducts)
        {
            InitializeComponent();
            this.formProducts = formProducts;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (!isBackButtonClicked)
            {
                Application.Exit();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (this.formProducts != null)
            {
                isBackButtonClicked = true;

                this.formProducts.Show();
                this.Close();
            }
        }
    }
}
