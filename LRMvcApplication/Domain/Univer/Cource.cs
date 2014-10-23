using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using LRMvcApplication.Libraries.LambdaExpressions;
using LRMvcApplication.Services.Repository;

namespace LRMvcApplication.Domain.Univer
{
	public class Cource
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MinLength(2)]
		[MaxLength(10)]
		public string Title { get; set; }

		[MaxLength(20)]
		public string Description { get; set; }

		public virtual IList<Student> Students { get; set; }

	}

	public class CourceMap : EntityTypeConfiguration<Cource>, ISlqConstraints
	{
		public CourceMap()
		{
			this.ToTable(this.TableName);
		}

		#region ISlqConstraints

		public string TableName
		{ 
			get
			{
				return typeof(Cource).Name;
			}
		}

		public IList<IList<string>> Uniques
		{
			get
			{
				var result = new List<IList<string>>();

				result.Add(
					new string[] 
                    {
                        LambdaExpressionsHelper.GetPropertyName<Cource, string>(x => x.Title)
                    }
				);

				return result;
			}
		}

		public IList<string> FullTextIndex
		{
			get
			{
				return new string[] 
				{
					LambdaExpressionsHelper.GetPropertyName<Cource, string>(x => x.Title),
					LambdaExpressionsHelper.GetPropertyName<Cource, string>(x => x.Description)
				};
			}
		}

		#endregion
	}

}