
using ApiLives.Core.Domain.Entities.ApiLives;
using ApiLives.Services.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System;
namespace ApiLives.Data.Repository
{
    public class LiveRepository : ILiveRepository
    {
        private readonly IConfiguration _config;

        public LiveRepository(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public async Task<List<Live>> GetByToday()
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT * FROM Live WHERE LiveDate = @DATA";
                conn.Open();
                var result = await conn.QueryAsync<Live>(sQuery, new { DATA = DateTime.Now.Date});
                return result.ToList();
            }
        }
        public async Task<List<Live>> GetByNext()
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT * FROM Live WHERE LiveDate > @DATA";
                conn.Open();
                var result = await conn.QueryAsync<Live>(sQuery, new { DATA = DateTime.Now.Date });
                return result.ToList();
            }
        }
        public async Task<List<Live>> GetByPrevious()
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT * FROM Live WHERE LiveDate < @DATA";
                conn.Open();
                var result = await conn.QueryAsync<Live>(sQuery, new { DATA = DateTime.Now.Date });
                return result.ToList();
            }
        }
        public async Task<List<Live>> GetAll()
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT *  FROM Live";
                conn.Open();
                var result = await conn.QueryAsync<Live>(sQuery);
                return result.ToList();
            }
        }

        public async Task<Live> Add(Live model)
        {
            using (IDbConnection conn = Connection)
            {

              
                conn.Open();
              
                string sQuery = "Insert into Live (liveName,channelName,liveLink,liveDate,liveTime,statusLive) values(@LIVENAME,@CHANNELNAME,@LIVELINK,@LIVEDATE,@LIVETIME,@STATUSLIVE)";
                var result = await conn.QueryAsync<Live>(sQuery, new { 
                    LIVENAME = model.liveName, 
                    LIVELINK = model.liveLink, 
                    LIVEDATE = model.liveDate ,
                    LIVETIME = model.liveTime,
                    CHANNELNAME = model.channelName,
                    STATUSLIVE = model.statusLive
                });

                return model;


            }
        }

        public async Task<Live> GetById(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT * FROM Live WHERE Id = @ID";
                conn.Open();
                var result = await conn.QueryAsync<Live>(sQuery, new { ID =id });
                return result.FirstOrDefault();
            }
        }

        public async Task<Live> Update(Live model)
        {
            using (IDbConnection conn = Connection)
            {


                conn.Open();

                string sQuery = "Update Live set liveName = @LIVENAME,channelName = @CHANNELNAME,liveLink = @LIVELINK,liveDate = @LIVEDATE,liveTime=@LIVETIME,statusLive=@STATUSLIVE where id=@ID";
                var result = await conn.ExecuteScalarAsync<Live>(sQuery, new
                {
                    ID = model.Id,
                    LIVENAME = model.liveName,
                    LIVELINK = model.liveLink,
                    LIVEDATE = model.liveDate,
                    LIVETIME = model.liveTime,
                    CHANNELNAME = model.channelName,
                    STATUSLIVE = model.statusLive

                });

                return model;


            }
        }

        public async Task DeleteById(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "Delete FROM Live WHERE Id = @ID";
                conn.Open();
                var result = await conn.ExecuteScalarAsync<Live>(sQuery, new { ID = id });
                
            }
        }
    }
}
