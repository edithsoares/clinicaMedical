﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain;

namespace Presentation
{
    public partial class FormRecoverPass : Form
    {
        public FormRecoverPass()
        {
            InitializeComponent();
        }

        private void btnSender_Click(object sender, EventArgs e)
        {
            var user = new UserModel();
            var result = user.RecoverPassword(txtRecover.Text);
            lblResult.Text = result;
        }
    }
}
