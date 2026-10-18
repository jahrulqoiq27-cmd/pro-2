using System;
using System.Collections.Generic;

namespace SistemPerpustakaan
{
    // ===================== SUPERCLASS =====================
    public class Item
    {
        public string Judul { get; set; }
        public int Tahun { get; set; }

        public Item(string judul, int tahun)
        {
            Judul = judul;
            Tahun = tahun;
        }

        // virtual: boleh di-override oleh subclass
        public virtual string Deskripsi()
        {
            return $"Item: {Judul} ({Tahun})";
        }

        public virtual string InfoItem()
        {
            return $"Judul: {Judul} | Tahun: {Tahun}";
        }
    }

    // ===================== BUKU & TURUNANNYA =====================
    public class Buku : Item
    {
        public string Penulis { get; set; }

        public Buku(string judul, int tahun, string penulis) : base(judul, tahun)
        {
            Penulis = penulis;
        }

        public void CekPenulis()
        {
            Console.WriteLine($"Penulis dari '{Judul}' adalah {Penulis}.");
        }

        public override string Deskripsi()
        {
            return $"Buku: {Judul} oleh {Penulis} ({Tahun})";
        }

        public override string InfoItem()
        {
            return base.InfoItem() + $" | Penulis: {Penulis}";
        }
    }

    public class Novel : Buku
    {
        public string Sinopsis { get; set; }

        public Novel(string judul, int tahun, string penulis, string sinopsis)
            : base(judul, tahun, penulis)
        {
            Sinopsis = sinopsis;
        }

        public void BacaSinopsis()
        {
            Console.WriteLine($"Sinopsis '{Judul}': {Sinopsis}");
        }

        public override string Deskripsi()
        {
            return $"Novel: {Judul} oleh {Penulis} ({Tahun})";
        }
    }

    public class Komik : Buku
    {
        public Komik(string judul, int tahun, string penulis) : base(judul, tahun, penulis)
        {
        }

        public void TampilkanIlustrasi()
        {
            Console.WriteLine($"Menampilkan halaman bergambar dari komik '{Judul}'.");
        }

        public override string Deskripsi()
        {
            return $"Komik: {Judul} oleh {Penulis} ({Tahun})";
        }
    }

    // ===================== MAJALAH & TURUNANNYA =====================
    public class Majalah : Item
    {
        public int Edisi { get; set; }

        public Majalah(string judul, int tahun, int edisi) : base(judul, tahun)
        {
            Edisi = edisi;
        }

        public void InfoEdisi()
        {
            Console.WriteLine($"'{Judul}' adalah edisi ke-{Edisi}.");
        } 

        public override string Deskripsi()
        {
            return $"Majalah: {Judul} edisi {Edisi} ({Tahun})";
        }

        public override string InfoItem()
        {
            return base.InfoItem() + $" | Edisi: {Edisi}";
        }
    }

    public class MajalahAnak : Majalah
    {
        public MajalahAnak(string judul, int tahun, int edisi) : base(judul, tahun, edisi)
        {
        }

        public void KategoriAnak()
        {
            Console.WriteLine($"Kategori '{Judul}': majalah untuk anak-anak.");
        }

        public override string Deskripsi()
        {
            return $"Majalah Anak: {Judul} edisi {Edisi} ({Tahun})";
        }
    }

    public class MajalahTeknologi : Majalah
    {
        public MajalahTeknologi(string judul, int tahun, int edisi) : base(judul, tahun, edisi)
        {
        }

        public void TopikTeknologi()
        {
            Console.WriteLine($"Topik '{Judul}': teknologi, gadget, dan komputer.");
        }

        public override string Deskripsi()
        {
            return $"Majalah Teknologi: {Judul} edisi {Edisi} ({Tahun})";
        }
    }

    // ===================== PERPUSTAKAAN (AGREGASI) =====================
    public class Perpustakaan
    {
        private List<Item> daftarItem = new List<Item>();

        public void TambahItem(Item item)
        {
            if (item == null)
            {
                Console.WriteLine("Item tidak boleh kosong.");
                return;
            }
            daftarItem.Add(item);
            Console.WriteLine($"Item '{item.Judul}' berhasil ditambahkan ke perpustakaan.");
        }

        public void DaftarItem()
        {
            if (daftarItem.Count == 0)
            {
                Console.WriteLine("Perpustakaan masih kosong.");
                return;
            }

            Console.WriteLine("Daftar item di perpustakaan:");
            int no = 1;
            foreach (Item item in daftarItem)
            {
                // Pemanggilan Deskripsi() bersifat polymorphic
                Console.WriteLine($"{no}. {item.Deskripsi()}");
                no++;
            }
        }
    }

    // ===================== PROGRAM UTAMA =====================
    class Program
    {
        static void Main(string[] args)
        {
            // a. Buat objek perpustakaan
            Console.WriteLine("=== A. Membuat objek Perpustakaan ===");
            Perpustakaan perpustakaan = new Perpustakaan();
            Console.WriteLine("Objek Perpustakaan berhasil dibuat.");
            Console.WriteLine();

            // b. Buat beberapa objek item
            Novel novel = new Novel("Negeri 5 Menara", 2009, "Ahmad Fuadi",
                "Kisah para santri yang mengejar cita-cita dengan semangat dan kerja keras.");
            Komik komik = new Komik("Detektif Conan Vol. 1", 1994, "Gosho Aoyama");
            MajalahTeknologi chip = new MajalahTeknologi("Chip", 2024, 8);

            // c. Tambahkan ke perpustakaan
            Console.WriteLine("=== B. Menambahkan item ke Perpustakaan ===");
            perpustakaan.TambahItem(novel);
            perpustakaan.TambahItem(komik);
            perpustakaan.TambahItem(chip);
            Console.WriteLine();

            // d. Tampilkan semua data
            Console.WriteLine("=== C. Menampilkan semua data ===");
            perpustakaan.DaftarItem();
            Console.WriteLine();

            // e. Demonstrasi polymorphism
            Console.WriteLine("=== D. Demonstrasi Polymorphism ===");
            Item[] koleksi = { novel, komik, chip };
            foreach (Item item in koleksi)
            {
                // Variabel bertipe Item, tetapi method yang jalan sesuai tipe objek aslinya
                Console.WriteLine(item.Deskripsi());
            }
            Console.WriteLine();

            // f. Panggil method khusus
            Console.WriteLine("=== E. Memanggil method khusus ===");
            novel.CekPenulis();
            novel.BacaSinopsis();
            komik.TampilkanIlustrasi();
            chip.InfoEdisi();
            chip.TopikTeknologi();
            Console.WriteLine();

            // ============ KASUS PERTANYAAN (nomor 1 - 5) ============
            Console.WriteLine("=== KASUS 1: Deskripsi() pada Novel dan Majalah (MajalahTeknologi) ===");
            Console.WriteLine(novel.Deskripsi());
            Console.WriteLine(chip.Deskripsi()); // chip = MajalahTeknologi, yaitu turunan Majalah
            Console.WriteLine();

            Console.WriteLine("=== KASUS 2: BacaSinopsis() pada Novel ===");
            novel.BacaSinopsis();
            // Item itemNovel = novel;
            // itemNovel.BacaSinopsis();  // ERROR CS1061: 'Item' tidak memiliki BacaSinopsis()
            Console.WriteLine();

            Console.WriteLine("=== KASUS 3: Informasi lengkap Novel ===");
            Console.WriteLine(novel.InfoItem());
            Console.WriteLine($"Judul: {novel.Judul}, Tahun: {novel.Tahun}, Penulis: {novel.Penulis}");
            Console.WriteLine();

            Console.WriteLine("=== KASUS 4: TopikTeknologi() pada MajalahTeknologi ===");
            chip.TopikTeknologi();
            Console.WriteLine();

            Console.WriteLine("=== KASUS 5: Variabel Item berisi objek Komik ===");
            Item itemKomik = new Komik("One Piece Vol. 1", 1997, "Eiichiro Oda");
            Console.WriteLine(itemKomik.Deskripsi());
            // itemKomik.TampilkanIlustrasi();  // ERROR CS1061 (tipe variabel = Item)
            ((Komik)itemKomik).TampilkanIlustrasi(); // bisa setelah casting
            Console.WriteLine();

            Console.WriteLine("ketik selesai untuk keluar...");
            Console.ReadKey();
        }
    }
}
