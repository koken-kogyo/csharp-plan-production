using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlanProduction
{
    public partial class FormPlanEntry : Form
    {
        // フォーム起動時の値
        private string 手配先コード;
        private string デフォルト担当者;
        private string デフォルト可動率;
        private int 可動率;

        public FormPlanEntry(string 手配先コード, string 担当者, string 可動率)
        {
            InitializeComponent();

            this.手配先コード = 手配先コード;
            this.デフォルト担当者 = 担当者;
            this.デフォルト可動率 = 可動率;
            this.textBox可動率.Text = 可動率;
            if (Int32.TryParse(可動率, out this.可動率)) this.可動率 = 75;
        }
    }
}
