using System.Text;
using System.Xml;
using System.Xml.Serialization;
using BusinessCard.CORE.Data;
using BusinessCard.CORE.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusinessCard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;
        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }
        [HttpGet]
        [Route("GetAllCards")]
        public List<Businesscard> GetAllCards()
        {
            return _cardService.GetAllCards();
        }

        [HttpPost]
        [Route("UploadImage")]
        public Businesscard UploadImage() 
        {
            var file = Request.Form.Files[0];
            var fileName = Guid.NewGuid().ToString() +"_" + file.FileName;
            var fullPath = Path.Combine("D:\\BusinessCard\\src\\assets\\CardImages", fileName);
            using (var stream = new FileStream(fullPath,FileMode.Create))
            {
                file.CopyTo(stream);
            }
            Businesscard item = new Businesscard();
            item.Imagepath = fileName;
            return item;
        }

        [HttpPost]
        [Route("CreateCard")]
        public void CreateCard(Businesscard businesscard)
        {
            _cardService.CreateCard(businesscard);

        }

        [HttpPost]
        [Route("ImportCsv")]
        public async Task<IActionResult> ImportCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("CSV file is required.");

            using var stream = new StreamReader(file.OpenReadStream());
            string headerLine = await stream.ReadLineAsync(); // Skip header

            while (!stream.EndOfStream)
            {
                var line = await stream.ReadLineAsync();
                var values = line.Split(',');

                var businesscard = new Businesscard
                {
                    Name = values[0],
                    Email = values[1],
                    Gender = values[2],
                    Dateofbirth = DateTime.Parse(values[3]),
                    Phone = values[4],
                    Imagepath = string.IsNullOrWhiteSpace(values[5]) ? null : values[5],
                    Address = values[6]
                };

                _cardService.CreateCard(businesscard);
            }

            return Ok("CSV imported successfully.");
        }
        [HttpPost]
        [Route("ImportXml")]
        public async Task<IActionResult> ImportXml(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("XML file is required.");

            using var stream = file.OpenReadStream();
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(stream);

            var cards = xmlDoc.SelectNodes("//BusinessCard");

            foreach (XmlNode card in cards)
            {
                var businesscard = new Businesscard
                {
                    Name = card["Name"]?.InnerText,
                    Email = card["Email"]?.InnerText,
                    Gender = card["Gender"]?.InnerText,
                    Dateofbirth = DateTime.Parse(card["DateOfBirth"]?.InnerText ?? DateTime.MinValue.ToString()),
                    Phone = card["Phone"]?.InnerText,
                    Imagepath = card["ImagePath"]?.InnerText,
                    Address = card["Address"]?.InnerText
                };

                _cardService.CreateCard(businesscard);
            }

            return Ok("XML imported successfully.");
        }
        [HttpDelete]
        [Route("DeleteCard/{id}")]
        public void DeleteCard(int id)
        {
            _cardService.DeleteCard(id);
        }
        [HttpGet]
        [Route("ExportCsv")]
        public IActionResult ExportCsv()
        {
            var cards = _cardService.GetAllCards();

            var sb = new StringBuilder();
            sb.AppendLine("Id,Name,Email,Gender,DateOfBirth,Phone,ImagePath,Address");

            foreach (var card in cards)
            {
                sb.AppendLine($"{card.Id},{card.Name},{card.Email},{card.Gender},{card.Dateofbirth:yyyy-MM-dd},{card.Phone},{card.Imagepath},{card.Address}");
            }

            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/csv", "business_cards.csv");
        }

        [HttpGet]
        [Route("ExportXml")]
        public IActionResult ExportXml()
        {
            var cards = _cardService.GetAllCards();

            var xmlSerializer = new XmlSerializer(typeof(List<Businesscard>));
            using var ms = new MemoryStream();
            var ns = new XmlSerializerNamespaces();
            ns.Add("", ""); 

            xmlSerializer.Serialize(ms, cards, ns);
            return File(ms.ToArray(), "application/xml", "business_cards.xml");
        }
        [HttpGet]
        [Route("GetCardByName/{name}")]
        public List<Businesscard> getCardByName(string name)
        {
            return _cardService.getCardByName(name);
        }
        [HttpGet]
        [Route("GetCardByDate/{date}")]
        public List<Businesscard> getcardByDOB(DateTime date)
        {
            return _cardService.getcardByDOB(date);
        }

        [HttpGet]
        [Route("GetCardByPhone/{phone}")]
        public List<Businesscard> getcardByPhone(string phone)
        {
            return _cardService.getcardByPhone(phone);
        }
        [HttpGet]
        [Route("GetCardByGender/{gender}")]
        public List<Businesscard> getcardByGender(string gender)
        {
            return _cardService.getcardByGender(gender);
        }
        [HttpGet]
        [Route("GetCardByEmail/{Email}")]
        public List<Businesscard> getcardByEmail(string email)
        {
            return _cardService.getcardByEmail(email);
        }

    }
}
