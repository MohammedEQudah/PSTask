using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCard.CORE.Data;
using BusinessCard.CORE.Repository;
using BusinessCard.CORE.Service;
using Dapper;

namespace BusinessCard.INFRA.Service
{
    public class CardService: ICardService
    {
        private readonly ICardRepository _cardRepository;
        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public List<Businesscard> GetAllCards()
        {
           return _cardRepository.GetAllCards();
        }
        public void CreateCard(Businesscard businesscard)
        {
            _cardRepository.CreateCard(businesscard);

        }
        public void DeleteCard(int id)
        {
            _cardRepository.DeleteCard(id);
        }
        public List<Businesscard> getCardByName(string name)
        {
           return _cardRepository.getCardByName(name);
        }
        public List<Businesscard> getcardByDOB(DateTime date)
        {
            return _cardRepository.getcardByDOB(date);
        }
        public List<Businesscard> getcardByPhone(string phone)
        {
           return _cardRepository.getcardByPhone(phone);
        }
        public List<Businesscard> getcardByGender(string gender)
        {
            return _cardRepository.getcardByGender(gender);
        }
        public List<Businesscard> getcardByEmail(string email)
        {
           return _cardRepository.getcardByEmail(email);
        }
    }
}
