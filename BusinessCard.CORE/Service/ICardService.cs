using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCard.CORE.Data;

namespace BusinessCard.CORE.Service
{
    public interface ICardService
    {
        List<Businesscard> GetAllCards();
        void CreateCard(Businesscard businesscard);
        void DeleteCard(int id);
        List<Businesscard> getCardByName(string name);
        List<Businesscard> getcardByDOB(DateTime date);
        List<Businesscard> getcardByPhone(string phone);
        List<Businesscard> getcardByGender(string gender);
        List<Businesscard> getcardByEmail(string email);
    }
}
