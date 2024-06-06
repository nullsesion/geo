using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Geo.Application.Common.Mappings
{
	public interface IMapWith<T>
	{
		void Mapping(Profile profile) =>
			profile.CreateMap(typeof(T), GetType());
	}
}
