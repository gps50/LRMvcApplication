using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using LRMvcApplication.Domain.Univer;

namespace LRMvcApplication.Models
{
	public static class CourceModelMap
	{
		static CourceModelMap()
        {
			Mapper.CreateMap<Cource, CourceModel>();
			Mapper.CreateMap<CourceModel, Cource>();
        }

		public static CourceModel CreateModel(Cource entity)
        {
			return Mapper.Map<Cource, CourceModel>(entity);
        }

		public static void UpdateEntity(CourceModel model, Cource entity)
        {
			Mapper.Map<CourceModel, Cource>(model, entity);
        }
	}
}