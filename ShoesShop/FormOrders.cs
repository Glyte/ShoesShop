using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Models;

namespace ShoesShop
{
    public partial class FormOrders : Form
    {
        private FormProducts formProducts;
        private bool isBackButtonClicked = false;

        public FormOrders()
        {
            InitializeComponent();
            InitDataGridView();
            LoadOrders();
        }

        public FormOrders(FormProducts formProducts)
        {
            InitializeComponent();
            this.formProducts = formProducts;
            InitDataGridView();
            LoadOrders();
        }

        private void InitDataGridView()
        {
            dgvOrders.Columns.Clear();
            dgvOrders.AutoGenerateColumns = false;

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", HeaderText = "Номер заказа", FillWeight = 10 });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "colOrderDate", HeaderText = "Дата заказа", FillWeight = 20 });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDeliveryDate", HeaderText = "Дата доставки", FillWeight = 20 });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCode", HeaderText = "Код получения", FillWeight = 15 });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStatus", HeaderText = "Статус (ID)", FillWeight = 20 });

            dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadOrders()
        {
            try
            {
                if (this.formProducts == null || this.formProducts.IsGuest || this.formProducts.CurrentUser == null)
                {
                    return;
                }

                using (var db = new ShopDbContext())
                {
                    int currentUserId = this.formProducts.CurrentUser.Id;

                    var orders = db.Orders
                        .Where(o => o.IdUser == currentUserId)
                        .ToList();

                    dgvOrders.SuspendLayout();
                    dgvOrders.Rows.Clear();

                    foreach (var order in orders)
                    {
                        int rowIndex = dgvOrders.Rows.Add();
                        var row = dgvOrders.Rows[rowIndex];

                        row.Cells["colId"].Value = order.Id;
                        row.Cells["colOrderDate"].Value = order.OrderDate.ToString("dd.MM.yyyy");
                        row.Cells["colDeliveryDate"].Value = order.DeliveryDate.ToString("dd.MM.yyyy");
                        row.Cells["colCode"].Value = order.Code;
                        row.Cells["colStatus"].Value = order.IdStatuses;
                    }

                    dgvOrders.ResumeLayout();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки заказов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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