using Data.API.Models.DBModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Data.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Exchange : ControllerBase
    {
        private provisioncaseContext _provisioncaseContext;

        public Exchange(provisioncaseContext provisioncaseContext)
        {
            _provisioncaseContext = provisioncaseContext;
        }

        [HttpGet]
        public IEnumerable<Kurlartbl> get()
        {
            var list = _provisioncaseContext.Kurlartbl.ToList();
            return list;
        }

        [HttpPost]
        public string PostDoviz()
        {
            XDocument tcmbdoviz = XDocument.Load("https://www.tcmb.gov.tr/kurlar/today.xml");

            var kurBilgileri = from kurlar in tcmbdoviz.Descendants("Currency")
                               select new
                               {
                                   kurUnit = kurlar.Element("Unit").Value,
                                   kuradiTR = kurlar.Element("Isim").Value,
                                   CurrencyName = kurlar.Element("CurrencyName").Value,
                                   ForexBuying = kurlar.Element("ForexBuying").Value,
                                   ForexSelling = kurlar.Element("ForexSelling").Value,
                                   BanknoteBuying = kurlar.Element("BanknoteBuying").Value,
                                   BanknoteSelling = kurlar.Element("BanknoteSelling").Value,
                                   CrossRateUSD = kurlar.Element("CrossRateUSD").Value,

                               };
            foreach (var item in kurBilgileri)
            {
                Kurlartbl kurlar = new Kurlartbl();
                kurlar.Unit = Convert.ToInt32(item.kurUnit);
                kurlar.Name = item.kuradiTR;
                kurlar.CurrencyName = item.CurrencyName;

                if (item.ForexBuying != "")
                {
                    kurlar.ForexBuying = Convert.ToDecimal(item.ForexBuying);
                }
                if (item.ForexSelling != "")
                {
                    kurlar.ForexSelling = Convert.ToDecimal(item.ForexSelling);
                }
                if (item.BanknoteSelling != "")
                {
                    kurlar.BanknoteSelling = Convert.ToDecimal(item.BanknoteSelling);
                }
                if (item.BanknoteBuying != "")
                {
                    kurlar.BanknoteBuying = Convert.ToDecimal(item.BanknoteBuying);
                }
                if (item.CrossRateUSD != "")
                {
                    kurlar.CrossRateUsd = Convert.ToDecimal(item.CrossRateUSD);
                }

                _provisioncaseContext.Kurlartbl.Add(kurlar);
                _provisioncaseContext.SaveChanges();
            }

            return "İşlem Başarılı";

        }

        /// <summary>
        /// Gelen Name Parametresine göre veritabanından bilgileri geitirir
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("GetToExchange/{name?}")]
        public async Task<ActionResult<Kurlartbl>> GetToExchange(string name)
        {
            var exchangeItem = _provisioncaseContext.Kurlartbl.Where(x => x.Name == name).FirstOrDefault();

            if (exchangeItem == null)
            {
                return NotFound();
            }
            return exchangeItem;
        }
    }
}
