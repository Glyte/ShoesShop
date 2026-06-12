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
            Application.Exit();
        }
    }
}
