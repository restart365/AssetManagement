using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AssetManagement.DataAccessObject;
using AssetManagement.Models;
using AutoMapper;

namespace AssetManagement.Controllers.Api
{
    public class AssetController : ApiController
    {
        // GET /api/asset
        public IEnumerable<Asset> GetAsset()
        {
            return AssetDao.ReadFromFile();
        }

        // GET /api/asset/1
        public Asset GetAsset(string id)
        {
            var asset = AssetDao.ReadFromFile().SingleOrDefault(c => Equals(c.Id, id));

            if(asset == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return asset;
        }

        // GET /api/asset/group/1
        [HttpGet]
        [Route("api/asset/group/{group}")]
        public IEnumerable<Asset> GetAsset(Group group)
        {
            var asset = AssetDao.ReadFromFile().Where(a => a.Group == group);

            if (asset == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return asset;
        }

        // POST /api/asset
        [HttpPost]
        public Asset CreateAsset(Asset asset)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var current = AssetDao.ReadFromFile().ToList();

            if (asset.Id == "")
            {
                var id = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                id = id.Replace('/', '_');
                id = id.Replace('+', '-');
                asset.Id = id;
            }
            else throw new HttpResponseException(HttpStatusCode.BadRequest); //ID has to be auto assigned

            current.Add(asset);

            AssetDao.WriteToFile(current);

            return asset;
        }

        // PUT /api/asset/1
        [HttpPut]
        public IHttpActionResult UpdateAsset(string id, Asset asset)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var list = AssetDao.ReadFromFile().ToList();
            var assetInFile = list.SingleOrDefault(c => Equals(c.Id, id));

            if (assetInFile == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            asset.Id = assetInFile.Id;

            list.Remove(assetInFile);
            list.Add(asset);

            AssetDao.WriteToFile(list);
            return Ok();
        }

        // PATCH /api/asset
        [HttpPatch]
        public IHttpActionResult UpdateAssetCount(AssetPatchRequest request)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var list = AssetDao.ReadFromFile().ToList();
            var assetInFile = list.SingleOrDefault(c => Equals(c.Id, request.Id));

            if (assetInFile == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            list.Remove(assetInFile);
            assetInFile.Count = request.Count;
            list.Add(assetInFile);
            AssetDao.WriteToFile(list);
            return Ok();
        }

        // DELETE /api/asset/1
        [HttpDelete]
        public IHttpActionResult DeleteAsset(string id)
        {
            var list = AssetDao.ReadFromFile().ToList();
            var assetInFile = list.SingleOrDefault(c => Equals(c.Id, id));

            if (assetInFile == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            list.Remove(assetInFile);

            AssetDao.WriteToFile(list);
            return Ok();
        }
    }

    public class AssetPatchRequest
    {
        [Required]
        public string Id { get; set; }

        [Range(0, Double.MaxValue)]
        public int Count { get; set; }
    }
}
