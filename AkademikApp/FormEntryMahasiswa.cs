using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AkademikApp
{
    public partial class FormEntryMahasiswa : Form
    {
        public delegate void SaveUpdateEventHandler(Mahasiswa obj);

        public event SaveUpdateEventHandler OnSave;

        public event SaveUpdateEventHandler OnUpdate;

        private bool isNewData = true;
        private Mahasiswa mhs = null;

        public FormEntryMahasiswa()
        {
            InitializeComponent();
        }

        public FormEntryMahasiswa(string header)
		: this()
	    {
            this.Text = header;
        }

        public FormEntryMahasiswa(string header, Mahasiswa obj)
		: this()
	    {
            this.Text = header;
            this.isNewData = false;
            this.mhs = obj;

            // untuk proses edit, tampilkan data lama
            txtNpm.Text = this.mhs.Npm;
            txtNama.Text = this.mhs.Nama;

            if (this.mhs.JenisKelamin == "Laki-laki")
                rdoLakilaki.Checked = true;
            else
                rdoPerempuan.Checked = true;

            txtAlamat.Text = this.mhs.Alamat;
        }




        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormEntryMahasiswa_Load(object sender, EventArgs e)
        {

        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (isNewData)
                mhs = new Mahasiswa();

            mhs.Npm = txtNpm.Text;
            mhs.Nama = txtNama.Text;
            mhs.JenisKelamin = rdoLakilaki.Checked ? "Laki-laki" : "Perempuan";
            mhs.Alamat = txtAlamat.Text;

            if (isNewData) // data baru
            {
                OnSave(mhs); // panggil event OnSave

                // reset form input
                txtNpm.Clear();
                txtNama.Clear();
                rdoLakilaki.Checked = true;
                txtAlamat.Clear();

                txtNpm.Focus();
            }
            else
            {
                OnUpdate(mhs); // panggil event OnUpdate
                this.Close();
            }
        }
    }
}
