﻿using AutoMapper;
using KiKaShop.Model.Models;
using KiKaShop.Service;
using KiKaShop.Web.Infrastructure.Core;
using KiKaShop.Web.Infrastructure.Extensions;
using KiKaShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace KiKaShop.Web.Api
{
    [RoutePrefix("api/slide")]
    [Authorize]
    public class SlideController : ApiControllerBase
    {
        #region Initialize
        private ISlideService _slideService;

        public SlideController(IErrorService errorService, ISlideService slideService)
            : base(errorService)
        {
            this._slideService = slideService;
        }
        #endregion

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 10)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;

                var model = _slideService.GetAll(keyword);
                totalRow = model.Count();
                var query = model.Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(query);

                var paginationSet = new PaginationSet<SlideViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };

                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, SlideViewModel slideVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var newSlide = new Slide();
                    //From Extensions
                    newSlide.UpdateSlide(slideVm);
                    _slideService.Add(newSlide);
                    _slideService.Save();

                    var responseData = Mapper.Map<Slide, SlideViewModel>(newSlide);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
        [Route("getById/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _slideService.GetById(id);

                var responseData = Mapper.Map<Slide, SlideViewModel>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, SlideViewModel slideVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var dbSlide = _slideService.GetById(slideVm.ID);
                    dbSlide.UpdateSlide(slideVm);
                    _slideService.Update(dbSlide);
                    _slideService.Save();

                    var responseData = Mapper.Map<Slide, SlideViewModel>(dbSlide);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var oldSlide = _slideService.Delete(id);
                    _slideService.Save();

                    var responseData = Mapper.Map<Slide, SlideViewModel>(oldSlide);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedSlides)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listSlide = new JavaScriptSerializer().Deserialize<List<int>>(checkedSlides);
                    foreach (var item in listSlide)
                    {
                        _slideService.Delete(item);
                    }

                    _slideService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listSlide.Count);
                }

                return response;
            });
        }

    }
}
