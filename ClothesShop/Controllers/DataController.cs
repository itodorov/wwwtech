using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothesShop.Data;

namespace ClothesShop.Controllers
{
    public class DataController : Controller
    {
        //
        // GET: /Data/

        public ActionResult Index()
        {
            return View();
        }

		[HttpGet]
		public ActionResult Products(int id)
		{
			return JsonResultilizer(DataHelper.GetSubCategoryItems(id));
		}

		private ActionResult JsonResultilizer(object result)
		{
			JsonResult res = Json(result);
			res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
			return res;
		}

		[HttpGet]
		public ActionResult Thumbnail(int id, int? size)
		{
			using (ClothesShopEntities entities = new ClothesShopEntities())
			{
				Image image = entities.Images.Where(x => x.ID == id).FirstOrDefault();
				if (image == null)
				{
					return new HttpStatusCodeResult(404);
				}

				return new ThumbnailResult(image.FileName, size ?? 120);
			}

			
		}
    }

	public class ThumbnailResult : ActionResult
	{
		int size;

		public ThumbnailResult(string virtualPath, int size)
		{
			this.size = size;
			this.VirtualPath = virtualPath;
		}

		public string VirtualPath { get; set; }

		public override void ExecuteResult(ControllerContext context)
		{
			context.HttpContext.Response.ContentType = "image/png";

			string fullFileName =
				context.HttpContext.Server.MapPath("~/ProductPictures/" + VirtualPath);
			using (System.Drawing.Image photoImg =
				System.Drawing.Image.FromFile(fullFileName))
			{
				using (System.Drawing.Image thumbPhoto =
					photoImg.GetThumbnailImage(size, size, null, new System.IntPtr()))
				{
					using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
					{
						thumbPhoto.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
						context.HttpContext.Response.BinaryWrite(ms.ToArray());
						context.HttpContext.Response.End();
					}
				}
			}
		}
	}
}
