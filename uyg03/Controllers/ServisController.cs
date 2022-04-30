using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using uyg03.Models;
using uyg03.ViewModel;

namespace uyg03.Controllers
{
    public class ServisController : ApiController
    {

        DB02Entities4 db = new DB02Entities4();
        SonucModel sonuc = new SonucModel();


        #region Kategori
        [HttpGet]
        [Route("api/kategoriliste")]
        public List<KategoriModel> KategoriListe()
        {
            List<KategoriModel> liste = db.Kategori.Select(x => new KategoriModel()
            {
                katId = x.katId,
                katAdi = x.katAdi,
                katUrunSay = x.Uruns.Count()

            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/kategoribyid/{katId}")]
        public KategoriModel KategoriById(int katId)
        {
            KategoriModel kayit = db.Kategori.Where(s => s.katId == katId).Select(x => new KategoriModel()
            {
                katId = x.katId,
                katAdi = x.katAdi,
                katUrunSay = x.Uruns.Count()
            }).SingleOrDefault();

            return kayit;
        }

        [HttpPost]
        [Route("api/kategoriekle")]
        public SonucModel KategoriEkle(KategoriModel model)
        {
            if (db.Kategori.Count(c => c.katAdi == model.katAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kategori Kayıtlıdır!";
                return sonuc;
            }
            Kategori yeni = new Kategori();
            yeni.katAdi = model.katAdi;
            db.Kategori.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Eklendi...";

            return sonuc;
        }

        [HttpPut]
        [Route("api/kategoriduzenle")]
        public SonucModel KategoriDuzenle(KategoriModel model)
        {
            Kategori kayit = db.Kategori.Where(s => s.katId == model.katId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            kayit.katAdi = model.katAdi;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Düzenlendi...";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/kategorisil/{katId}")]

        public SonucModel KategoriSil(int katId)
        {
            Kategori kayit = db.Kategori.Where(s => s.katId == katId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            if (db.Urun.Count(c => c.urunKatId == katId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Kayıtlı Ürün Olan Kategori Silinemez!";
                return sonuc;

            }

            db.Kategori.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Silindi...";
            return sonuc;
        }
        #endregion

        #region Urun

        [HttpGet]
        [Route("api/urunliste")]
        public List<UrunModel> UrunListe()
        {
            List<UrunModel> liste = db.Urun.Select(x => new UrunModel()
            {
                urunId = x.urunId,
                urunAdi = x.urunAdi,
                urunFiyat = x.urunFiyat,
                urunKatId = x.urunKatId,
                urunKatAdi = x.Kategori.katAdi

            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/urunlistebykatid/{katId}")]
        public List<UrunModel> UrunListeByKatId(int katId)
        {
            List<UrunModel> liste = db.Urun.Where(s => s.urunKatId == katId).Select(x => new UrunModel()
            {
                urunId = x.urunId,
                urunAdi = x.urunAdi,
                urunFiyat = x.urunFiyat,
                urunKatId = x.urunKatId,
                urunKatAdi = x.Kategori.katAdi

            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/urunbyid/{urunId}")]
        public UrunModel UrunById(int urunId)
        {
            UrunModel kayit = db.Urun.Where(s => s.urunId == urunId).Select(x => new UrunModel()
            {
                urunId = x.urunId,
                urunAdi = x.urunAdi,
                urunFiyat = x.urunFiyat,
                urunKatId = x.urunKatId,
                urunKatAdi = x.Kategori.katAdi
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/urunekle")]
        public SonucModel UrunEkle(UrunModel model)
        {
            if (db.Urun.Count(c => c.urunAdi == model.urunAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Ürün Kayıtlıdır!";
                return sonuc;
            }

            Urun yeni = new Urun();
            yeni.urunAdi = model.urunAdi;
            yeni.urunKatId = model.urunKatId;
            yeni.urunFiyat = model.urunFiyat;
            db.Urun.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ürün Eklendi...";
            return sonuc;
        }

        [HttpPut]
        [Route("api/urunduzenle")]
        public SonucModel UrunDuzenle(UrunModel model)
        {
            Urun kayit = db.Urun.Where(s => s.urunId == model.urunId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
            }
            kayit.urunAdi = model.urunAdi;
            kayit.urunKatId = model.urunKatId;
            kayit.urunFiyat = model.urunFiyat;

            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ürün Düzenlendi...";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/urunsil/{urunId}")]
        public SonucModel UrunSil(int urunId)
        {
            Urun kayit = db.Urun.Where(s => s.urunId == urunId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            db.Urun.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ürün Silindi...";
            return sonuc;
        }
        #endregion

        [HttpPost]
        [Route("api/sepet")]
        public SonucModel SepetEkle(SepetUrunModel model)
        {
            SepetUrun yeni = new SepetUrun();
            yeni.cartitemName = model.cartitemName;
            yeni.cartitemId = model.cartitemId;
            db.SepetUrun.Add(yeni);

            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Sepete Eklendi...";
            return sonuc;
        }

    }
}
