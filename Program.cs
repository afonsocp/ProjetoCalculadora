using System;
using System.Windows.Forms;

namespace Calculadora
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormCalculadora());
        }
    }

    public class FormCalculadora : Form
    {
        private readonly TextBox txtDisplay;
        private double valorAnterior = 0;
        private string operacaoAtual = "";

        public FormCalculadora()
        {
            this.Text = "Calculadora Semi-Científica";
            this.Width = 300;
            this.Height = 400;

            txtDisplay = new TextBox
            {
                Width = 260,
                Height = 30,
                Top = 20,
                Left = 20,
                TextAlign = HorizontalAlignment.Right,
                ReadOnly = true
            };

            string[] botoes = { "7", "8", "9", "/", "4", "5", "6", "*", "1", "2", "3", "-", "0", ".", "=", "+" };
            int left = 20, top = 70;

            for (int i = 0; i < botoes.Length; i++)
            {
                var btn = new Button
                {
                    Text = botoes[i],
                    Width = 60,
                    Height = 40,
                    Left = left + (i % 4) * 65,
                    Top = top + (i / 4) * 45
                };
                btn.Click += Btn_Click;
                this.Controls.Add(btn);
            }

            var btnRaiz = new Button { Text = "√", Width = 60, Height = 40, Left = 20, Top = 250 };
            var btnCubo = new Button { Text = "x³", Width = 60, Height = 40, Left = 85, Top = 250 };
            var btnLimpar = new Button { Text = "C", Width = 60, Height = 40, Left = 150, Top = 250 };
            var btnSobre = new Button { Text = "Sobre", Width = 60, Height = 40, Left = 215, Top = 250 };

            btnRaiz.Click += BtnEspecial_Click;
            btnCubo.Click += BtnEspecial_Click;
            btnLimpar.Click += BtnLimpar_Click;
            btnSobre.Click += BtnSobre_Click;

            this.Controls.AddRange(new Control[] { txtDisplay, btnRaiz, btnCubo, btnLimpar, btnSobre });
        }

        private void Btn_Click(object? sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn?.Text == null) return;

            if ("0123456789.".Contains(btn.Text))
            {
                txtDisplay.Text += btn.Text;
            }
            else if ("+-×÷−".Contains(btn.Text))
            {
                valorAnterior = double.Parse(txtDisplay.Text);
                operacaoAtual = btn.Text;
                txtDisplay.Clear();
            }
            else if (btn.Text == "=")
            {
                double valorAtual = double.Parse(txtDisplay.Text);
                double resultado = 0;

                switch (operacaoAtual)
                {
                    case "+": resultado = valorAnterior + valorAtual; break;
                    case "-": resultado = valorAnterior - valorAtual; break;
                    case "*": resultado = valorAnterior * valorAtual; break;
                    case "/": resultado = valorAnterior / valorAtual; break;
                }

                txtDisplay.Text = resultado.ToString();
            }
        }

        private void BtnEspecial_Click(object? sender, EventArgs e)
        {
            var btn = sender as Button ?? throw new InvalidOperationException("Sender must be a Button");
            double valor = double.Parse(txtDisplay.Text);

            if (btn.Text == "√")
                txtDisplay.Text = Math.Sqrt(valor).ToString();
            else if (btn.Text == "x³")
                txtDisplay.Text = Math.Pow(valor, 3).ToString();
        }

        private void BtnLimpar_Click(object? sender, EventArgs e)
        {
            txtDisplay.Clear();
            valorAnterior = 0;
            operacaoAtual = "";
        }

        private void BtnSobre_Click(object? sender, EventArgs e)
        {
            string mensagem = "Integrantes do Grupo:\n\n";
            mensagem += "Nome: Afonso Correia Pereira\nRM: 557863\n\n";
            mensagem += "Nome: Adel Mouhaidly\nRM: 557705\n\n";
            mensagem += "Nome: Felipe Horta Gresele\nRM: 556955\n\n";
            mensagem += "Nome: Tiago Augusto Desiderato\nRM: 558485\n\n";
            mensagem += "Nome: Arthur\nRM: 550615\n\n";
            mensagem += "Nome: João Henrique\nRM: 556221\n\n";

            MessageBox.Show(mensagem, "Sobre", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}