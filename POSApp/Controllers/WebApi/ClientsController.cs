using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using POSApp.Core;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;
using POSApp.Core.ViewModels.Sync;
using POSApp.Services;

namespace POSApp.Controllers.WebApi
{
    public class ClientsController : ApiController
    {
        private IUnitOfWork _unitOfWork;
        private ApplicationUserManager _userManager;
        public ClientsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ClientsController(ApplicationUserManager userManager, IUnitOfWork unitOfWork)
        {
            UserManager = userManager;
            _unitOfWork = unitOfWork;
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: api/Clients
        public async Task<IHttpActionResult> GetClients(int storeId)
        {
            return Ok(Mapper.Map<ClientViewModel[]>(await _unitOfWork.ClientRepository.GetClientsAsync()));
        }

        // GET: api/Clients/5
        public async Task<IHttpActionResult> GetClient(int id, int storeId)
        {
            return Ok(await _unitOfWork.ClientRepository.GetClientAsync(id));
        }

        // POST: api/Clients
        public async Task<IHttpActionResult> AddClient([FromBody]ClientApiViewModel clients)
        {
            try
            {
                var client=new Client();
                client.Name = clients.Name;
                client.Email = clients.Email;
                client.Address = clients.Address;
                client.Contact = clients.Contact;
                client.City = clients.City;
                client.State = clients.State;
                client.VatNumber = clients.VatNumber;
                client.Stores=new List<Store>(){new Store
                {
                    Name = clients.Name,
                    Address = clients.Address,
                    BusinessStartTime = clients.BusinessStartTime,
                    IsOperational = clients.IsOperational,
                    City = clients.City,
                    Currency = clients.Currency,
                    State = clients.State,
                    Contact = clients.Contact,
                    
                   
                   AppCounters = new List<AppCounter>(){new AppCounter
                   {
                       
                   }}

                 
          
                }};
                _unitOfWork.ClientRepository.AddClient(client);
                _unitOfWork.Complete();
                var shift = new Shift() { Name = clients.Name };
                _unitOfWork.ShiftRepository.AddShift(shift);
                var emp = new Employee {Name = clients.Name,Address = clients.Address,ShiftId = shift.ShiftId};
                _unitOfWork.EmployeeRepository.AddEmployee(emp);
                _unitOfWork.Complete();
                var user = new ApplicationUser { UserName = clients.Email, Email = clients.Email,EmployeeId = emp.Id, StoreId = client.Stores.FirstOrDefault().Id, PasswordEncrypt = Security.EncryptString(clients.Password, "E546C8DF278CD5931069B522E695D4F2") };

                var result = await UserManager.CreateAsync(user, clients.Password);



                
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
