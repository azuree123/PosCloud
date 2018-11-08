using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using POSApp.Core;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;
using POSApp.Core.ViewModels.Sync;

namespace POSApp.Controllers.WebApi
{
    public class ClientsController : ApiController
    {
        private IUnitOfWork _unitOfWork;

        public ClientsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/Clients
        public async Task<IHttpActionResult> GetClients(int storeId)
        {
            return Ok(Mapper.Map<ClientViewModel[]>(_unitOfWork.ClientRepository.GetClients()));
        }

        // GET: api/Clients/5
        public async Task<IHttpActionResult> GetClient(int id, int storeId)
        {
            return Ok(_unitOfWork.ClientRepository.GetClient(id));
        }

        // POST: api/Clients
        public async Task<IHttpActionResult> AddClient([FromBody]SyncObject sync)
        {
            try
            {
                List<Client> clients = System.Web.Helpers.Json.Decode<List<Client>>(sync.Object);
                foreach (var client in clients)
                {
                    client.Code = client.Id.ToString();
                    client.Synced = true;
                    client.SyncedOn = DateTime.Now;
                    _unitOfWork.ClientRepository.AddClient(client);
                }
                _unitOfWork.Complete();
                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/Clients/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Clients/5
        public void Delete(int id)
        {
        }
    }
}
