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
    public partial class Form1 : Form
    {
        private IList<Mahasiswa> listOfMahasiswa = new List<Mahasiswa>();
        private FormEntryMahasiswa frmEntry;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InisialisasiListView();
            InisialisasiDataDummy();
        }

        private void InisialisasiDataDummy()
        {
            listOfMahasiswa.Add(new Mahasiswa { Npm = "17.11.0919", Nama = "HERDIANTO", JenisKelamin = "Laki-laki" });
            listOfMahasiswa.Add(new Mahasiswa { Npm = "17.11.0920", Nama = "SYELVI NUR DWI JULIANA", JenisKelamin = "Perempuan" });
            listOfMahasiswa.Add(new Mahasiswa { Npm = "17.11.0921", Nama = "MUHAMMAD KHAIRUL RIJAL", JenisKelamin = "Laki-laki" });
            listOfMahasiswa.Add(new Mahasiswa { Npm = "17.11.0922", Nama = "YOHANES EUDES ANJAS SUSETYA", JenisKelamin = "Laki-laki" });
            listOfMahasiswa.Add(new Mahasiswa { Npm = "17.11.0923", Nama = "ALFADLY SOPANDI", JenisKelamin = "Laki-laki" });
            listOfMahasiswa.Add(new Mahasiswa { Npm = "17.11.0924", Nama = "BAGUS PRIYA UTAMA", JenisKelamin = "Laki-laki" });
            listOfMahasiswa.Add(new Mahasiswa { Npm = "17.11.0925", Nama = "MUHAMAD RIZAL NURIANANG", JenisKelamin = "Laki-laki" });
            listOfMahasiswa.Add(new Mahasiswa { Npm = "17.11.0926", Nama = "IFA DWI PUSPITASARI", JenisKelamin = "Perempuan" });
            listOfMahasiswa.Add(new Mahasiswa { Npm = "17.11.0927", Nama = "MUHAMMAD ZAID HANIF ABDILLAH", JenisKelamin = "Laki-laki" });
            listOfMahasiswa.Add(new Mahasiswa { Npm = "17.11.0928", Nama = "DENDY WAHYU PRASETYO", JenisKelamin = "Laki-laki" });

            foreach (var obj in listOfMahasiswa)
            {
                FillToListView(true, obj);
            }
        }

        private void FillToListView(bool isNewData, Mahasiswa mhs)
        {
            if (isNewData) // data baru
            {
                int noUrut = lvwMahasiswa.Items.Count + 1;

                ListViewItem item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(mhs.Npm);
                item.SubItems.Add(mhs.Nama);
                item.SubItems.Add(mhs.JenisKelamin);
                item.SubItems.Add(mhs.Alamat);

                lvwMahasiswa.Items.Add(item);
            }
            else // edit data
            {
                int row = lvwMahasiswa.SelectedIndices[0];

                ListViewItem itemRow = lvwMahasiswa.Items[row];
                itemRow.SubItems[1].Text = mhs.Npm;
                itemRow.SubItems[2].Text = mhs.Nama;
                itemRow.SubItems[3].Text = mhs.JenisKelamin;
                itemRow.SubItems[4].Text = mhs.Alamat;
            }
        }

        private void InisialisasiListView()
        {
            lvwMahasiswa.View = System.Windows.Forms.View.Details;
            lvwMahasiswa.FullRowSelect = true;
            lvwMahasiswa.GridLines = true;

            lvwMahasiswa.Columns.Add("No.", 30, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Npm", 91, HorizontalAlignment.Left);
            lvwMahasiswa.Columns.Add("Nama", 250, HorizontalAlignment.Left);
            lvwMahasiswa.Columns.Add("Jenis Kelamin", 90, HorizontalAlignment.Left);
            lvwMahasiswa.Columns.Add("Alamat", 200, HorizontalAlignment.Left);
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            frmEntry = new FormEntryMahasiswa("Tambah Data Mahasiswa");
            frmEntry.OnSave += FormEntryMahasiswa_OnSave; // mendaftarkan event handler untuk event OnSave
            frmEntry.ShowDialog();
        }

        private void FormEntryMahasiswa_OnSave(Mahasiswa obj)
        {
            listOfMahasiswa.Add(obj);
            FillToListView(true, obj);
        }

        private void btnPerbaiki_Click(object sender, EventArgs e)
        {
            if (lvwMahasiswa.SelectedItems.Count > 0)
            {
                var mhs = listOfMahasiswa[lvwMahasiswa.SelectedIndices[0]];

                frmEntry = new FormEntryMahasiswa("Edit Data Mahasiswa", mhs);
                frmEntry.OnUpdate += FormEntryMahasiswa_OnUpdate; // mendaftarkan event handler untuk event OnUpdate
                frmEntry.ShowDialog();
            }
            else // data belum dipilih
            {
                MessageBox.Show("Data belum dipilih", "Peringatan", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
            }
        }

        private void FormEntryMahasiswa_OnUpdate(Mahasiswa obj)
        {
            FillToListView(false, obj);
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (lvwMahasiswa.SelectedItems.Count > 0)
            {
                var mhs = listOfMahasiswa[lvwMahasiswa.SelectedIndices[0]];

                var msg = string.Format("Apakah data mahasiswa '{0}' ingin dihapus ?", mhs.Nama);

                if (MessageBox.Show(msg, "Konfirmasi", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    listOfMahasiswa.Remove(mhs); // hapus objek mahasiswa dari collection

                    lvwMahasiswa.Items.Clear();
                    foreach (var obj in listOfMahasiswa)
                    {
                        FillToListView(true, obj);
                    }
                }
            }
            else // data belum dipilih
            {
                MessageBox.Show("Data belum dipilih", "Peringatan", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
            }

        }

        private void btnSelesai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
