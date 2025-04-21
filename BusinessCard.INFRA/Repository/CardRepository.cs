using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCard.CORE.Common;
using BusinessCard.CORE.Data;
using BusinessCard.CORE.Repository;
using Dapper;

namespace BusinessCard.INFRA.Repository
{
    public class CardRepository:ICardRepository
    {
        private readonly IDbContext _dbContext;
        public CardRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Businesscard> GetAllCards() 
        {
            IEnumerable<Businesscard> result = _dbContext.Connection.Query<Businesscard>("BusinessCard_Package.getAllCards", commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public void CreateCard(Businesscard businesscard)
        {
            var p = new DynamicParameters();
            p.Add("Cname",businesscard.Name,dbType:DbType.String,direction:ParameterDirection.Input);
            p.Add("Cemail",businesscard.Email,dbType:DbType.String,direction: ParameterDirection.Input);
            p.Add("Cgender", businesscard.Gender, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Cbirthdate", businesscard.Dateofbirth, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            p.Add("Cphone",businesscard.Phone,dbType:DbType.String,direction: ParameterDirection.Input);
            p.Add("Cimagepath", businesscard.Imagepath, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Caddress",businesscard.Address,dbType:DbType.String, direction: ParameterDirection.Input);
            var result=_dbContext.Connection.Execute("BusinessCard_Package.CreateCard",p, commandType: CommandType.StoredProcedure);

        }

        public void DeleteCard(int id) 
        {
            var p=new DynamicParameters();
            p.Add("Cid", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = _dbContext.Connection.Execute("BusinessCard_Package.DeleteCard", p, commandType: CommandType.StoredProcedure);
        }

        public List<Businesscard> getCardByName(string name) 
        {
            var p=new DynamicParameters();
            p.Add("Cname", name, dbType: DbType.String, direction: ParameterDirection.Input);
            IEnumerable<Businesscard> result = _dbContext.Connection.Query<Businesscard>("BusinessCard_Package.getCardByName", p, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
        public List<Businesscard> getcardByDOB(DateTime date)
        {
            var p = new DynamicParameters();
            p.Add("CDOB", date, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            IEnumerable<Businesscard> result = _dbContext.Connection.Query<Businesscard>("BusinessCard_Package.getcardByDOB", p, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
        public List<Businesscard> getcardByPhone(string phone)
        {
            var p = new DynamicParameters();
            p.Add("Cphone",phone , dbType: DbType.String, direction: ParameterDirection.Input);
            IEnumerable<Businesscard> result = _dbContext.Connection.Query<Businesscard>("BusinessCard_Package.getcardByPhone", p, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
        public List<Businesscard> getcardByGender(string gender)
        {
            var p = new DynamicParameters();
            p.Add("Cgender", gender, dbType: DbType.String, direction: ParameterDirection.Input);
            IEnumerable<Businesscard> result = _dbContext.Connection.Query<Businesscard>("BusinessCard_Package.getcardByGender", p, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
        public List<Businesscard> getcardByEmail(string email)
        {
            var p = new DynamicParameters();
            p.Add("Cemail", email, dbType: DbType.String, direction: ParameterDirection.Input);
            IEnumerable<Businesscard> result = _dbContext.Connection.Query<Businesscard>("BusinessCard_Package.getcardByEmail", p, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
    }
}
