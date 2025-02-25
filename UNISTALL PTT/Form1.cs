using System;
using System.Drawing;
using System.Windows.Forms;
using SharpAdbClient;
using System.Linq;
using System.Timers;
using System.Diagnostics;

namespace UNISTALL_PTT
{
    public partial class Desinstalar : Form
    {
        private AdbClient client;
        private System.Timers.Timer timer;

        public Desinstalar()
        {
            InitializeComponent();
            this.Shown += Desinstalar_Shown;
        }

        private void Desinstalar_Shown(object sender, EventArgs e)
        {
            // Iniciar la verificación inmediata y luego cada 3 segundos
            VerificarDispositivos();
            IniciarTimer();
        }

        private void IniciarTimer()
        {
            timer = new System.Timers.Timer(3000); // 3 segundos
            timer.Elapsed += (sender, e) => VerificarDispositivos();
            timer.AutoReset = true; // Se repite automáticamente
            timer.Enabled = true;
        }

        private void VerificarDispositivos()
        {
            try
            {
                client = new AdbClient();
                var devices = client.GetDevices();

                this.Invoke((MethodInvoker)delegate
                {
                    if (devices.Any()) // Si hay dispositivos conectados
                    {
                        lblStatus.Text = "Dispositivo conectado";
                        lblStatus.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblStatus.Text = "No hay dispositivos";
                        lblStatus.ForeColor = Color.Red;
                    }
                });
            }
            catch (Exception ex)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    lblStatus.Text = "Error: " + ex.Message;
                    lblStatus.ForeColor = Color.Red;
                });
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";  // Ejecutar en CMD directamente
                process.StartInfo.Arguments = "/c adb shell pm uninstall -k --user 0 com.corget"; // /c ejecuta el comando y cierra CMD
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                // Mostrar el resultado en un MessageBox
                if (!string.IsNullOrWhiteSpace(output))
                {
                    MessageBox.Show("Resultado:\n" + output, "ADB Output");
                }
                if (!string.IsNullOrWhiteSpace(error))
                {
                    MessageBox.Show("Error:\n" + error, "ADB Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ejecutando ADB: " + ex.Message, "Error");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";  // Ejecutar en CMD directamente
                process.StartInfo.Arguments = "/c adb shell pm uninstall -k --user 0 com.veclink.vecsipsimple"; // /c ejecuta el comando y cierra CMD
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                // Mostrar el resultado en un MessageBox
                if (!string.IsNullOrWhiteSpace(output))
                {
                    MessageBox.Show("Resultado:\n" + output, "ADB Output");
                }
                if (!string.IsNullOrWhiteSpace(error))
                {
                    MessageBox.Show("Error:\n" + error, "ADB Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ejecutando ADB: " + ex.Message, "Error");
            }
        }
    }
}
