using MagicManager.dal.Repositories;
using MagicManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

/// <summary>
/// Class api Angular - localdb (Sql en local)
/// </summary>
namespace MagicManager
{
    public class GamesController : ApiController
    {
        // GET api/<controller>
        /// <summary>
        /// Retourne toutes la collection d'Games présent en db
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("api/Game/get")]
        public IHttpActionResult Get()
        {
            var repo = new GameRepo();
            var games = repo.GetAll();//.ToList();
            //return new string[] { "value1", "value2" };
            if (games == null)
            {
                return NotFound();
            }
            return Ok(games);
        }

        [Route("api/Game/id/get")]
        public IHttpActionResult Get(int id)
        {
            var repo = new GameRepo();
            var game = repo.FindBy(a => a.GameId == id);
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);    
        }

        // GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        // [FromBody] : Utile que pour des page cshtml, ici on prend un objet db
        //public void Post(Game Game)
        //{
        //    var repo = new GameRepo();
        //    repo.Add(Game);
        //    //On sauve le context, pour forcer la màj db
        //    repo.Save();
        //}

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}